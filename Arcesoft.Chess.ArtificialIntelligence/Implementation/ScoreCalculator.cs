using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.ArtificialIntelligence.Implementation
{
    internal class ScoreCalculator : IScoreCalculator
    {
        private static Dictionary<ChessPiece, int> ScoresForWhite { get; } = new Dictionary<ChessPiece, int>();
        private static Dictionary<ChessPiece, int> ScoresForBlack { get; } = new Dictionary<ChessPiece, int>();

        static ScoreCalculator()
        {
            ScoresForWhite.Add(ChessPiece.WhiteKing, 900);
            ScoresForWhite.Add(ChessPiece.WhiteQueen, 90);
            ScoresForWhite.Add(ChessPiece.WhiteRook, 50);
            ScoresForWhite.Add(ChessPiece.WhiteBishop, 30);
            ScoresForWhite.Add(ChessPiece.WhiteKnight, 30);
            ScoresForWhite.Add(ChessPiece.WhitePawn, 10);

            ScoresForWhite.Add(ChessPiece.BlackKing, -900);
            ScoresForWhite.Add(ChessPiece.BlackQueen, -90);
            ScoresForWhite.Add(ChessPiece.BlackRook, -50);
            ScoresForWhite.Add(ChessPiece.BlackBishop, -30);
            ScoresForWhite.Add(ChessPiece.BlackKnight, -30);
            ScoresForWhite.Add(ChessPiece.BlackPawn, -10);

            ScoresForBlack.Add(ChessPiece.WhiteKing, -900);
            ScoresForBlack.Add(ChessPiece.WhiteQueen, -90);
            ScoresForBlack.Add(ChessPiece.WhiteRook, -50);
            ScoresForBlack.Add(ChessPiece.WhiteBishop, -30);
            ScoresForBlack.Add(ChessPiece.WhiteKnight, -30);
            ScoresForBlack.Add(ChessPiece.WhitePawn, -10);

            ScoresForBlack.Add(ChessPiece.BlackKing, 900);
            ScoresForBlack.Add(ChessPiece.BlackQueen, 90);
            ScoresForBlack.Add(ChessPiece.BlackRook, 50);
            ScoresForBlack.Add(ChessPiece.BlackBishop, 30);
            ScoresForBlack.Add(ChessPiece.BlackKnight, 30);
            ScoresForBlack.Add(ChessPiece.BlackPawn, 10);
        }

        public int Score(IReadOnlyBoard readOnlyBoard, Player player)
        {
            return CalculateScore(readOnlyBoard, player);
        }

        private int CalculateScore(IReadOnlyBoard readOnlyBoard, Player player)
        {
            var score = 0;
            Dictionary<ChessPiece, int> dictionaryToUseForScore = player == Player.White ? ScoresForWhite : ScoresForBlack;

            foreach (ChessPiece piece in readOnlyBoard)
            {
                if (piece.BelongsToWhite())
                {
                    score += dictionaryToUseForScore[piece];
                }
                else if (piece.BelongsToBlack())
                {
                    score += dictionaryToUseForScore[piece];
                }
            }

            return score;
        }
    }
}
