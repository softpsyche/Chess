using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Chess
{
	/// <summary>
	/// Summary description for ctlUIChessBoard.
	/// </summary>
	public class ctlUIChessBoard : System.Windows.Forms.UserControl
	{
		//stuff related to dbouble buffering...
		private	Graphics	graphics;
		private Bitmap		memoryBitmap;
		private	int			width;
		private	int			height;

		public Chess.ChessGame chessgame;
		private Chess.ChessPiece movingpiece;
		private ArrayList movingpiecepossiblemoves;
		private int movingboardx,movingboardy;
		private int mousex,mousey;
		private Pen penborders;
		private Brush blacksquarebrush;
		private Brush whitesquarebrush;
		private Brush blackboardtextbrush;
        private Brush redboardtextbrush;
		private Brush boardblackthreatbrush;
		private Brush boardwhitethreatbrush;
		private Brush boardlegalmovebrush;
        private Font arialfont;

		//debug stuff
		public bool showThreatStateInfo;


		public System.Windows.Forms.ImageList imageList16;
		public System.Windows.Forms.ImageList imageList32;
		private System.Windows.Forms.ImageList imageList16Misc;
		private System.Windows.Forms.ToolTip tlTip;
		private System.ComponentModel.IContainer components;

		public ctlUIChessBoard()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.chessgame=null;
			this.penborders = new Pen(Color.Black,1);
			this.whitesquarebrush= new SolidBrush(Color.MintCream);
			this.blacksquarebrush = new SolidBrush(Color.Wheat);
			this.blackboardtextbrush = new SolidBrush(Color.Black);
            this.redboardtextbrush = new SolidBrush(Color.Red);
			this.boardblackthreatbrush=new SolidBrush(Color.Red);
			this.boardwhitethreatbrush=new SolidBrush(Color.Blue);
			this.boardlegalmovebrush=new SolidBrush(Color.Blue );
			this.movingpiece =Chess.ChessPiece.EMPTY;
            this.arialfont = new Font("Arial", 6.5F);

			//create a memory bitmap buffer...
			//this.CreateDoubleBuffer(513,513);
			//this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Opaque, true);

			this.SetStyle(ControlStyles.DoubleBuffer | 
				ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint,true);
			


			/*
			this.tlTip.SetToolTip(this,
				"NONE=		0\n"+
				"DIRECT=	1\n"+
				"NORTH=		2\n"+
				"SOUTH=		4\n"+
				"EAST=		8\n"+
				"WEST=		16\n"+
				"NORTHEAST=	32\n"+
				"NORTHWEST=	64\n"+
				"SOUTHEAST=	128\n"+
				"SOUTHWEST=	256");
				
			*/

			//debug stuff
//			this.chessgame = new ChessGame();
//			this.chessgame.NewGame(Chess.Side.WHITE);
//			for(int count=0;count < 8;count++)
//			{
//				this.chessgame.board[count,6].chesspiece= Chess.ChessPiece.EMPTY;
//				this.chessgame.board[count,7].chesspiece= Chess.ChessPiece.EMPTY;
//			}
//
//			this.chessgame.board[4,4].chesspiece = Chess.ChessPiece.WHITEKING   ;
//			this.chessgame.SetAllBoardThreats(this.chessgame.board);
//			this.movingpiece=ChessPiece.EMPTY;
//			this.paintBlackThreats=false;
//			this.paintWhiteThreats=false;

//			this.paintBlackThreats=true;
//			this.paintWhiteThreats=true;
		}


		/// <summary>
		/// Creates double buffer object
		/// </summary>
		/// <param name="g">Window forms Graphics Object</param>
		/// <param name="width">width of paint area</param>
		/// <param name="height">height of paint area</param>
		/// <returns>true/false if double buffer is created</returns>
		public bool CreateDoubleBuffer(int width, int height)
		{

			if (memoryBitmap != null)
			{
				memoryBitmap.Dispose();
				memoryBitmap = null;
			}

			if (graphics != null)
			{
				graphics.Dispose();
				graphics = null;
			}

			if (width == 0 || height == 0)
				return false;


			if ((width != this.width) || (height != this.height))
			{
				this.width = width;
				this.height = height;

				memoryBitmap	= new Bitmap(width, height);
				graphics		= Graphics.FromImage(memoryBitmap);
			}

			return true;
		}

		/// <summary>
		/// Renders the double buffer to the screen
		/// </summary>
		/// <param name="g">Window forms Graphics Object</param>
		public void RenderFromBuffer(Graphics g)
		{
			if (memoryBitmap != null)
				g.DrawImage(memoryBitmap, new Rectangle(0,0, width, height),0,0, width, height, GraphicsUnit.Pixel);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
					this.penborders.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		public void setChessGame(ChessGame chessGame)
		{
			this.chessgame=chessGame;
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ctlUIChessBoard));
			this.imageList16 = new System.Windows.Forms.ImageList(this.components);
			this.imageList32 = new System.Windows.Forms.ImageList(this.components);
			this.imageList16Misc = new System.Windows.Forms.ImageList(this.components);
			this.tlTip = new System.Windows.Forms.ToolTip(this.components);
			// 
			// imageList16
			// 
			this.imageList16.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList16.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
			this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imageList32
			// 
			this.imageList32.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList32.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
			this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imageList16Misc
			// 
			this.imageList16Misc.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList16Misc.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList16Misc.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16Misc.ImageStream")));
			this.imageList16Misc.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ctlUIChessBoard
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Name = "ctlUIChessBoard";
			this.Size = new System.Drawing.Size(513, 513);
			this.Click += new System.EventHandler(this.ctlUIChessBoard_Click);
			this.Load += new System.EventHandler(this.ctlUIChessBoard_Load);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ctlUIChessBoard_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ctlUIChessBoard_Paint);
			this.MouseHover += new System.EventHandler(this.ctlUIChessBoard_MouseHover);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ctlUIChessBoard_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctlUIChessBoard_MouseDown);

		}
		#endregion


		#region UI Paint Functions

