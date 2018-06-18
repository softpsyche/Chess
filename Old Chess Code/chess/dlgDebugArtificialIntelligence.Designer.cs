namespace Chess
{
    partial class dlgDebugArtificialIntelligence
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.trvBoardPositions = new System.Windows.Forms.TreeView();
            this.btnGenerateTree = new System.Windows.Forms.Button();
            this.nudDepth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ctlUIChessBoard = new Chess.ctlUIChessBoard();
            ((System.ComponentModel.ISupportInitialize)(this.nudDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // trvBoardPositions
            // 
            this.trvBoardPositions.Location = new System.Drawing.Point(12, 13);
            this.trvBoardPositions.Name = "trvBoardPositions";
            this.trvBoardPositions.Size = new System.Drawing.Size(246, 487);
            this.trvBoardPositions.TabIndex = 1;
            this.trvBoardPositions.DoubleClick += new System.EventHandler(this.trvBoardPositions_DoubleClick);
            this.trvBoardPositions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvBoardPositions_AfterSelect);
            // 
            // btnGenerateTree
            // 
            this.btnGenerateTree.Location = new System.Drawing.Point(139, 506);
            this.btnGenerateTree.Name = "btnGenerateTree";
            this.btnGenerateTree.Size = new System.Drawing.Size(119, 20);
            this.btnGenerateTree.TabIndex = 2;
            this.btnGenerateTree.Text = "Generate Tree";
            this.btnGenerateTree.UseVisualStyleBackColor = true;
            this.btnGenerateTree.Click += new System.EventHandler(this.btnGenerateTree_Click);
            // 
            // nudDepth
            // 
            this.nudDepth.Location = new System.Drawing.Point(91, 506);
            this.nudDepth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDepth.Name = "nudDepth";
            this.nudDepth.Size = new System.Drawing.Size(42, 20);
            this.nudDepth.TabIndex = 4;
            this.nudDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudDepth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 510);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Search Depth";
            // 
            // ctlUIChessBoard
            // 
            this.ctlUIChessBoard.BackColor = System.Drawing.Color.White;
            this.ctlUIChessBoard.Location = new System.Drawing.Point(273, 13);
            this.ctlUIChessBoard.Name = "ctlUIChessBoard";
            this.ctlUIChessBoard.Size = new System.Drawing.Size(513, 513);
            this.ctlUIChessBoard.TabIndex = 0;
            // 
            // dlgDebugArtificialIntelligence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 538);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudDepth);
            this.Controls.Add(this.btnGenerateTree);
            this.Controls.Add(this.trvBoardPositions);
            this.Controls.Add(this.ctlUIChessBoard);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgDebugArtificialIntelligence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dlgDebugArtificialIntelligence";
            this.Load += new System.EventHandler(this.dlgDebugArtificialIntelligence_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDepth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctlUIChessBoard ctlUIChessBoard;
        private System.Windows.Forms.TreeView trvBoardPositions;
        private System.Windows.Forms.Button btnGenerateTree;
        private System.Windows.Forms.NumericUpDown nudDepth;
        private System.Windows.Forms.Label label1;
    }
}