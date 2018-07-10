using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{
    public enum MoveType : byte
    {
        Move = 0,
        CapturePawn = 1,
        CaptureKnight = 2,
        CaptureBishop = 3,
        CaptureRook = 4,
        CaptureQueen = 5,
        PawnPromotionKnight = 10,
        PawnPromotionBishop = 11,
        PawnPromotionRook = 12,
        PawnPromotionQueen = 13,
        AuPassant= 20,
        CastleKingside = 30,
        CastleQueenside = 31
    }
}
