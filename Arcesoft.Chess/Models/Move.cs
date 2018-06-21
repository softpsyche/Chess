using Arcesoft.Chess.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{
    /// <summary>
    /// This class is immutable and thus also threadsafe.
    /// </summary>
    public class Move
    {
        public BoardLocation Source { get; private set; }
        public BoardLocation Destination { get; private set; }

        public Move(BoardLocation source, BoardLocation destination)
        {
            Source = source;
            Destination = destination;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Move;
            if ((obj == null) || (other == null)) return false;

            return
                Source.Equals(other.Source) &&
                Destination.Equals(other.Destination);
        }

        public override int GetHashCode()
        {
            return Source.ToByte().GetHashCode() ^ Destination.ToByte().GetHashCode();
        }
    }
}
