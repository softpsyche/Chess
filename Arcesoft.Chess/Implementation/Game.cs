using Arcesoft.Chess.Models;
using Arcesoft.Chess.Models.Internal;
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
        private List<IMove> _moveHistory = new List<IMove>();
        private List<Move> _moves = null;
        private ThreatMatrix _threatMatrix = null;

        public Game(Board board, ThreatProvider threatProvider)
        {
            _board = board;
            _threatProvider = threatProvider;
        }

        public IReadOnlyList<IMove> MoveHistory => _moveHistory;

        public bool GameIsOver => GameState != GameState.InPlay;

        public GameState GameState { get; private set; } = GameState.InPlay;

        public IReadOnlyBoard Board => _board as IReadOnlyBoard;

        public Player CurrentPlayer => (_moveHistory.Count % 2) == 0 ? Player.White : Player.Black;

        public void MakeMove(BoardLocation source, BoardLocation destination, PawnPromotionType? promotionType)
            => MakeMove(TryFindMove(source, destination, promotionType));

        public bool IsLegalMove(BoardLocation source, BoardLocation destination, PawnPromotionType? promotionType)
        {
            return TryFindMove(source, destination, promotionType) != null;
        }

        public void MakeMove(IMove move)
        {
            if (GameIsOver)
            {
                throw new ChessException(ChessErrorCode.InvalidMoveGameOver, "The move is not valid because the game is over.");
            }

            if (!MoveIsLegal(move))
            {
                throw new ChessException(ChessErrorCode.IllegalMove, "The move is not valid because it is not legal.");
            }

            //Make the move
            MakeMoveForMoveType(move);

            //add the history
            _moveHistory.Add(move);

            //clear the moves and threats (for caching of next moves)
            _moves = null;
            _threatMatrix = null;

            //determine the game state now that this move has been made
            DetermineGameState();
        }

        private IMove TryFindMove(BoardLocation source, BoardLocation destination, PawnPromotionType? promotionType)
            => FindMoves().SingleOrDefault(a => a.Source == source && a.Destination == destination && (promotionType.HasValue == false || a.Type == promotionType.Value.ToMoveType()));

        public bool IsLegalMove(IMove move) => GameIsOver ? false : FindMoves().Contains(move);

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

        public IReadOnlyList<IMove> FindMoves()
        {
            if (_moves != null)
            {
                return _moves;
            }

            var moves = new List<Move>();
            if (GameIsOver)
            {
                _moves = moves;
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
            if (_moveHistory?.Any() == false)
            {
                throw new ChessException(ChessErrorCode.UndoMoveIllegal,
                    $"Unable to undo last move because no moves have been made");
            }

            //undo this move now
            var lastMove = _moveHistory.Last();
            
            UndoLastMoveForMoveType(lastMove);

            _moveHistory.RemoveAt(_moveHistory.Count - 1);

            //clear the moves and threats (for caching of next moves)
            _moves = null;
            _threatMatrix = null;

            //determine the game state now that this move has been made
            DetermineGameState();
        }

        #region Private methods
        private MoveType DetermineMoveType(IMove move)
        {
            return DetermineMoveType(move.Destination);
        }
        private MoveType DetermineMoveType(BoardLocation destination)
        {
            var destinationPiece = _board[destination];

            switch (destinationPiece)
            {
                case ChessPiece.WhitePawn:
                case ChessPiece.BlackPawn:
                    return MoveType.CapturePawn;
                case ChessPiece.WhiteKnight:
                case ChessPiece.BlackKnight:
                    return MoveType.CaptureKnight;
                case ChessPiece.WhiteBishop:
                case ChessPiece.BlackBishop:
                    return MoveType.CaptureBishop;
                case ChessPiece.WhiteRook:
                case ChessPiece.BlackRook:
                    return MoveType.CaptureRook;
                case ChessPiece.WhiteQueen:
                case ChessPiece.BlackQueen:
                    return MoveType.CaptureQueen;
                case ChessPiece.WhiteKing:
                case ChessPiece.BlackKing:
                    throw new InvalidOperationException("Invalid game state detected, cannot execute move because it would result in capture of king.");
                default:
                    return MoveType.Move;
            }
        }
        private void DetermineGameState()
        {
            //find the king and moves for this player
            var kingLocation = _board.GetKingsLocation(CurrentPlayer);
            var moves = FindMoves();

            //if we have no moves, this gets easy...
            if (!moves.Any())
            {
                //checkmate, son
                if (_threatMatrix.ContainsKey(kingLocation))
                {
                    GameState = CurrentPlayer == Player.White ? GameState.BlackWin : GameState.WhiteWin;
                }
                else
                {
                    GameState = GameState.DrawStalemate;
                }
                return;
            }

            //we need to check for draw due to insufficient material
            if (HasInsufficientMaterialForMate(CurrentPlayer) && 
                HasInsufficientMaterialForMate(CurrentPlayer.OpposingPlayer()))
            {
                GameState = GameState.DrawInsufficientMaterial;
            }

            //TODO
            //https://en.wikipedia.org/wiki/Draw_(chess)
            //TODO:Check threefold repetition
            //TODO:Check fifty-move rule
            //TODO: Check 'dead position' (not sure how that is done)
        }
        private bool HasInsufficientMaterialForMate(Player player)
        {
            var playerPieceLocations = _board.FindPlayerPieceLocations(player);

            //only king left
            if (playerPieceLocations.Count == 1) return true;

            //if we a queen rook or pawn we have enough
            if (playerPieceLocations.Any(a => _board[a].IsQueen(player) || _board[a].IsRook(player) || _board[a].IsPawn(player)))
            {
                return false;
            }

            //more than one knight we have enough
            var knightCount = playerPieceLocations.Count(a => _board[a].IsKnight(player));
            if (knightCount > 1)
            {
                return false;
            }

            //bishop and a knight we have enough
            var bishopCount = playerPieceLocations.Count(a => _board[a].IsBishop(player));
            if ((knightCount > 0) && (bishopCount > 0))
            {
                return false;
            }

            //the last thing left is if we have more than 1 bishop of a different color
            if (playerPieceLocations
                .Where(a => _board[a].IsBishop(player))
                .Select(a => a.Color())
                .Distinct()
                .Count() > 1)
            {
                return false;
            }

            //we can to assume we have insuficient material
            return true;
        }
        private void MakeMoveForMoveType(IMove move)
        {
            switch (move.Type)
            {
                //these cases are super simple, just put the new piece on the destination square, done
                case MoveType.Move:
                case MoveType.CapturePawn:
                case MoveType.CaptureKnight:
                case MoveType.CaptureBishop:
                case MoveType.CaptureRook:
                case MoveType.CaptureQueen:
                    MovePieceOnBoard(move.Source, move.Destination);
                    break;
                case MoveType.AuPassant:
                    //move the pawn
                    MovePieceOnBoard(move.Source,move.Destination);

                    //remove the capture pawn
                    var capturedPawnLocation = CurrentPlayer == Player.White ? move.Destination - 1 : move.Destination + 1;
                    _board[capturedPawnLocation] = ChessPiece.None;

                    break;
                case MoveType.CastleKingside:
                    //move the king
                    MovePieceOnBoard(move.Source, move.Destination);

                    if (CurrentPlayer == Player.White)
                    {
                        //move the rook
                        MovePieceOnBoard(BoardLocation.H1, BoardLocation.F1);
                    }
                    else
                    {
                        MovePieceOnBoard(BoardLocation.H8, BoardLocation.F8);
                    }
                    break;
                case MoveType.CastleQueenside:
                    //move the king
                    MovePieceOnBoard(move.Source, move.Destination);

                    if (CurrentPlayer == Player.White)
                    {
                        //move the rook
                        MovePieceOnBoard(BoardLocation.A1, BoardLocation.D1);
                    }
                    else
                    {
                        MovePieceOnBoard(BoardLocation.A8, BoardLocation.D8);
                    }
                    break;
                case MoveType.PawnPromotionKnight:
                    _board[move.Destination] = CurrentPlayer == Player.White ? ChessPiece.WhiteKnight : ChessPiece.BlackKnight;
                    _board[move.Source] = ChessPiece.None;
                    break;
                case MoveType.PawnPromotionBishop:
                    _board[move.Destination] = CurrentPlayer == Player.White ? ChessPiece.WhiteBishop : ChessPiece.BlackBishop;
                    _board[move.Source] = ChessPiece.None;
                    break;
                case MoveType.PawnPromotionRook:
                    _board[move.Destination] = CurrentPlayer == Player.White ? ChessPiece.WhiteRook : ChessPiece.BlackRook;
                    _board[move.Source] = ChessPiece.None;
                    break;
                case MoveType.PawnPromotionQueen:
                    _board[move.Destination] = CurrentPlayer == Player.White ? ChessPiece.WhiteQueen : ChessPiece.BlackQueen;
                    _board[move.Source] = ChessPiece.None;
                    break;
            }
        }
        private void UndoLastMoveForMoveType(IMove lastMove)
        {
            var playerWhoMadeMove = CurrentPlayer.OpposingPlayer();

            switch (lastMove.Type)
            {
                case MoveType.Move:
                    //simply undo the move
                    MovePieceOnBoard(lastMove.Destination, lastMove.Source);
                    break;
                case MoveType.CapturePawn:
                    MovePieceOnBoard(lastMove.Destination, lastMove.Source);
                    _board[lastMove.Destination] = playerWhoMadeMove == Player.White ? ChessPiece.BlackPawn : ChessPiece.WhitePawn;
                    break;
                case MoveType.CaptureKnight:
                    MovePieceOnBoard(lastMove.Destination, lastMove.Source);
                    _board[lastMove.Destination] = playerWhoMadeMove == Player.White ? ChessPiece.BlackKnight : ChessPiece.WhiteKnight;
                    break;
                case MoveType.CaptureBishop:
                    MovePieceOnBoard(lastMove.Destination, lastMove.Source);
                    _board[lastMove.Destination] = playerWhoMadeMove == Player.White ? ChessPiece.BlackBishop : ChessPiece.WhiteBishop;
                    break;
                case MoveType.CaptureRook:
                    MovePieceOnBoard(lastMove.Destination, lastMove.Source);
                    _board[lastMove.Destination] = playerWhoMadeMove == Player.White ? ChessPiece.BlackRook : ChessPiece.WhiteRook;
                    break;
                case MoveType.CaptureQueen:
                    MovePieceOnBoard(lastMove.Destination, lastMove.Source);
                    _board[lastMove.Destination] = playerWhoMadeMove == Player.White ? ChessPiece.BlackQueen : ChessPiece.WhiteQueen;
                    break;
                case MoveType.AuPassant:
                    MovePieceOnBoard(lastMove.Destination, lastMove.Source);

                    //resurrect the captured pawn
                    var capturedPawnLocation = playerWhoMadeMove == Player.White ? lastMove.Destination - 1 : lastMove.Destination + 1;
                    _board[capturedPawnLocation] = playerWhoMadeMove == Player.White ? ChessPiece.BlackPawn : ChessPiece.WhitePawn;
                    break;
                case MoveType.PawnPromotionBishop:
                case MoveType.PawnPromotionKnight:
                case MoveType.PawnPromotionRook:
                case MoveType.PawnPromotionQueen:
                    _board[lastMove.Destination] = ChessPiece.None;
                    _board[lastMove.Source] = playerWhoMadeMove == Player.White ? ChessPiece.WhitePawn : ChessPiece.BlackPawn;
                    break;
                case MoveType.CastleKingside:
                    if (playerWhoMadeMove == Player.White)
                    {
                        //white castle occurred
                        _board[BoardLocation.E1] = ChessPiece.WhiteKing;
                        _board[BoardLocation.F1] = ChessPiece.None;
                        _board[BoardLocation.G1] = ChessPiece.None;
                        _board[BoardLocation.H1] = ChessPiece.WhiteRook;
                    }
                    else
                    {
                        //black castle occurred
                        _board[BoardLocation.E8] = ChessPiece.BlackKing;
                        _board[BoardLocation.F8] = ChessPiece.None;
                        _board[BoardLocation.G8] = ChessPiece.None;
                        _board[BoardLocation.H8] = ChessPiece.BlackRook;
                    }
                     
                    break;
                case MoveType.CastleQueenside:
                    if (playerWhoMadeMove == Player.White)
                    {
                        //white castle occurred
                        _board[BoardLocation.E1] = ChessPiece.WhiteKing;
                        _board[BoardLocation.C1] = ChessPiece.None;
                        _board[BoardLocation.D1] = ChessPiece.None;
                        _board[BoardLocation.A1] = ChessPiece.WhiteRook;
                    }
                    else
                    {
                        //black castle occurred
                        _board[BoardLocation.E8] = ChessPiece.BlackKing;
                        _board[BoardLocation.C8] = ChessPiece.None;
                        _board[BoardLocation.D8] = ChessPiece.None;
                        _board[BoardLocation.A8] = ChessPiece.BlackRook;
                    }
                    
                    break;
            }
        }
        private void MovePieceOnBoard(BoardLocation source, BoardLocation destination)
        {
            _board[destination] = _board[source];
            _board[source] = ChessPiece.None;
        }

        private bool MoveIsLegal(IMove move) => FindMoves().Contains(move);

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
                var row = firstMarchLocation.Row();
                if (row == 0 || row == 7)//we can assume this is a pawn promotion
                {
                    moves.Add(new Move(playerPieceLocation, firstMarchLocation, MoveType.PawnPromotionKnight));
                    moves.Add(new Move(playerPieceLocation, firstMarchLocation, MoveType.PawnPromotionBishop));
                    moves.Add(new Move(playerPieceLocation, firstMarchLocation, MoveType.PawnPromotionRook));
                    moves.Add(new Move(playerPieceLocation, firstMarchLocation, MoveType.PawnPromotionQueen));
                }
                else
                {
                    moves.Add(new Move(playerPieceLocation, firstMarchLocation, MoveType.Move));
                }

                //we can also move up two spaces if the next northern square is empty
                if ((playerPieceLocation.IsPawnStartingLocation(player)) &&
                    (_board.NeighboringLocationIsEmpty(firstMarchLocation, marchDirection)))
                {
                    moves.Add(new Move(playerPieceLocation, firstMarchLocation.Neighbor(marchDirection).Value, MoveType.Move));
                }
            }

            //check to see if we can move along the grade
            //note that any line or slope threats will block this move
            if (playerPieceThreatDirection.HasThreats(ThreatDirection.Line | ThreatDirection.Slope) == false)
            {
                //if we have an opposing piece in the next square, make hte move
                if (_board.NeighboringLocationIsOccupiedBy(playerPieceLocation, gradeAttackDirection, opposingPlayer))
                {
                    var neighbor = playerPieceLocation.Neighbor(gradeAttackDirection).Value;
                    moves.Add(new Move(playerPieceLocation, neighbor, DetermineMoveType(neighbor)));
                }
                else if (LastMoveAllowsEnPassantFor(playerPieceLocation, gradeAttackDirection))
                {//we can do an en passant.
                    var neighbor = playerPieceLocation.Neighbor(gradeAttackDirection).Value;
                    moves.Add(new Move(playerPieceLocation,neighbor, MoveType.AuPassant));
                }
            }

            //check to see if we can move along the slope
            //note that any line or grade threats will block this move
            if (playerPieceThreatDirection.HasThreats(ThreatDirection.Line | ThreatDirection.Grade) == false)
            {
                //if we have an opposing piece in the next square, make hte move
                if (_board.NeighboringLocationIsOccupiedBy(playerPieceLocation, slopeAttackDirection, opposingPlayer))
                {
                    var neighbor = playerPieceLocation.Neighbor(slopeAttackDirection).Value;
                    moves.Add(new Move(playerPieceLocation, neighbor, DetermineMoveType(neighbor)));
                }
                else if (LastMoveAllowsEnPassantFor(playerPieceLocation, slopeAttackDirection))
                {//we can do an en passant.
                    var neighbor = playerPieceLocation.Neighbor(slopeAttackDirection).Value;
                    moves.Add(new Move(playerPieceLocation, neighbor, MoveType.AuPassant));
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
                    moves.Add(new Move(playerPieceLocation, location, DetermineMoveType(location)));
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
                    moves.Add(new Move(playerPieceLocation, location, DetermineMoveType(location)));
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
                    moves.Add(new Move(playerPieceLocation, location,DetermineMoveType(location)));
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
                    moves.Add(new Move(playerPieceLocation, location, DetermineMoveType(location)));
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
                    moves.Add(new Move(playerPieceLocation, location, DetermineMoveType(location)));
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

            if (CastleQueensideMoveAvailable(player, threats))
            {
                moves.Add(new Move(playerPieceLocation, player == Player.White ? BoardLocation.C1: BoardLocation.C8, MoveType.CastleQueenside));
            }

            if (CastleKingsideMoveAvailable(player, threats))
            {
                moves.Add(new Move(playerPieceLocation, player == Player.White ? BoardLocation.G1 : BoardLocation.G8, MoveType.CastleKingside));
            }
        }

        private bool CastleQueensideMoveAvailable(Player player, ThreatMatrix threats)
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

        private bool CastleKingsideMoveAvailable(Player player, ThreatMatrix threats)
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
