using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{
    public enum GameState : byte
    {   //from https://en.wikipedia.org/wiki/Draw_(chess)
        InPlay = 0,
        WhiteWin = 1,
        BlackWin = 2,
        DrawStalemate = 3,// when the player to move has no legal move and is not in check
        DrawThreeFoldRepetition = 4, // when the same position occurs three times with the same player to move
        DrawFiftyMoveRule = 5,// when the last fifty successive moves made by both players contain no capture or pawn move
        DrawInDeadPosition = 6,// when no sequence of legal moves can lead to checkmate, most commonly when neither player has sufficient material to checkmate the opponent
        DrawInsufficientMaterial = 7// when no sequence of legal moves can lead to checkmate, most commonly when neither player has sufficient material to checkmate the opponent
    }
}
