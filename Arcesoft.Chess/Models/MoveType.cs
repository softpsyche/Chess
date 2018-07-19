using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{
    [Flags]
    public enum MoveType : int
    {
        Move = 1,
        CapturePawn = 2,
        CaptureKnight = 4,
        CaptureBishop = 8,
        CaptureRook = 16,
        CaptureQueen = 32,
        PawnPromotionKnight = 64,
        PawnPromotionBishop = 128,
        PawnPromotionRook = 256,
        PawnPromotionQueen = 512,
        AuPassant = 1024,
        CastleKingside = 2048,
        CastleQueenside = 4096         
    }
}
