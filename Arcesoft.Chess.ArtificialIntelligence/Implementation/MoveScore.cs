using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.ArtificialIntelligence.Implementation
{
    internal class MoveScore
    {
        internal const int WinScore = 9999;
        internal const int DrawScore = 0;
        internal const int LoseScore = WinScore * -1;
        internal const int WorstScore = int.MinValue;

        public IMove Move { get; private set; }
        public int Score { get; set; }

        public MoveScore(IMove move, int score)
        {
            Move = move;
            Score = score;
        }

        public static MoveScore WorstMoveScore = new MoveScore(null, WorstScore);
        public static MoveScore WinningMoveScore(IMove move) => new MoveScore(move, WinScore);
        public static MoveScore LosingMoveScore(IMove move) => new MoveScore(move, LoseScore);
        public static MoveScore DrawMoveScore(IMove move) => new MoveScore(move, DrawScore);
    }
}
