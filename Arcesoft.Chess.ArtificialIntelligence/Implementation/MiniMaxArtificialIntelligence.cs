using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcesoft.Chess.ArtificialIntelligence.Implementation
{

    internal class MiniMaxArtificialIntelligence : IArtificialIntelligence
    {
        private IScoreCalculator _scoreCalculator;

        public MiniMaxArtificialIntelligence(IScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        public IMove TryFindBestMove(IGame game, int depth)
        {
            if (game.GameIsOver)
            {
                return null;
            }

            return CalculateBestMoveScore(game, depth, null).Move;
        }

        public IMove TryFindBestMove(IGame game, int depth, MiniMaxGraph miniMaxGraph)
        {
            if (game.GameIsOver)
            {
                return null;
            }

            miniMaxGraph.Clear();

            miniMaxGraph.Root.BoardVisual = game.Board.ToString();
            miniMaxGraph.Root.PlayerWhoScoreIsFor = game.CurrentPlayer;

            var bestMove = CalculateBestMoveScore(game, depth, miniMaxGraph.Root).Move;

            return bestMove;
        }

        #region Private

        private MoveScore CalculateBestMoveScore(IGame game, int depth, Node currentNode)
        {
            //Recursive exit condition
            //eval the current board. Our search ends when the game is over or the max depth has been reached
            if ((depth == 0) || (game.GameIsOver))
            {
                MoveScore result = null;
                //if the game is over, we will decide a score for the state
                switch (game.GameState)
                {
                    case GameState.DrawFiftyMoveRule:
                    case GameState.DrawInDeadPosition:
                    case GameState.DrawInsufficientMaterial:
                    case GameState.DrawStalemate:
                    case GameState.DrawThreeFoldRepetition:
                        result = MoveScore.DrawMoveScore(game.MoveHistory[game.MoveHistory.Count - 1]);
                        break;
                    case GameState.WhiteWin:
                    case GameState.BlackWin:
                        //Ok, lets explain this magic: 
                        //The only way to get a winning condition is if the last move made was by the
                        //player who won. i.e., it is impossible to for a move from white to result in a win for black
                        //and vice versa. Thus, we can safely assume the the score being calculated is for the player who
                        //made the move.
                        result = MoveScore.WinningMoveScore(game.MoveHistory[game.MoveHistory.Count - 1]);
                        break;
                    case GameState.InPlay:
                        result = new MoveScore(
                            game.MoveHistory[game.MoveHistory.Count - 1],
                            _scoreCalculator.Score(game.Board, game.CurrentPlayer.OpposingPlayer())
                        );
                        break;
                }

                if (currentNode != null)
                {
                    currentNode.Score = result;
                }
                return result;
            }

            MoveScore currentMoveScore;
            MoveScore bestMoveScore = MoveScore.WorstMoveScore;
            var moves = game.FindMoves();
            Node childNode = null;

            foreach (var move in moves)
            {
                game.MakeMove(move);

                //add a child node to our AI graph if we have one
                if (currentNode != null)
                {
                    childNode = new Node
                    {
                        Parent = currentNode,
                        Score = null,
                        BoardVisual = game.Board.ToString(),
                        PlayerWhoScoreIsFor = game.CurrentPlayer.OpposingPlayer()
                    };

                    currentNode?.Children.Add(childNode);
                }
                             
                currentMoveScore = CalculateBestMoveScore(game, depth - 1, childNode);

                if (currentMoveScore.Score > bestMoveScore.Score)
                {
                    bestMoveScore = new MoveScore(move, currentMoveScore.Score);
                }

                game.UndoLastMove();
            }

            if (currentNode != null)
            {
                currentNode.Score = bestMoveScore;
            }

            return bestMoveScore;
        }
        #endregion
    }

    internal class Node
    {
        public Node Parent { get; set; }
        public List<Node> Children { get; } = new List<Node>();
        public MoveScore Score { get; set; }
        public Player PlayerWhoScoreIsFor { get; set; }
        public string BoardVisual { get; set; }
        public int Depth
        {
            get
            {
                int depth = 0;
                Node node = this;
                while (node.Parent != null)
                {
                    depth++;
                    node = node.Parent;
                }

                return depth;
            }
        }

        public override string ToString()
        {
            return $"{Score.Move.ToString()} {PlayerWhoScoreIsFor}-{Score.Score.ToString("0000")} ({Depth})";
        }
    }
    internal class MiniMaxGraph
    {
        public Node Root { get; } = new Node();

        public void Clear()
        {
            Root.Children.Clear();
            Root.BoardVisual = string.Empty;
            Root.Score = MoveScore.WorstMoveScore;
        }
    }
}
