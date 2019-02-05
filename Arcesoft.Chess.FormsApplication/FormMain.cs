using Arcesoft.Chess.ArtificialIntelligence;
using Arcesoft.Chess.Implementation;
using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arcesoft.Chess.FormsApplication
{
    public partial class FormMain : Form
    {
        private readonly IGameFactory _gameFactory;     
        private readonly IArtificialIntelligence _artificialIntelligence;
        private readonly IMatchFactory _matchFactory;

        private IGame _game;

        public FormMain(IGameFactory gameFactory, IArtificialIntelligence artificialIntelligence, IMatchFactory matchFactory)
        {
            InitializeComponent();
            _gameFactory = gameFactory;
            _artificialIntelligence = artificialIntelligence;
            _matchFactory = matchFactory;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NewGame();
        }

        private void DoIt()
        {
            try
            {
                var board = new Board();
                var threatProvider = new ThreatProvider();
                var gamey = new Game(board, threatProvider);

                var legalMoves = gamey.FindMoves();

                board.Clear();
                board[Models.BoardLocation.A1] = ChessPiece.WhiteKing;
                board[Models.BoardLocation.D4] = ChessPiece.BlackQueen;
                board[Models.BoardLocation.D7] = ChessPiece.BlackPawn;
                board[Models.BoardLocation.D2] = ChessPiece.WhitePawn;

                //board[Models.BoardLocation.E2] = ChessPiece.BlackKnight;
                var threats = threatProvider.FindThreatsForPlayer(board, Player.White);
                var billy = gamey.GetThreatenedBoardDisplay(Player.White);


                var sally = "3434";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //throw new InvalidOperationException();
            DoIt();
        }

        private void undoLastMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _game?.UndoLastMove();
            uxChessBoardMain.Refresh();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
            uxChessBoardMain.Refresh();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MatchOpenFileDialog.ShowDialog(this) == DialogResult.OK)
                { 
                    var pgnFileText = File.ReadAllText(MatchOpenFileDialog.FileName);
                    var matches = _matchFactory.Load(pgnFileText);

                    switch (matches.Count)
                    {
                        case 0:
                            MessageBox.Show("No games found", "Invalid or empty PGN file specified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case 1:
                            NewGame(matches.First().Game);
                            break;
                        default:
                            NewGame(matches.First().Game);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToMessageBox();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NotImplemented yet...");
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NotImplemented yet...");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Helpers
        private void NewGame(IGame game = null)
        {
            _game = game ?? _gameFactory.NewGame();

            uxChessBoardMain.setChessGame(_game);
        }
        #endregion
    }
}
