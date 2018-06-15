﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Homeless
{

    public class Game
    {
        private readonly Board _board;
        private GameState _gameState = GameState.InPlay;
        private List<GameMove> _moves = new List<GameMove>();

        public bool GameIsOver => _gameState != GameState.InPlay;
        public Player PlayerTurn => (_moves.Count % 2) == 0 ? Player.White : Player.Black;
        public bool IsValidMove(GameMove gameMove) => GameIsOver ? false : GetLegalMoves().Contains(gameMove);

        public GameState MakeMove(GameMove gameMove)
        {
            return _gameState;
        }

        public List<GameMove> GetLegalMoves()
        {
            var moves = new List<GameMove>();

            if (GameIsOver) return moves;


            return null;
        }

        public Game(Board board)
        {
            _board = board;
        }

        #region Private Methods
        public bool IsKingInCheck(Player player)
        {
            //Check for knight threats...
            return false;
        }
        public Dictionary<BoardLocation, PinType> GetThreatenedBoardLocations(Player player)
        {
            //find all opposing pieces
            var opposingPlayer = player.OpposingPlayer();
            var opposingPieces = _board.GetPiecesForPlayer(opposingPlayer);

            Dictionary<BoardLocation, PinType> threats = new Dictionary<BoardLocation, PinType>();

            opposingPieces.ForEach(
        }
        private void AddPieceThreats(ChessPiece chessPiece, Dictionary<BoardLocation, PinType> threatenedBoardLocations)
        {
            switch (chessPiece)
            {
                case ChessPiece.BlackPawn:
                case ChessPiece.WhitePawn:
                    break;
                case ChessPiece.BlackRook:
                case ChessPiece.WhiteRook:

                    break;
                case ChessPiece.BlackKnight:
                case ChessPiece.WhiteKnight:
                    break;
                case ChessPiece.BlackBishop:
                case ChessPiece.WhiteBishop:
                    break;
                case ChessPiece.BlackQueen:
                case ChessPiece.WhiteQueen:
                    break;
                case ChessPiece.BlackKing:
                case ChessPiece.WhiteKing:
                    break;
                default:
                    throw new Exception($"Unexpected chessPiece value {chessPiece.ToString()}");
            }
        }

        #endregion

    }

    /// <summary>
    /// This class is immutable and thus also threadsafe.
    /// </summary>
    public sealed class GameMove
    {
        public BoardLocation Source { get; private set; }
        public BoardLocation Destination { get; private set; }

        public GameMove(BoardLocation source, BoardLocation destination)
        {
            Source = source;
            Destination = destination;
        }

        public override bool Equals(object obj)
        {
            var other = obj as GameMove;
            if ((obj == null) || (other == null)) return false;

            return 
                Source.Equals(other.Source) &&
                Destination.Equals(other.Destination);
        }

        public override int GetHashCode()
        {
            return Source.ToByte().GetHashCode() ^ Destination.ToByte().GetHashCode();
        }
    }

    /// <summary>
    /// Describes a 
    /// </summary>
    [Flags]
    public enum PinType : byte
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        Slope = 4,
        Grade = 8
    }

    public enum GameState : byte
    {   //from https://en.wikipedia.org/wiki/Draw_(chess)
        InPlay = 0,
        WhiteWin = 1,
        BlackWin = 2,
        DrawStalemate = 3,// when the player to move has no legal move and is not in check
        DrawThreeFoldRepetition = 4, // when the same position occurs three times with the same player to move
        DrawFiftyMoveRule = 5,// when the last fifty successive moves made by both players contain no capture or pawn move
        DrawInDeadPosition = 6// when no sequence of legal moves can lead to checkmate, most commonly when neither player has sufficient material to checkmate the opponent
    }

    public class Board
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
        private static readonly ChessPiece[] StartingBoard;

        private ChessPiece[] BoardSquares = null;

        public ChessPiece this[BoardLocation boardLocation]
        {
            get
            {
                return BoardSquares[boardLocation.ToByte()];
            }
            set
            {
                BoardSquares[boardLocation.ToByte()] = value;
            }
        }

        public Board()
        {
            Reset();
        }

        public void Reset() => BoardSquares = (ChessPiece[])StartingBoard.Clone();

        public ChessPiece[] GetPiecesForPlayer(Player player)
            => (player == Player.Black) ? 
                BoardSquares.Where(a => a.ToByte() >= 10 && a.ToByte() <= 15).ToArray(): 
                BoardSquares.Where(a => a.ToByte() >= 20 && a.ToByte() <= 25).ToArray();

        static Board()
        {
            StartingBoard = new ChessPiece[64];

            //White peasantry
            StartingBoard[BoardLocation.A2.ToByte()] = ChessPiece.WhitePawn;
            StartingBoard[BoardLocation.B2.ToByte()] = ChessPiece.WhitePawn;
            StartingBoard[BoardLocation.C2.ToByte()] = ChessPiece.WhitePawn;
            StartingBoard[BoardLocation.D2.ToByte()] = ChessPiece.WhitePawn;
            StartingBoard[BoardLocation.E2.ToByte()] = ChessPiece.WhitePawn;
            StartingBoard[BoardLocation.F2.ToByte()] = ChessPiece.WhitePawn;
            StartingBoard[BoardLocation.G2.ToByte()] = ChessPiece.WhitePawn;
            StartingBoard[BoardLocation.H2.ToByte()] = ChessPiece.WhitePawn;
            //White aristocracy
            StartingBoard[BoardLocation.A1.ToByte()] = ChessPiece.WhiteRook;
            StartingBoard[BoardLocation.B1.ToByte()] = ChessPiece.WhiteKnight;
            StartingBoard[BoardLocation.C1.ToByte()] = ChessPiece.WhiteBishop;
            StartingBoard[BoardLocation.D1.ToByte()] = ChessPiece.WhiteQueen;
            StartingBoard[BoardLocation.E1.ToByte()] = ChessPiece.WhiteKing;
            StartingBoard[BoardLocation.F1.ToByte()] = ChessPiece.WhiteBishop;
            StartingBoard[BoardLocation.G1.ToByte()] = ChessPiece.WhiteKnight;
            StartingBoard[BoardLocation.H1.ToByte()] = ChessPiece.WhiteRook;

            //Black peasantry
            StartingBoard[BoardLocation.A7.ToByte()] = ChessPiece.BlackPawn;
            StartingBoard[BoardLocation.B7.ToByte()] = ChessPiece.BlackPawn;
            StartingBoard[BoardLocation.C7.ToByte()] = ChessPiece.BlackPawn;
            StartingBoard[BoardLocation.D7.ToByte()] = ChessPiece.BlackPawn;
            StartingBoard[BoardLocation.E7.ToByte()] = ChessPiece.BlackPawn;
            StartingBoard[BoardLocation.F7.ToByte()] = ChessPiece.BlackPawn;
            StartingBoard[BoardLocation.G7.ToByte()] = ChessPiece.BlackPawn;
            StartingBoard[BoardLocation.H7.ToByte()] = ChessPiece.BlackPawn;
            //Black aristocracy                                    
            StartingBoard[BoardLocation.A8.ToByte()] = ChessPiece.BlackRook;
            StartingBoard[BoardLocation.B8.ToByte()] = ChessPiece.BlackKnight;
            StartingBoard[BoardLocation.C8.ToByte()] = ChessPiece.BlackBishop;
            StartingBoard[BoardLocation.D8.ToByte()] = ChessPiece.BlackQueen;
            StartingBoard[BoardLocation.E8.ToByte()] = ChessPiece.BlackKing;
            StartingBoard[BoardLocation.F8.ToByte()] = ChessPiece.BlackBishop;
            StartingBoard[BoardLocation.G8.ToByte()] = ChessPiece.BlackKnight;
            StartingBoard[BoardLocation.H8.ToByte()] = ChessPiece.BlackRook;
        }
    }

    internal static class Extensions
    {
        public static byte ToByte(this BoardLocation boardLocation) => (byte)boardLocation;

        public static byte ToByte(this ChessPiece boardSquare) => (byte)boardSquare;

        public static Player OpposingPlayer(this Player player) => player == Player.White ? Player.Black : Player.White;

        /// <summary>
        /// Performs the specified action on each element of the enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }
    }

    public enum BoardLocation : byte
    {
        //A
        A1 = 0,
        A2,
        A3,
        A4,
        A5,
        A6,
        A7,
        A8,
        //B
        B1,
        B2,
        B3,
        B4,
        B5,
        B6,
        B7,
        B8,
        //C
        C1,
        C2,
        C3,
        C4,
        C5,
        C6,
        C7,
        C8,
        //D
        D1,
        D2,
        D3,
        D4,
        D5,
        D6,
        D7,
        D8,
        //E
        E1,
        E2,
        E3,
        E4,
        E5,
        E6,
        E7,
        E8,
        //F
        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        //G
        G1,
        G2,
        G3,
        G4,
        G5,
        G6,
        G7,
        G8,
        //H
        H1,
        H2,
        H3,
        H4,
        H5,
        H6,
        H7,
        H8
    }

    public enum Player : byte
    {
        White = 0,
        Black
    }

    public enum ChessPiece : byte
    {
        None = 0,
        BlackPawn=10,
        BlackKnight=11,
        BlackBishop=12,
        BlackRook=13,
        BlackQueen=14,
        BlackKing=15,
        WhitePawn=20,
        WhiteKnight=21,
        WhiteBishop=22,
        WhiteRook=23,
        WhiteQueen=24,
        WhiteKing=26
    }

    public class ChessException : Exception
    {

    }
}
