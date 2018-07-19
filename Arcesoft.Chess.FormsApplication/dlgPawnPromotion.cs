using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Arcesoft.Chess;
using Arcesoft.Chess.Models;

namespace Arcesoft.Chess.FormsApplication
{
    /// <summary>
    /// Summary description for frmPawnPromotion.
    /// </summary>
    public class DlgPawnPromotion : System.Windows.Forms.Form
    {
        private Player _player;


        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoQueen;
        private System.Windows.Forms.PictureBox pbxQueen;
        private System.Windows.Forms.PictureBox pbxRook;
        private System.Windows.Forms.PictureBox pbxBishop;
        private System.Windows.Forms.PictureBox pbxKnight;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton rdoRook;
        private System.Windows.Forms.RadioButton rdoBishop;
        private System.Windows.Forms.RadioButton rdoKnight;
        public ImageList imageList32;
        private System.ComponentModel.IContainer components;

        public DlgPawnPromotion(Player player)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _player = player;
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
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgPawnPromotion));
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoKnight = new System.Windows.Forms.RadioButton();
            this.rdoBishop = new System.Windows.Forms.RadioButton();
            this.rdoRook = new System.Windows.Forms.RadioButton();
            this.rdoQueen = new System.Windows.Forms.RadioButton();
            this.pbxQueen = new System.Windows.Forms.PictureBox();
            this.pbxRook = new System.Windows.Forms.PictureBox();
            this.pbxBishop = new System.Windows.Forms.PictureBox();
            this.pbxKnight = new System.Windows.Forms.PictureBox();
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxQueen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxRook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBishop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxKnight)).BeginInit();
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
            // rdoKnight
            // 
            this.rdoKnight.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoKnight.Location = new System.Drawing.Point(12, 72);
            this.rdoKnight.Name = "rdoKnight";
            this.rdoKnight.Size = new System.Drawing.Size(32, 16);
            this.rdoKnight.TabIndex = 19;
            // 
            // rdoBishop
            // 
            this.rdoBishop.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoBishop.Location = new System.Drawing.Point(52, 72);
            this.rdoBishop.Name = "rdoBishop";
            this.rdoBishop.Size = new System.Drawing.Size(32, 16);
            this.rdoBishop.TabIndex = 18;
            // 
            // rdoRook
            // 
            this.rdoRook.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoRook.Location = new System.Drawing.Point(92, 72);
            this.rdoRook.Name = "rdoRook";
            this.rdoRook.Size = new System.Drawing.Size(32, 16);
            this.rdoRook.TabIndex = 17;
            // 
            // rdoQueen
            // 
            this.rdoQueen.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoQueen.Checked = true;
            this.rdoQueen.Location = new System.Drawing.Point(132, 72);
            this.rdoQueen.Name = "rdoQueen";
            this.rdoQueen.Size = new System.Drawing.Size(32, 16);
            this.rdoQueen.TabIndex = 16;
            this.rdoQueen.TabStop = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.pbxQueen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxRook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBishop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxKnight)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void frmPawnPromotion_Load(object sender, System.EventArgs e)
        {
            this.drawChoices();
        }

        private void drawChoices()
        {
            if (_player == Player.Black)
            {
                pbxKnight.Image = this.imageList32.Images[ToImageIndex(ChessPiece.BlackKnight)];
                pbxBishop.Image = this.imageList32.Images[ToImageIndex(ChessPiece.BlackBishop)];
                pbxRook.Image = this.imageList32.Images[ToImageIndex(ChessPiece.BlackRook)];
                pbxQueen.Image = this.imageList32.Images[ToImageIndex(ChessPiece.BlackQueen)];
            }
            else
            {
                pbxKnight.Image = this.imageList32.Images[ToImageIndex(ChessPiece.WhiteKnight)];
                pbxBishop.Image = this.imageList32.Images[ToImageIndex(ChessPiece.WhiteBishop)];
                pbxRook.Image = this.imageList32.Images[ToImageIndex(ChessPiece.WhiteRook)];
                pbxQueen.Image = this.imageList32.Images[ToImageIndex(ChessPiece.WhiteQueen)];
            }
        }

        private int ToImageIndex(ChessPiece chessPiece)
        {
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
            this.rdoQueen.Checked = true;
        }

        private void pbxBishop_Click(object sender, System.EventArgs e)
        {
            this.rdoBishop.Checked = true;
        }

        private void pbxKnight_Click(object sender, System.EventArgs e)
        {
            this.rdoKnight.Checked = true;
        }

        public PawnPromotionType SelectedPawnPromotion
        {
            get
            {
                if (rdoKnight.Checked)
                    return PawnPromotionType.Knight;
                else if (rdoBishop.Checked)
                    return PawnPromotionType.Bishop;
                else if (rdoRook.Checked)
                    return PawnPromotionType.Rook;
                else
                    return PawnPromotionType.Queen;
            }
        }

        public static PawnPromotionType? PromotePawn(Player player)
        {
            using (var dialog = new DlgPawnPromotion(player))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPawnPromotion;
                }
            }

            return null;
        }
    }
}
