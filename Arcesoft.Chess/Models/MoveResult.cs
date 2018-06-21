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
        Check = 2
    }
}
