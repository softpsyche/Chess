using System;
using System.Collections.Generic;
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

        public static bool BelongsTo(this ChessPiece chessPiece, Player player)
        {
            return player == Chess.Player.White ? chessPiece.BelongsToWhite() : chessPiece.BelongsToBlack();
        }
        public static bool BelongsToWhite(this ChessPiece chessPiece)
        {
            return chessPiece.ToByte() >= 20;
        }
        public static bool BelongsToBlack(this ChessPiece chessPiece)
        {
            return chessPiece.ToByte() >= 10 || chessPiece.ToByte() < 20;
        }
        public static Player Player(this ChessPiece chessPiece)
        {
            if (chessPiece == ChessPiece.None)
            {
                throw new InvalidOperationException($"The chessPiece '{chessPiece}' does not belong to any player.");
            }

            return chessPiece.BelongsToWhite() ? Chess.Player.White : Chess.Player.Black;
        }

        public static byte ToByte(this ChessPiece boardSquare) => (byte)boardSquare;

        public static Player OpposingPlayer(this Player player) => player == Chess.Player.White ? Chess.Player.Black : Chess.Player.White;

        /// <summary>
        /// Performs the specified action on each element of the enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }
    }
}
