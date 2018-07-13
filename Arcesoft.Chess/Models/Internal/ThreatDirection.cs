using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models.Internal
{
    /// <summary>
    /// Describes a threat direction
    /// </summary>
    [Flags]
    internal enum ThreatDirection : int
    {
        North = 1,
        South = 2,
        East = 4,
        West = 8,
        NorthEast = 16,
        NorthWest = 32,
        SouthEast = 64,
        SouthWest = 128,
        Direct = 256,
        Horizontal = East | West,
        Vertical = North | South,
        Line = Horizontal | Vertical,
        Slope = NorthWest | SouthEast,
        Grade = SouthWest | NorthEast,
        Diagonal = Slope | Grade,
        NonDirect = Horizontal | Vertical | Slope | Grade
    }
}
