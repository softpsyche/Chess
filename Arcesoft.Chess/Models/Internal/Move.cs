using Arcesoft.Chess.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models.Internal
{
    /// <summary>
    /// This class is immutable and thus also threadsafe.
    /// </summary>
    internal class Move : IMove
    {
        public BoardLocation Source { get; private set; }
        public BoardLocation Destination { get; private set; }
        public MoveType Type { get; private set; }

        public Move(BoardLocation source, BoardLocation destination, MoveType moveType)
        {
            Source = source;
            Destination = destination;
            Type = moveType;
        }

        public override string ToString()
        {
            return $"{Source}-{Destination}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Move;
            if ((obj == null) || (other == null)) return false;

            return
                Source.Equals(other.Source) &&
                Destination.Equals(other.Destination) &&
                Type.Equals(other.Type);
        }

        public override int GetHashCode()
        {
            return 
                Source.ToByte().GetHashCode() 
                ^ Destination.ToByte().GetHashCode() 
                ^ Type.GetHashCode();
        }
    }
}
