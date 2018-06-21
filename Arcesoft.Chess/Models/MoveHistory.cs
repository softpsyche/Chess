using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{
    public class MoveHistory : Move
    {
        public MoveResult Result { get; internal set; }

        public MoveHistory(BoardLocation source, BoardLocation destination, MoveResult moveResult)
            :base(source, destination)        
        {
            Result = moveResult;
        }
    }
}
