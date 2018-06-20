using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcesoft.Chess.Implementation
{
    internal class Game : IGame
    {
        private readonly Board _board;
        private readonly ThreatProvider _threatProvider;

        private GameState _gameState = GameState.InPlay;
        private List<Move> _moveHistory = new List<Move>();

        public bool GameIsOver => _gameState != GameState.InPlay;

        public GameState GameState => _gameState;

        public IReadOnlyBoard Board => _board as IReadOnlyBoard;

        public Player CurrentPlayer => (_moveHistory.Count % 2) == 0 ? Player.White : Player.Black;

        public bool IsValidMove(Move gameMove) => GameIsOver ? false : FindMoves().Contains(gameMove);

        public Game(Board board, ThreatProvider threatProvider)
        {
            _board = board;
            _threatProvider = threatProvider;
        }

        public GameState MakeMove(Move gameMove)
        {
            throw new NotImplementedException();
        }

        public string GetThreatenedBoardDisplay(Player player)
        {
            var threats = _threatProvider.FindThreatsForPlayer(_board, player);

            StringBuilder sb = new StringBuilder();

            for (int row = 7; row >= 0; row--)
            {
                for (int column = 0; column < 8; column++)
                {
                    var location = (BoardLocation)(row + (column * 8));

                    var threatString = threats.ContainsKey(location) ? "X|" : " |";
                    sb.Append(column == 0 ? "|" + threatString : threatString);
                }

                sb.Append("\r\n");
            }

            return sb.ToString();
        }

        public List<Move> FindMoves()
        {
            var moves = new List<Move>();

            if (GameIsOver)
            {
                return moves;
            }

            //get threats against current player
            var threats = _threatProvider.FindThreatsForPlayer(_board, CurrentPlayer);

            //find da man and
            //find if any chumps be threatenin da man
            var playerKingLocation = _board.GetKingsLocation(CurrentPlayer);
            var playerKingThreatDirection = threats.ContainsKey(playerKingLocation) ? threats[playerKingLocation] : default(ThreatDirection?);

            //if ad man is in check, we need to find only the moves that remove the check...
            if (playerKingThreatDirection.HasValue)
            {
                FindMovesWhenKingsInCheck(
                    moves,
                    threats,
                    playerKingLocation,
                    playerKingThreatDirection);
            }
            else
            {//da man is not in check..

                //get all pieces for current player. 
                var playerPieceLocations = _board.FindPlayerPieceLocations(CurrentPlayer);

                //find moves for each given piece
                playerPieceLocations
                    .ForEach(a => FindMoves(moves, threats, a));
            }

            //return list of available moves
            return moves;
        }

        #region Private methods

        private void FindMovesWhenKingsInCheck(
            List<Move> moves,
            Dictionary<BoardLocation, ThreatDirection?> threats,
            BoardLocation playerKingLocation,
            ThreatDirection? playerKingThreatDirection)
        {
            //if the king has multiple threats, the only valid moves require the king moving
            if (playerKingThreatDirection.HasMultipleThreats())
            {
                //only chance for is for the king to move to a location that is no longer threatening him. 
                FindMoves(moves, threats, playerKingLocation);
            }
            else
            {
                //the king is in check but not on multiple fronts
                throw new NotImplementedException();
            }
        }

        private void FindMoves(
            List<Move> moves,
            Dictionary<BoardLocation, ThreatDirection?> threats,
            BoardLocation playerPieceLocation)
        {
            var chessPiece = _board[playerPieceLocation];
            var player = chessPiece.Player();
            var playerPieceThreatDirection = threats.ContainsKey(playerPieceLocation) ? threats[playerPieceLocation] : default(ThreatDirection?);

            switch (chessPiece)
            {
                case ChessPiece.BlackPawn:
                case ChessPiece.WhitePawn:
                    FindPawnMoves(moves, threats, player, playerPieceLocation, playerPieceThreatDirection);
                    break;
                case ChessPiece.BlackKnight:
                case ChessPiece.WhiteKnight:
                    FindKnightMoves(moves, threats, player, playerPieceLocation, playerPieceThreatDirection);
                    break;
                case ChessPiece.BlackBishop:
                case ChessPiece.WhiteBishop:
                    FindBishopMoves(moves, threats, player, playerPieceLocation, playerPieceThreatDirection);
                    break;
                case ChessPiece.BlackRook:
                case ChessPiece.WhiteRook:
                    FindRookMoves(moves, threats, player, playerPieceLocation, playerPieceThreatDirection);
                    break;
                case ChessPiece.BlackQueen:
                case ChessPiece.WhiteQueen:
                    FindQueenMoves(moves, threats, player, playerPieceLocation, playerPieceThreatDirection);
                    break;
                case ChessPiece.BlackKing:
                case ChessPiece.WhiteKing:
                    FindKingMoves(moves, threats, player, playerPieceLocation, playerPieceThreatDirection);
                    break;
                default:
                    break;
            }
        }

        private void FindPawnMoves(
            List<Move> moves,
            Dictionary<BoardLocation, ThreatDirection?> threats,
            Player player,
            BoardLocation playerPieceLocation,
            ThreatDirection? playerPieceThreatDirection)
        {
            var opposingPlayer = player.OpposingPlayer();
            var marchDirection = player == Player.White ? Direction.North : Direction.South;
            var gradeAttackDirection = player == Player.White ? Direction.NorthEast : Direction.SouthWest;
            var slopeAttackDirection = player == Player.White ? Direction.NorthWest : Direction.SouthEast;

            //check to see if we can move up/down
            //note that any diagonal or horizontal threats will block these moves
            if ((playerPieceThreatDirection.HasThreats(ThreatDirection.Horizontal | ThreatDirection.Diagonal) == false) &&
                (_board.NeighboringLocationIsEmpty(playerPieceLocation, marchDirection)))
            {
                var firstMarchLocation = playerPieceLocation.Neighbor(marchDirection).Value;
                moves.Add(new Move(playerPieceLocation, firstMarchLocation));


                //we can also move up two spaces if the next northern square is empty
                if ((playerPieceLocation.IsPawnStartingLocation(player)) &&
                    (_board.NeighboringLocationIsEmptyOrOccupiedBy(firstMarchLocation, marchDirection, opposingPlayer)))
                {
                    moves.Add(new Move(playerPieceLocation, firstMarchLocation.Neighbor(marchDirection).Value));
                }
            }

            //check to see if we can move along the grade
            //note that any line or slope threats will block this move
            if (playerPieceThreatDirection.HasThreats(ThreatDirection.Line | ThreatDirection.Slope) == false)
            {
                //if we have an opposing piece in the next square, make hte move
                if (_board.NeighboringLocationIsOccupiedBy(playerPieceLocation, gradeAttackDirection, opposingPlayer))
                {
                    moves.Add(new Move(playerPieceLocation, playerPieceLocation.Neighbor(gradeAttackDirection).Value));
                }
                else if (LastMoveAllowsEnPassantFor(playerPieceLocation, gradeAttackDirection))
                {//we can do an en passant.
                    moves.Add(new Move(playerPieceLocation, playerPieceLocation.Neighbor(gradeAttackDirection).Value));
                }

            }

            //check to see if we can move along the slope
            //note that any line or grade threats will block this move
            if (playerPieceThreatDirection.HasThreats(ThreatDirection.Line | ThreatDirection.Grade) == false)
            {
                //if we have an opposing piece in the next square, make hte move
                if (_board.NeighboringLocationIsOccupiedBy(playerPieceLocation, slopeAttackDirection, opposingPlayer))
                {
                    moves.Add(new Move(playerPieceLocation, playerPieceLocation.Neighbor(slopeAttackDirection).Value));
                }
                else if (LastMoveAllowsEnPassantFor(playerPieceLocation, slopeAttackDirection))
                {//we can do an en passant.
                    moves.Add(new Move(playerPieceLocation, playerPieceLocation.Neighbor(slopeAttackDirection).Value));
                }
            }

        }

        private bool LastMoveAllowsEnPassantFor(BoardLocation pawnLocation, Direction pawnCaptureDirection)
        {
            var lastMove = _moveHistory.LastOrDefault();
            if (lastMove == null)
            {
                return false;
            }

            var pawnLocationColumn = pawnLocation.Column();
            int lastMoveExpectedColumn = 0;

            switch (pawnCaptureDirection)
            {
                case Direction.NorthEast:
                case Direction.SouthEast:
                    lastMoveExpectedColumn = pawnLocationColumn + 1;
                    break;
                case Direction.NorthWest:
                case Direction.SouthWest:
                    lastMoveExpectedColumn = pawnLocationColumn - 1;
                    break;
            }

            //If (and ONLY if):
            //-: the last move was a pawn move AND
            //-: the last move was two spaces AND
            //-: the last move was right next to the given pawn in the right direction
            //(Jesus christ almighty this was not easy)
            if ((_board.ContainsPawn(lastMove.Destination)) &&
                (Math.Abs(lastMove.Destination.ToByte() - lastMove.Source.ToByte()) == 2) &&
                (lastMove.Destination.Column() == lastMoveExpectedColumn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void FindKnightMoves(
            List<Move> moves,
            Dictionary<BoardLocation, ThreatDirection?> threats,
            Player player,
            BoardLocation playerPieceLocation,
            ThreatDirection? playerPieceThreatDirection)
        {
            //There are NO possible moves if the knight is under non-direct threat because
            //he is not capable of removing any linear or diagonal threats ever. This is due to his L-shaped
            //move pattern (thank god for small favors)
            if (playerPieceThreatDirection.HasThreats(ThreatDirection.NonDirect))
            {
                return;
            }

            //we will use the threats matrix for this knight to calculate our moves...
            var knightThreatLocations = _threatProvider.FindThreatsForBoardLocation(_board, playerPieceLocation);

            foreach (var location in knightThreatLocations.Keys)
            {
                if (_board.LocationIsEmptyOrOccupiedBy(location, player.OpposingPlayer()))
                {
                    moves.Add(new Move(playerPieceLocation, location));
                }
            }
        }

        private void FindBishopMoves(
            List<Move> moves,
            Dictionary<BoardLocation, ThreatDirection?> threats,
            Player player,
            BoardLocation playerPieceLocation,
            ThreatDirection? playerPieceThreatDirection)
        {
            //There are NO possible moves if the bishop is under any line threats. 
            if (playerPieceThreatDirection.HasThreats(ThreatDirection.Line))
            {
                return;
            }

            //we will use the threats matrix for this bishop to calculate our moves...
            var bishopThreatLocations = _threatProvider.FindThreatsForBoardLocation(_board, playerPieceLocation);
            var hasGradeThreats = playerPieceThreatDirection.HasThreats(ThreatDirection.Grade);
            var hasSlopeThreats = playerPieceThreatDirection.HasThreats(ThreatDirection.Slope);

            foreach (var location in bishopThreatLocations.Keys)
            {
                //only add moves that do a capture or move along the same threat vector
                if ((_board.LocationIsEmptyOrOccupiedBy(location, player.OpposingPlayer())) &&
                    ((hasGradeThreats == false) || (location.IsOnSameGradeAs(playerPieceLocation))) &&
                    ((hasSlopeThreats == false) || (location.IsOnSameSlopeAs(playerPieceLocation))))
                {
                    moves.Add(new Move(playerPieceLocation, location));
                }
            }
        }

        private void FindRookMoves(
            List<Move> moves,
            Dictionary<BoardLocation, ThreatDirection?> threats,
            Player player,
            BoardLocation playerPieceLocation,
            ThreatDirection? playerPieceThreatDirection)
        {
            //There are NO possible moves if the rook is under any diagonal threats. 
            if (playerPieceThreatDirection.HasThreats(ThreatDirection.Diagonal))
            {
                return;
            }

            //we will use the threats matrix for this rook to calculate our moves...
            var rookThreatLocations = _threatProvider.FindThreatsForBoardLocation(_board, playerPieceLocation);
            var hasVerticalThreats = playerPieceThreatDirection.HasThreats(ThreatDirection.Vertical);
            var hasHorizontalThreats = playerPieceThreatDirection.HasThreats(ThreatDirection.Horizontal);

            foreach (var location in rookThreatLocations.Keys)
            {
                //only add moves that do a capture or move along the same threat vector
                if ((_board.LocationIsEmptyOrOccupiedBy(location, player.OpposingPlayer())) &&
                    ((hasVerticalThreats == false) || (location.IsOnSameColumnAs(playerPieceLocation))) &&
                    ((hasHorizontalThreats == false) || (location.IsOnSameRowAs(playerPieceLocation))))
                {
                    moves.Add(new Move(playerPieceLocation, location));
                }
            }
        }

        private void FindQueenMoves(
            List<Move> moves,
            Dictionary<BoardLocation, ThreatDirection?> threats,
            Player player,
            BoardLocation playerPieceLocation,
            ThreatDirection? playerPieceThreatDirection)
        {
            //again, this trick comes in handy...
            FindBishopMoves(moves, threats, player, playerPieceLocation, playerPieceThreatDirection);
            FindRookMoves(moves, threats, player, playerPieceLocation, playerPieceThreatDirection);
        }

        private void FindKingMoves(
            List<Move> moves,
            Dictionary<BoardLocation, ThreatDirection?> threats,
            Player player,
            BoardLocation playerPieceLocation,
            ThreatDirection? playerPieceThreatDirection)
        {
            //we will use the threats matrix for this king to calculate our moves...
            var kingThreatLocations = _threatProvider.FindThreatsForBoardLocation(_board, playerPieceLocation);

            foreach (var location in kingThreatLocations.Keys)
            {
                //only add moves that do not have a threat on them
                if ((_board.LocationIsEmptyOrOccupiedBy(location, player.OpposingPlayer())) &&
                    (threats.ContainsKey(location) == false))
                {
                    moves.Add(new Move(playerPieceLocation, location));
                }
            }
        }
        #endregion
    }
}
