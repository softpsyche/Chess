using Arcesoft.Chess.Models;
using ilf.pgn.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Implementation
{
    internal static class PgnExtensions
    {
        public static BoardLocation ToBoardLocation(this Square square)
        {
            return (BoardLocation)Enum.Parse(typeof(BoardLocation), square.File.ToString() + square.Rank.ToString());
        }
        public static ChessPiece ToChessPiece(this PieceType pieceType, Player player)
        {
            switch (pieceType)
            {
                case PieceType.King:
                    return player == Player.White ? ChessPiece.WhiteKing : ChessPiece.BlackKing;
                case PieceType.Queen:
                    return player == Player.White ? ChessPiece.WhiteQueen : ChessPiece.BlackQueen;
                case PieceType.Rook:
                    return player == Player.White ? ChessPiece.WhiteRook : ChessPiece.BlackRook;
                case PieceType.Bishop:
                    return player == Player.White ? ChessPiece.WhiteBishop : ChessPiece.BlackBishop;
                case PieceType.Knight:
                    return player == Player.White ? ChessPiece.WhiteKnight : ChessPiece.BlackKnight;
                case PieceType.Pawn:
                    return player == Player.White ? ChessPiece.WhitePawn : ChessPiece.BlackPawn;
                default:
                    throw new Exception("Bad stuffs");
            }
        }

        public static int ToColumn(this File file)
        {
            switch (file)
            {
                case File.A:
                    return 0;
                case File.B:
                    return 1;
                case File.C:
                    return 2;
                case File.D:
                    return 3;
                case File.E:
                    return 4;
                case File.F:
                    return 5;
                case File.G:
                    return 6;
                case File.H:
                    return 7;
                default:
                    throw new ArgumentException();
            }
        }

    }
}
