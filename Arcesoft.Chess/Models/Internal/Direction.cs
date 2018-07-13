using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models.Internal
{
    /// <summary>
    /// Describes a direction
    /// </summary>
    internal enum Direction : byte
    {
        North = 1,
        South,
        East,
        West,
        NorthEast,
        NorthWest,
        SouthEast,
        SouthWest
    }
}
