using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess
{
    public class ChessException : Exception
    {
        public ChessErrorCode ErrorCode { get; private set; }

        public ChessException(ChessErrorCode chessErrorCode, string message = null, Exception exception = null) : base(message, null)
        {
            ErrorCode = chessErrorCode;
        }
    }
}
