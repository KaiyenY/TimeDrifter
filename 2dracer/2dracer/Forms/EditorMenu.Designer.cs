namespace _2dracer
{
    partial class EditorMenu
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
            this.createButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.xValueBox = new System.Windows.Forms.NumericUpDown();
            this.yValueBox = new System.Windows.Forms.NumericUpDown();
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.xValueBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yValueBox)).BeginInit();
            this.SuspendLayout();
            // 
            // createButton
            // 
            this.createButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createButton.Location = new System.Drawing.Point(12, 41);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(230, 38);
            this.createButton.TabIndex = 0;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(12, 146);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(230, 38);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(12, 217);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(230, 38);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // openDialog
            // 
            this.openDialog.DefaultExt = "Main";
            this.openDialog.Filter = "Map File (*.map)|*.map";
            this.openDialog.InitialDirectory = "C:/";
            // 
            // xValueBox
            // 
            this.xValueBox.Location = new System.Drawing.Point(62, 101);
            this.xValueBox.Name = "xValueBox";
            this.xValueBox.Size = new System.Drawing.Size(52, 20);
            this.xValueBox.TabIndex = 3;
            this.xValueBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xValueBox.ValueChanged += new System.EventHandler(this.ValueChange);
            // 
            // yValueBox
            // 
            this.yValueBox.Location = new System.Drawing.Point(157, 101);
            this.yValueBox.Name = "yValueBox";
            this.yValueBox.Size = new System.Drawing.Size(52, 20);
            this.yValueBox.TabIndex = 4;
            this.yValueBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.yValueBox.ValueChanged += new System.EventHandler(this.ValueChange);
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(36, 103);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(20, 13);
            this.xLabel.TabIndex = 5;
            this.xLabel.Text = "X :";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(131, 103);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(20, 13);
            this.yLabel.TabIndex = 6;
            this.yLabel.Text = "Y :";
            // 
            // EditorMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 286);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.yValueBox);
            this.Controls.Add(this.xValueBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.createButton);
            this.Name = "EditorMenu";
            this.Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)(this.xValueBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yValueBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.NumericUpDown xValueBox;
        private System.Windows.Forms.NumericUpDown yValueBox;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
    }
}