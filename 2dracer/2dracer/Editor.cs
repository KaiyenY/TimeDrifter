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
using Microsoft.Xna.Framework;

namespace _2dracer
{
    /// <summary>
    /// External tool to help with building the map
    /// </summary>
    public partial class Editor : Form
    {
        // Fields
        private ImageList images;           // Holds a list of textures
        private Map map;                    // Holds the map in the editor
        private PictureBox[,] eTiles;       // List of PictureBoxes representing tiles
        private PictureBox selectedTile;    // The last clicked tile
        private StreamWriter sw;            // Writes information to Map.txt
        private string[] imagePaths;        // Paths of all images

        // Properties

        // Constructor
        public Editor()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            
            imagePaths = Directory.GetFiles(@"..\..\..\..\Content\Textures\Tiles");         // Grabs the paths of tile textures

            TextureSetUp();                 // Organize textures into list

            map = new Map();                // Load default map when starting editor

            EditorSetUp();                  // Setup all elements in the editor as need be
        }

        // Methods
        /// <summary>
        /// Sets up the editor tiles into a map
        /// </summary>
        private void EditorSetUp()
        {
            // Set the initial values for the size values
            xValueBox.Value = map.Tiles.GetLength(0);
            yValueBox.Value = map.Tiles.GetLength(1);

            // Set up the texture list with images
            textureBox.SmallImageList = images;

            // Populate the list
            for (int i = 0; i < 6; i++)
            {
                textureBox.Items.Add("", i);
            }

            ETileSetUp();
        }

        /// <summary>
        /// Organizes textures into an image list
        /// </summary>
        private void TextureSetUp()
        {
            images = new ImageList()
            {
                ImageSize = new Size((int)(textureBox.Width / 1.25f), (int)(textureBox.Width / 1.25f))
            };

            try
            {
                foreach (string path in imagePaths)
                {
                    images.Images.Add(Image.FromFile(path));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Sets up all editor tiles
        /// </summary>
        private void ETileSetUp()
        {
            eTiles = new PictureBox[map.Tiles.GetLength(0), map.Tiles.GetLength(1)];
            for (int y = 0; y < eTiles.GetLength(1); y++)
            {
                for (int x = 0; x < eTiles.GetLength(0); x++)
                {
                    // Create a PictureBox to represent a tile in the editor
                    eTiles[x, y] = new PictureBox()
                    {
                        Image = Image.FromFile(imagePaths[(int)map.Tiles[x, y].Type]),
                        Location = new System.Drawing.Point(384 * x, 384 * y),
                        Tag = $"{x},{y}",
                        Size = new Size(384, 384)
                    };

                    // Get the corresponding map tile
                    Tile mTile = GetTile(eTiles[x, y].Tag.ToString());

                    // Set the rotation of the editor tile
                    eTiles[x, y].Image = RotateImage(eTiles[x, y].Image, (float)Math.Round(mTile.Rotation * (180 / Math.PI)));

                    // Add the editor tile to control group
                    tilePanel.Controls.Add(eTiles[x, y]);

                    // Subscribe click event to TileClick
                    eTiles[x, y].Click += TileClick;
                    eTiles[x, y].MouseEnter += EnterControl;
                    eTiles[x, y].MouseLeave += ExitControl;
                }
            }
        }

        /// <summary>
        /// Rotates image
        /// </summary>
        private Image RotateImage(Image image, float rotation)
        {
            if (Math.Round(rotation) % 90 == 0)
            {
                if (rotation % 360 == 0)
                {
                    image.RotateFlip(RotateFlipType.RotateNoneFlipNone);            // No transformation
                }
                else if (rotation % 360 == 90 || rotation % 360 == -270)
                {
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);              // Clockwise rotation
                }
                else if (rotation % 360 == 180 || rotation % 360 == -180)
                {
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);             // 180 degree rotation
                }
                else if (rotation % 360 == 270 || rotation % 360 == -90)
                {
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);             // CounterClockwise rotation
                }
                else
                {
                    MessageBox.Show("Error in RotateImage : being checked is " + (rotation % 360));         // Something broke
                }
            }
            else
            {
                MessageBox.Show("Error : Rotation not multiple of 90. RotateImage failed");
            }
            

            return image;
        }

        /// <summary>
        /// Return the map tile the editor tile is refering to
        /// </summary>
        private Tile GetTile(string eTileTag)
        {
            string[] eTileInfo = eTileTag.Split(',');

            int[] index = { int.Parse(eTileInfo[0]), int.Parse(eTileInfo[1]) };

            return map.Tiles[index[0], index[1]];
        }
        
