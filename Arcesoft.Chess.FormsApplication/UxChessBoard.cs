using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Arcesoft.Chess;
using Arcesoft.Chess.Models;
using System.Linq;
using System.Collections.Generic;
using Arcesoft.Chess.Implementation;

namespace Arcesoft.Chess.FormsApplication
{
    /// <summary>
    /// Summary description for ctlUIChessBoard.
    /// </summary>
    public class UxChessBoard : System.Windows.Forms.UserControl
    {
        public IGame Game;
        private ChessPiece MovingPiece;
        private List<IMove> MovingPiecePossibleMoves;
        private BoardLocation MovingPieceBoardLocation;
        private int mousex, mousey;
        private Pen PenBorder;
        private Brush DarkSquareBrush;
        private Brush LightSquareBrush;
        private Brush BlackBoardTextbrush;
        private Brush RedBoardTextBrush;
        private Brush LegalMoveBrush;
        private Font ArialFont;

        //debug stuff
        public bool showThreatStateInfo;

        public ImageList imageList32;
        private ImageList imageList16Misc;
        private ToolTip tlTip;
        private IContainer components;

        public UxChessBoard()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            Game = null;
            PenBorder = new Pen(Color.Black, 1);
            LightSquareBrush = new SolidBrush(Color.MintCream);
            DarkSquareBrush = new SolidBrush(Color.Wheat);
            BlackBoardTextbrush = new SolidBrush(Color.Black);
            RedBoardTextBrush = new SolidBrush(Color.Red);
            LegalMoveBrush = new SolidBrush(Color.Blue);
            MovingPiece = ChessPiece.None;
            ArialFont = new Font("Arial", 6.5F);

            SetStyle(
                ControlStyles.DoubleBuffer |ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                    PenBorder.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public void setChessGame(IGame chessGame)
        {
            Game = chessGame;
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UxChessBoard));
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.imageList16Misc = new System.Windows.Forms.ImageList(this.components);
            this.tlTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // imageList32
            // 
            this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList32.Images.SetKeyName(0, "01pone.gif");
            this.imageList32.Images.SetKeyName(1, "03knight.gif");
            this.imageList32.Images.SetKeyName(2, "04bishop.gif");
            this.imageList32.Images.SetKeyName(3, "02castle.gif");
            this.imageList32.Images.SetKeyName(4, "05queen.gif");
            this.imageList32.Images.SetKeyName(5, "06king.gif");
            this.imageList32.Images.SetKeyName(6, "07pone.gif");
            this.imageList32.Images.SetKeyName(7, "09knight.gif");
            this.imageList32.Images.SetKeyName(8, "10bishop.gif");
            this.imageList32.Images.SetKeyName(9, "08castle.gif");
            this.imageList32.Images.SetKeyName(10, "11queen.gif");
            this.imageList32.Images.SetKeyName(11, "12king.gif");
            // 
            // imageList16Misc
            // 
            this.imageList16Misc.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList16Misc.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList16Misc.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // UxChessBoard
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Name = "UxChessBoard";
            this.Size = new System.Drawing.Size(513, 513);
            this.Load += new System.EventHandler(this.UxChessBoard_Load);
            this.Click += new System.EventHandler(this.UxChessBoard_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ctlUIChessBoard_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UxChessBoard_MouseDown);
            this.MouseHover += new System.EventHandler(this.UxChessBoard_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UxChessBoard_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UxChessBoard_MouseUp);
            this.ResumeLayout(false);

        }
        #endregion


        #region UI Paint Functions

