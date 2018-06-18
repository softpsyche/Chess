namespace Chess
{
    partial class dlgDebugSelectPosition
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
            this.lstPositions = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstPositions
            // 
            this.lstPositions.FormattingEnabled = true;
            this.lstPositions.Items.AddRange(new object[] {
            "Two Kings",
            "Two Kings One Knight",
            "Two Kings One Bishop",
            "Two Kings One Rook",
            "Two Kings One Queen"});
            this.lstPositions.Location = new System.Drawing.Point(12, 12);
            this.lstPositions.Name = "lstPositions";
            this.lstPositions.Size = new System.Drawing.Size(268, 212);
            this.lstPositions.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(68, 233);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(149, 233);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dlgDebugSelectPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 268);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lstPositions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgDebugSelectPosition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "dlgDebugSelectPosition";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstPositions;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}