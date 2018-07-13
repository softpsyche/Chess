using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models.Internal
{
    internal class ThreatMatrix : Dictionary<BoardLocation, Threat>
    {
        public void Add(BoardLocation boardLocation, ThreatDirection threatDirection, ThreatDirection? pin)
        {
            var threat = new Threat();
            threat.AddDirection(threatDirection);
            if (pin.HasValue) threat.AddPin(pin.Value);

            Add(boardLocation, threat);
        }

        public override string ToString() => ToVisualString(this);

        private static string ToVisualString(IDictionary<BoardLocation, Threat> threatDictionary)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 7; row >= 0; row--)
            {
                for (int column = 0; column < 8; column++)
                {
                    var location = (BoardLocation)(row + (column * 8));

                    sb.Append(ToBoardLocationThreatString(threatDictionary, location, column == 0));
                }

                sb.Append("\r\n");
            }

            return sb.ToString();
        }

        private static string ToBoardLocationThreatString(IDictionary<BoardLocation, Threat> threatDictionary, BoardLocation boardLocation, bool printStartingPipe)
        {
            string pieceString = "  ";

            if (threatDictionary.ContainsKey(boardLocation))
            {
                pieceString = "<>";
            }

            return printStartingPipe ? $"|{pieceString}|" : $"{pieceString}|";
        }
    }
}
