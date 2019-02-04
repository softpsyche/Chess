using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Models
{
    public enum SpecialMoveType : int
    {
        PawnPromotionKnight,
        PawnPromotionBishop,
        PawnPromotionRook,
        PawnPromotionQueen, 
        AuPassant,
        CastleKingside, 
        CastleQueenside        
    }
}
