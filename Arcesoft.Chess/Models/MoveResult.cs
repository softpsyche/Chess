using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{

    [Flags]
    public enum MoveResult : byte
    {
        None = 0,
        Capture = 1,
        CaptureAuPassant = 2,
        Castle = 3,
        Check = 4
    }
}
