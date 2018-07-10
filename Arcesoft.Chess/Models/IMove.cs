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
        /// The type of move being made
        /// </summary>
        MoveType Type { get; }
    }
}