        private void ctlUIChessBoard_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (Game != null)
            {
                DrawSquares(e, e.Graphics);
                DrawMovingPiece(e, e.Graphics);
            }
        }
        private void DrawMovingPiece(System.Windows.Forms.PaintEventArgs e, Graphics surface)
        {
            if (MovingPiece != ChessPiece.None)
                surface.DrawImage(imageList32.Images[(int)MovingPiece],
                    mousex - 16, mousey - 16, 32, 32);
        }
        private void DrawSquares(System.Windows.Forms.PaintEventArgs e, Graphics surface)
        {
            //draws board from top to bottom
            for (int count = 0; count < 8; count++)
            {
                for (int count2 = 0; count2 < 8; count2++)
                {
                    DrawSquare(e, surface, ToBoardLocation(count, count2),
                        count * 64, count2 * 64);
                }
            }
        }
        private void DrawSquare(System.Windows.Forms.PaintEventArgs e, Graphics surface,
            BoardLocation boardLocation, int x, int y)
        {
            //draw bounding black square
            surface.DrawRectangle(PenBorder, x, y, 64, 64);

            //draw light or dark square background
            surface.FillRectangle(
                boardLocation.Color() == BoardLocationColor.Light ? LightSquareBrush: DarkSquareBrush,
                x + 1, 
                y + 1, 
                63, 63);

            //draw square as a valid move square
            if (MovingPiece != ChessPiece.None)
            {
                if (IsValidMove(boardLocation))
                {
                    surface.FillRectangle(LegalMoveBrush, x + 1, y + 1, 63, 63);
                }
            }

            //Draw the legend for squares (a-h and 1-8)
            if (boardLocation.Column() == 0)
            {
                surface.DrawString(Convert.ToString((boardLocation.Row() + 1)), Font,
                    BlackBoardTextbrush, x + 3, y + 25);
            }        
            if (boardLocation.Row()== 7)
            {//ascii A=65
                surface.DrawString(Convert.ToChar(65 + boardLocation.Column()).ToString(), Font,
                    BlackBoardTextbrush, x + 27, y + 48);
            }

            //dont draw piece if its being moved...
            if ((MovingPiece != ChessPiece.None) &&
                (boardLocation == MovingPieceBoardLocation))
                return;

            //draw the icon on the board..
            if (Game.Board[boardLocation] != ChessPiece.None)
            {
                surface.DrawImage(
                    imageList32.Images[ToImageIndex(boardLocation)],
                    x + 16, y + 16, 32, 32);
            }
        }

        private int ToImageIndex(BoardLocation boardLocation)
        {
            ChessPiece chessPiece = Game.Board[boardLocation];
            switch (chessPiece)
            {
                case ChessPiece.WhitePawn:
                    return 0;
                case ChessPiece.WhiteKnight:
                    return 1;
                case ChessPiece.WhiteBishop:
                    return 2;
                case ChessPiece.WhiteRook:
                    return 3;
                case ChessPiece.WhiteQueen:
                    return 4;
                case ChessPiece.WhiteKing:
                    return 5;
                case ChessPiece.BlackPawn:
                    return 6;
                case ChessPiece.BlackKnight:
                    return 7;
                case ChessPiece.BlackBishop:
                    return 8;
                case ChessPiece.BlackRook:
                    return 9;
                case ChessPiece.BlackQueen:
                    return 10;
                case ChessPiece.BlackKing:
                    return 11;
                default:
                    throw new InvalidOperationException();
            }
        }

        private bool IsValidMove(BoardLocation boardLocation)
        {
            if ((MovingPiece != ChessPiece.None) &&
                (MovingPiecePossibleMoves?.Any()).GetValueOrDefault())
            {
                return MovingPiecePossibleMoves
                    .Count(a => a.Destination == boardLocation) > 0;
            }

            return false;
        }

        private void UxChessBoard_Click(object sender, System.EventArgs e)
        {

        }

        private void UxChessBoard_Load(object sender, System.EventArgs e)
        {
        }

        private void UxChessBoard_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int boardx = -1, boardy = -1;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    BoardHitTest(e.X, e.Y, ref boardx, ref boardy);
                    var boardLocation = ToBoardLocation(boardx, boardy);

                    //games over pal
                    if (Game.GameIsOver)
                    {
                        return;
                    }

                    if (MovingPiece == ChessPiece.None)
                    {
                        //if the piece does not belong to the current player do nothing
                        if (!Game.Board[boardLocation].BelongsTo(Game.CurrentPlayer))
                        {
                            return;
                        }
                        //we have a valid piece for this side...

                        MovingPieceBoardLocation = boardLocation;
                        mousex = e.X;
                        mousey = e.Y;
                        MovingPiece = Game.Board[boardLocation];
                        MovingPiecePossibleMoves = Game.FindMoves().Where(a => a.Source == boardLocation).ToList();

                        Invalidate();
                    }
                    else
                    {//we are finishing a move...
                        var destination = ToBoardLocation(boardx, boardy);

                        Game.MakeMove(MovingPieceBoardLocation, destination);

                        MovingPiecePossibleMoves = null;
                        Invalidate();
                        UxChessBoard_MouseDown_CancelMove(e);
                    }
                    break;
                case MouseButtons.Right:
                    UxChessBoard_MouseDown_CancelMove(e);
                    Invalidate();
                    break;
            }
        }

        private void UxChessBoard_MouseDown_CancelMove(System.Windows.Forms.MouseEventArgs e)
        {
            MovingPiece = ChessPiece.None;
            Invalidate(new Rectangle(mousex - 16, mousey - 16, 64, 64));
        }

        private void BoardHitTest(int mousex, int mousey, ref int boardx, ref int boardy)
        {
            boardx = mousex / 64;
            boardy = mousey / 64;
        }

        private void UxChessBoard_MouseHover(object sender, System.EventArgs e)
        {
            if (showThreatStateInfo)
            {
                //tlTip.
            }
        }

        private void UxChessBoard_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //movingpiece = ChessPiece.EMPTY;
            //Invalidate(new Rectangle(e.X,e.Y,64,64));
            //Invalidate(new Rectangle(movingboardx *64,movingboardy *64,64,64));
        }

        private void UxChessBoard_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (MovingPiece != ChessPiece.None)
            {
                Invalidate(new Rectangle(mousex - 16, mousey - 16, 32, 32));
                mousex = e.X;
                mousey = e.Y;
                Invalidate(new Rectangle(e.X - 16, e.Y - 16, 32, 32));
            }
        }


        private BoardLocation ToBoardLocation(int boardX, int boardY)
        {
            return BoardLocation.A1;
        }
        #endregion
    }
}