        /// <summary>
        /// Determines what happens when a button is clicked
        /// </summary>
        private void ButtonClick(Object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                Tile mTile = null;

                if (selectedTile != null)
                {
                    mTile = GetTile(selectedTile.Tag.ToString());
                }

                switch (button.Name)
                {
                    case "saveButton":
                        SaveMap();
                        break;

                    case "cwButton":
                        if (mTile != null)
                        {
                            mTile.Rotation -= MathHelper.PiOver2;
                            selectedTile.Image = RotateImage(selectedTile.Image, -90);     // rotating 90 not 90 + rotation
                        }

                        // Update Tile info
                        UpdateTileInfo();
                        break;

                    case "ccwButton":
                        if (mTile != null)
                        {
                            mTile.Rotation += MathHelper.PiOver2;
                            selectedTile.Image = RotateImage(selectedTile.Image, 90);     // rotating 90 not 90 + rotation
                        }

                        // Update Tile info
                        UpdateTileInfo();
                        break;

                    default:
                        int[] size = { (int)xValueBox.Value, (int)yValueBox.Value };
                        map = new Map(size);
                        tilePanel.Controls.Clear();
                        ETileSetUp();

                        // Update Tile info
                        UpdateTileInfo();
                        break;
                }
            }
            else
            {

            }
        }
        
        /// <summary>
        /// Determines what happens when a tile is clicked
        /// </summary>
        private void TileClick(Object sender, EventArgs e)
        {
            if (sender is PictureBox pic && imagePaths != null && textureBox.SelectedItems.Count > 0)
            {
                // Change the currently selected tile
                selectedTile = pic;

                // Change the image to whichever selected texture
                pic.Image = Image.FromFile(imagePaths[textureBox.SelectedItems[0].ImageIndex]);

                // Grab the associtated map tile using the editor tile tag
                Tile mTile = GetTile(pic.Tag.ToString());

                // Change the type of the tile
                mTile.Type = (TileType)textureBox.SelectedItems[0].ImageIndex;

                // Reset the rotation of the tile
                mTile.Rotation = 0;

                // Update Tile info
                UpdateTileInfo();
            }
            else if (sender is PictureBox pict)
            {
                selectedTile = pict;

                // Update Tile info
                UpdateTileInfo();
            }
            else
            {
                Console.WriteLine("Error : Something happened in TileClick");
            }
        }

        /// <summary>
        /// Controls what happens when mouse enters control
        /// </summary>
        private void EnterControl(Object sender, EventArgs e)
        {
            if (sender is PictureBox pic && textureBox.SelectedItems.Count > 0)
            {
                pic.Image = Image.FromFile(imagePaths[textureBox.SelectedItems[0].ImageIndex]);
            }
        }

        /// <summary>
        /// Controls what happens when mouse exits control
        /// </summary>
        private void ExitControl(Object sender, EventArgs e)
        {
            if (sender is PictureBox pic)
            {
                Tile mTile = GetTile(pic.Tag.ToString());

                pic.Image = Image.FromFile(imagePaths[(int)mTile.Type]);

                pic.Image = RotateImage(pic.Image, MathHelper.ToDegrees(mTile.Rotation));
            }
        }
        
        /// <summary>
        /// Saves the current configuration to file
        /// </summary>
        private void SaveMap()
        {
            try
            {
                sw = new StreamWriter(@"..\..\..\..\Content\Map.txt");

                sw.WriteLine($"{map.Tiles.GetLength(0)},{map.Tiles.GetLength(1)}");       // Write map array size

                for (int y = 0; y < map.Tiles.GetLength(1); y++)
                {
                    for (int x = 0; x < map.Tiles.GetLength(0); x++)
                    {
                        Tile mTile = GetTile(eTiles[x, y].Tag.ToString());

                        sw.WriteLine($"{(int)mTile.Type},{MathHelper.ToDegrees(mTile.Rotation)}");                // Name = TileType, Tag = Rotation (in deg)
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// Updates selected tile information
        /// </summary>
        private void UpdateTileInfo()
        {
            if (selectedTile != null)
            {
                string[] eTileInfo = selectedTile.Tag.ToString().Split(',');

                int[] index = { int.Parse(eTileInfo[0]), int.Parse(eTileInfo[1]) };

                indexValueLabel.Text = $"({index[0]}, {index[1]})";
                rotationValueLabel.Text = $"{MathHelper.ToDegrees(GetTile(selectedTile.Tag.ToString()).Rotation)}";
            }
        }

        /// <summary>
        /// Chagnes the GameState back to Menu
        /// </summary>
        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game1.GameState = GameState.Menu;
        }
    }
}

// Genoah