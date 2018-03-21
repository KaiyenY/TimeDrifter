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
            this.tileDropDown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tileDropDown
            // 
            this.tileDropDown.FormattingEnabled = true;
            this.tileDropDown.Location = new System.Drawing.Point(12, 12);
            this.tileDropDown.Name = "tileDropDown";
            this.tileDropDown.Size = new System.Drawing.Size(213, 21);
            this.tileDropDown.TabIndex = 0;
            this.tileDropDown.Text = "Please select a tile . . .";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 555);
            this.Controls.Add(this.tileDropDown);
            this.Name = "Editor";
            this.Text = "Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox tileDropDown;
    }
}