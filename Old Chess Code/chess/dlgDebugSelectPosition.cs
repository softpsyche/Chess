using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Chess
{
    public partial class dlgDebugSelectPosition : Form
    {
        private Square[,] board;

        public Square[,] Board
        {
            get { return board; }
        }

        public dlgDebugSelectPosition()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChessGame newGame;

            if (this.lstPositions.SelectedIndex == -1)
            {
                MessageBox.Show("Select A Position");
                return;
            }

            newGame = new ChessGame();
            newGame.NewGame(Side.WHITE);

            switch (this.lstPositions.SelectedItem.ToString())
            {
                default:
                case "Two Kings":
                    this.board = newGame.Board;
                    Chess.ChessGame.ClearBoard(this.board);
                    this.board[0,0].chesspiece = ChessPiece.WHITEKING;
                    this.board[7,7].chesspiece = ChessPiece.BLACKKING;
                    break;
                //case "Two Kings One Knight":
                //    break;
                //Two Kings One Bishop
                //Two Kings One Rook
                //Two Kings One Queen
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}