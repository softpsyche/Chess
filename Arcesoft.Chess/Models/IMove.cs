using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{
    /// <summary>
    /// Represents a chess move
    /// </summary>
    public interface IMove
    {
        /// <summary>
        /// Source board location of piece moving
        /// </summary>
        BoardLocation Source { get; }
        /// <summary>
        /// Destination board location of piece moving
        /// </summary>
        BoardLocation Destination { get; }
        /// <summary>
        /// The piece that was captured. Null if no piece was captured.
        /// </summary>
        ChessPiece? CapturedPiece { get; }
        /// <summary>
        /// If the move is special, denotes what special type it is
        /// </summary>
        SpecialMoveType? SpecialMoveType { get; }
    }
}
