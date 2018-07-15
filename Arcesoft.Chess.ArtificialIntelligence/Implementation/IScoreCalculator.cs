using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.ArtificialIntelligence.Implementation
{
    internal interface IScoreCalculator
    {
        int Score(IReadOnlyBoard readOnlyBoard, Player player);
    }
}
