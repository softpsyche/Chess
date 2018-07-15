using Arcesoft.Chess.Implementation;
using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;

namespace Arcesoft.Chess.ArtificialIntelligence
{
    public interface IArtificialIntelligence
    {
        IMove TryFindBestMove(IGame game, int depth);
    }

}
