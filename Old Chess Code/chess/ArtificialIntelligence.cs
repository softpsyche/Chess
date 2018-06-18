using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class ArtificialIntelligence
    {

        #region Events
        //public delegate void AIBestMoveEventHandler(object sender, Chess.GameMoveEventArgs e);

        ////create event handler from signature...
        ////public event GameMoveEventHandler GameMoveEvent;

        ////create function to raise said event..
        //protected void OnGameMove(ChessPiece sourcePiece, ChessPiece pieceCaptured)
        //{
        //    if (this.GameMoveEvent != null)
        //    {
        //        //TODO get and send last move..
        //        this.GameMoveEvent(this, new GameMoveEventArgs(
        //            this.gamemoves[this.gamemovecount - 1],
        //            sourcePiece,
        //            pieceCaptured));
        //    }
        //}
        #endregion
        private Chess.ChessGame chessGame   = null;
        private Chess.GameState gameTree = null;

        private Chess.Side sideAI;

        public Chess.Side SideAI
        {
            get 
            { 
                return sideAI; 
            }
            set 
            { 
                sideAI = value;
            }
        }

        public Chess.GameState GameTree
        {
            get { return gameTree; }
            set { gameTree = value; }
        }

        public ChessGame ChessGame
        {
            get
            {
                return this.chessGame;
            }
            set
            {
                this.chessGame = value;
            }
        }

        public ArtificialIntelligence()
        {
            this.chessGame = new ChessGame();
            this.gameTree = new GameState(null, 
                ChessGame.CopyBoard(this.chessGame.Board), 
                this.chessGame.GetTurn());
            this.sideAI = ChessGame.Computer;
        }
        public ArtificialIntelligence(ChessGame chessGame)
        {
            this.chessGame = chessGame;
            this.gameTree = new GameState(null,
                ChessGame.CopyBoard(this.chessGame.Board),
                this.chessGame.GetTurn());
            this.sideAI = ChessGame.Computer;
        }

        public void FindPly(GameState gameState)
        {
            this.FindPlies(gameState, 1);
        }
        public void FindPlies(GameState gameState,Int32 depth)
        {
            System.Collections.ObjectModel.Collection<Square[,]> boardPositions;
            GameState childState;
            Side newTurn;

            //if we are not past our current depth...
            if(depth > 0)
            {
                //get the board positions for this node...
                boardPositions = this.chessGame.GetChildBoardPositions(gameState.Board, gameState.Turn);

                if (boardPositions != null)
                {
                    depth -= 1;

                    newTurn = Chess.ChessGame.NextTurn(gameState.Turn);

                    for (int count = 0; count < boardPositions.Count; count++)
                    {
                        childState = new GameState(gameState, boardPositions[count], newTurn);
                        gameState.Children.Add(childState);
                        FindPlies(childState, depth);
                    }
                }
            }
        }


    }
}
