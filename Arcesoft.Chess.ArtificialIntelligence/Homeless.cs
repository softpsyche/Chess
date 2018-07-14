using Arcesoft.Chess.Models;
using System;

namespace Arcesoft.Chess.ArtificialIntelligence
{
    public class BoardScoreCalculator
    {
        public ScoreResult Score(IReadOnlyBoard readOnlyBoard)
        {
            foreach (var piece in readOnlyBoard)
            {

            }
        }
    }

    public class ScoreResult
    {
        public int WhiteScore { get; set; }
        public int BlackScore { get; set; }
    }
}
