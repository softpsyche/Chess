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
        public ChessPiece? CapturedPiece { get; private set; }
        public SpecialMoveType? SpecialMoveType { get; private set; }

        public Move(BoardLocation source, BoardLocation destination)
            : this(source, destination, null, null)
        {

        }
        public Move(BoardLocation source, BoardLocation destination, ChessPiece capturedPiece)
            : this(source, destination, capturedPiece, null)
        {

        }
        public Move(BoardLocation source, BoardLocation destination, SpecialMoveType specialMoveType)
            : this(source, destination, null, specialMoveType)
        {

        }
        public Move(BoardLocation source, BoardLocation destination, ChessPiece capturedPiece, SpecialMoveType specialMoveType)
            : this(source, destination, (ChessPiece?)capturedPiece, (SpecialMoveType?)specialMoveType)
        {

        }

        public Move(BoardLocation source, BoardLocation destination, ChessPiece? capturedPiece, SpecialMoveType? moveType)
        {
            Source = source;
            Destination = destination;
            SpecialMoveType = moveType;
            //we will not accept a none chess piece.
            CapturedPiece = capturedPiece != ChessPiece.None ? capturedPiece : default(ChessPiece?);
        }

        public override string ToString()
        {
            return $"{Source}-{Destination}";
            //return $"{Source}-{Destination} ({SpecialMoveType})";
        }

        public override bool Equals(object obj)
        {
            var move = obj as Move;
            if (move == null) return false;

            return
                Source.Equals(move.Source) &&
                Destination.Equals(move.Destination) &&
                CapturedPiece.Equals(move.CapturedPiece) &&
                SpecialMoveType.Equals(move.SpecialMoveType);
        }

        public override int GetHashCode()
        {
            return 
                Source.GetHashCode() 
                ^ Destination.GetHashCode() 
                ^ CapturedPiece.GetHashCode()
                ^ SpecialMoveType.GetHashCode();
        }
    }
}
