namespace _2dracer
{
    partial class Editor
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
            this.xValueBox = new System.Windows.Forms.NumericUpDown();
            this.yValueBox = new System.Windows.Forms.NumericUpDown();
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.tilePanel = new System.Windows.Forms.Panel();
            this.textureBox = new System.Windows.Forms.ListView();
            this.textureTab = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.detailsTab = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textureBoxLabel = new System.Windows.Forms.Label();
            this.sizeButton = new System.Windows.Forms.Button();
            this.cwButton = new System.Windows.Forms.Button();
            this.ccwButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.indexValueLabel = new System.Windows.Forms.Label();
            this.rotationValueLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.xValueBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yValueBox)).BeginInit();
            this.SuspendLayout();
            // 
            // xValueBox
            // 
            this.xValueBox.BackColor = System.Drawing.SystemColors.Control;
            this.xValueBox.Location = new System.Drawing.Point(1223, 12);
            this.xValueBox.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.xValueBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xValueBox.Name = "xValueBox";
            this.xValueBox.Size = new System.Drawing.Size(152, 20);
            this.xValueBox.TabIndex = 1;
            this.xValueBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // yValueBox
            // 
            this.yValueBox.BackColor = System.Drawing.SystemColors.Control;
            this.yValueBox.Location = new System.Drawing.Point(1223, 55);
            this.yValueBox.Name = "yValueBox";
            this.yValueBox.Size = new System.Drawing.Size(152, 20);
            this.yValueBox.TabIndex = 2;
            this.yValueBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.xLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.xLabel.Location = new System.Drawing.Point(1197, 14);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(20, 13);
            this.xLabel.TabIndex = 3;
            this.xLabel.Text = "X :";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.yLabel.Location = new System.Drawing.Point(1197, 57);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(20, 13);
            this.yLabel.TabIndex = 4;
            this.yLabel.Text = "Y :";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1195, 691);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(180, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // tilePanel
            // 
            this.tilePanel.AutoScroll = true;
            this.tilePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tilePanel.Location = new System.Drawing.Point(12, 12);
            this.tilePanel.Name = "tilePanel";
            this.tilePanel.Size = new System.Drawing.Size(1166, 702);
            this.tilePanel.TabIndex = 7;
            // 
            // textureBox
            // 
            this.textureBox.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.textureBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.textureBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.textureTab,
            this.detailsTab});
            this.textureBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textureBox.Location = new System.Drawing.Point(1195, 241);
            this.textureBox.MultiSelect = false;
            this.textureBox.Name = "textureBox";
            this.textureBox.Size = new System.Drawing.Size(180, 415);
            this.textureBox.TabIndex = 8;
            this.textureBox.UseCompatibleStateImageBehavior = false;
            this.textureBox.View = System.Windows.Forms.View.SmallIcon;
            // 
            // textureTab
            // 
            this.textureTab.Text = "Textures";
            this.textureTab.Width = 90;
            // 
            // detailsTab
            // 
            this.detailsTab.Text = "Details";
            this.detailsTab.Width = 90;
            // 
            // textureBoxLabel
            // 
            this.textureBoxLabel.AutoSize = true;
            this.textureBoxLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.textureBoxLabel.Location = new System.Drawing.Point(1259, 225);
            this.textureBoxLabel.Name = "textureBoxLabel";
            this.textureBoxLabel.Size = new System.Drawing.Size(48, 13);
            this.textureBoxLabel.TabIndex = 11;
            this.textureBoxLabel.Text = "Textures";
            // 
            // sizeButton
            // 
            this.sizeButton.Location = new System.Drawing.Point(1249, 81);
            this.sizeButton.Name = "sizeButton";
            this.sizeButton.Size = new System.Drawing.Size(82, 23);
            this.sizeButton.TabIndex = 12;
            this.sizeButton.Text = "Change Size";
            this.sizeButton.UseVisualStyleBackColor = true;
            this.sizeButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // cwButton
            // 
            this.cwButton.Location = new System.Drawing.Point(1195, 662);
            this.cwButton.Name = "cwButton";
            this.cwButton.Size = new System.Drawing.Size(75, 23);
            this.cwButton.TabIndex = 13;
            this.cwButton.Text = "Rotate Left";
            this.cwButton.UseVisualStyleBackColor = true;
            this.cwButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // ccwButton
            // 
            this.ccwButton.Location = new System.Drawing.Point(1300, 662);
            this.ccwButton.Name = "ccwButton";
            this.ccwButton.Size = new System.Drawing.Size(75, 23);
            this.ccwButton.TabIndex = 14;
            this.ccwButton.Text = "Rotate Right";
            this.ccwButton.UseVisualStyleBackColor = true;
            this.ccwButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(1192, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Selected Tile Info";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(1192, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Index :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(1192, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Rotation :";
            // 
            // indexValueLabel
            // 
            this.indexValueLabel.AutoSize = true;
            this.indexValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.indexValueLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.indexValueLabel.Location = new System.Drawing.Point(1251, 144);
            this.indexValueLabel.Name = "indexValueLabel";
            this.indexValueLabel.Size = new System.Drawing.Size(103, 18);
            this.indexValueLabel.TabIndex = 18;
            this.indexValueLabel.Text = "Select a tile. . .";
            // 
            // rotationValueLabel
            // 
            this.rotationValueLabel.AutoSize = true;
            this.rotationValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rotationValueLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.rotationValueLabel.Location = new System.Drawing.Point(1251, 175);
            this.rotationValueLabel.Name = "rotationValueLabel";
            this.rotationValueLabel.Size = new System.Drawing.Size(103, 18);
            this.rotationValueLabel.TabIndex = 19;
            this.rotationValueLabel.Text = "Select a tile. . .";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1387, 726);
            this.Controls.Add(this.rotationValueLabel);
            this.Controls.Add(this.indexValueLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ccwButton);
            this.Controls.Add(this.cwButton);
            this.Controls.Add(this.sizeButton);
            this.Controls.Add(this.textureBoxLabel);
            this.Controls.Add(this.textureBox);
            this.Controls.Add(this.tilePanel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.yValueBox);
            this.Controls.Add(this.xValueBox);
            this.Name = "Editor";
            this.Text = "Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.xValueBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yValueBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown xValueBox;
        private System.Windows.Forms.NumericUpDown yValueBox;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel tilePanel;
        private System.Windows.Forms.ListView textureBox;
        private System.Windows.Forms.ColumnHeader textureTab;
        private System.Windows.Forms.ColumnHeader detailsTab;
        private System.Windows.Forms.Label textureBoxLabel;
        private System.Windows.Forms.Button sizeButton;
        private System.Windows.Forms.Button cwButton;
        private System.Windows.Forms.Button ccwButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label indexValueLabel;
        private System.Windows.Forms.Label rotationValueLabel;
    }
}