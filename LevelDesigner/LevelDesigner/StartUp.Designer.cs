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
            this.createButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.yValue = new System.Windows.Forms.NumericUpDown();
            this.xValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.yValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xValue)).BeginInit();
            this.SuspendLayout();
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(15, 49);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(257, 37);
            this.createButton.TabIndex = 3;
            this.createButton.Text = "Create Map";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(15, 108);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(257, 37);
            this.loadButton.TabIndex = 7;
            this.loadButton.Text = "Load Map";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // yValue
            // 
            this.yValue.Location = new System.Drawing.Point(189, 7);
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
            5,
            0,
            0,
            0});
            // 
            // xValue
            // 
            this.xValue.Location = new System.Drawing.Point(78, 7);
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
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Map Size : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(167, 9);
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
            this.ClientSize = new System.Drawing.Size(284, 165);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.xValue);
            this.Controls.Add(this.yValue);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.createButton);
            this.Name = "StartUp";
            this.Text = "StartUp";
            ((System.ComponentModel.ISupportInitialize)(this.yValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.NumericUpDown yValue;
        private System.Windows.Forms.NumericUpDown xValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}