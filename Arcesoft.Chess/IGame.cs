using Arcesoft.Chess.Implementation;
using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess
{
    public interface IGame
    {
        Player CurrentPlayer { get; }

        bool GameIsOver { get; }

        GameState GameState { get; }

        IReadOnlyBoard Board { get; }

        IReadOnlyList<MoveHistory> MoveHistory { get; }

        IReadOnlyList<Move> FindMoves();

        bool IsLegalMove(Move gameMove);

        void MakeMove(Move gameMove);

        void UndoLastMove();
    }
}
