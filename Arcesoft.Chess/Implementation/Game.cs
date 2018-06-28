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
        private List<MoveHistory> _moveHistory = new List<MoveHistory>();
        private List<Move> _moves = null;
        private ThreatMatrix _threatMatrix = null;

        public IReadOnlyList<MoveHistory> MoveHistory => _moveHistory;

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

        public void MakeMove(Move gameMove)
        {
            if (GameIsOver)
            {
                throw new ChessException(ChessErrorCode.InvalidMoveGameOver, "The move is not valid because the game is over.");
            }

            if (!MoveIsLegal(gameMove))
            {
                throw new ChessException(ChessErrorCode.IllegalMove, "The move is not valid because it is not legal.");
            }

            //now we need to make the move..
            //Deal with these 'special' moves first
            if (MakeCastleMove(gameMove) || MakeAuPassantMove(gameMove))
            {
                return;
            }

            //make the normal move
            var moveResult = (_board[gameMove.Destination] == ChessPiece.None) ? MoveResult.None : MoveResult.Capture;
            MovePieceOnBoard(gameMove);
            _moveHistory.Add(new MoveHistory(gameMove.Source, gameMove.Destination, moveResult));

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
            if (_moves != null)
            {
                return _moves;
            }

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
            var playerKingThreat = threats.ContainsKey(playerKingLocation) ? threats[playerKingLocation] : null;

            //if da king has multiple threats then fleeing is his only option
            if (playerKingThreat!= null && playerKingThreat.HasMultipleKingThreats)
            {
                //only chance for is for the king to move to a location that is not threatened.
                FindMoves(moves, threats, playerKingLocation);
            }
            else
            {//we have a few more options

                //get all pieces for current player. 
                var playerPieceLocations = _board.FindPlayerPieceLocations(CurrentPlayer);

                //find moves for each given piece
                playerPieceLocations
                    .ForEach(a => FindMoves(moves, threats, a));

                //if the king is in check, now we need to trim the moves appropriately
                if (playerKingThreat != null)
                {
                    moves = TrimMovesForKingInCheck(moves, threats, playerKingLocation, playerKingThreat);
                }
            }

            //cache the moves and threats
            _moves = moves;
            _threatMatrix = threats;

            //return list of available moves
            return moves;
        }

        public void UndoLastMove()
        {
            throw new NotImplementedException();
        }

        #region Private methods
        private bool MakeAuPassantMove(Move move)
        {
            //if the moving piece is a pawn AND the destination is not on the same row (i.e. a capture) AND the destination is empty
            if ((!move.Source.IsOnSameRowAs(move.Destination)) &&
                (_board[move.Source].IsPawn(CurrentPlayer)) &&
                (_board[move.Destination] == ChessPiece.None))
            {
                MovePieceOnBoard(move);

                var capturedPawnLocation = CurrentPlayer == Player.White ? move.Destination - 1 : move.Destination + 1;
                _board[capturedPawnLocation] = ChessPiece.None;

                return true;
            }

            return false;
        }
        private bool MakeCastleMove(Move move)
        {
            BoardLocation? castlingRookLocation = null;
            BoardLocation? castlingRookDestination = null;

            if (_board[move.Source].IsKing(CurrentPlayer))
            {
                if (move.Source == BoardLocation.E1)
                {
                    if (move.Destination == BoardLocation.G1)
                    {
                        castlingRookLocation = BoardLocation.H1;
                        castlingRookDestination = BoardLocation.F1;
                    }
                    else if (move.Destination == BoardLocation.C1)
                    {
                        castlingRookLocation = BoardLocation.A1;
                        castlingRookDestination = BoardLocation.D1;
                    }
                }
                else if (move.Source == BoardLocation.E8)
                {
                    if (move.Destination == BoardLocation.G8)
                    {
                        castlingRookLocation = BoardLocation.H8;
                        castlingRookDestination = BoardLocation.F8;
                    }
                    else if (move.Destination == BoardLocation.C8)
                    {
                        castlingRookLocation = BoardLocation.A8;
                        castlingRookDestination = BoardLocation.D8;
                    }
                }
            }

            if (castlingRookLocation.HasValue)
            {
                //move the king
                MovePieceOnBoard(move);

                //move the rook
                MovePieceOnBoard(castlingRookLocation.Value, castlingRookDestination.Value);

                //add the move history
                _moveHistory.Add(new MoveHistory(move.Source, move.Destination, MoveResult.Castle));
            }

            return castlingRookLocation.HasValue;
        }
        private void MovePieceOnBoard(Move move)
        {
            _board[move.Destination] = _board[move.Source];
            _board[move.Source] = ChessPiece.None;
        }
        private void MovePieceOnBoard(BoardLocation source, BoardLocation destination)
        {
            _board[destination] = _board[source];
            _board[source] = ChessPiece.None;
        }

        private bool MoveIsLegal(Move move) => FindMoves().Contains(move);

        private List<Move> TrimMovesForKingInCheck(
            List<Move> moves,
            ThreatMatrix threats,
            BoardLocation playerKingLocation,
            Threat playerKingThreat)
        {
            var pieceThreateningKing = Board[playerKingThreat.FirstPieceThreateningKingBoardLocation.Value];

            //if we have a direct threat to the king then the only moves allowed are ones that capture
            //the piece which is threatening the king, or of course the ones that represent the king itself moving
            if (playerKingThreat.ThreatDirection == ThreatDirection.Direct)
            {
                return moves
                    .Where(a => playerKingLocation == a.Source || a.Destination == playerKingThreat.FirstPieceThreateningKingBoardLocation.Value)
                    .ToList();
            }
            else
            {
                //ok, so if this is a non-direct threat then any move that is either 
                //captures the piece in question or blocks the threat path is valid
                //AND of course, any move that represents the king moving
                List<BoardLocation> allowedBoardLocations = new List<BoardLocation>();
                BoardLocation? currentLocation = playerKingThreat.FirstPieceThreateningKingBoardLocation.Value;
                var direction = playerKingThreat.ThreatDirection.ToDirection();

                //get all allowable moves
                while ((currentLocation != null) && (currentLocation.Value != playerKingLocation))
                {
                    allowedBoardLocations.Add(currentLocation.Value);

                    currentLocation = currentLocation.Neighbor(direction);
                }

                //get kings moves and remove them from the original moves list and put them into the finalmoves
                var finalMoves = moves.Where(a => a.Source == playerKingLocation).ToList();
                moves.RemoveAll(a => a.Source == playerKingLocation);

                //Now that the moves list only contains the moves that are non king moves, we can simply
                //join to the allowableboardlocations and add this to the final moves
                finalMoves.AddRange(
                    moves
                    .Join(allowedBoardLocations, a => a.Destination, a => a, (a, b) => a));

                return finalMoves;
            }
        }

        private void FindMoves(
            List<Move> moves,
            ThreatMatrix threats,
            BoardLocation playerPieceLocation)
        {
            var chessPiece = _board[playerPieceLocation];
            var player = chessPiece.Player();
            var playerPiecePinDirection = threats.ContainsKey(playerPieceLocation) ? threats[playerPieceLocation].Pin : default(ThreatDirection?);

            switch (chessPiece)
            {
                case ChessPiece.BlackPawn:
                case ChessPiece.WhitePawn:
                    FindPawnMoves(moves, threats, player, playerPieceLocation, playerPiecePinDirection);
                    break;
                case ChessPiece.BlackKnight:
                case ChessPiece.WhiteKnight:
                    FindKnightMoves(moves, threats, player, playerPieceLocation, playerPiecePinDirection);
                    break;
                case ChessPiece.BlackBishop:
                case ChessPiece.WhiteBishop:
                    FindBishopMoves(moves, threats, player, playerPieceLocation, playerPiecePinDirection);
                    break;
                case ChessPiece.BlackRook:
                case ChessPiece.WhiteRook:
                    FindRookMoves(moves, threats, player, playerPieceLocation, playerPiecePinDirection);
                    break;
                case ChessPiece.BlackQueen:
                case ChessPiece.WhiteQueen:
                    FindQueenMoves(moves, threats, player, playerPieceLocation, playerPiecePinDirection);
                    break;
                case ChessPiece.BlackKing:
                case ChessPiece.WhiteKing:
                    //da king cant be pinned yo!
                    FindKingMoves(moves, threats, player, playerPieceLocation);
                    break;
                default:
                    break;
            }
        }

        private void FindPawnMoves(
            List<Move> moves,
            ThreatMatrix threats,
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
                    (_board.NeighboringLocationIsEmpty(firstMarchLocation, marchDirection)))
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
            //-: we can thus infer it belongs to the opposite player AND
            //-: the last move was right next to the given pawn 
            //-: the last move was in the right column (based on capture direction)
            //(Jesus christ almighty this was not easy)
            if ((_board.ContainsPawn(lastMove.Destination)) &&
                (Math.Abs(lastMove.Destination.ToByte() - lastMove.Source.ToByte()) == 2) &&
                (lastMove.Destination.Row() == pawnLocation.Row()) &&
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
            ThreatMatrix threats,
            Player player,
            BoardLocation playerPieceLocation,
            ThreatDirection? playerPiecePinDirection)
        {
            //There are NO possible moves if the knight is under non-direct threat because
            //he is not capable of removing any linear or diagonal threats ever. This is due to his L-shaped
            //move pattern (thank god for small favors)
            if (playerPiecePinDirection.HasThreats(ThreatDirection.NonDirect))
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
            ThreatMatrix threats,
            Player player,
            BoardLocation playerPieceLocation,
            ThreatDirection? playerPiecePinDirection)
        {
            //There are NO possible moves if the bishop is under any line threats. 
            if (playerPiecePinDirection.HasThreats(ThreatDirection.Line))
            {
                return;
            }

            //we will use the threats matrix for this bishop to calculate our moves...
            var bishopThreatLocations = _threatProvider.FindThreatsForBoardLocation(_board, playerPieceLocation);
            var hasGradeThreats = playerPiecePinDirection.HasThreats(ThreatDirection.Grade);
            var hasSlopeThreats = playerPiecePinDirection.HasThreats(ThreatDirection.Slope);

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
            ThreatMatrix threats,
            Player player,
            BoardLocation playerPieceLocation,
            ThreatDirection? playerPiecePinDirection)
        {
            //There are NO possible moves if the rook is under any diagonal threats. 
            if (playerPiecePinDirection.HasThreats(ThreatDirection.Diagonal))
            {
                return;
            }

            //we will use the threats matrix for this rook to calculate our moves...
            var rookThreatLocations = _threatProvider.FindThreatsForBoardLocation(_board, playerPieceLocation);
            var hasVerticalThreats = playerPiecePinDirection.HasThreats(ThreatDirection.Vertical);
            var hasHorizontalThreats = playerPiecePinDirection.HasThreats(ThreatDirection.Horizontal);

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
            ThreatMatrix threats,
            Player player,
            BoardLocation playerPieceLocation,
            ThreatDirection? playerPiecePinDirection)
        {
            //we will use the threats matrix for this queen to calculate our moves...
            var queenThreatLocations = _threatProvider.FindThreatsForBoardLocation(_board, playerPieceLocation);
            var hasVerticalThreats = playerPiecePinDirection.HasThreats(ThreatDirection.Vertical);
            var hasHorizontalThreats = playerPiecePinDirection.HasThreats(ThreatDirection.Horizontal);
            var hasSlopeThreats = playerPiecePinDirection.HasThreats(ThreatDirection.Slope);
            var hasGradeThreats = playerPiecePinDirection.HasThreats(ThreatDirection.Grade);

            foreach (var location in queenThreatLocations.Keys)
            {
                //only add moves that do a capture or move along the same threat vector
                if ((_board.LocationIsEmptyOrOccupiedBy(location, player.OpposingPlayer())) &&
                    ((hasGradeThreats == false) || (location.IsOnSameGradeAs(playerPieceLocation))) &&
                    ((hasSlopeThreats == false) || (location.IsOnSameSlopeAs(playerPieceLocation))) &&
                    ((hasVerticalThreats == false) || (location.IsOnSameColumnAs(playerPieceLocation))) &&
                    ((hasHorizontalThreats == false) || (location.IsOnSameRowAs(playerPieceLocation))))
                {
                    moves.Add(new Move(playerPieceLocation, location));
                }
            }
        }

        private void FindKingMoves(
            List<Move> moves,
            ThreatMatrix threats,
            Player player,
            BoardLocation playerPieceLocation)
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

            //find our castle moves
            FindKingCastleMoves(moves, threats, player, playerPieceLocation);
        }
        private void FindKingCastleMoves(
            List<Move> moves,
            ThreatMatrix threats,
            Player player,
            BoardLocation playerPieceLocation)
        {
            //When are you not allowed to castle?
            //    There are a number of cases when castling is not permitted:
            //    -Your king has been moved earlier in the game.
            //    -The rook that castles has been moved earlier in the game.
            //    -There are pieces standing between your king and rook.
            //    -The king is in check.
            //    -The king moves through a square that is attacked by a piece of the opponent.
            //    -The king would be in check after castling.

            if ((KingHasMoved(player)) ||//king has moved
                (threats.ContainsKey(playerPieceLocation)))//king in check
            {
                return;
            }

            if (WesternCastleMoveAvailable(player, threats))
            {
                moves.Add(new Move(playerPieceLocation, player == Player.White ? BoardLocation.C1: BoardLocation.C8));
            }

            if (EasternCastleMoveAvailable(player, threats))
            {
                moves.Add(new Move(playerPieceLocation, player == Player.White ? BoardLocation.G1 : BoardLocation.G8));
            }
        }

        private bool WesternCastleMoveAvailable(Player player, ThreatMatrix threats)
        {
            if (player == Player.White)
            {
                return
                    (!_moveHistory.Any(a => a.Source == BoardLocation.A1)) &&//rook has not moved
                    (!threats.ContainsKey(BoardLocation.D1) && !threats.ContainsKey(BoardLocation.C1)) && //no threats in path
                    (_board[BoardLocation.D1].IsEmpty() && _board[BoardLocation.C1].IsEmpty());//path is empty
            }
            else
            {
                return
                    (!_moveHistory.Any(a => a.Source == BoardLocation.A8)) &&//rook has not moved
                    (!threats.ContainsKey(BoardLocation.D8) && !threats.ContainsKey(BoardLocation.C8)) && //no threats in path
                    (_board[BoardLocation.D8].IsEmpty() && _board[BoardLocation.C8].IsEmpty());//path is empty
            }
        }

        private bool EasternCastleMoveAvailable(Player player, ThreatMatrix threats)
        {
            if (player == Player.White)
            {
                return
                    (!_moveHistory.Any(a => a.Source == BoardLocation.H1)) &&//rook has not moved
                    (!threats.ContainsKey(BoardLocation.F1) && !threats.ContainsKey(BoardLocation.G1)) && //no threats in path
                    (_board[BoardLocation.G1].IsEmpty() && _board[BoardLocation.G1].IsEmpty());//path is empty
            }
            else
            {
                return
                    (!_moveHistory.Any(a => a.Source == BoardLocation.H8)) &&//rook has not moved
                    (!threats.ContainsKey(BoardLocation.F8) && !threats.ContainsKey(BoardLocation.G8)) && //no threats in path
                    (_board[BoardLocation.F8].IsEmpty() && _board[BoardLocation.G8].IsEmpty());//path is empty
            }
        }

        private bool KingHasMoved(Player player)
        {
            return player == Player.White ? 
                _board[BoardLocation.E1] != ChessPiece.WhiteKing || _moveHistory.Any(a => a.Source == BoardLocation.E1) : 
                _board[BoardLocation.E8] != ChessPiece.BlackKing || _moveHistory.Any(a => a.Source == BoardLocation.E8);
        }
        #endregion
    }
}
