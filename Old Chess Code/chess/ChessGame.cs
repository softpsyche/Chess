using System;
using System.Drawing;

namespace Chess
{
	public class GameMoveEventArgs: System.EventArgs
	{
		public Chess.ChessMove chessMove;
		public Chess.ChessPiece sourcePiece;
		public Chess.ChessPiece pieceCaptured;

		public GameMoveEventArgs(
			Chess.ChessMove chessMove,
			ChessPiece sourcePiece,
			ChessPiece pieceCaptured)
		{
			this.chessMove		= chessMove;
			this.sourcePiece	= sourcePiece;
			this.pieceCaptured	= pieceCaptured;
		}
	}
	#region Enums and Structures
    /// <summary>
    /// Direction bit flag used to mark a direction or combination of directions on the chess board. 
    /// </summary>
    [Flags]
    public enum Direction
    {
        //direction constants represented as bit flags...
        NONE            = 0,
        DIRECT          = 1,
        NORTH           = 2,
        SOUTH           = 4,
        EAST            = 8,
        WEST            = 16,
        NORTHEAST       = 32,
        NORTHWEST       = 64,
        SOUTHEAST       = 128,
        SOUTHWEST       = 256,
        //direction combinations
        DIAGONAL	    = 480,//northeast+northwest+southeast+southwest
		SLOPERISING	    = 288,//SOUTHWEST + NORTHEAST
		SLOPEFALLING	= 192,//NORTHWEST + SOUTHEAST
		STRAIGHT	    = 30,//NORTH + SOUTH + EAST + WEST
		HORIZONTAL		= 24,//east + west;
		VERTICAL	    = 6,//north + south
    }
    /// <summary>
    /// Black or White.
    /// </summary>
	public enum Side
	{
		WHITE,
		BLACK
	}
    /// <summary>
    /// Enum representation of all possible chess pieces, including the empty case.
    /// </summary>
	public enum ChessPiece
	{
		WHITEKING,//0
		WHITEQUEEN,
		WHITEROOK,
		WHITEKNIGHT,
		WHITEBISHOP,
		WHITEPAWN,//5
		BLACKKING,//6
		BLACKQUEEN,
		BLACKROOK,
		BLACKKNIGHT,
		BLACKBISHOP,
		BLACKPAWN,//11
		EMPTY//12
	}

//	public enum MoveResult
//	{
//		MOVE,
//		CAPTURE,
//		CHECK,
//		CHECKMATE,
//		LEFTCASTLE,
//		RIGHTCASTLE
//	}

	public class Square
	{
		public ChessPiece chesspiece;
		public int xpos,ypos;

		public Direction whitekinglinesight,blackkinglinesight;
		public Direction whitethreat,blackthreat;
		public bool piecethreateningking;

        /// <summary>
        /// Copies the square and its values. Returns the copied square.
        /// </summary>
        /// <returns></returns>
        public Square Copy()
        {
            Square copy                 = new Square();

            copy.chesspiece             = this.chesspiece;
            copy.xpos                   = this.xpos;
            copy.ypos                   = this.ypos;
            copy.whitekinglinesight     = this.whitekinglinesight;
            copy.blackkinglinesight     = this.blackkinglinesight;
            copy.whitethreat            = this.whitethreat;
            copy.blackthreat            = this.blackthreat;
            copy.piecethreateningking   = this.piecethreateningking;

            return copy;
        }
		public bool isEmpty()
		{
			if(this.chesspiece == Chess.ChessPiece.EMPTY)
				return true;
			else
				return false;
		}
		public bool PieceIsBlack()
		{
			if(((int)chesspiece > 5) && ((int)chesspiece < 12))
				return true;
			else
				return false;
		}

		public bool PieceIsWhite()
		{
			if((int)chesspiece < 6)
				return true;
			else
				return false;
		}

		public void setThreat(Side side,Direction dir)
		{
			if(side == Side.WHITE)
				this.whitethreat |= dir;
			else
				this.blackthreat |= dir;
		}

		public bool hasThreat(Side side)
		{
			if(side == Side.WHITE)
			{
				if(this.whitethreat > 0)
					return true;
			}
			else
			{
				if(this.blackthreat > 0)
					return true;
			}

			return false;
		}
		public bool hasMultipleThreats(Side threatside)
		{
			Direction threats;
			int threatcount=0;


			if(threatside == Side.WHITE)
				threats = this.whitethreat;
			else
				threats = this.blackthreat;

			for(int count=0;count <= 8;count++)
			{
				if((threats & (Direction)Convert.ToUInt32(Math.Pow(2,count))) > 0)
					threatcount++;
			}

			if(threatcount > 1)
				return true;
			else
				return false;
		}
		public Direction getPiecePinDirection()
		{
			if(this.PieceIsWhite())
			{
				return this.blackthreat & this.whitekinglinesight;
			}
			else if(this.PieceIsBlack())
			{
				return this.whitethreat & this.blackkinglinesight;
			}
			else
				return Chess.Direction.NONE;
		}
		public bool hasLineThreat(Side side,Direction  dir)
		{
			if(side == Side.WHITE)
			{
				if((this.whitethreat & dir) == dir)
					return true;
			}
			else
			{
				if((this.blackthreat & dir) == dir)
					return true;
			}

			return false;
		}

		static public bool IsValidBoardIndex(int index)
		{
			if((index >=0) && (index <= 7))
				return true;
			else
				return false;
		}
		static public bool PieceIsBlack(ChessPiece piece)
		{
			if(((int)piece > 5) && ((int)piece < 12))
				return true;
			else
				return false;
		}

		static public bool PieceIsWhite(ChessPiece piece)
		{
			if((int)piece < 6)
				return true;
			else
				return false;
		}
	}

	public struct ChessMove
	{
		//public ChessMove parent;
		//public ChessMove [] children;
		public ChessPiece sourcepiece,targetpiece;
		public int moveresult;
		public int sourcex,sourcey,targetx,targety;

		public void Clear()
		{

		}

		public bool isMove(int sourceX,int sourceY,int targetX,int targetY)
		{
			if(this.sourcex != sourceX)
				return false;
			if(this.sourcey != sourceY)
				return false;
			if(this.targetx != targetX)
				return false;
			if(this.targety != targetY)
				return false;

			return true;
		}
		public void SetMove(ChessPiece src,ChessPiece target,int sourcex,int sourcey,
			int targetx,int targety)
		{
			this.sourcepiece=src;
			this.targetpiece=target;
			this.sourcex=sourcex;
			this.sourcey=sourcey;
			this.targetx=targetx;
			this.targety=targety;
			
			if(target == Chess.ChessPiece.EMPTY)
				this.moveresult=Chess.ChessGame.MOVE;
			else
				this.moveresult=Chess.ChessGame.CAPTURE;
		}
	}

	#endregion
	/// <summary>
	/// Summary description for ChessGame.
	/// </summary>
	public class ChessGame
	{
		#region constants

        //move constants represent what type of flag we have...
        //public const int FLAGMOVE

		/*
		//other constants used in threat and moves represented as bit flags..
		public const int FAR			=1;
		public const int DIAGONAL		=1;
		public const int HORIZONTAL		=1;
		public const int PAWN			=1;
		public const int KING			=
		*/

		//
		public const int MOVE	=0;
		public const int CAPTURE=1;

		#endregion

		#region Static Members
		public static string TranslateChessCoordinatesToEnglish(int x,int y)
		{
			return Convert.ToChar(65 + x).ToString() + (Math.Abs( 8 - (y))).ToString();
		}
		public static string GetChessPieceName(ChessPiece piece)
		{
			switch(piece)
			{
				case Chess.ChessPiece.WHITEPAWN:
				case Chess.ChessPiece.BLACKPAWN:
					return "Pawn";
				case Chess.ChessPiece.WHITEKNIGHT:
				case Chess.ChessPiece.BLACKKNIGHT:
					return "Knight";
				case Chess.ChessPiece.WHITEBISHOP:
				case Chess.ChessPiece.BLACKBISHOP:
					return "Bishop";
				case Chess.ChessPiece.WHITEROOK:
				case Chess.ChessPiece.BLACKROOK:
					return "Rook";
				case Chess.ChessPiece.WHITEQUEEN:
				case Chess.ChessPiece.BLACKQUEEN:
					return "Queen";
				case Chess.ChessPiece.WHITEKING:
				case Chess.ChessPiece.BLACKKING:
					return "King";
				default:
					return "None";
			}
		}
		#endregion
		#region Private variables
		//private bool blackontop;//THIS IS ONLY FOR THE UI...AS FAR AS THIS CLASS IS CONCERNED,
		//BLACK IS ALWAYS ON TOP (another words, dont write code to logically deal with this
		//issue since you can just deal with it another way...BLACK = TOP ALWAYS WHITE = BOTTOM ALWAYS
		//private bool whiteKingMoved,whiteLeftRookMoved,whiteRightRookMoved;
        private Side human, computer, turn;
        private Square[,] board; //board is in form of x,y

