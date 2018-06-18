using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Chess
{
    public partial class dlgDebugArtificialIntelligence : Form
    {
        private ArtificialIntelligence ai;
        private ChessGame chessGame;

        public dlgDebugArtificialIntelligence(ChessGame chessGame)
        {
            InitializeComponent();

            this.chessGame                  = new ChessGame();
            this.chessGame.Board            = Chess.ChessGame.CopyBoard(chessGame.Board);
            this.chessGame.Turn             = chessGame.Turn;
            this.chessGame.Human            = chessGame.Human;
            this.chessGame.Computer         = chessGame.Computer;
            this.ctlUIChessBoard.chessgame  = this.chessGame;
            ai = new ArtificialIntelligence(this.chessGame);
        }

        private void dlgDebugArtificialIntelligence_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerateTree_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            ai.FindPlies(ai.GameTree,Convert.ToInt32(this.nudDepth.Value));

            this.LoadGameTree();

            this.Cursor = Cursors.Default;
        }

        private void LoadGameTree()
        {
            //clear the positions...
            this.trvBoardPositions.Nodes.Clear();

            //load the whole tree into the control...
            this.AddGameNode(this.trvBoardPositions.Nodes, ai.GameTree);
        }

        private void AddGameNode(TreeNodeCollection nodeCollection, GameState gameState)
        {
            TreeNode newNode = new TreeNode("Board Position");
            newNode.Tag = gameState;

            nodeCollection.Add(newNode);

            for (int count = 0; count < gameState.Children.Count; count++)
                this.AddGameNode(newNode.Nodes, gameState.Children[count]);
        }

        private void trvBoardPositions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            GameState gameState;
            TreeNode selectedNode;

            if (e.Action != TreeViewAction.Unknown)
            {
                selectedNode = e.Node;

                if (selectedNode != null)
                {
                    gameState               = (GameState)selectedNode.Tag;
                    this.chessGame.Board    = gameState.Board;
                    this.chessGame.Turn     = gameState.Turn;

                    //force the guy to redraw...
                    ctlUIChessBoard.Invalidate();
                }
            }
        }

        private void trvBoardPositions_DoubleClick(object sender, EventArgs e)
        {
            this.trvBoardPositions.BeginUpdate();
            this.trvBoardPositions.ExpandAll();
            this.trvBoardPositions.EndUpdate();
        }
    }
}