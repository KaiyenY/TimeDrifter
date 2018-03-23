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
using _2dracer.MapElements;

namespace _2dracer
{
    public partial class EditorMenu : Form
    {
        // Fields
        private Editor editor;
        private Stream stream;

        // Properties

        // Constructors
        public EditorMenu()
        {
            Application.EnableVisualStyles();
            InitializeComponent();

            stream = null;
        }

        // Methods
        /// <summary>
        /// Processes what happens on button clicks
        /// </summary>
        public void ButtonClick(Object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Name == "createButton")
            {
                // Set map size
                byte[] mapSize = { (byte)xValueBox.Value, (byte)yValueBox.Value };

                // Push the map into the editor
                editor = new Editor(new Map(mapSize));

                editor.Show();
                Hide();
            }
            else if (button.Name == "loadButton")
            {
                // Open file dialog
                openDialog.ShowDialog();
                
                // Open map file, push into editor
                if ((stream = openDialog.OpenFile()) != null)       // Array out of bounds error
                {
                    editor = new Editor(new Map(stream));
                    editor.Show();
                    Hide();
                }
            }
            else
            {
                Hide();
            }
        }

        /// <summary>
        /// Processes what happens when the value changes in the box
        /// </summary>
        public void ValueChange(Object sender, EventArgs e)
        {
            NumericUpDown value = (NumericUpDown)sender;

            if (value.Value < 1)
            {
                value.Value = 1;
            }
            else if (value.Value > 100)
            {
                value.Value = 100;
            }
        }
    }
}
