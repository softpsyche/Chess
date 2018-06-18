using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Implementation
{
    public class MoveProvider
    {
        private readonly ThreatProvider _threatProvider;

        public MoveProvider(ThreatProvider threatProvider)
        {
            _threatProvider = threatProvider;
        }

        public List<Move> GetLegalMoves()
        {
            throw new NotImplementedException();
        }
    }
}