		//used in move determination functions
		private ChessMove [] sidemovebuffer;
		private int sidemovebuffercount;

		//used to keep track of all moves throughout the game...
		private Chess.ChessMove[] gamemoves;
		private int gamemovecount;

		//used to keep track of the current turns moves...
		private Chess.ChessMove[] turnmoves;
		//used to keep track of the currently selected piece...
		//private Chess.ChessPiece selectedPiece;

		private System.Windows.Forms.Timer gametimer;
		private TimeSpan whitetime,blacktime;
		private DateTime gamestart;

		#endregion
        #region Properties
        public Square[,] Board
        {
            get
            {
                return board;
            }
            set
            {
                this.board = value;
            }
        }
        public Side Computer
        {
            get
            {
                return computer;
            }
            set
            {
                computer = value;
            }
        }
        public Side Human
        {
            get
            {
                return human;
            }
            set
            {
                human = value;
            }
        }
        public Side Turn
        {
            get
            {
                return turn;
            }
            set
            {
                turn = value;
            }
        }
        #endregion
        #region Delegates And Events

        //define function signature...
		public delegate void GameMoveEventHandler(object sender,Chess.GameMoveEventArgs e);

		//create event handler from signature...
		public event GameMoveEventHandler GameMoveEvent;

		//create function to raise said event..
		protected void OnGameMove(ChessPiece sourcePiece,ChessPiece pieceCaptured)
		{
			if(this.GameMoveEvent != null)
			{
				//TODO get and send last move..
				this.GameMoveEvent(this,new GameMoveEventArgs(
					this.gamemoves[this.gamemovecount -1],
					sourcePiece,
					pieceCaptured));
			}
		}
//
//		//define function signature...
//		public delegate void GameTimeEventHandler(TimeSpan timeSpent);
//		//create event handler from signature...
//		public event GameMoveEventHandler GameMoveEvent;
//		//create function to raise said event..
//		protected void OnGameMove(bool validMove)
//		{
//			if(this.GameMoveEvent != null)
//			{
//				//TODO get last move..
//				if(validMove)
//					this.GameMoveEvent(this.gamemoves[this.gamemovecount -1]);
//				else
//					this.GameMoveEvent(new ChessMove());
//			}
//		}

//		//define function signature...
//		public delegate void PieceSelectedEventHandler(Square pieceSquare);
//		//create event handler from signature...
//		public event Chess.ChessGame.PieceSelectedEventHandler PieceSelectedEvent;
//		//create function to raise said event..
//		protected void OnPieceSelected()
//		{
//			if(this.PieceSelectedEvent != null)
//			{
//
//			}
//		}

		#endregion
		#region Public functions
        #region Helper functions for external Artificial Intelligence

        public static void ClearBoard(Square[,] board)
        {
            for (int count = 0; count < 8; count++)
                for (int count2 = 0; count2 < 8; count2++)
                    board[count, count2].chesspiece = ChessPiece.EMPTY;
        }
        public static Side NextTurn(Side currentTurn)
        {
            if (currentTurn == Side.BLACK)
                return Side.WHITE;
            else
                return Side.BLACK;
        }
        public static Square[,] CopyBoard(Square[,] sourceBoard)
        {
            Square[,] newBoard = new Square[8, 8];
            Square copy;

            for (int count = 0; count < 8; count++)
            {
                for (int count2 = 0; count2 < 8; count2++)
                {
                    copy = sourceBoard[count, count2].Copy();
                    newBoard[count, count2] = copy;
                }
            }

            return newBoard;
        }

        public System.Collections.ObjectModel.Collection<Square[,]>
            GetChildBoardPositions(Square[,] sourceBoard, Side side)
        {
            System.Collections.ObjectModel.Collection<Square[,]> boardPositions = null;
            Square[,] boardPosition;
            ChessMove[] possibleMoves;
            ChessMove currentMove;

            possibleMoves = this.GetMovesForSide(sourceBoard, side);

            //if we found any moves...
            if (possibleMoves != null)
            {
                boardPositions = new System.Collections.ObjectModel.Collection<Square[,]>();

                //iterate through each move and generate the new board layout...
                for (int count = 0; count < possibleMoves.Length; count++)
                {
                    //copy the original board...
                    boardPosition = Chess.ChessGame.CopyBoard(sourceBoard);

                    //set this to the current move...
                    currentMove = possibleMoves[count];

                    //execute the move by first moving the source piece to its target square...
                    boardPosition[currentMove.targetx, currentMove.targety].chesspiece =
                        boardPosition[currentMove.sourcex, currentMove.sourcey].chesspiece;

                    //then empty out the source square.
                    boardPosition[currentMove.sourcex, currentMove.sourcey].chesspiece = ChessPiece.EMPTY;

                    //add this new board position to our collection.
                    boardPositions.Add(boardPosition);
                }
            }

            //return all the possible board positions.
            return boardPositions;
        }
        #endregion
		public ChessGame()
		{
			//
			// TODO: Add constructor logic here
			//

			//create an array to hold our moves..
			this.gamemoves = new ChessMove[200];//no game should exceed this length...
			this.gamemovecount =0;

			//set our movebuffer to the max number of possible moves for any chess
			//piece...
			this.sidemovebuffer = new ChessMove[300];//maybe 322 as max
			//possible 9 queens = 9 * 27,1 king = 8,2 knights = 8,
			//2 bishops=13,2 rooks =14,pawns - taken care of by queens...

		}
		public void NewGame(Side Human)
		{
			//this.blackontop =BlackOnTop;
			turn = Side.WHITE;
			this.ResetGame();
		}
		public Side GetTurn()
		{
			return turn;
		}
		public bool ExecuteMove(int sourcex,int sourcey,int targetx,int targety)
		{
			ChessMove move;
			ChessPiece targetSquarePiece;
			ChessPiece sourceSquarePiece;
			ChessPiece pieceCaptured;

			for(int count =0;count < this.turnmoves.Length;count++)
			{
				if(this.turnmoves[count].isMove(sourcex,sourcey,targetx,targety))
				{
					//get a copy of the actual move..
					move = this.turnmoves[count];

					//save target and source square piece. 
					targetSquarePiece = this.board[move.targetx,move.targety].chesspiece;
					sourceSquarePiece = this.board[move.sourcex,move.sourcey].chesspiece;

					//piece captured is the move target coordinates. (Special case is au passant
					//which we deal with later in code)
					pieceCaptured = targetSquarePiece;

					//Then move the piece from its source square and place in target square.
					this.board[move.sourcex,move.sourcey].chesspiece = ChessPiece.EMPTY;
					this.board[move.targetx,move.targety].chesspiece= move.sourcepiece;

					//execute post move logic, mainly for special cases (au passant,castle, pawn promotion);
					this.ExecuteMove_PostMoveLogic(move,targetSquarePiece,ref pieceCaptured);

					//record this move...
					this.gamemoves[this.gamemovecount] = move;
					this.gamemovecount++;

					//get next set of possible moves...
					this.GetMovesForCurrentSide();

					//send pieceMoveEvent
					this.OnGameMove(sourceSquarePiece,pieceCaptured);

					//let user know move was accepted
					return true;
				}
			}

			return false;
		}

		public System.Collections.ArrayList GetMovesForPiece(int sourcex,int sourcey)
		{
			ChessPiece spiece;
			System.Collections.ArrayList almoves= new System.Collections.ArrayList();

			spiece=this.board[sourcex,sourcey].chesspiece;

			for(int count =0;count < this.turnmoves.Length;count++)
			{
				if(this.turnmoves[count].sourcex == sourcex)
					if(this.turnmoves[count].sourcey==sourcey)
						almoves.Add(this.turnmoves[count]);
			}

			return almoves;
		}

