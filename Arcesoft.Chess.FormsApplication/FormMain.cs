using Arcesoft.Chess.ArtificialIntelligence;
using Arcesoft.Chess.Implementation;
using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arcesoft.Chess.FormsApplication
{
    public partial class FormMain : Form
    {
        SimpleInjector.Container IocContainer { get; }
        private IGameFactory _gameFactory;
        private IGame _game;
        private IArtificialIntelligence _artificialIntelligence;

        public FormMain(SimpleInjector.Container container)
        {
            InitializeComponent();

            IocContainer = container;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _gameFactory = IocContainer.GetInstance<IGameFactory>();
            _game = _gameFactory.NewGame();
            _artificialIntelligence = IocContainer.GetInstance<IArtificialIntelligence>();

            uxChessBoardMain.setChessGame(_game);
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
            DoIt();
        }
    }
}
