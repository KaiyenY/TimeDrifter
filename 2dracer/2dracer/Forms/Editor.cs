using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _2dracer.MapElements;

namespace _2dracer
{
    /// <summary>
    /// Edits map
    /// </summary>
    public partial class Editor : Form
    {
        // Fields
        Map map;
        
        // Properties

        // Constructor
        public Editor(Map map)
        {
            Application.EnableVisualStyles();
            InitializeComponent();

            this.map = map;
        }

        // Methods
        /// <summary>
        /// Loads the map into the editor
        /// </summary>
        public void LoadMap()
        {
            tileDropDown.Items.Add("yes");
        }
    }
}
