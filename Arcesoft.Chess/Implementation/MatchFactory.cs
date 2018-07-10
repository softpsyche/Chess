using Arcesoft.Chess.Models;
using ilf.pgn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcesoft.Chess.Implementation
{
    public class MatchFactory : IMatchFactory
    {
        private IGameFactory _gameFactory;

        public MatchFactory(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public List<Match> LoadFromFile(string filePath)
        {
            var reader = new PgnReader();

            var database = reader.ReadFromFile(filePath);
            return database
                .Games
                .Select(a => ToMatch(a))
                .ToList();
        }

        public List<Match> Load(string pgnString)
        {
            var reader = new PgnReader();
            var database = reader.ReadFromString(pgnString);

            return database
                .Games
                .Select(a => ToMatch(a))
                .ToList();
        }

        #region Private methods
        private Match ToMatch(ilf.pgn.Data.Game game)
        {
            return new Match
            {
                BlackPlayerName = game.BlackPlayer,
                Day = game.Day,
                Event = game.Event,
                Month = game.Month,
                Round = game.Round,
                Site = game.Site,
                WhitePlayerName = game.WhitePlayer,
                Year = game.Year,
                Tags = game.Tags,
                Game = ToGame(game)
            };
        }
        private GameState ToGameState(ilf.pgn.Data.GameResult gameResult)
        {
            switch (gameResult)
            {
                case ilf.pgn.Data.GameResult.Black:
                    return GameState.BlackWin;
                case ilf.pgn.Data.GameResult.White:
                    return GameState.WhiteWin;
                case ilf.pgn.Data.GameResult.Draw:
                    return GameState.DrawStalemate;//this is questionable
                case ilf.pgn.Data.GameResult.Open:
                    return GameState.InPlay;
                default:
                    throw new Exception();

            }
        }
        private IGame ToGame(ilf.pgn.Data.Game pgnGame)
        {
            var game = _gameFactory.NewGame();

            foreach (var pgnMove in pgnGame.MoveText)
            {
                if (pgnMove.Type == ilf.pgn.Data.MoveTextEntryType.MovePair)
                {
                    var twoMoves = pgnMove as ilf.pgn.Data.MovePairEntry;

                    MakeMove(game, twoMoves.White);
                    MakeMove(game, twoMoves.Black);
                }
                else if (pgnMove.Type == ilf.pgn.Data.MoveTextEntryType.SingleMove)
                {
                    MakeMove(game, (pgnMove as ilf.pgn.Data.HalfMoveEntry).Move);
                }
            }

            return game;
        }
        private void MakeMove(IGame game, ilf.pgn.Data.Move move)
        {
            switch (move.Type)
            {
                case ilf.pgn.Data.MoveType.Simple:
                case ilf.pgn.Data.MoveType.Capture:
                case ilf.pgn.Data.MoveType.CaptureEnPassant:
                    MakeMoveSimpleOrCapture(game, move);
                    break;
                case ilf.pgn.Data.MoveType.CastleKingSide:
                case ilf.pgn.Data.MoveType.CastleQueenSide:
                    BoardLocation targetLocation;
                    if (game.CurrentPlayer == Player.White)
                    {
                        targetLocation = move.Type == ilf.pgn.Data.MoveType.CastleKingSide ? BoardLocation.G1 : BoardLocation.C1;
                    }
                    else
                    {
                        targetLocation = move.Type == ilf.pgn.Data.MoveType.CastleKingSide ? BoardLocation.G8 : BoardLocation.C8;
                    }
                    game.MakeMove(
                        game.CurrentPlayer == Player.White ? BoardLocation.E1 : BoardLocation.E8,
                        targetLocation);
                    break;
            }
        }
        private void MakeMoveSimpleOrCapture(IGame game, ilf.pgn.Data.Move move)
        {
            var availableMoves = game.FindMoves();
            var targetBoardLocation = move.TargetSquare.ToBoardLocation();
            var chessPieceMoving = move.Piece.Value.ToChessPiece(game.CurrentPlayer);

            var qualifyingMoves = availableMoves
                .Where(a => a.Destination == targetBoardLocation && game.Board[a.Source] == chessPieceMoving)
                .ToList();

            if (qualifyingMoves.Count == 0)
            {
                throw new Exception("Unexpected");
            }
            else if (qualifyingMoves.Count == 1)
            {
                game.MakeMove(qualifyingMoves.Single());
            }
            else
            {
                if (move.OriginFile.HasValue)
                {
                    var requiredColumn = move.OriginFile.Value.ToColumn();
                    qualifyingMoves.RemoveAll(a => a.Source.Column() != requiredColumn);
                }
                else if (move.OriginRank.HasValue)
                {
                    var requiredRow = move.OriginRank.Value -1;//remove 1 to adjust for zero based goodness
                    qualifyingMoves.RemoveAll(a => a.Source.Row() != requiredRow);
                }
                else
                {
                    //we need to do more digging, more than one piece can make this move...
                    throw new NotImplementedException("Well get there...");
                }

                game.MakeMove(qualifyingMoves.Single());
            }
        }
        #endregion
    }

}
