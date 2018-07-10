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

        IReadOnlyList<IMove> MoveHistory { get; }

        IReadOnlyList<IMove> FindMoves();

        bool IsLegalMove(BoardLocation source, BoardLocation destination, PawnPromotionType? promotionType = null);
        bool IsLegalMove(IMove gameMove);

        void MakeMove(BoardLocation source, BoardLocation destination, PawnPromotionType? promotionType = null);
        void MakeMove(IMove gameMove);

        void UndoLastMove();
    }
}
