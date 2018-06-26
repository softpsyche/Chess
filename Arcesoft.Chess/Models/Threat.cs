using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{
    internal class Threat
    {
        public BoardLocation? FirstPieceThreateningKingBoardLocation { get; set; }

        public BoardLocation? SecondPieceThreateningKingBoardLocation { get; set; }

        public bool HasMultipleKingThreats => FirstPieceThreateningKingBoardLocation.HasValue && SecondPieceThreateningKingBoardLocation.HasValue;

        public ThreatDirection ThreatDirection { get; private set; }

        public ThreatDirection? Pin { get; private set; }

        public void AddDirection(ThreatDirection newDirection)
        {
            ThreatDirection |= newDirection;
        }

        public void AddPin(ThreatDirection newPin)
        {
            if (Pin.HasValue)
            {
                Pin |= newPin;
            }
            else
            {
                Pin = newPin;
            }
        }

        public override string ToString()
        {
            return $"{ThreatDirection}-{(Pin.HasValue?Pin.ToString():"None")}";
        }
    }
}