		#endregion
		#region private Functions
		private void ExecuteMove_PostMoveLogic(ChessMove move,ChessPiece targetSquarePiece,ref ChessPiece pieceCaptured)
		{
			if(this.turn == Side.WHITE)
			{
				switch(move.sourcepiece)
				{
					case Chess.ChessPiece.WHITEKING:
						if(move.sourcex - move.targetx == 2)
						{//left castle happened. Move rook as well
							this.board[0,7].chesspiece=ChessPiece.EMPTY;
							this.board[3,7].chesspiece=ChessPiece.WHITEROOK;
						}
						if(move.sourcex - move.targetx == -2)
						{//right castle happened. Move rook as well.
							this.board[7,7].chesspiece=ChessPiece.EMPTY;
							this.board[5,7].chesspiece=ChessPiece.WHITEROOK;
						}
						break;
					case Chess.ChessPiece.WHITEPAWN:
						//au passant logic
						if((move.sourcex != move.targetx) && (targetSquarePiece == ChessPiece.EMPTY))
						{
							pieceCaptured = this.board[move.targetx,move.targety + 1].chesspiece;
							this.board[move.targetx,move.targety + 1].chesspiece = ChessPiece.EMPTY;
						}
						break;
				}

				turn = Side.BLACK;
			}
			else
			{
				switch(move.sourcepiece)
				{
					case Chess.ChessPiece.BLACKKING:
						if(move.sourcex - move.targetx == 2)
						{//left castle happened
							this.board[0,0].chesspiece=ChessPiece.EMPTY;
							this.board[3,0].chesspiece=ChessPiece.BLACKROOK;
						}
						if(move.sourcex - move.targetx == -2)
						{//right castle happened.
							this.board[7,0].chesspiece=ChessPiece.EMPTY;
							this.board[5,0].chesspiece=ChessPiece.BLACKROOK;
						}
						break;
					case Chess.ChessPiece.BLACKPAWN:
						//au passant logic
						if((move.sourcex != move.targetx) && (targetSquarePiece == ChessPiece.EMPTY))
						{
							pieceCaptured = this.board[move.targetx,move.targety - 1].chesspiece;
							this.board[move.targetx,move.targety - 1].chesspiece = ChessPiece.EMPTY;
						}
						break;
				}

				turn = Side.WHITE;
			}
		}
		private void ResetGame()
		{
			this.ResetGame(Side.WHITE,TimeSpan.MaxValue);
		}
		private void ResetGame(Side Human,TimeSpan gameTime)
		{
			this.human = Human;
			this.turn= Side.WHITE;

			if(this.human == Side.BLACK)
				this.computer =Side.WHITE;
			else
				this.computer =Side.BLACK;


			this.gamemovecount=0;
			this.ResetBoard();
			this.ResetClock(gameTime);
			this.turnmoves=this.GetMovesForSide(this.board,this.turn);
		}

		private void ResetClock(TimeSpan gameTime)
		{

			if(this.gametimer == null)
			{
				//instantiate the timer object...
				this.gametimer = new System.Windows.Forms.Timer();

				//hook into tick event
				this.gametimer.Tick+=new EventHandler(gametimer_Tick);
			}

			this.gametimer.Stop();//stop the timer if its currently running

			//dont start the timer if we are playing without one...
			if(gameTime != TimeSpan.MaxValue)
			{
				this.gametimer.Interval = 1000;//set the tick interval...
				this.gametimer.Start();

				this.gamestart =	System.DateTime.Now;
				this.whitetime =	gameTime;
				this.blacktime =	gameTime;
			}
		}

		private void gametimer_Tick(object sender, EventArgs e)
		{
			
		}

		private void ResetBoard()
		{
			this.board = new Square[8,8];
			
			for(int count=0;count < 8;count++)
				for(int count2=0;count2< 8;count2++)
				{
                    this.board[count, count2] = new Square();
					this.board[count,count2].xpos=count;
					this.board[count,count2].ypos=count2;
					this.board[count,count2].whitekinglinesight=Direction.NONE;
                    this.board[count, count2].blackkinglinesight = Direction.NONE;

					switch(count2)
					{
						case 0:
						switch(count)
						{
								//rooks...
							case 0:
							case 7:
								this.board[count,count2].chesspiece=ChessPiece.BLACKROOK;
								break;
								//knights...
							case 1:
							case 6:
								this.board[count,count2].chesspiece=ChessPiece.BLACKKNIGHT;
								break;
								//bishops...
							case 2:
							case 5:
								this.board[count,count2].chesspiece=ChessPiece.BLACKBISHOP;
								break;
								//queen...
							case 3:
								this.board[count,count2].chesspiece=ChessPiece.BLACKQUEEN;
								break;
								//king...
							case 4:
								this.board[count,count2].chesspiece=ChessPiece.BLACKKING;
								break;
						}
							break;
						case 1:
							this.board[count,count2].chesspiece=ChessPiece.BLACKPAWN;
							break;
						case 6:
							this.board[count,count2].chesspiece=ChessPiece.WHITEPAWN;
							break;
						case 7:
						switch(count)
						{
								//rooks...
							case 0:
							case 7:
								this.board[count,count2].chesspiece=ChessPiece.WHITEROOK;
								break;
								//knights...
							case 1:
							case 6:
								this.board[count,count2].chesspiece=ChessPiece.WHITEKNIGHT;
								break;
								//bishops...
							case 2:
							case 5:
								this.board[count,count2].chesspiece=ChessPiece.WHITEBISHOP;
								break;
								//queen...
							case 3:
								this.board[count,count2].chesspiece=ChessPiece.WHITEQUEEN;
								break;
								//king...
							case 4:
								this.board[count,count2].chesspiece=ChessPiece.WHITEKING;
								break;
						}
							break;
						default:
							this.board[count,count2].chesspiece=ChessPiece.EMPTY;
							break;
					}
				}
		}

		#region King line Sight Functions
		private void SetKingsLineSight()
		{
			int whitekingx=-1,whitekingy=-1;
			int blackkingx=-1,blackkingy=-1;

			//clear previous king line sights...
			this.SetKingsLineSight_ClearKingsLineSight();

			//find the white kings x and y position...
			this.FindKing(ChessPiece.WHITEKING,ref whitekingx,ref whitekingy);

			//find the black kings x and y position...
			this.FindKing(ChessPiece.BLACKKING,ref blackkingx,ref blackkingy);

			//get line of sight for both kings...
			this.SetKingsLineSight_SetSquares(ChessPiece.WHITEKING,whitekingx,whitekingy);
			this.SetKingsLineSight_SetSquares(ChessPiece.BLACKKING,blackkingx,blackkingy);
		}
		private void SetKingsLineSight_SetSquares(ChessPiece king,int kingx, int kingy)
		{
			this.SetKingsLineSight_SetSquaresPath(king,kingx,kingy,0,-1);//north..
            this.SetKingsLineSight_SetSquaresPath(king, kingx, kingy, 0, 1);//south
            this.SetKingsLineSight_SetSquaresPath(king, kingx, kingy, 1, 0);//east..
            this.SetKingsLineSight_SetSquaresPath(king, kingx, kingy, -1, 0);//west..
            this.SetKingsLineSight_SetSquaresPath(king, kingx, kingy, 1, -1);//northeast...
            this.SetKingsLineSight_SetSquaresPath(king, kingx, kingy, -1, -1);//northwest...
            this.SetKingsLineSight_SetSquaresPath(king, kingx, kingy, -1, 1);//southeast...
            this.SetKingsLineSight_SetSquaresPath(king, kingx, kingy, 1, 1);//southwest...
		}
		private void SetKingsLineSight_ClearKingsLineSight()
		{
			for(int count=0;count < 8;count++)
				for(int count2=0;count2< 8;count2++)
				{
                    this.board[count, count2].whitekinglinesight = Direction.NONE;
                    this.board[count, count2].blackkinglinesight = Direction.NONE;
				}
		}
		private void SetKingsLineSight_SetSquaresPath(ChessPiece king,int kingx,
			int kingy, int xadvance,int yadvance)
		{
			int xcurpos=kingx,ycurpos=kingy;

			for(int count=0;count < 8;count++)
			{
				xcurpos += xadvance;
				ycurpos += yadvance;

				if(Square.IsValidBoardIndex(xcurpos) && 
					(Square.IsValidBoardIndex(ycurpos)))
				{
					//mark this square as in line of sight with king..
					if(king == ChessPiece.WHITEKING)
						this.board[xcurpos,ycurpos].whitekinglinesight=
							this.GetDirectionToXYAdvance(xadvance,yadvance);
							//this.GetDirectionOfXYAdvance(xadvance,yadvance);
					else
						this.board[xcurpos,ycurpos].blackkinglinesight=
							this.GetDirectionToXYAdvance(xadvance,yadvance);
							//this.GetDirectionOfXYAdvance(xadvance,yadvance);

					//this square contains a piece, stop looking...line of sight is blocked..
					if(this.board[xcurpos,ycurpos].chesspiece != ChessPiece.EMPTY)
						return;
				}
				else//we wandered of the board...just return..
					return;
			}
		}

