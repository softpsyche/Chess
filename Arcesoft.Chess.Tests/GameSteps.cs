using Arcesoft.Chess.Implementation;
using Arcesoft.Chess.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Arcesoft.Chess.Tests
{
    [Binding]
    internal sealed class GameSteps : Steps
    {
        [Then(@"I expect the game to not be over")]
        public void ThenIExpectTheGameToNotBeOver()
        {
            Game.GameIsOver.Should().BeFalse();
        }

        [Then(@"I expect the current player is '(.*)'")]
        public void ThenIExpectTheCurrentPlayerIs(Player player)
        {
            player.Should().Be(player);
        }

        [Then(@"I expect the gamestate to be '(.*)'")]
        public void ThenIExpectTheGamestateToBe(GameState gameState)
        {
            Game.GameState.Should().Be(gameState);
        }

        [Then(@"I expect no moves to have been made")]
        public void ThenIExpectNoMovesToHaveBeenMade()
        {
            Game.MoveHistory.Should().BeEmpty();
        }

        [Then(@"I expect the current board to contain the following")]
        public void ThenIExpectTheCurrentBoardToContainTheFollowing(Table table)
        {
            Game.Board.ToString().Should().Be(table.ToBoard().ToString());
        }

        [When(@"I find moves for the current game")]
        public void WhenIFindMovesForTheCurrentGame()
        {
            Invoke(() => Moves = Game.FindMoves().ToList());
        }

        [Then(@"I expect the moves found should contain")]
        public void ThenIExpectTheMovesFoundToContain(Table table)
        {
            table.CompareToSet(Moves);
        }

        [Then(@"I expect the moves found should NOT contain")]
        public void ThenIExpectTheMovesFoundShouldNotContain(Table table)
        {
            Moves.Should().NotContain(table.ToMoves());
        }

        [Then(@"I expect no moves were found")]
        public void ThenIExpectNoMovesWereFound()
        {
            Moves.Should().BeEmpty();
        }

        [Given(@"I start a new game in the following state")]
        public void GivenIStartANewGameInTheFollowingState(Table table)
        {
            Invoke(() => Game = GameFactory.NewGame());

            Game.SetPrivateField("_board", table.ToBoard());
        }

        [Given(@"I have the following move history")]
        public void GivenIHaveTheFollowingMoveHistory(Table table)
        {
            Game.SetPrivateField("_moveHistory", table.ToMoveHistory());
        }

        [Then(@"I expect the moves found should NOT contain '(.*)'")]
        public void ThenIExpectTheMovesFoundShouldNOTContain(string expectedMoves)
        {
            var moves = expectedMoves.ToMoves().Select(a => a.ToString()).ToList();

            if (moves.Any())
            {
                Moves.Select(a => a.ToString()).ToList().Should().NotContain(moves);
            }
        }

        [Then(@"I expect the moves found should contain '(.*)'")]
        public void ThenIExpectTheMovesFoundShouldContain(string nonExpectedMoves)
        {
            var moves = nonExpectedMoves.ToMoves().Select(a => a.ToString()).ToList();

            if (moves.Any())
            {
                Moves.Select(a => a.ToString()).ToList().Should().Contain(moves);
            }
        }

        [Given(@"The game is in the following gamestate '(.*)'")]
        public void GivenTheGameIsInTheFollowingGamestate(GameState gameState)
        {
            //thank you, filthy filthy reflection
            Game.SetPrivateProperty(nameof(Game.GameState), gameState);
        }

        [When(@"I make the following move")]
        public void WhenIMakeTheFollowingMove(Table table)
        {
            var move = table.CreateInstance<MoveTestInput>();

            Invoke(() => Game.MakeMove(move.Source, move.Destination, move.PromotionType));
        }

        [Then(@"I expect the following ChessException to be thrown")]
        public void ThenIExpectTheFollowingChessExceptionToBeThrown(Table table)
        {
            Exception.Should().BeOfType(typeof(ChessException));
            table.CompareToInstance(Exception);
        }

        [When(@"I make the following moves")]
        public void WhenIMakeTheFollowingMoves(Table table)
        {
            Invoke(() => table.CreateSet<MoveTestInput>().ForEach(a => Game.MakeMove(a.Source,a.Destination,a.PromotionType)));
        }

        [Then(@"I expect the following move history")]
        public void ThenIExpectTheFollowingMoveHistory(Table table)
        {
            table.CompareToSet(Game.MoveHistory);
        }

        [Given(@"I have a match factory")]
        public void GivenIHaveAMatchFactory()
        {
            MatchFactory = Container.GetInstance<IMatchFactory>();
        }

        [When(@"I load the match '(.*)'")]
        public void WhenILoadTheMatch(string embeddedResource)
        {
            Invoke(() =>
            {
                Match = MatchFactory.Load(Assembly.GetExecutingAssembly().EmbeddedResourceString(embeddedResource)).Single();
                Game = Match.Game;
            });
        }

        [When(@"I undo the last move")]
        public void WhenIUndoTheLastMove()
        {
            Invoke(() => Game.UndoLastMove());
        }
    }

    public class MoveTestInput
    {
        public BoardLocation Source { get; set; }
        public BoardLocation Destination { get; set; }
        public PawnPromotionType? PromotionType { get; set; }
    }
}
