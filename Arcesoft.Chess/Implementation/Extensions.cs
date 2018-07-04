﻿using Arcesoft.Chess.Models;
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


        public static Player OpposingPlayer(this Player player) => player == Models.Player.White ? Models.Player.Black : Models.Player.White;

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

        public static string ToVisualString(this IList<Move> moves)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 7; row >= 0; row--)
            {
                for (int column = 0; column < 8; column++)
                {
                    var location = (BoardLocation)(row + (column * 8));


                    sb.Append(ToBoardLocationMoveString(moves.FirstOrDefault(a => a.Destination == location), column == 0));
                }

                sb.Append("\r\n");
            }

            return sb.ToString();
        }

        private static string ToBoardLocationMoveString(Move move, bool printStartingPipe)
        {
            string pieceString = "  ";

            if (move != null)
            {
                pieceString = "::";
            }

            return printStartingPipe ? $"|{pieceString}|" : $"{pieceString}|";
        }
    }

    public static class ChessPieceExtensions
    {
        public static byte ToByte(this ChessPiece boardSquare) => (byte)boardSquare;

        public static bool BelongsTo(this ChessPiece chessPiece, Player player)
        {
            return player == Models.Player.White ? chessPiece.BelongsToWhite() : chessPiece.BelongsToBlack();
        }

        public static bool BelongsToWhite(this ChessPiece chessPiece)
        {
            return chessPiece.ToByte() >= 20;
        }

        public static bool BelongsToBlack(this ChessPiece chessPiece)
        {
            return chessPiece.ToByte() >= 10 && chessPiece.ToByte() < 20;
        }

        public static Player Player(this ChessPiece chessPiece)
        {
            if (chessPiece == ChessPiece.None)
            {
                throw new InvalidOperationException($"The chessPiece '{chessPiece}' does not belong to any player.");
            }

            return chessPiece.BelongsToWhite() ? Models.Player.White : Models.Player.Black;
        }

        public static bool IsEmpty(this ChessPiece chessPiece) => chessPiece == ChessPiece.None;
        
        public static bool IsKing(this ChessPiece chessPiece, Player player)
        {
            return player == Models.Player.White ? chessPiece == ChessPiece.WhiteKing : chessPiece == ChessPiece.BlackKing;
        }
        public static bool IsQueen(this ChessPiece chessPiece, Player player)
        {
            return player == Models.Player.White ? chessPiece == ChessPiece.WhiteQueen : chessPiece == ChessPiece.BlackQueen;
        }
        public static bool IsRook(this ChessPiece chessPiece, Player player)
        {
            return player == Models.Player.White ? chessPiece == ChessPiece.WhiteRook : chessPiece == ChessPiece.BlackRook;
        }
        public static bool IsBishop(this ChessPiece chessPiece, Player player)
        {
            return player == Models.Player.White ? chessPiece == ChessPiece.WhiteBishop : chessPiece == ChessPiece.BlackBishop;
        }
        public static bool IsKnight(this ChessPiece chessPiece, Player player)
        {
            return player == Models.Player.White ? chessPiece == ChessPiece.WhiteKnight : chessPiece == ChessPiece.BlackKnight;
        }
        public static bool IsPawn(this ChessPiece chessPiece, Player player)
        {
            return player == Models.Player.White ? chessPiece == ChessPiece.WhitePawn : chessPiece == ChessPiece.BlackPawn;
        }
    }
}
