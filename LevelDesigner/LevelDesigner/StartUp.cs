using LevelDesigner.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LevelDesigner
{
    public partial class StartUp : Form
    {
        // Fields
        public static List<byte> Data;
        public static byte[] MapSize;

        // Constructor
        public StartUp()
        {
            InitializeComponent();
        }

        // Methods
        /// <summary>
        /// Calls different methods depending on which <see cref="Button"/> is clicked.
        /// </summary>
        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Name == "createButton")
                {
                    // Create a new map for the editor
                    MapSize = new byte[2] { (byte)xValue.Value, (byte)yValue.Value };

                    Hide();

                    StartDesigner();
                }
                else
                {
                    // Initialize the map from file
                    Data = FileManager.Load();

                    if (Data.Count > 0)
                    {
                        Hide();

                        StartDesigner();
                    }
                }
            }
        }

        /// <summary>
        /// Starts up the designer with the passed in values
        /// </summary>
        private void StartDesigner()
        {
            using (Designer designer = new Designer((int)heightValue.Value, (int)widthValue.Value, this))
                designer.Run();
        }
    }
}

// -- Genoah Martinelli