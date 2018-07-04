using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcesoft.Chess.Implementation
{
    public class Board : IReadOnlyBoard
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

        private ChessPiece[] _board = null;

        public ChessPiece this[BoardLocation boardLocation]
        {
            get
            {
                return _board[boardLocation.ToByte()];
            }
            set
            {
                _board[boardLocation.ToByte()] = value;
            }
        }

        public Board()
        {
            Reset();
        }

        public void Reset() => _board = (ChessPiece[])StartingBoard.Clone();

        public void Clear() => _board = new ChessPiece[64];

        public List<BoardLocation> FindPlayerPieceLocations(Player player)
        {
            var listy = new List<BoardLocation>();
            byte lowRange, upperRange;
            if (player == Player.White)
            {
                lowRange = ChessPiece.WhitePawn.ToByte();
                upperRange = ChessPiece.WhiteKing.ToByte();
            }
            else
            {
                lowRange = ChessPiece.BlackPawn.ToByte();
                upperRange = ChessPiece.BlackKing.ToByte();
            }

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                var pieceValue = (byte)_board[i];

                if (pieceValue >= lowRange && pieceValue <= upperRange)
                {
                    listy.Add((BoardLocation)i);
                }
            }

            return listy;
        }

        //public List<ChessPiece> FindPieces()
        //{
        //    return _board.Where(a => a != ChessPiece.None).ToList();
        //}
        public List<ChessPiece> FindPieces(Player player)
        {
            byte lowRange, upperRange;
            if (player == Player.White)
            {
                lowRange = ChessPiece.WhitePawn.ToByte();
                upperRange = ChessPiece.WhiteKing.ToByte();
            }
            else
            {
                lowRange = ChessPiece.BlackPawn.ToByte();
                upperRange = ChessPiece.BlackKing.ToByte();
            }

            return _board.Where(a => a.ToByte() >= lowRange && a.ToByte() <= upperRange).ToList();
        }

        public BoardLocation GetKingsLocation(Player player)
        {
            var king = player == Player.Black ? ChessPiece.BlackKing : ChessPiece.WhiteKing;

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (_board[i] == king) return (BoardLocation)i;
            }

            throw new InvalidOperationException($"Unable to find king for player '{player}'. Invalid board configuration.");
        }

        public bool ContainsPawn(BoardLocation boardLocation)
        {
            var piece = this[boardLocation];

            return piece == ChessPiece.BlackPawn || piece == ChessPiece.WhitePawn;
        }

        /// <summary>
        /// Determines if the neighboring square is empty.
        /// Returns true if the neighboring square exists and is empty. Otherwise returns false.
        /// </summary>
        /// <param name="boardLocation"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool NeighboringLocationIsEmpty(BoardLocation boardLocation, Direction direction)
        {
            var neighbor = boardLocation.Neighbor(direction);

            if (neighbor.HasValue && this[neighbor.Value] == ChessPiece.None)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines if the neighboring square is occupied by the given player.
        /// Returns true if the neighboring square exists and is occupied by the given player. Otherwise returns false.
        /// </summary>
        /// <param name="boardLocation"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool NeighboringLocationIsOccupiedBy(BoardLocation boardLocation, Direction direction, Player player)
        {
            var neighbor = boardLocation.Neighbor(direction);

            if ((neighbor.HasValue) &&
                (this[neighbor.Value].BelongsTo(player)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines if the neighboring square is empty or occupied by the supplied player.
        /// Returns true if the neighboring square exists and is either empty or has a piece belonging to the given player. Otherwise returns false.
        /// </summary>
        /// <param name="boardLocation"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool NeighboringLocationIsEmptyOrOccupiedBy(BoardLocation boardLocation, Direction direction, Player player)
        {
            var neighbor = boardLocation.Neighbor(direction);
            var piece = this[neighbor.Value];

            if ((neighbor.HasValue) &&
                (piece.BelongsTo(player) || piece == ChessPiece.None))
            {
                return true;
            }

            return false;
        }

        public bool LocationIsEmpty(BoardLocation boardLocation) => this[boardLocation] == ChessPiece.None;
        public bool LocationIsOccupiedBy(BoardLocation boardLocation, Player player) => this[boardLocation].Player() == player;
        public bool LocationIsEmptyOrOccupiedBy(BoardLocation boardLocation, Player player)
        {
            //why not just chain together with isempy and occupiedby? performance dear watson, performance.
            var piece = this[boardLocation];

            return piece == ChessPiece.None || piece.Player() == player;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 7; row >= 0; row--)
            {
                for (int column = 0; column < 8; column++)
                {
                    var location = (BoardLocation)(row + (column * 8));

                    sb.Append(ToBoardLocationString(location, column == 0));
                }

                sb.Append("\r\n");
            }

            return sb.ToString();
        }

        private string ToBoardLocationString(BoardLocation boardLocation, bool printStartingPipe)
        {
            var piece = this[boardLocation];
            string prefix = piece.BelongsToWhite() ? "W" : "B";
            string pieceString = "  ";

            switch (piece)
            {
                case ChessPiece.WhitePawn:
                case ChessPiece.BlackPawn:
                    pieceString = $"{prefix}P";
                    break;
                case ChessPiece.WhiteKnight:
                case ChessPiece.BlackKnight:
                    pieceString = $"{prefix}N";
                    break;
                case ChessPiece.WhiteBishop:
                case ChessPiece.BlackBishop:
                    pieceString = $"{prefix}B";
                    break;
                case ChessPiece.WhiteRook:
                case ChessPiece.BlackRook:
                    pieceString = $"{prefix}R";
                    break;
                case ChessPiece.WhiteQueen:
                case ChessPiece.BlackQueen:
                    pieceString = $"{prefix}Q";
                    break;
                case ChessPiece.WhiteKing:
                case ChessPiece.BlackKing:
                    pieceString = $"{prefix}K";
                    break;
            }

            return printStartingPipe ? $"|{pieceString}|" : $"{pieceString}|";
        }

        public Board Clone()
        {
            var newBoard = new Board();

            newBoard._board = (ChessPiece[])_board.Clone();

            return newBoard;
        }

        #region Static methods
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
        #endregion
    }
}
