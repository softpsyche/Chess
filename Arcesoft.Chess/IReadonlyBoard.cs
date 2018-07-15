using Arcesoft.Chess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess
{
    public interface IReadOnlyBoard: IEnumerable
    {
        ChessPiece this[BoardLocation boardLocation] { get; }
    }
}
