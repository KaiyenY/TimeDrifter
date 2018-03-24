using _2dracer.MapElements;
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

namespace _2dracer
{
    public partial class Editor : Form
    {
        // Fields
        private ImageList images;
        private Map map;                    // Holds the map in the editor
        private PictureBox[,] tiles;        // List of PictureBoxes representing tiles

        // Properties

        // Constructor
        public Editor()
        {
            Application.EnableVisualStyles();
            InitializeComponent();

            for (int i = 0; i < 6; i++)
            {
                // Load textures into listview maybe
            }

            map = new Map();                // Load default map when starting editor

            // Set up the PictureBox tiles
            tiles = new PictureBox[map.Tiles.GetLength(0), map.Tiles.GetLength(1)];
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    tiles[x, y] = new PictureBox()
                    {
                        Location = new System.Drawing.Point(),          // Determine location
                        Size = new Size()                               // Determine size
                    };
                    tiles[x, y].Show();
                }
            }

            // Set up elements of the form
            SetUp();
        }

        // Methods
        public void SetUp()
        {
            /// Setup the texture selection box
            // Instantiate image list
            images = new ImageList()
            {
                ImageSize = new Size(150, 150)
            };

            // Get array of all tile texture paths
            string[] paths = Directory.GetFiles(@"..\..\..\..\Content\Textures\Tiles");

            try
            {
                // Put each image into the imagelist
                foreach (string path in paths)
                {
                    images.Images.Add(Image.FromFile(path));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error : " + e.Message);
            }

            // Add all 
            textureBox.SmallImageList = images;
            textureBox.Items.Add("", 0);            // Building
            textureBox.Items.Add("", 1);            // Straight road
            textureBox.Items.Add("", 2);            // Corner road
            textureBox.Items.Add("", 3);            // 3-Way Intersection
            textureBox.Items.Add("", 4);            // 4-Way Intersection


            /// Set up the tile view
            // Get the amount of rows and columns, add images into the panel accordingly
        }


        public void ButtonClick(Object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Name == "loadButton")
            {

            }
            else if (button.Name == "saveButton")
            {

            }
        }

        public void PictureClick(Object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;            // Make picturebox tag = index

            
        }
    }
}

// Genoah