		private void FindKing(ChessPiece king,ref int kingx,ref int kingy)
		{
			for(int count=0;count < 8;count++)
				for(int count2=0;count2< 8;count2++)
					if(this.board[count,count2].chesspiece == king)
					{
						kingx = count;//this.board[count,count2].xpos;
						kingy = count2;
						return;
					}
		}

		#endregion
		#region Threat Determination functions

		private Direction GetDirectionToXYAdvance(int x,int y)
		{
			return this.GetDirectionOfXYAdvance(x *-1,y*-1);
		}
		private Direction GetDirectionOfXYAdvance(int x,int y)
		{
			switch(x)
			{
				case 0://no horizontal movement...
				switch(y)
				{
					case 1://moving down = south
                        return Direction.SOUTH;
					case -1:
                        return Direction.NORTH;
				}
					break;
				case 1://moving right = EAST
				switch(y)
				{
					case 0:
                        return Direction.EAST;
					case 1:
                        return Direction.SOUTHEAST;
					case -1:
                        return Direction.NORTHEAST;
				}
					break;
				case -1:
				switch(y)//moving left = WEST
				{
					case 0:
                        return Direction.WEST;
					case 1:
                        return Direction.SOUTHWEST;
					case -1:
                        return Direction.NORTHWEST;
				}
					break;
			}

            return Direction.NONE;//dont know where we are going...
		}

		private void ClearAllBoardThreats(Square[,] chessboard)
		{
			for(int count =0;count < 8;count ++)
				for(int count2=0;count2 < 8;count2++)
				{
					chessboard[count,count2].piecethreateningking=false;
					chessboard[count,count2].whitethreat=
                        chessboard[count, count2].blackthreat = Direction.NONE;
				}
		}
		private void ClearBoardThreats(Square[,] chessboard,Side whichside)
		{
			for(int count =0;count < 8;count ++)
				for(int count2=0;count2 < 8;count2++)
				{
					chessboard[count,count2].piecethreateningking=false;
					if(whichside == Side.WHITE)
                        chessboard[count, count2].whitethreat = Direction.NONE;
					else
                        chessboard[count, count2].blackthreat = Direction.NONE;
				}
		}

		private void SetAllBoardThreats(Square[,] chessboard)
		{
			this.SetBoardThreats(chessboard,true,true);
		}
		private void SetBoardThreats(Square[,] chessboard,bool white,bool black)
		{
			for(int count =0;count < 8;count ++)
				for(int count2=0;count2 < 8;count2++)
				{
					if((chessboard[count,count2].PieceIsWhite())&& (white))
						this.SetBoardThreats_ForPiece(chessboard,this.board[count,count2],Side.WHITE);
					if((chessboard[count,count2].PieceIsBlack())&& (black))
						this.SetBoardThreats_ForPiece(chessboard,this.board[count,count2],Side.BLACK);
				}
		}
		private void SetBoardThreats_ForPiece(Square[,] chessboard,Square boardsquare,Side whichside)
		{
			bool diagonal=false,horizontalandvertical=false,far=false,
				knight=false,pawn=false;

			switch(boardsquare.chesspiece)
			{
				case ChessPiece.WHITEKING:
				case ChessPiece.BLACKKING:
					horizontalandvertical=true;
					diagonal=true;
					break;
				case ChessPiece.WHITEQUEEN:
				case ChessPiece.BLACKQUEEN:
					horizontalandvertical=true;
					diagonal=true;
					far = true;
					break;
				case ChessPiece.WHITEROOK:
				case ChessPiece.BLACKROOK:
					horizontalandvertical=true;
					far = true;
					break;
				case ChessPiece.WHITEKNIGHT:
				case ChessPiece.BLACKKNIGHT:
					knight =true;
					break;
				case ChessPiece.WHITEBISHOP:
				case ChessPiece.BLACKBISHOP:
					diagonal=true;
					far = true;
					break;
				case ChessPiece.WHITEPAWN:
				case ChessPiece.BLACKPAWN:
					pawn=true;
					break;
				case ChessPiece.EMPTY:
					return;
			}

			this.SetBoardThreats_ForPieceType(chessboard,boardsquare,whichside,far,
				horizontalandvertical,diagonal,pawn,knight);
		}
		private void SetBoardThreats_ForPieceType(Square[,] chessboard,Square boardsquare,
			Side threatside,bool far,bool horizontalandvertical,
			bool diagonal,bool pawn,bool knight)
		{
			int boardx=boardsquare.xpos,boardy=boardsquare.ypos;

			if(pawn)//calculate pawn moves...
			{
				if(threatside == Side.BLACK)
				{
					this.SetBoardThreats_ForPieceTypeSetThreat(chessboard,boardsquare,boardx+1,boardy+1,threatside);
					this.SetBoardThreats_ForPieceTypeSetThreat(chessboard,boardsquare,boardx-1,boardy+1,threatside);
				}
				else//side is white
				{

					this.SetBoardThreats_ForPieceTypeSetThreat(chessboard,boardsquare,boardx+1,boardy-1,threatside);
                    this.SetBoardThreats_ForPieceTypeSetThreat(chessboard, boardsquare, boardx - 1, boardy - 1, threatside);
				}

				return;//were done...its a pawn
			}

			if(knight)//calculate knight moves...
			{
                this.SetBoardThreats_ForPieceTypeSetThreat(chessboard, boardsquare, boardx + 2, boardy + 1, threatside);
                this.SetBoardThreats_ForPieceTypeSetThreat(chessboard, boardsquare, boardx + 2, boardy - 1, threatside);
                this.SetBoardThreats_ForPieceTypeSetThreat(chessboard, boardsquare, boardx - 2, boardy + 1, threatside);
                this.SetBoardThreats_ForPieceTypeSetThreat(chessboard, boardsquare, boardx - 2, boardy - 1, threatside);
                this.SetBoardThreats_ForPieceTypeSetThreat(chessboard, boardsquare, boardx + 1, boardy + 2, threatside);
                this.SetBoardThreats_ForPieceTypeSetThreat(chessboard, boardsquare, boardx - 1, boardy + 2, threatside);
				this.SetBoardThreats_ForPieceTypeSetThreat(chessboard,boardsquare,boardx+1,boardy-2,threatside);
				this.SetBoardThreats_ForPieceTypeSetThreat(chessboard,boardsquare,boardx-1,boardy-2,threatside);
				return;//were done, its a knight..
			}

			if(horizontalandvertical)//calculate horizontal and vertical moves...
			{
				this.SetBoardThreats_ForPieceTypeSetFarThreats(chessboard,boardsquare,boardx,boardy,0,1,threatside,far);//down
                this.SetBoardThreats_ForPieceTypeSetFarThreats(chessboard, boardsquare, boardx, boardy, 0, -1, threatside, far);//up
                this.SetBoardThreats_ForPieceTypeSetFarThreats(chessboard, boardsquare, boardx, boardy, 1, 0, threatside, far);//right
                this.SetBoardThreats_ForPieceTypeSetFarThreats(chessboard, boardsquare, boardx, boardy, -1, 0, threatside, far);//left
			}

			if(diagonal)
			{
                this.SetBoardThreats_ForPieceTypeSetFarThreats(chessboard, boardsquare, boardx, boardy, 1, 1, threatside, far);//southeast
                this.SetBoardThreats_ForPieceTypeSetFarThreats(chessboard, boardsquare, boardx, boardy, -1, -1, threatside, far);//northwest
                this.SetBoardThreats_ForPieceTypeSetFarThreats(chessboard, boardsquare, boardx, boardy, 1, -1, threatside, far);//northeast
                this.SetBoardThreats_ForPieceTypeSetFarThreats(chessboard, boardsquare, boardx, boardy, -1, 1, threatside, far);//southwest
			}
		}