//		protected override void OnPaintBackground(PaintEventArgs pevent)
//		{
//		}

		private void ctlUIChessBoard_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			
			//this.getd
			if(this.chessgame != null)
			{
				//this.drawSquares(e,this.graphics);
				//this.drawMovingPiece(e,this.graphics);
				//this.RenderFromBuffer(e.Graphics);

				this.drawSquares(e,e.Graphics);
				this.drawMovingPiece(e,e.Graphics);
			}
		}
		private void drawMovingPiece(System.Windows.Forms.PaintEventArgs e,Graphics surface)
		{
			if(this.movingpiece != ChessPiece.EMPTY)
				surface.DrawImage(this.imageList32.Images[(int)this.movingpiece],
					this.mousex-16,this.mousey-16,32,32);
		}
		private void drawSquares(System.Windows.Forms.PaintEventArgs e,Graphics surface)
		{
			for(int count=0;count < 8;count ++)
				for(int count2=0;count2 < 8;count2++)
				{
					this.drawSquare(e,surface,this.chessgame.Board[count,count2],
						count * 64,count2 * 64);
				}
		}
		private void drawSquare(System.Windows.Forms.PaintEventArgs e,Graphics surface,
			Chess.Square square,int x,int y)
		{
			//draw bounding black square
			surface.DrawRectangle(this.penborders,x,y,64,64);
            Brush brushPtr;

			//draw filler in box...
			if(square.ypos%2 == 0)
			{
				if(square.xpos %2 == 0)
					surface.FillRectangle(this.whitesquarebrush,x+1,y+1,63,63);
				else
					surface.FillRectangle(this.blacksquarebrush,x+1,y+1,63,63);
			}
			else
			{
				if(square.xpos %2 == 0)
					surface.FillRectangle(this.blacksquarebrush,x+1,y+1,63,63);
				else
					surface.FillRectangle(this.whitesquarebrush,x+1,y+1,63,63);
			}

			//draw square as a valid move square
			if(this.movingpiece != ChessPiece.EMPTY)
			{
				if(this.squareIsValidMoveSquare(square))
				{
					surface.FillRectangle(this.boardlegalmovebrush,x+1,y+1,63,63);
				}
			}

			if(square.xpos == 0)
			{
				surface.DrawString(Convert.ToString((8 - square.ypos)),this.Font,
					this.blackboardtextbrush,x+3,y+25);
			}

			//ascii A=65
			if(square.ypos == 7)
			{
				surface.DrawString(Convert.ToChar(65+ square.xpos).ToString(),this.Font,
					this.blackboardtextbrush,x+27,y+48);
			}

			/*
			//draw square being threatened
			if(this.movingpiece != ChessPiece.EMPTY)
			{
				if(this.squareIsValidMoveSquare(square))
				{
					surface.DrawImage(this.imageList16Misc.Images[0],
						x+24,y ,16,16);
				}
			}
			*/

			//dont draw piece if its being moved...
			if((this.movingpiece != ChessPiece.EMPTY) &&
				(square.xpos == this.movingboardx) && (square.ypos == this.movingboardy))
				return;

			//draw the icon on the board..
			if(square.chesspiece != ChessPiece.EMPTY)
				surface.DrawImage(this.imageList32.Images[(int)square.chesspiece],
					x+16,y +16,32,32);

			if(this.showThreatStateInfo)
			{
                brushPtr = this.redboardtextbrush;
                //if (square.PieceIsBlack())
                //    brushPtr = this.whiteboardtextbrush;
                //else
                //    brushPtr = this.blackboardtextbrush;

                surface.DrawString("WT=" + square.whitethreat.ToString(), this.arialfont,
                    brushPtr, x, y);
                surface.DrawString("WL=" + square.whitekinglinesight.ToString(), this.arialfont,
                    brushPtr, x, y + 8);
                surface.DrawString("BT=" + square.blackthreat.ToString(), this.arialfont,
                    brushPtr, x , y + 16);
                surface.DrawString("BL=" + square.blackkinglinesight.ToString(), this.arialfont,
                    brushPtr, x , y + 24);
                surface.DrawString("TK=" + square.piecethreateningking.ToString().Substring(0, 1), this.arialfont,
                    brushPtr, x, y + 54);
                /*
				surface.DrawString("WT=" + square.whitethreat.ToString(),this.arialfont,
                    brushPtr, x, y);
                surface.DrawString("WL=" + square.whitekinglinesight.ToString(), this.arialfont,
                    brushPtr, x, y + 8);
                surface.DrawString("BT=" + square.blackthreat.ToString(), this.arialfont,
                    brushPtr, x + 32, y);
                surface.DrawString("BL=" + square.blackkinglinesight.ToString(), this.arialfont,
                    brushPtr, x + 32, y + 8);
                surface.DrawString("TK=" + square.piecethreateningking.ToString().Substring(0, 1), this.arialfont,
                    brushPtr, x, y + 54);
                */
			}

			//RenderFromBuffer(e.Graphics);
		}
		private bool squareIsValidMoveSquare(Square square)
		{
			ChessMove move;

			if((this.movingpiece != ChessPiece.EMPTY) &&
				(this.movingpiecepossiblemoves != null))
			{
				for(int count=0;count < this.movingpiecepossiblemoves.Count;count++)
				{
					move = (ChessMove)this.movingpiecepossiblemoves[count];

					if((move.sourcex == this.movingboardx)
						&& (move.sourcey == this.movingboardy))
					{
						if((move.targetx == square.xpos) && (move.targety == square.ypos))
							return true;
					}
				}
			}

			return false;
		}

		private void ctlUIChessBoard_Click(object sender, System.EventArgs e)
		{

		}

		private void ctlUIChessBoard_Load(object sender, System.EventArgs e)
		{
		}

		private void ctlUIChessBoard_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int boardx=-1,boardy=-1;

			switch(e.Button)
			{
				case MouseButtons.Left:
					this.boardHitTest(e.X,e.Y,ref boardx,ref boardy);

					if(this.movingpiece  == ChessPiece.EMPTY) 
					{
						if(this.chessgame.GetTurn() == Side.WHITE)
						{
							if(this.chessgame.Board[boardx,boardy].PieceIsBlack())
								return;
						}
						else
						{
							if(this.chessgame.Board[boardx,boardy].PieceIsWhite())
								return;
						}
						//we have a valid piece for this side...

						this.movingboardx =boardx;
						this.movingboardy =boardy;
						this.mousex = e.X;
						this.mousey = e.Y;
						this.movingpiece = this.chessgame.Board[boardx,boardy].chesspiece;
						this.movingpiecepossiblemoves=this.chessgame.GetMovesForPiece(movingboardx,movingboardy);

						if(this.movingpiecepossiblemoves != null)
							this.Invalidate();
						else
						{
							this.Invalidate(new Rectangle(this.movingboardx*64,this.movingboardy*64,64,64));
							this.Invalidate(new Rectangle(this.mousex -16,this.mousey-16,32,32));
						}
					}
					else
					{//we are finishing a move...
						if(this.chessgame.ExecuteMove(movingboardx,movingboardy,boardx,boardy))
						{
							this.movingpiece=ChessPiece.EMPTY;
							this.movingpiecepossiblemoves=null;
							this.Invalidate();
						}
						else
						{
							this.movingpiece=ChessPiece.EMPTY;
							this.movingpiecepossiblemoves =null;
							this.Invalidate();
						}
						//this.ctlUIChessBoard_MouseDown_CancelMove(e);
					}
					break;
				case MouseButtons.Right:
					this.ctlUIChessBoard_MouseDown_CancelMove(e);
					this.Invalidate();
					break;
			}
		}

		private void ctlUIChessBoard_MouseDown_CancelMove(System.Windows.Forms.MouseEventArgs e)
		{
			this.movingpiece = ChessPiece.EMPTY;
			this.Invalidate(new Rectangle(this.mousex-16,this.mousey-16,64,64));
			//this.Invalidate(new Rectangle(e.X-16,e.Y-16,64,64));
			//this.Invalidate(new Rectangle(this.movingboardx *64,this.movingboardy *64,64,64));
		}

		private void boardHitTest(int mousex,int mousey,ref int boardx,ref int boardy)
		{
			boardx = mousex /64;
			boardy = mousey /64;
		}

		private void ctlUIChessBoard_MouseHover(object sender, System.EventArgs e)
		{
			if(this.showThreatStateInfo)
			{
				//this.tlTip.
			}
		}

		private void ctlUIChessBoard_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//this.movingpiece = ChessPiece.EMPTY;
			//this.Invalidate(new Rectangle(e.X,e.Y,64,64));
			//this.Invalidate(new Rectangle(this.movingboardx *64,this.movingboardy *64,64,64));
		}

		private void ctlUIChessBoard_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(this.movingpiece != ChessPiece.EMPTY)
			{
				//if((Math.Abs(this.mousex - e.X) > 5) ||
				//	(Math.Abs(this.mousey - e.Y) > 5))
				//{
					this.Invalidate(new Rectangle(this.mousex-16,this.mousey-16,32,32));
					this.mousex=e.X;
					this.mousey=e.Y;
					this.Invalidate(new Rectangle(e.X-16,e.Y-16,32,32));
				//}
			}
		}

		#endregion
	}
}
