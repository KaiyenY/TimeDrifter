namespace LevelDesigner
{
    partial class StartUp
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
            this.label1 = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.widthValue = new System.Windows.Forms.NumericUpDown();
            this.heightValue = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.yValue = new System.Windows.Forms.NumericUpDown();
            this.xValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.widthValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xValue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Resolution :";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(15, 103);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(257, 37);
            this.createButton.TabIndex = 3;
            this.createButton.Text = "Create Map";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // widthValue
            // 
            this.widthValue.Location = new System.Drawing.Point(81, 12);
            this.widthValue.Maximum = new decimal(new int[] {
            3840,
            0,
            0,
            0});
            this.widthValue.Minimum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.widthValue.Name = "widthValue";
            this.widthValue.Size = new System.Drawing.Size(83, 20);
            this.widthValue.TabIndex = 4;
            this.widthValue.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // heightValue
            // 
            this.heightValue.Location = new System.Drawing.Point(188, 12);
            this.heightValue.Maximum = new decimal(new int[] {
            2160,
            0,
            0,
            0});
            this.heightValue.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.heightValue.Name = "heightValue";
            this.heightValue.Size = new System.Drawing.Size(84, 20);
            this.heightValue.TabIndex = 5;
            this.heightValue.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(170, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "x";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(15, 165);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(257, 37);
            this.loadButton.TabIndex = 7;
            this.loadButton.Text = "Load Map";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // yValue
            // 
            this.yValue.Location = new System.Drawing.Point(189, 58);
            this.yValue.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.yValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.yValue.Name = "yValue";
            this.yValue.Size = new System.Drawing.Size(83, 20);
            this.yValue.TabIndex = 8;
            this.yValue.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // xValue
            // 
            this.xValue.Location = new System.Drawing.Point(81, 58);
            this.xValue.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.xValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xValue.Name = "xValue";
            this.xValue.Size = new System.Drawing.Size(83, 20);
            this.xValue.TabIndex = 9;
            this.xValue.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(15, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Map Size : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(171, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "x";
            // 
            // StartUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(284, 221);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.xValue);
            this.Controls.Add(this.yValue);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.heightValue);
            this.Controls.Add(this.widthValue);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.label1);
            this.Name = "StartUp";
            this.Text = "StartUp";
            ((System.ComponentModel.ISupportInitialize)(this.widthValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.NumericUpDown widthValue;
        private System.Windows.Forms.NumericUpDown heightValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.NumericUpDown yValue;
        private System.Windows.Forms.NumericUpDown xValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}