		private void SetBoardThreats_ForPieceTypeSetFarThreats(Square[,] chessboard,Square source,int boardx,int boardy,
			int xstep,int ystep,Side threatside,bool far)
		{
			boardx += xstep;
			boardy += ystep;

			while(this.SetBoardThreats_ForPieceTypeSetThreat(chessboard,source,boardx,boardy,threatside,far,
				xstep,ystep))
			{
				//break out if we hit a piece that blocks our line sight OR
				//if we are not far...
				if((chessboard[boardx,boardy].chesspiece != ChessPiece.EMPTY) || (!far))
					return;

				boardx += xstep;
				boardy += ystep;
			}
		}

		private bool SetBoardThreats_ForPieceTypeSetThreat(Square[,] chessboard,Square source,int boardx,int boardy,
			Side threatside)
		{
			return this.SetBoardThreats_ForPieceTypeSetThreat(chessboard,source,boardx,boardy,
				threatside,false,0,0);
		}
		private bool SetBoardThreats_ForPieceTypeSetThreat(Square[,] chessboard,Square source,int boardx,int boardy,
			Side threatside,bool far,int xadvance,int yadvance)
		{
			if(boardx < 0)
				return false;
			if(boardx > 7)
				return false;
			if(boardy < 0)
				return false;
			if(boardy > 7)
				return false;

			//if the threat is a line threat as well, set its line threat direction...
			if(far)
			{
				chessboard[boardx,boardy].setThreat(threatside,
					this.GetDirectionOfXYAdvance(xadvance,yadvance));
			}
			else
			{
				//set direct threat...direct threat means the threat
				//DEFINITELY cannot be countered by blocking a threatline
				//(NOTE: a far threat may not be able to be blocked if the threatening
				//piece is adjacent to the king)
                chessboard[boardx, boardy].setThreat(threatside, Direction.DIRECT);
			}

			//if the square being threatened contains the opponents king,
			//make a note of it.
			if(threatside == Side.WHITE)
			{
				if(chessboard[boardx,boardy].chesspiece == Chess.ChessPiece.BLACKKING)
					chessboard[source.xpos,source.ypos].piecethreateningking =true;
			}
			else
			{
				if(chessboard[boardx,boardy].chesspiece == Chess.ChessPiece.WHITEKING)
					chessboard[source.xpos,source.ypos].piecethreateningking =true;
			}


			return true;
		}



		#endregion
        #region Move Determination functions
        public void GetMovesForCurrentSide()
		{
			this.turnmoves = this.GetMovesForSide(this.board,this.turn);
		}

		private ChessMove[] GetMovesForSide(Square[,] chessboard,Side side)
		{
			ChessMove[] moves;
			int legalmovecount=0;

			//clear and get our board threats..
			this.ClearAllBoardThreats(chessboard);
			//get our board threats...
			this.SetAllBoardThreats(chessboard);
			//get our king line sight...
			this.SetKingsLineSight();

			//find our moves...
			this.FindMovesForSide(chessboard,side,this.sidemovebuffer,ref this.sidemovebuffercount);

			for(int count=0;count < this.sidemovebuffercount;count++)
			{
				if(this.sidemovebuffer[count].sourcepiece != Chess.ChessPiece.EMPTY)
					legalmovecount++;
			}

			moves = new ChessMove[legalmovecount];
			legalmovecount=0;

			for(int count=0;count < this.sidemovebuffercount;count++)
			{
				if(this.sidemovebuffer[count].sourcepiece != Chess.ChessPiece.EMPTY)
				{
					moves[legalmovecount] = this.sidemovebuffer[count];
					legalmovecount++;
				}
			}

			return moves;
		}
		private void FindMovesForSide(Square[,] chessboard,Side side,
			ChessMove[] buffer,ref int buffercount)
		{
			this.sidemovebuffercount=0;
			int kingx=0,kingy=0;
			Direction kingthreat=0;
			bool kingchecked=false;


			//find king first...
			if(side == Side.WHITE)
			{
				this.FindKing(Chess.ChessPiece.WHITEKING,ref kingx,ref kingy);

				//if king is in check...check to see how badly king is in check..
				//if king is in check by two pieces, only option is to move..
				//therefore we only need to calculate the possible king moves...
				kingthreat=chessboard[kingx,kingy].blackthreat;
				if(kingthreat > 0)
				{
					kingchecked=true;

					if(chessboard[kingx,kingy].hasMultipleThreats(Side.BLACK))
					{
						this.GetMoves_ForPieceType(chessboard,chessboard[kingx,kingy],side,false,
							true,true,false,false,true,buffer,ref buffercount);
						return;
					}
				}
			}
			else//black side
			{
				this.FindKing(Chess.ChessPiece.BLACKKING,ref kingx,ref kingy);

				//if king is in check...check to see how badly king is in check..
				//if king is in check by two pieces, only option is to move..
				//therefore we only need to calculate the possible king moves...
				kingthreat=chessboard[kingx,kingy].whitethreat;
				if(kingthreat > 0)
				{
					kingchecked=true;

					if(chessboard[kingx,kingy].hasMultipleThreats(Side.WHITE))
					{
						this.GetMoves_ForPieceType(chessboard,chessboard[kingx,kingy],side,false,
							true,true,false,false,true,buffer,ref buffercount);
						return;
					}
				}
			}

			for(int count=0;count < 8;count ++)
				for(int count2=0;count2 < 8;count2++)
				{
					if(side == Side.WHITE)
					{
						if(chessboard[count,count2].PieceIsWhite())
							GetMovesForSide_PieceMoves(chessboard,side,
								chessboard[count,count2],buffer,ref buffercount);
					}

					if(side == Side.BLACK)
					{
						if(chessboard[count,count2].PieceIsBlack())
							GetMovesForSide_PieceMoves(chessboard,side,
								chessboard[count,count2],buffer,ref buffercount);
					}
				}

			//get the castling moves.
			this.GetMoves_Castling(chessboard,side,buffer,ref buffercount);

			//if the king is in check, we must trim the moves appropriately
			if(kingchecked)
				this.GetMovesForSide_DeleteIllegalMoves(chessboard,side,chessboard[kingx,kingy],
					kingthreat,buffer,ref buffercount);

		}

		private void GetMovesForSide_DeleteIllegalMoves(Square[,] chessboard,Side whichside,Square kingsquare,
			Direction kingthreat,ChessMove [] buffer,ref int buffercount)
		{
			int count2;
			Point [] validtargetsquares;

			validtargetsquares=this.GetMovesForSide_DeleteIllegalMoves_GetValidTargetSquares(
				chessboard,whichside,kingsquare,kingthreat,buffer,ref buffercount);

			for(int count=0;count < buffercount;count++)
			{
				//special case for the king...we must find out which way he is going.
                if ((buffer[count].sourcepiece == Chess.ChessPiece.BLACKKING) ||
                    (buffer[count].sourcepiece == Chess.ChessPiece.WHITEKING))
                {
                    //if the king is moving in the same direction of the threat,
                    //we cant allow this.
                    if (this.GetDirectionToXYAdvance(
                        buffer[count].sourcex - buffer[count].targetx,
                        buffer[count].sourcey - buffer[count].targety) == kingthreat)
                    {
                        buffer[count].sourcepiece = Chess.ChessPiece.EMPTY;
                    }

                    continue;
                }

				for(count2=0;count2 < validtargetsquares.Length;count2++)
				{
					if(buffer[count].targetx == validtargetsquares[count2].X)
						if(buffer[count].targety== validtargetsquares[count2].Y)
						{
							//count=-100;
							break;
						}
				}

				//empty source square means no move...
				if(count2 == validtargetsquares.Length)
					buffer[count].sourcepiece=Chess.ChessPiece.EMPTY;
			}
		}

