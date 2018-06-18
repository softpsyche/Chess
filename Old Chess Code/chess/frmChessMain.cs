using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Chess
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmChessMain : System.Windows.Forms.Form
	{
		Chess.ChessGame chessgame;
		//Timer.


		private System.Windows.Forms.MainMenu mnuRoot;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.StatusBarPanel statusBarPanel3;
		private System.Windows.Forms.ColumnHeader moveNumber;
		private System.Windows.Forms.ColumnHeader Move;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.ImageList imageList16;
		private System.Windows.Forms.ListView lvwMoves;
		private System.Windows.Forms.Panel pnlInformation;
		private System.Windows.Forms.GroupBox gbxDifficulty;
		private System.Windows.Forms.TrackBar tbrDifficulty;
		private Chess.ctlUIChessBoard UIChessBoard;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem mnuDebugShowSquareThreatInfo;
		private System.Windows.Forms.MenuItem mnuHelpAbout;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem mnuGame;
		private System.Windows.Forms.MenuItem mnuGameNew;
		private System.Windows.Forms.MenuItem mnuGamePause;
		private System.Windows.Forms.StatusBar sbarGameStatus;
		private System.Windows.Forms.StatusBarPanel statusMessage;
		private System.Windows.Forms.StatusBarPanel statusTurn;
		private System.Windows.Forms.Button button2;
        private Button button3;
		private System.ComponentModel.IContainer components;

		public frmChessMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChessMain));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "A1 - B5",
            "- Pawn Moves"}, 0, System.Drawing.Color.Empty, System.Drawing.Color.MintCream, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "B3 - G6",
            "- Bishop Captures Bishop"}, 10, System.Drawing.Color.Empty, System.Drawing.Color.Wheat, null);
            this.sbarGameStatus = new System.Windows.Forms.StatusBar();
            this.statusMessage = new System.Windows.Forms.StatusBarPanel();
            this.statusTurn = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel3 = new System.Windows.Forms.StatusBarPanel();
            this.mnuRoot = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuFileExit = new System.Windows.Forms.MenuItem();
            this.mnuGame = new System.Windows.Forms.MenuItem();
            this.mnuGameNew = new System.Windows.Forms.MenuItem();
            this.mnuGamePause = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.mnuDebugShowSquareThreatInfo = new System.Windows.Forms.MenuItem();
            this.lvwMoves = new System.Windows.Forms.ListView();
            this.moveNumber = new System.Windows.Forms.ColumnHeader();
            this.Move = new System.Windows.Forms.ColumnHeader();
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.pnlInformation = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gbxDifficulty = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbrDifficulty = new System.Windows.Forms.TrackBar();
            this.button3 = new System.Windows.Forms.Button();
            this.UIChessBoard = new Chess.ctlUIChessBoard();
            ((System.ComponentModel.ISupportInitialize)(this.statusMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusTurn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).BeginInit();
            this.pnlInformation.SuspendLayout();
            this.gbxDifficulty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrDifficulty)).BeginInit();
            this.SuspendLayout();
            // 
            // sbarGameStatus
            // 
            this.sbarGameStatus.Location = new System.Drawing.Point(0, 565);
            this.sbarGameStatus.Name = "sbarGameStatus";
            this.sbarGameStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusMessage,
            this.statusTurn,
            this.statusBarPanel3});
            this.sbarGameStatus.ShowPanels = true;
            this.sbarGameStatus.Size = new System.Drawing.Size(794, 22);
            this.sbarGameStatus.TabIndex = 0;
            this.sbarGameStatus.Text = "gamestatus";
            // 
            // statusMessage
            // 
            this.statusMessage.Icon = ((System.Drawing.Icon)(resources.GetObject("statusMessage.Icon")));
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Text = "Ready.";
            this.statusMessage.Width = 300;
            // 
            // statusTurn
            // 
            this.statusTurn.Icon = ((System.Drawing.Icon)(resources.GetObject("statusTurn.Icon")));
            this.statusTurn.Name = "statusTurn";
            this.statusTurn.Text = "12/18/45";
            // 
            // statusBarPanel3
            // 
            this.statusBarPanel3.Icon = ((System.Drawing.Icon)(resources.GetObject("statusBarPanel3.Icon")));
            this.statusBarPanel3.Name = "statusBarPanel3";
            this.statusBarPanel3.Text = "time Remaing";
            this.statusBarPanel3.Width = 300;
            // 
            // mnuRoot
            // 
            this.mnuRoot.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.mnuGame,
            this.menuItem4,
            this.menuItem6});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFileExit});
            this.menuItem1.Text = "File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Index = 0;
            this.mnuFileExit.Text = "Exit";
            // 
            // mnuGame
            // 
            this.mnuGame.Index = 1;
            this.mnuGame.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuGameNew,
            this.mnuGamePause});
            this.mnuGame.Text = "Game";
            // 
            // mnuGameNew
            // 
            this.mnuGameNew.Index = 0;
            this.mnuGameNew.Text = "New Game";
            this.mnuGameNew.Click += new System.EventHandler(this.mnuGameNew_Click);
            // 
            // mnuGamePause
            // 
            this.mnuGamePause.Index = 1;
            this.mnuGamePause.Text = "Pause Game";
            this.mnuGamePause.Click += new System.EventHandler(this.mnuGamePause_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuHelpAbout});
            this.menuItem4.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Index = 0;
            this.mnuHelpAbout.Text = "About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuDebugShowSquareThreatInfo});
            this.menuItem6.Text = "Debug";
            // 
            // mnuDebugShowSquareThreatInfo
            // 
            this.mnuDebugShowSquareThreatInfo.Index = 0;
            this.mnuDebugShowSquareThreatInfo.Text = "Show Square Threat State Info";
            this.mnuDebugShowSquareThreatInfo.Click += new System.EventHandler(this.mnuDebugShowSquareThreatInfo_Click);
            // 
            // lvwMoves
            // 
            this.lvwMoves.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.moveNumber,
            this.Move});
            this.lvwMoves.FullRowSelect = true;
            this.lvwMoves.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwMoves.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvwMoves.Location = new System.Drawing.Point(8, 8);
            this.lvwMoves.Name = "lvwMoves";
            this.lvwMoves.Size = new System.Drawing.Size(240, 128);
            this.lvwMoves.SmallImageList = this.imageList16;
            this.lvwMoves.TabIndex = 2;
            this.lvwMoves.UseCompatibleStateImageBehavior = false;
            this.lvwMoves.View = System.Windows.Forms.View.Details;
            // 
            // moveNumber
            // 
            this.moveNumber.Text = "#";
            this.moveNumber.Width = 65;
            // 
            // Move
            // 
            this.Move.Width = 150;
            // 
            // imageList16
            // 
            this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList16.Images.SetKeyName(0, "");
            this.imageList16.Images.SetKeyName(1, "");
            this.imageList16.Images.SetKeyName(2, "");
            this.imageList16.Images.SetKeyName(3, "");
            this.imageList16.Images.SetKeyName(4, "");
            this.imageList16.Images.SetKeyName(5, "");
            this.imageList16.Images.SetKeyName(6, "");
            this.imageList16.Images.SetKeyName(7, "");
            this.imageList16.Images.SetKeyName(8, "");
            this.imageList16.Images.SetKeyName(9, "");
            this.imageList16.Images.SetKeyName(10, "");
            this.imageList16.Images.SetKeyName(11, "");
            this.imageList16.Images.SetKeyName(12, "");
            // 
            // pnlInformation
            // 
            this.pnlInformation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlInformation.Controls.Add(this.button3);
            this.pnlInformation.Controls.Add(this.button2);
            this.pnlInformation.Controls.Add(this.button1);
            this.pnlInformation.Controls.Add(this.gbxDifficulty);
            this.pnlInformation.Controls.Add(this.lvwMoves);
            this.pnlInformation.Location = new System.Drawing.Point(528, 32);
            this.pnlInformation.Name = "pnlInformation";
            this.pnlInformation.Size = new System.Drawing.Size(256, 512);
            this.pnlInformation.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 346);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 64);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 461);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(240, 43);
            this.button1.TabIndex = 5;
            this.button1.Text = "Load Position";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbxDifficulty
            // 
            this.gbxDifficulty.Controls.Add(this.label3);
            this.gbxDifficulty.Controls.Add(this.label2);
            this.gbxDifficulty.Controls.Add(this.label1);
            this.gbxDifficulty.Controls.Add(this.tbrDifficulty);
            this.gbxDifficulty.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDifficulty.Location = new System.Drawing.Point(8, 136);
            this.gbxDifficulty.Name = "gbxDifficulty";
            this.gbxDifficulty.Size = new System.Drawing.Size(240, 72);
            this.gbxDifficulty.TabIndex = 4;
            this.gbxDifficulty.TabStop = false;
            this.gbxDifficulty.Text = "Game Difficulty";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(192, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hard";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(96, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Medium";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Easy";
            // 
            // tbrDifficulty
            // 
            this.tbrDifficulty.AutoSize = false;
            this.tbrDifficulty.Location = new System.Drawing.Point(8, 16);
            this.tbrDifficulty.Maximum = 20;
            this.tbrDifficulty.Name = "tbrDifficulty";
            this.tbrDifficulty.Size = new System.Drawing.Size(224, 32);
            this.tbrDifficulty.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(8, 214);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(240, 126);
            this.button3.TabIndex = 7;
            this.button3.Text = "Debug AI";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // UIChessBoard
            // 
            this.UIChessBoard.BackColor = System.Drawing.Color.White;
            this.UIChessBoard.Location = new System.Drawing.Point(8, 32);
            this.UIChessBoard.Name = "UIChessBoard";
            this.UIChessBoard.Size = new System.Drawing.Size(513, 513);
            this.UIChessBoard.TabIndex = 1;
            // 
            // frmChessMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.ClientSize = new System.Drawing.Size(794, 587);
            this.Controls.Add(this.pnlInformation);
            this.Controls.Add(this.UIChessBoard);
            this.Controls.Add(this.sbarGameStatus);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Menu = this.mnuRoot;
            this.MinimumSize = new System.Drawing.Size(800, 640);
            this.Name = "frmChessMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Euclid";
            this.Load += new System.EventHandler(this.frmChessMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statusMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusTurn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).EndInit();
            this.pnlInformation.ResumeLayout(false);
            this.gbxDifficulty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbrDifficulty)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try
			{
				Application.Run(new frmChessMain());
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void NewGame()
		{
			this.chessgame=new ChessGame();
			this.chessgame.GameMoveEvent+=new Chess.ChessGame.GameMoveEventHandler(chessgame_GameMoveEvent);
			this.chessgame.NewGame(Side.WHITE);
			this.ClearMoveHistory();
			this.UIChessBoard.chessgame=this.chessgame;
			this.UIChessBoard.Invalidate();
		}
		private void frmChessMain_Load(object sender, System.EventArgs e)
		{
			this.NewGame();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
            dlgDebugSelectPosition dialog = new dlgDebugSelectPosition();

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.chessgame.Board = dialog.Board;
                this.UIChessBoard.Refresh();
            }
		}

		private void mnuDebugShowSquareThreatInfo_Click(object sender, System.EventArgs e)
		{
			this.mnuDebugShowSquareThreatInfo.Checked= !this.mnuDebugShowSquareThreatInfo.Checked;
			this.UIChessBoard.showThreatStateInfo=this.mnuDebugShowSquareThreatInfo.Checked;
			this.UIChessBoard.Invalidate();
		}

		private void mnuHelpAbout_Click(object sender, System.EventArgs e)
		{
			Chess.dlgAboutEuclid dlgabout = new dlgAboutEuclid();

			dlgabout.ShowDialog(this);
			dlgabout.Dispose();
		}


		private void mnuGameNew_Click(object sender, System.EventArgs e)
		{
			if(this.Message("Are You Sure You Want To Start A New Game?",MessageBoxButtons.YesNo,
				MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.NewGame();
			}
		}


		private DialogResult Message(string msg)
		{
			return MessageBox.Show(this,msg,"Euclid",MessageBoxButtons.OK,MessageBoxIcon.None);
		}

		private DialogResult Message(string msg,MessageBoxButtons buttons)
		{
			return MessageBox.Show(this,msg,"Euclid",buttons,MessageBoxIcon.None);
		}

		private DialogResult Message(string msg,MessageBoxButtons buttons,MessageBoxIcon icon)
		{
			return MessageBox.Show(this,msg,"Euclid",buttons,icon);
		}

		private void mnuGamePause_Click(object sender, System.EventArgs e)
		{
		
		}

		private void flashStatusMessage(string msg,Image img)
		{
			this.sbarGameStatus.Panels[0].Text=msg;
			//this.sbarGameStatus.Panels[0].Icon=img;
		}

		private void chessgame_GameMoveEvent(object sender, GameMoveEventArgs e)
		{
			if(this.chessgame.GetTurn() == Side.WHITE)
			{
				this.sbarGameStatus.Panels[1].Text="Whites Turn.";
			}
			else
			{
				this.sbarGameStatus.Panels[1].Text="Blacks Turn.";
			}

			AddMoveToHistory(e);
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			Chess.dlgPawnPromotion dialog = new dlgPawnPromotion(this.chessgame);

			dialog.ShowDialog(this);
		}

		private void AddMoveToHistory(GameMoveEventArgs e)
		{
			ListViewItem lvwItem;
			string source,target,action,moveDescription="";

			source = Chess.ChessGame.TranslateChessCoordinatesToEnglish(
				e.chessMove.sourcex,
				e.chessMove.sourcey);

			target = Chess.ChessGame.TranslateChessCoordinatesToEnglish(
				e.chessMove.targetx,
				e.chessMove.targety);

			if(e.pieceCaptured == ChessPiece.EMPTY)
				action = "-";
			else
				action = "x";

			lvwItem = new ListViewItem(source + action + target,
				(int)this.chessgame.Board[e.chessMove.targetx,e.chessMove.targety].chesspiece);
			
			if(this.chessgame.GetTurn() == Chess.Side.BLACK)
			{
				lvwItem.BackColor = System.Drawing.Color.MintCream;
				moveDescription = "White ";
			}
			else
			{
				lvwItem.BackColor = System.Drawing.Color.Wheat;
				moveDescription = "Black ";
			}

			if(e.pieceCaptured == ChessPiece.EMPTY)
			{
				moveDescription += Chess.ChessGame.GetChessPieceName(e.sourcePiece)+ " Moved.";
			}
			else
			{
				moveDescription += Chess.ChessGame.GetChessPieceName(e.sourcePiece)+ " Captured " +
					Chess.ChessGame.GetChessPieceName(e.pieceCaptured) + ".";
			}

			lvwItem.SubItems.Add(moveDescription);

			this.lvwMoves.Items.Add(lvwItem);

			this.lvwMoves.Items[this.lvwMoves.Items.Count-1].EnsureVisible();
			//this.lvwMoves.Items.s
			//this.lvwMoves.SelectedItems = this.lvwMoves.Items[this.lvwMoves.Items.Count-1];
		}
		private void ClearMoveHistory()
		{
			this.lvwMoves.Items.Clear();
		}

        private void button3_Click(object sender, EventArgs e)
        {
            dlgDebugArtificialIntelligence dialog = 
                new dlgDebugArtificialIntelligence(this.chessgame);

            dialog.ShowDialog();
        }
	}
}
