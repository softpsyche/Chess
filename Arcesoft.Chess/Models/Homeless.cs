using Arcesoft.Chess.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arcesoft.Chess.Models
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
        Line = Horizontal| Vertical,
        Slope = NorthWest | SouthEast,
        Grade = SouthWest | NorthEast,
        Diagonal = Slope | Grade,
        NonDirect = Horizontal | Vertical | Slope | Grade
    }

    /// <summary>
    /// Describes a direction
    /// </summary>
    public enum Direction : byte
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

    public enum GameState : byte
    {   //from https://en.wikipedia.org/wiki/Draw_(chess)
        InPlay = 0,
        WhiteWin = 1,
        BlackWin = 2,
        DrawStalemate = 3,// when the player to move has no legal move and is not in check
        DrawThreeFoldRepetition = 4, // when the same position occurs three times with the same player to move
        DrawFiftyMoveRule = 5,// when the last fifty successive moves made by both players contain no capture or pawn move
        DrawInDeadPosition = 6// when no sequence of legal moves can lead to checkmate, most commonly when neither player has sufficient material to checkmate the opponent
    }

    public enum Player : byte
    {
        White = 0,
        Black
    }

    public enum ChessPiece : byte
    {
        None = 0,
        BlackPawn=10,
        BlackKnight=11,
        BlackBishop=12,
        BlackRook=13,
        BlackQueen=14,
        BlackKing=15,
        WhitePawn=20,
        WhiteKnight=21,
        WhiteBishop=22,
        WhiteRook=23,
        WhiteQueen=24,
        WhiteKing=25
    }


}