		private Point [] GetMovesForSide_DeleteIllegalMoves_GetValidTargetSquares(
			Square[,] chessboard,Side whichside,Square kingsquare,
			Direction kingthreat,ChessMove [] buffer,ref int buffercount)
		{
			int threatpiecex=0,threatpiecey=0,xadvance,yadvance,numlinetargetmoves;
			Point [] validtargetsquares=null;

			//get the piece threatening the king..
			this.GetMovesForSide_DeleteIllegalMoves_GetPieceThreateningKing(
				chessboard,whichside,ref threatpiecex,ref threatpiecey);

			//a direct threat can be countered only by capturing the
			//piece threatening the king, or by moving the king...
            if ((kingthreat & Direction.DIRECT) > 0)
			{
				validtargetsquares		= new Point[1];
				validtargetsquares[0].X	= threatpiecex;
				validtargetsquares[0].Y = threatpiecey;
			}
			else
			{// a line threat can be countered by either capturing the
				//target piece (like a direct threat) OR by blocking the threat line (if any);

				//xadvance = (threatpiecex - kingsquare.xpos);
				//yadvance = (threatpiecey - kingsquare.ypos);

				xadvance = (kingsquare.xpos-threatpiecex);
				yadvance = (kingsquare.ypos-threatpiecey);

				if(xadvance > 0)
					numlinetargetmoves=xadvance;
				else
					numlinetargetmoves=yadvance;

				//same as Math.Abs but less costly
				if(numlinetargetmoves < 0)
					numlinetargetmoves *=-1;

				//shave these so that we know what direction to go
				if(xadvance > 1)
					xadvance=1;
				if(xadvance < -1)
					xadvance=-1;

				if(yadvance > 1)
					yadvance=1;
				if(yadvance < -1)
					yadvance=-1;

				//allocate the necessary memory for our target moves..
				validtargetsquares = new Point[numlinetargetmoves];

				for(int count=0;count < numlinetargetmoves;count++)
				{
					validtargetsquares[count].X=threatpiecex;
					validtargetsquares[count].Y=threatpiecey;

					threatpiecex+=xadvance;
					threatpiecey+=yadvance;
				}
			}

			return validtargetsquares;
		}
		private void GetMovesForSide_DeleteIllegalMoves_GetPieceThreateningKing(
			Square[,] chessboard,Side whichside,ref int threatpiecex,ref int threatpiecey)
		{
			for(int count=0;count < 8;count++)
				for(int count2=0;count2 < 8;count2++)
				{
					if(whichside == Side.WHITE)
					{
						if(chessboard[count,count2].piecethreateningking)
							if(chessboard[count,count2].PieceIsBlack())
							{
								threatpiecex=count;
								threatpiecey=count2;
							}
					}
					else//black...
					{
						if(chessboard[count,count2].piecethreateningking)
							if(chessboard[count,count2].PieceIsWhite())
							{
								threatpiecex=count;
								threatpiecey=count2;
							}
					}
				}
		}
		private void GetMovesForSide_PieceMoves(Square[,] chessboard,Side whichside,Square boardsquare,
			ChessMove [] buffer,ref int buffercount)
		{
			if((boardsquare.PieceIsWhite()) && (whichside == Side.WHITE ))
				this.GetMoves_ForPiece(chessboard,boardsquare,whichside,buffer,ref buffercount);
			else if((boardsquare.PieceIsBlack()) && (whichside == Side.BLACK ))
				this.GetMoves_ForPiece(chessboard,boardsquare,whichside,buffer,ref buffercount);
			else
				return;
		}
		
		private void GetMoves_Castling(Square[,] chessboard,Side whichside,ChessMove [] buffer,
			ref int buffercount)
		{
			bool kingmoved=true,kingmovedchecked=false;

			if(whichside == Side.WHITE)
			{
				//make sure pieces are where they need to be first...
				if(chessboard[4,7].chesspiece == Chess.ChessPiece.WHITEKING)
				{//king must be here...check nothing else unless king is here....
					
					//check for left castle move first...
					if(chessboard[0,7].chesspiece == Chess.ChessPiece.WHITEROOK)
					{
						//now make sure the spaces between them are empty and not threatened..
						//left castle first...
						if( (chessboard[1,7].chesspiece == Chess.ChessPiece.EMPTY) && 
							(chessboard[2,7].chesspiece == Chess.ChessPiece.EMPTY) && 
							(chessboard[3,7].chesspiece == Chess.ChessPiece.EMPTY) &&
							(chessboard[1,7].blackthreat== Chess.Direction.NONE	 ) && 
							(chessboard[2,7].blackthreat== Chess.Direction.NONE	 ) && 
							(chessboard[3,7].blackthreat== Chess.Direction.NONE  ))
						{
							kingmoved = this.PieceHasMoved(Chess.ChessPiece.WHITEKING,
								4,7,this.gamemoves,this.gamemovecount);

							//set this flag so we dont recalc the kingmoved 
							kingmovedchecked = true;

							//if the king has not moved, go ahead and check that the
							//left rook has not moved either...
							if(!kingmoved)
							{
								//the rook has not moved
								if(!this.PieceHasMoved(Chess.ChessPiece.WHITEROOK,0,7,this.gamemoves,
									this.gamemovecount))
								{
									//left castle is valid...
									this.GetMoves_ForPieceTypeGetMove(chessboard,chessboard[4,7],2,7,
										whichside,buffer,ref buffercount);
								}
							}
						}

						if( (chessboard[5,7].chesspiece == Chess.ChessPiece.EMPTY) && 
							(chessboard[6,7].chesspiece == Chess.ChessPiece.EMPTY) && 
							(chessboard[5,7].blackthreat== Chess.Direction.NONE	 ) && 
							(chessboard[6,7].blackthreat== Chess.Direction.NONE  ))
						{
							if(!kingmovedchecked)
								kingmoved = this.PieceHasMoved(Chess.ChessPiece.WHITEKING,
									4,7,this.gamemoves,this.gamemovecount);

							//if the king has not moved, go ahead and check that the
							//right rook has not moved either...
							if(!kingmoved)
							{
								//the rook has not moved
								if(!this.PieceHasMoved(Chess.ChessPiece.WHITEROOK,7,7,this.gamemoves,
									this.gamemovecount))
								{
									//left castle is valid...
									this.GetMoves_ForPieceTypeGetMove(chessboard,chessboard[4,7],6,7,
										whichside,buffer,ref buffercount);
								}
							}
						}
					}
				}
			}
			else
			{
				//make sure pieces are where they need to be first...
				if(chessboard[4,0].chesspiece == Chess.ChessPiece.BLACKKING)
				{//king must be here...check nothing else unless king is here....
					
					//check for left castle move first...
					if(chessboard[0,0].chesspiece == Chess.ChessPiece.BLACKROOK)
					{
						//now make sure the spaces between them are empty and not threatened..
						//left castle first...
						if( (chessboard[1,0].chesspiece == Chess.ChessPiece.EMPTY) && 
							(chessboard[2,0].chesspiece == Chess.ChessPiece.EMPTY) && 
							(chessboard[3,0].chesspiece == Chess.ChessPiece.EMPTY) &&
							(chessboard[1,0].whitethreat== Chess.Direction.NONE	 ) && 
							(chessboard[2,0].whitethreat== Chess.Direction.NONE	 ) && 
							(chessboard[3,0].whitethreat== Chess.Direction.NONE  ))
						{
							kingmoved = this.PieceHasMoved(Chess.ChessPiece.BLACKKING,
								4,0,this.gamemoves,this.gamemovecount);

							//set this flag so we dont recalc the kingmoved 
							kingmovedchecked = true;

							//if the king has not moved, go ahead and check that the
							//left rook has not moved either...
							if(!kingmoved)
							{
								//the rook has not moved
								if(!this.PieceHasMoved(Chess.ChessPiece.BLACKROOK,0,0,this.gamemoves,
									this.gamemovecount))
								{
									//left castle is valid...
									this.GetMoves_ForPieceTypeGetMove(chessboard,chessboard[4,0],2,0,
										whichside,buffer,ref buffercount);
								}
							}
						}

						if( (chessboard[5,0].chesspiece == Chess.ChessPiece.EMPTY) && 
							(chessboard[6,0].chesspiece == Chess.ChessPiece.EMPTY) && 
							(chessboard[5,0].whitethreat== Chess.Direction.NONE	 ) && 
							(chessboard[6,0].whitethreat== Chess.Direction.NONE  ))
						{
							if(!kingmovedchecked)
								kingmoved = this.PieceHasMoved(Chess.ChessPiece.BLACKKING,
									4,0,this.gamemoves,this.gamemovecount);

							//if the king has not moved, go ahead and check that the
							//right rook has not moved either...
							if(!kingmoved)
							{
								//the rook has not moved
								if(!this.PieceHasMoved(Chess.ChessPiece.BLACKROOK,7,0,this.gamemoves,
									this.gamemovecount))
								{
									//left castle is valid...
									this.GetMoves_ForPieceTypeGetMove(chessboard,chessboard[4,0],6,0,
										whichside,buffer,ref buffercount);
								}
							}
						}
					}
				}
			}
		}

