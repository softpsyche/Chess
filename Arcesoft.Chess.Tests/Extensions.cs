using Arcesoft.Chess.Implementation;
using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Arcesoft.Chess.Tests
{
    internal static class Extensions
    {
        //PGN format for the board
        //    A  B  C  D  E  F  G  H
        //8:| 7|15|23|31|39|47|55|63|
        //7:| 6|14|22|30|38|46|54|62|
        //6:| 5|13|21|29|37|45|53|61|
        //5:| 4|12|20|28|36|44|52|60|
        //4:| 3|11|19|27|35|43|51|59|
        //3:| 2|10|18|26|34|42|50|58|
        //2:| 1| 9|17|25|33|41|49|57|
        //1:| 0| 8|16|24|32|40|48|56|
        public static Board ToBoard(this Table table)
        {
            Board board = new Board();
            board.Clear();
            int boardPosition = 0;

            for (int column = 0; column < 8; column++)
            {
                for (int row = 7; row >= 0; row--)
                {
                    var chessPiece = table.Rows[row][column].ToChessPieceFromInputString();

                    board[(BoardLocation)boardPosition] = chessPiece;
                    boardPosition++;
                }
            }

            return board;
        }

        public static List<Move> ToMoves(this string pgnString)
        {
            if (string.IsNullOrWhiteSpace(pgnString))
                return new List<Move>();

            return pgnString.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.ToMove())
                .ToList();
        }

        public static Move ToMove(this string moveString)
        {
            var pieces = moveString.ToUpperInvariant().Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            return new Move(pieces[0].ToBoardLocation(), pieces[1].ToBoardLocation(), MoveType.Move);

        }

        public static BoardLocation ToBoardLocation(this string boardLocation)
        {
            return boardLocation.ToEnum<BoardLocation>();
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static Move ToMove(this Table table)
        {
            return table.ToMoves().Single();
        }

        public static List<Move> ToMoves(this Table table)
        {
            List<Move> moves = new List<Move>();

            foreach (var row in table.Rows)
            {
                moves.Add(new Move(
                    (BoardLocation)Enum.Parse(typeof(BoardLocation), row[nameof(Move.Source)]),
                    (BoardLocation)Enum.Parse(typeof(BoardLocation), row[nameof(Move.Destination)]),
                    table.Header.Contains(nameof(Move.Type)) ? MoveType.Move: (MoveType)Enum.Parse(typeof(MoveType), row[nameof(Move.Type)])));
            }

            return moves;
        }

        public static List<IMove> ToMoveHistory(this Table table)
        {
            List<IMove> moveHistories = new List<IMove>();

            foreach (var row in table.Rows)
            {
                moveHistories.Add(new Move(
                    (BoardLocation)Enum.Parse(typeof(BoardLocation), row[nameof(Move.Source)]),
                    (BoardLocation)Enum.Parse(typeof(BoardLocation), row[nameof(Move.Destination)]),
                    (MoveType)Enum.Parse(typeof(MoveType), row[nameof(Move.Type)])));
            }

            return moveHistories;
        }

        public static ChessPiece ToChessPieceFromInputString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ChessPiece.None;

            switch (value?.ToUpperInvariant())
            {
                case "WP":
                    return ChessPiece.WhitePawn;
                case "WN":
                    return ChessPiece.WhiteKnight;
                case "WB":
                    return ChessPiece.WhiteBishop;
                case "WR":
                    return ChessPiece.WhiteRook;
                case "WQ":
                    return ChessPiece.WhiteQueen;
                case "WK":
                    return ChessPiece.WhiteKing;
                case "BP":
                    return ChessPiece.BlackPawn;
                case "BN":
                    return ChessPiece.BlackKnight;
                case "BB":
                    return ChessPiece.BlackBishop;
                case "BR":
                    return ChessPiece.BlackRook;
                case "BQ":
                    return ChessPiece.BlackQueen;
                case "BK":
                    return ChessPiece.BlackKing;
                default:
                    throw new InvalidOperationException($"Unexpected piece value of '{value}' found.");
            }
        }

        public static void SetPrivateField(this Object obj, string name, Object value)
        {
            obj.GetType()
               .GetField(name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
               .SetValue(obj, value);
        }
        public static void SetPrivateProperty(this Object obj, string name, Object value)
        {
            obj.GetType()
               .GetProperty(name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.SetProperty)
               .SetValue(obj, value);
        }
    }
}
