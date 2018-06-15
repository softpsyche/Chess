using System;
using System.Collections.Generic;

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
        private static readonly BoardSquare[] StartingBoard;

        private BoardSquare[] BoardSquares = null;

        public BoardSquare this[BoardLocation boardLocation]
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

        public void Reset() => BoardSquares = (BoardSquare[])StartingBoard.Clone();

        static Board()
        {
            StartingBoard = new BoardSquare[64];

            //White peasantry
            StartingBoard[BoardLocation.A2.ToByte()] = BoardSquare.WhitePawn;
            StartingBoard[BoardLocation.B2.ToByte()] = BoardSquare.WhitePawn;
            StartingBoard[BoardLocation.C2.ToByte()] = BoardSquare.WhitePawn;
            StartingBoard[BoardLocation.D2.ToByte()] = BoardSquare.WhitePawn;
            StartingBoard[BoardLocation.E2.ToByte()] = BoardSquare.WhitePawn;
            StartingBoard[BoardLocation.F2.ToByte()] = BoardSquare.WhitePawn;
            StartingBoard[BoardLocation.G2.ToByte()] = BoardSquare.WhitePawn;
            StartingBoard[BoardLocation.H2.ToByte()] = BoardSquare.WhitePawn;
            //White aristocracy
            StartingBoard[BoardLocation.A1.ToByte()] = BoardSquare.WhiteRook;
            StartingBoard[BoardLocation.B1.ToByte()] = BoardSquare.WhiteKnight;
            StartingBoard[BoardLocation.C1.ToByte()] = BoardSquare.WhiteBishop;
            StartingBoard[BoardLocation.D1.ToByte()] = BoardSquare.WhiteQueen;
            StartingBoard[BoardLocation.E1.ToByte()] = BoardSquare.WhiteKing;
            StartingBoard[BoardLocation.F1.ToByte()] = BoardSquare.WhiteBishop;
            StartingBoard[BoardLocation.G1.ToByte()] = BoardSquare.WhiteKnight;
            StartingBoard[BoardLocation.H1.ToByte()] = BoardSquare.WhiteRook;

            //Black peasantry
            StartingBoard[BoardLocation.A7.ToByte()] = BoardSquare.BlackPawn;
            StartingBoard[BoardLocation.B7.ToByte()] = BoardSquare.BlackPawn;
            StartingBoard[BoardLocation.C7.ToByte()] = BoardSquare.BlackPawn;
            StartingBoard[BoardLocation.D7.ToByte()] = BoardSquare.BlackPawn;
            StartingBoard[BoardLocation.E7.ToByte()] = BoardSquare.BlackPawn;
            StartingBoard[BoardLocation.F7.ToByte()] = BoardSquare.BlackPawn;
            StartingBoard[BoardLocation.G7.ToByte()] = BoardSquare.BlackPawn;
            StartingBoard[BoardLocation.H7.ToByte()] = BoardSquare.BlackPawn;
            //Black aristocracy                                    
            StartingBoard[BoardLocation.A8.ToByte()] = BoardSquare.BlackRook;
            StartingBoard[BoardLocation.B8.ToByte()] = BoardSquare.BlackKnight;
            StartingBoard[BoardLocation.C8.ToByte()] = BoardSquare.BlackBishop;
            StartingBoard[BoardLocation.D8.ToByte()] = BoardSquare.BlackQueen;
            StartingBoard[BoardLocation.E8.ToByte()] = BoardSquare.BlackKing;
            StartingBoard[BoardLocation.F8.ToByte()] = BoardSquare.BlackBishop;
            StartingBoard[BoardLocation.G8.ToByte()] = BoardSquare.BlackKnight;
            StartingBoard[BoardLocation.H8.ToByte()] = BoardSquare.BlackRook;
        }
    }

    internal static class Extensions
    {
        public static byte ToByte(this BoardLocation boardLocation) => (byte)boardLocation;
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

    public enum BoardSquare : byte
    {
        Empty = 0,
        BlackPawn,
        BlackKnight,
        BlackBishop,
        BlackRook,
        BlackQueen,
        BlackKing,
        WhitePawn,
        WhiteKnight,
        WhiteBishop,
        WhiteRook,
        WhiteQueen,
        WhiteKing
    }

    public class ChessException : Exception
    {

    }
}