		private bool PieceHasMoved(Chess.ChessPiece piece,int startx,int starty,
			ChessMove [] movelist,int movelistcount)
		{
			for(int count=0;count < movelistcount;count++)
			{
				if(movelist[count].sourcepiece == piece)
				{
					//verify its the piece by its starting coordinates...
					if((movelist[count].sourcex == startx) && 
						(movelist[count].sourcey  == starty))
						return true;
				}
			}

			return false;
		}
		private void GetMoves_ForPiece(
            Square[,] chessboard,
            Square boardsquare,
            Side whichside,
            ChessMove [] buffer,
			ref int buffercount)
		{
			bool diagonal=false,horizontalandvertical=false,far=false,
				knight=false,pawn=false,king = false ;

			switch(boardsquare.chesspiece)
			{
				case ChessPiece.WHITEKING:
				case ChessPiece.BLACKKING:
					horizontalandvertical=true;
					diagonal=true;
                    king = true;
					break;
				case ChessPiece.WHITEQUEEN:
				case ChessPiece.BLACKQUEEN:
					horizontalandvertical=true;
					diagonal=true;
					far = true;
                    king = true;
					break;
				case ChessPiece.WHITEROOK:
				case ChessPiece.BLACKROOK:
					horizontalandvertical=true;
					far = true;
					break;
				case ChessPiece.WHITEKNIGHT:
				case ChessPiece.BLACKKNIGHT:
					knight =true;
					break;
				case ChessPiece.WHITEBISHOP:
				case ChessPiece.BLACKBISHOP:
					diagonal=true;
					far = true;
					break;
				case ChessPiece.WHITEPAWN:
				case ChessPiece.BLACKPAWN:
					pawn=true;
					break;
				case ChessPiece.EMPTY:
					return;
			}

			this.GetMoves_ForPieceType(chessboard,boardsquare,whichside,far,
				horizontalandvertical,diagonal,pawn,knight,king,buffer,ref buffercount);
		}
		private void GetMoves_ForPieceType(
			Square[,] chessboard,
			Square boardsquare,
			Side whichside,
			bool far,
			bool horizontalandvertical,
			bool diagonal,
			bool pawn,
			bool knight,
            bool king,
			ChessMove[] buffer,
			ref int buffercount)
		{
			int boardx=boardsquare.xpos,boardy=boardsquare.ypos;

            //if((boardsquare.xpos == 2) && (boardsquare.ypos == 2))
            //{
            //    boardsquare.xpos=2;
            //}
			//get the piece pin dir for this square...
			Direction piecepindir=boardsquare.getPiecePinDirection();;
			

			if(pawn)//calculate pawn moves...
			{
				this.GetMoves_ForPieceTypePawn(chessboard,boardsquare,whichside,piecepindir,buffer,ref buffercount);
				return;
			}

			if(knight)//calculate knight moves...
			{
				//easy enough...if the knight is pinned from any direction, it cant move.
				if(piecepindir ==0)
				{
					this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx+2,boardy+1,whichside,buffer,ref buffercount);
					this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx+2,boardy-1,whichside,buffer,ref buffercount);
					this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx-2,boardy+1,whichside,buffer,ref buffercount);
					this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx-2,boardy-1,whichside,buffer,ref buffercount);
					this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx+1,boardy+2,whichside,buffer,ref buffercount);
					this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx-1,boardy+2,whichside,buffer,ref buffercount);
					this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx+1,boardy-2,whichside,buffer,ref buffercount);
					this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx-1,boardy-2,whichside,buffer,ref buffercount);
				}
				else
				{
					return;
				}
				return;//were done, its a knight..
			}

			if(horizontalandvertical)//calculate horizontal and vertical moves...
			{
				if(piecepindir == 0)
				{//no pins, get all moves...
					this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,0,1,whichside,far,buffer,ref buffercount);//down
					this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,0,-1,whichside,far,buffer,ref buffercount);//up
					this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,1,0,whichside,far,buffer,ref buffercount);//right
					this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,-1,0,whichside,far,buffer,ref buffercount);//left
				}
				else
				{//we have king pins in effect...find out which dir and move accordingly

					//if we are pinned diagonally...any vertical or horizontal moves are invalid...
					//other wise...we have to check further...
					if((piecepindir & Direction.DIAGONAL) == 0)
					{
						//ok, not pinned diagonally...which means we are either pinned vertically or horizontally..
						//find out which...
						if((piecepindir & Direction.VERTICAL) == 0)//no vertical pins means we can move horizontally
						{
							this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,1,0,whichside,far,buffer,ref buffercount);//right
							this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,-1,0,whichside,far,buffer,ref buffercount);//left
						}
						else//no horizontal pins means we can move vertically
						{
							this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,0,1,whichside,far,buffer,ref buffercount);//down
							this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,0,-1,whichside,far,buffer,ref buffercount);//up
						}
					}
				}
			}

			if(diagonal)
			{
				if(piecepindir == 0)
				{//no pin, all moves valid
					this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,1,1,whichside,far,buffer,ref buffercount);//southeast
					this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,-1,-1,whichside,far,buffer,ref buffercount);//northwest
					this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,1,-1,whichside,far,buffer,ref buffercount);//northeast
					this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,-1,1,whichside,far,buffer,ref buffercount);//southwest
				}
				else
				{
					//check for vertical or horizontal pin...only if none exist are moves possible...
                    if ((piecepindir & Direction.STRAIGHT) == 0)
					{
                        if ((piecepindir & Direction.SLOPEFALLING) == 0)
						{
							this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,1,-1,whichside,far,buffer,ref buffercount);//northeast
							this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,-1,1,whichside,far,buffer,ref buffercount);//southwest
						}
						else
						{
							this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,1,1,whichside,far,buffer,ref buffercount);//southeast
							this.GetMoves_ForPieceTypeGetFarMoves(chessboard,boardsquare,boardx,boardy,-1,-1,whichside,far,buffer,ref buffercount);//northwest
						}
					}
					//we have a king pin...find out which dir and move accordingly
				}
			}
		}

		private void GetMoves_ForPieceTypePawn_AuPassant(Square[,] chessboard,Square boardsquare,
			Side whichside,Direction piecepindir,ChessMove[] buffer,ref int buffercount)
		{
			ChessMove lastmove = this.gamemoves[this.gamemovecount-1];
			int xcapturedir;


			if (whichside == Side.WHITE)
			{
				//last move was a black pawn...
				if (lastmove.sourcepiece == Chess.ChessPiece.BLACKPAWN)
				{
					//last move was black pawn two spaces forward ...
					if ((lastmove.targety - lastmove.sourcey) == 2)
					{
						xcapturedir = lastmove.sourcex - boardsquare.xpos;

						//pawn is left of me
						if (xcapturedir == -1)
						{
							if ((piecepindir == 0) || (piecepindir == Direction.NORTHWEST))
							{
								this.GetMoves_ForPieceTypeGetMove(chessboard,
									boardsquare, boardsquare.xpos - 1, boardsquare.ypos - 1,
									Side.WHITE, buffer, ref buffercount);
							}
						}

						//pawn is right of me
						if (xcapturedir == 1)
						{
							if ((piecepindir == 0) || (piecepindir == Direction.NORTHEAST))
							{
								this.GetMoves_ForPieceTypeGetMove(chessboard,
									boardsquare, boardsquare.xpos + 1, boardsquare.ypos - 1,
									Side.WHITE, buffer, ref buffercount);
							}
						}
					}
				}
			}
			else
			{
				//last move was a white pawn
				if (lastmove.sourcepiece == Chess.ChessPiece.WHITEPAWN)
				{
					//last move was white pawn two spaces forward ...
					if ((lastmove.targety - lastmove.sourcey) == -2)
					{
						xcapturedir = lastmove.sourcex - boardsquare.xpos;
						if (xcapturedir == -1)
						{
							if ((piecepindir == 0) || (piecepindir == Direction.SOUTHWEST))
							{
								this.GetMoves_ForPieceTypeGetMove(chessboard,
									boardsquare, boardsquare.xpos - 1, boardsquare.ypos + 1,
									Side.BLACK, buffer, ref buffercount);
							}
						}

						if (xcapturedir == 1)
						{
							if ((piecepindir == 0) || (piecepindir == Direction.SOUTHEAST))
							{
								this.GetMoves_ForPieceTypeGetMove(chessboard,
									boardsquare, boardsquare.xpos + 1, boardsquare.ypos + 1,
									Side.BLACK, buffer, ref buffercount);
							}
						}
					}
				}
			}

			//if(lastmove.
		}
		private void GetMoves_ForPieceTypePawn(Square[,] chessboard,Square boardsquare,
			Side whichside,Direction piecepindir,ChessMove[] buffer,ref int buffercount)
		{
			//TODO : AU PASSANT MOVES>>>
			int boardx=boardsquare.xpos,boardy=boardsquare.ypos;
			
			if(whichside == Side.BLACK)
			{
				//all moves are valid, king is not threatened/pinned.
				if(piecepindir == 0)
				{
					//set forward moves...
					if(boardy < 7)
						if(chessboard[boardx,boardy+1].isEmpty())
						{
							//set move forward..
							this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx,boardy+1,
								whichside,buffer,ref buffercount);

							//special case set move forward 2 spaces forward.
							if(boardsquare.ypos == 1)
							{
								if(chessboard[boardx,boardy+2].isEmpty())
									this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx,boardy+2,
										whichside,buffer,ref buffercount);
							}
						}

					//set capture move if square has opponent piece..
					if((boardx < 7) && (boardy < 7))
						if(chessboard[boardx+1,boardy+1].PieceIsWhite())
							this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx+1,boardy+1,
								whichside,buffer,ref buffercount);

					//set capture move if square has opponent piece..
					if((boardx > 0) && (boardy < 7))
						if(chessboard[boardx-1,boardy+1].PieceIsWhite())
							this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx-1,boardy+1,
								whichside,buffer,ref buffercount);
				}
				else
				{
					//check to see if the piece is pinned diagonally..only if it isnt are forward moves possible
                    if ((piecepindir & Direction.DIAGONAL) == 0)
					{
						//if piece is not pinned horizontally, we can move forward...
                        if ((piecepindir & Direction.HORIZONTAL) == 0)
						{
							//set forward moves...
							if(boardy < 7)
								if(chessboard[boardx,boardy+1].isEmpty())
								{
									//set move forward..
									this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx,boardy+1,
										whichside,buffer,ref buffercount);

									//special case set move forward 2 spaces forward.
									if(boardsquare.ypos == 1)
									{
										if(chessboard[boardx,boardy+2].isEmpty())
											this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx,boardy+2,
												whichside,buffer,ref buffercount);
									}
								}
						}
					}
					//only if the piece is NOT in a straight pin a diagonal move possible..
                    if ((piecepindir & Direction.STRAIGHT) == 0)
					{//only a capture move is possible...

                        if ((piecepindir & Direction.SLOPERISING) == 0)
						{
							//set capture move if square has opponent piece..
							if((boardx < 7) && (boardy < 7))
								if(chessboard[boardx+1,boardy+1].PieceIsWhite())
									this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx+1,boardy+1,
										whichside,buffer,ref buffercount);
						}
						else
						{
							//set capture move if square has opponent piece..
							if((boardx > 0) && (boardy < 7))
								if(chessboard[boardx-1,boardy+1].PieceIsWhite())
									this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx-1,boardy+1,
										whichside,buffer,ref buffercount);
						}
					}
				}

				//check for au passantmove if we are in a valid au passant square..
				if(boardy == 4)
					this.GetMoves_ForPieceTypePawn_AuPassant(chessboard,boardsquare,whichside,
						piecepindir,buffer,ref buffercount);
			}
			else//side is white
			{
				//all moves are valid, king is not threatened/pinned.
				if(piecepindir == 0)
				{
					//set forward moves...
					if(boardy > 0)
						if(chessboard[boardx,boardy-1].isEmpty())
						{
							//set move forward..
							this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx,boardy-1,
								whichside,buffer,ref buffercount);

							//special case set move forward 2 spaces forward.
							if(boardsquare.ypos == 6)
							{
								if(chessboard[boardx,boardy-2].isEmpty())
									this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx,boardy-2,
										whichside,buffer,ref buffercount);
							}
						}

					//set capture move if square has opponent piece..
					if((boardx < 7) && (boardy > 0))
						if(chessboard[boardx+1,boardy-1].PieceIsBlack())
							this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx+1,boardy-1,
								whichside,buffer,ref buffercount);

					//set capture move if square has opponent piece..
					if((boardx > 0) && (boardy > 0))
						if(chessboard[boardx-1,boardy-1].PieceIsBlack())
							this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx-1,boardy-1,
								whichside,buffer,ref buffercount);
				}
				else
				{
					//check to see if the piece is pinned diagonally..only if it isnt are forward moves possible
                    if ((piecepindir & Direction.DIAGONAL) == 0)
					{
						//if piece is not pinned horizontally, we can move forward...
                        if ((piecepindir & Direction.HORIZONTAL) == 0)
						{
							//set forward moves...
							if(boardy > 0)
								if(chessboard[boardx,boardy-1].isEmpty())
								{
									//set move forward..
									this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx,boardy-1,
										whichside,buffer,ref buffercount);

									//special case set move forward 2 spaces forward.
									if(boardsquare.ypos == 6)
									{
										if(chessboard[boardx,boardy-2].isEmpty())
											this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx,boardy-2,
												whichside,buffer,ref buffercount);
									}
								}
						}
					}
					//only if the piece is NOT in a straight pin a diagonal move possible..
                    if ((piecepindir & Direction.STRAIGHT) == 0)
					{//only a capture move is possible...

                        if ((piecepindir & Direction.SLOPERISING) == 0)
						{
							//set capture move if square has opponent piece..
							if((boardx < 7) && (boardy > 0))
								if(chessboard[boardx+1,boardy-1].PieceIsBlack())
									this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx+1,boardy-1,
										whichside,buffer,ref buffercount);
						}
						else
						{
							//set capture move if square has opponent piece..
							if((boardx > 0) && (boardy > 0))
								if(chessboard[boardx-1,boardy-1].PieceIsBlack())
									this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx-1,boardy-1,
										whichside,buffer,ref buffercount);
						}
					}
				}

				//check for au passantmove if we are in a valid au passant square..
				if(boardy == 3)
					this.GetMoves_ForPieceTypePawn_AuPassant(chessboard,boardsquare,whichside,
						piecepindir,buffer,ref buffercount);
			}

			return;//were done...its a pawn
		}

		private void GetMoves_ForPieceTypeGetFarMoves(Square[,] chessboard,Square boardsquare,int boardx,int boardy,
			int xstep,int ystep,Side whichside,bool far,ChessMove[] buffer,ref int buffercount)
		{
			boardx += xstep;
			boardy += ystep;

			while(this.GetMoves_ForPieceTypeGetMove(chessboard,boardsquare,boardx,boardy,whichside,buffer,ref buffercount))
			{
				if(!far)
					return;

				boardx += xstep;
				boardy += ystep;
			}
		}

		private bool GetMoves_ForPieceTypeGetMove(Square[,] chessboard,Square source,int targetx,int targety,
			Side whichside,ChessMove[] buffer,ref int buffercount)
		{
			if(targetx < 0)
				return false;
			if(targetx > 7)
				return false;
			if(targety < 0)
				return false;
			if(targety > 7)
				return false;

			if(whichside == Side.WHITE)
			{
				if(chessboard[targetx,targety].PieceIsWhite())
					return false;
				else
				{
					//do not allow king to move into threatened squares..
					if(source.chesspiece == Chess.ChessPiece.WHITEKING)
						if(chessboard[targetx,targety].blackthreat > 0)
							return false;

					buffer[buffercount].SetMove(source.chesspiece,chessboard[targetx,targety].chesspiece,
						source.xpos,source.ypos,targetx,targety);
					buffercount++;

					if(!chessboard[targetx,targety].isEmpty())
						return false;
				}
			}
			else
			{
				if(chessboard[targetx,targety].PieceIsBlack())
					return false;
				else
				{
					//do not allow king to move into threatened squares..
					if(source.chesspiece == Chess.ChessPiece.BLACKKING)
						if(chessboard[targetx,targety].whitethreat > 0)
							return false;

					buffer[buffercount].SetMove(source.chesspiece,chessboard[targetx,targety].chesspiece,
						source.xpos,source.ypos,targetx,targety);
					buffercount++;

					if(!chessboard[targetx,targety].isEmpty())
						return false;
				}
			}

			return true;
		}

		#endregion
		#endregion
	}
}
