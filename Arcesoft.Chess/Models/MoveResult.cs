using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{

    [Flags]
    public enum MoveResult : byte
    {
        Move = 0,
        CapturePawn = 1,
        CaptureKnight = 2,
        CaptureBishop = 4,
        CaptureRook = 8,
        CaptureQueen = 16,
        AuPassant = 32,
        Castle = 64,
        Check = 128
    }
}
