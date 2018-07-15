using Arcesoft.Chess.Models;
using Arcesoft.Chess.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcesoft.Chess.Implementation
{
    internal static class Extensions
    {
        public static ThreatDirection ToThreatDirection(this Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    return ThreatDirection.East;
                case Direction.North:
                    return ThreatDirection.North;
                case Direction.NorthEast:
                    return ThreatDirection.NorthEast;
                case Direction.NorthWest:
                    return ThreatDirection.NorthWest;
                case Direction.South:
                    return ThreatDirection.South;
                case Direction.SouthEast:
                    return ThreatDirection.SouthEast;
                case Direction.SouthWest:
                    return ThreatDirection.SouthWest;
                case Direction.West:
                    return ThreatDirection.West;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static Direction ToDirection(this ThreatDirection threatDirection)
        {
            switch (threatDirection)
            {
                case ThreatDirection.East:
                    return Direction.East;
                case ThreatDirection.North:
                    return Direction.North;
                case ThreatDirection.NorthEast:
                    return Direction.NorthEast;
                case ThreatDirection.NorthWest:
                    return Direction.NorthWest;
                case ThreatDirection.South:
                    return Direction.South;
                case ThreatDirection.SouthEast:
                    return Direction.SouthEast;
                case ThreatDirection.SouthWest:
                    return Direction.SouthWest;
                case ThreatDirection.West:
                    return Direction.West;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static byte ToByte(this ChessPiece boardSquare) => (byte)boardSquare;
    }
}
