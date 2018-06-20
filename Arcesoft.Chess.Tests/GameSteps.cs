using Arcesoft.Chess.Implementation;
using Arcesoft.Chess.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        }


        [Then(@"I expect the current board to contain the following")]
        public void ThenIExpectTheCurrentBoardToContainTheFollowing(Table table)
        {
            Game.Board.ToString().Should().Be(table.ToBoard().ToString());
        }
    }

    public static class Extensions
    {
        //PGN format for the board
        //    A  B  C  D  E  F  G  H
        //8:| 7|15|23|31|39|47|55|63|
        //7:| 6|14|22|30|38|46|54|62|
        //6:| 5|13|21|29|37|45|53|61|
        //5:| 4|12|20|28|36|44|52|60|
        //4:| 3|11|19|27|35|43|51|59|
        //3:| 2|10|18|26|34|42|50|58|
        //2:| 1| 9|17|25|33|41|49|57|
        //1:| 0| 8|16|24|32|40|48|56|

        public static Board ToBoard(this Table table)
        {
            Board board = new Board();
            board.Clear();
            int boardPosition = 0;

            for (int column = 7; column >= 0; column--)
            {
                for (int row = 7; row >= 0; row--)
                {
                    var chessPiece = table.Rows[row][column].ToChessPieceFromInputString();

                    board[(BoardLocation)boardPosition] = chessPiece;
                    boardPosition++;
                }
            }

            return board;
        }

        public static ChessPiece ToChessPieceFromInputString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ChessPiece.None;

            switch (value?.ToUpperInvariant())
            {
                case "WP":
                    return ChessPiece.WhitePawn;
                case "WN":
                    return ChessPiece.WhiteKnight;
                case "WB":
                    return ChessPiece.WhiteBishop;
                case "WR":
                    return ChessPiece.WhiteRook;
                case "WQ":
                    return ChessPiece.WhiteQueen;
                case "WK":
                    return ChessPiece.WhiteKing;
                case "BP":
                    return ChessPiece.BlackPawn;
                case "BN":
                    return ChessPiece.BlackKnight;
                case "BB":
                    return ChessPiece.BlackBishop;
                case "BR":
                    return ChessPiece.BlackRook;
                case "BQ":
                    return ChessPiece.BlackQueen;
                case "BK":
                    return ChessPiece.BlackKing;
                default:
                    throw new InvalidOperationException($"Unexpected piece value of '{value}' found.");
            }
        }
    }
}
