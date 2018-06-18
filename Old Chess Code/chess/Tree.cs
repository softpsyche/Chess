using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace Chess
{
    //public class GameTree
    //{
    //    private GameState root;

    //    public GameTree(Square[,] board)
    //    {
    //        root = new GameState(null, board);
    //    }
    //}

    public class GameState
    {
        private bool evaluated = false;
        private Side turn;
        private Square[,] board;
        private GameState parent = null;
        private System.Collections.ObjectModel.Collection<GameState> children = null;

        public bool Evaluated
        {
            get { return evaluated; }
            set { evaluated = value; }
        }
        public Side Turn
        {
            get 
            { 
                return turn;
            }
        }
        public Square[,] Board
        {
            get
            {
                return this.board;
            }
        }
        public GameState Parent
        {
            get
            {
                return this.parent;
            }
        }
        public Int32 Depth
        {
            get
            {
                Int32 count;
                GameState parent = this.parent;

                for (count = 0; parent != null; count++)
                    parent = parent.parent;

                return count;
            }
        }
        public System.Collections.ObjectModel.Collection<GameState> Children
        {
            get
            {
                if (this.children == null)
                    this.children = new Collection<GameState>();

                return this.children;
            }
        }
        public GameState(GameState parent, Square[,] board,Side turn)
        {
            this.parent = parent;
            this.board = board;
            this.turn = turn;
        }

		/*
		public Int32 ScoreGameState()
		{
			Int32 WhiteMaterial, BlackMaterial;

			for (int count = 0; count < 8;count++)
				for (int count2; count2 < 8; count2++)
				{
					switch(this.board[count,count2].chesspiece)
					{
						case ChessPiece.BLACKQUEEN:
						case ChessPiece.WHITEQUEEN:
							break;
						case ChessPiece.BLACKROOK:
						case ChessPiece.WHITEROOK:
							break;
						case ChessPiece.BLACKBISHOP:
						case ChessPiece.WHITEBISHOP:
							break;
						case ChessPiece.BLACKKNIGHT:
						case ChessPiece.WHITEKNIGHT:
							break;
						case ChessPiece.BLACKQUEEN:
						case ChessPiece.BLACKKING:
							break;
					}
				}
		}
		 * */

	}
}
