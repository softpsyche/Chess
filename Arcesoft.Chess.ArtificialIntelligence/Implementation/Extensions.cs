using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.ArtificialIntelligence.Implementation
{
    internal static class Extensions
    {
        public static List<ChessPiece> AllPieces(this IReadOnlyBoard readOnlyBoard)
        {
            List<ChessPiece> listy = new List<ChessPiece>();
            foreach (ChessPiece chessPiece in readOnlyBoard)
            {
                if (chessPiece != ChessPiece.None)
                {
                    listy.Add(chessPiece);
                }
            }

            return listy;
        }
    }
}
