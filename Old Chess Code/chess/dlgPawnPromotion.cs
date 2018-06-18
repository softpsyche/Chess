using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Chess
{
	/// <summary>
	/// Summary description for frmPawnPromotion.
	/// </summary>
	public class dlgPawnPromotion : System.Windows.Forms.Form
	{
		private Chess.ChessGame chessGame;


		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdoQueen;
		private System.Windows.Forms.PictureBox pbxQueen;
		private System.Windows.Forms.PictureBox pbxRook;
		private System.Windows.Forms.PictureBox pbxBishop;
		private System.Windows.Forms.PictureBox pbxKnight;
		private System.Windows.Forms.Button btnOk;
		public System.Windows.Forms.ImageList imageList32;
		private System.Windows.Forms.RadioButton rdoRook;
		private System.Windows.Forms.RadioButton rdoBishop;
		private System.Windows.Forms.RadioButton rdoKnight;
		private System.ComponentModel.IContainer components;

		public dlgPawnPromotion(ChessGame chessGame)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.chessGame = chessGame;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgPawnPromotion));
			this.btnOk = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdoQueen = new System.Windows.Forms.RadioButton();
			this.pbxQueen = new System.Windows.Forms.PictureBox();
			this.pbxRook = new System.Windows.Forms.PictureBox();
			this.pbxBishop = new System.Windows.Forms.PictureBox();
			this.pbxKnight = new System.Windows.Forms.PictureBox();
			this.imageList32 = new System.Windows.Forms.ImageList(this.components);
			this.rdoRook = new System.Windows.Forms.RadioButton();
			this.rdoBishop = new System.Windows.Forms.RadioButton();
			this.rdoKnight = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(57, 112);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 24);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdoKnight);
			this.groupBox1.Controls.Add(this.rdoBishop);
			this.groupBox1.Controls.Add(this.rdoRook);
			this.groupBox1.Controls.Add(this.rdoQueen);
			this.groupBox1.Controls.Add(this.pbxQueen);
			this.groupBox1.Controls.Add(this.pbxRook);
			this.groupBox1.Controls.Add(this.pbxBishop);
			this.groupBox1.Controls.Add(this.pbxKnight);
			this.groupBox1.Location = new System.Drawing.Point(9, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(176, 96);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Select desired pawn promotion";
			// 
			// rdoQueen
			// 
			this.rdoQueen.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rdoQueen.Checked = true;
			this.rdoQueen.Location = new System.Drawing.Point(132, 72);
			this.rdoQueen.Name = "rdoQueen";
			this.rdoQueen.Size = new System.Drawing.Size(32, 16);
			this.rdoQueen.TabIndex = 16;
			// 
			// pbxQueen
			// 
			this.pbxQueen.Location = new System.Drawing.Point(132, 24);
			this.pbxQueen.Name = "pbxQueen";
			this.pbxQueen.Size = new System.Drawing.Size(32, 40);
			this.pbxQueen.TabIndex = 13;
			this.pbxQueen.TabStop = false;
			this.pbxQueen.Click += new System.EventHandler(this.pbxQueen_Click);
			// 
			// pbxRook
			// 
			this.pbxRook.Location = new System.Drawing.Point(92, 24);
			this.pbxRook.Name = "pbxRook";
			this.pbxRook.Size = new System.Drawing.Size(32, 40);
			this.pbxRook.TabIndex = 12;
			this.pbxRook.TabStop = false;
			this.pbxRook.Click += new System.EventHandler(this.pbxRook_Click);
			// 
			// pbxBishop
			// 
			this.pbxBishop.Location = new System.Drawing.Point(52, 24);
			this.pbxBishop.Name = "pbxBishop";
			this.pbxBishop.Size = new System.Drawing.Size(32, 40);
			this.pbxBishop.TabIndex = 11;
			this.pbxBishop.TabStop = false;
			this.pbxBishop.Click += new System.EventHandler(this.pbxBishop_Click);
			// 
			// pbxKnight
			// 
			this.pbxKnight.Location = new System.Drawing.Point(12, 24);
			this.pbxKnight.Name = "pbxKnight";
			this.pbxKnight.Size = new System.Drawing.Size(32, 40);
			this.pbxKnight.TabIndex = 10;
			this.pbxKnight.TabStop = false;
			this.pbxKnight.Click += new System.EventHandler(this.pbxKnight_Click);
			// 
			// imageList32
			// 
			this.imageList32.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList32.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
			this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// rdoRook
			// 
			this.rdoRook.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rdoRook.Location = new System.Drawing.Point(92, 72);
			this.rdoRook.Name = "rdoRook";
			this.rdoRook.Size = new System.Drawing.Size(32, 16);
			this.rdoRook.TabIndex = 17;
			// 
			// rdoBishop
			// 
			this.rdoBishop.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rdoBishop.Location = new System.Drawing.Point(52, 72);
			this.rdoBishop.Name = "rdoBishop";
			this.rdoBishop.Size = new System.Drawing.Size(32, 16);
			this.rdoBishop.TabIndex = 18;
			// 
			// rdoKnight
			// 
			this.rdoKnight.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rdoKnight.Location = new System.Drawing.Point(12, 72);
			this.rdoKnight.Name = "rdoKnight";
			this.rdoKnight.Size = new System.Drawing.Size(32, 16);
			this.rdoKnight.TabIndex = 19;
			// 
			// dlgPawnPromotion
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(194, 144);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "dlgPawnPromotion";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Pawn Promotion";
			this.Load += new System.EventHandler(this.frmPawnPromotion_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmPawnPromotion_Load(object sender, System.EventArgs e)
		{
			this.drawChoices();
		}

		private void drawChoices()
		{
			if(this.chessGame.GetTurn() == Chess.Side.BLACK)
			{
				this.pbxKnight.Image	= this.imageList32.Images[(int)Chess.ChessPiece.BLACKKNIGHT];
				this.pbxBishop.Image	= this.imageList32.Images[(int)Chess.ChessPiece.BLACKBISHOP];
				this.pbxRook.Image		= this.imageList32.Images[(int)Chess.ChessPiece.BLACKROOK];
				this.pbxQueen.Image		= this.imageList32.Images[(int)Chess.ChessPiece.BLACKQUEEN];
			}
			else
			{
				this.pbxKnight.Image	= this.imageList32.Images[(int)Chess.ChessPiece.WHITEKNIGHT];
				this.pbxBishop.Image	= this.imageList32.Images[(int)Chess.ChessPiece.WHITEBISHOP];
				this.pbxRook.Image		= this.imageList32.Images[(int)Chess.ChessPiece.WHITEROOK];
				this.pbxQueen.Image		= this.imageList32.Images[(int)Chess.ChessPiece.WHITEQUEEN];
			}
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void pbxRook_Click(object sender, System.EventArgs e)
		{
			this.rdoRook.Checked = true;
		}

		private void pbxQueen_Click(object sender, System.EventArgs e)
		{
			this.rdoQueen.Checked =true;
		}

		private void pbxBishop_Click(object sender, System.EventArgs e)
		{
			this.rdoBishop.Checked=true;
		}

		private void pbxKnight_Click(object sender, System.EventArgs e)
		{
			this.rdoKnight.Checked =true;
		}
	}
}
