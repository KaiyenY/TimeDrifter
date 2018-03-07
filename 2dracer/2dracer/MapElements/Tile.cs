using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2dracer.MapElements
{
    public enum TileType
    {
        Straight,
        Corner,
        ThreeWay,
        FourWay,
        Building,
        Grass
    }

    /// <summary>
    /// Holds information for each tile drawn in the map
    /// </summary>
    public class Tile
    {
        // Fields
        private Node node;                          // The node associated with this tile
        private Point size;                         // The size of a tile drawn to the screen
        private Rectangle rect;                     // The position and size of the tile
        private Rectangle sourceRect;               // Which sprite in the sheet the tile needs
        private Texture2D spritesheet;              // Spritesheet of tile textures
        private TileType type;                      // Type of tile
        private bool isEnabled;                     // Determines if tile draws to screen / updates
        private int[] index;                        // Index of this tile in array
        
        // Properties
        public Node Node { get { return node; } }

        // Constructor
        public Tile(Texture2D spritesheet, int[] index, int type)
        {
            size = new Point(768);
            isEnabled = false;
            this.index = index;
            this.type = (TileType)type;
            this.spritesheet = spritesheet;
            sourceRect = SetSource();
            rect = SetRect();
        }

        // Methods
        public void Update()
        {
            isEnabled = OnScreen();

            Scroll();
        }
        public void Draw()
        {
            if (isEnabled)
            {
                Game1.spriteBatch.Draw(
                    spritesheet,
                    rect,
                    sourceRect,
                    Color.White);
            }
        }

        /// <summary>
        /// Determines the sprite for this tile
        /// </summary>
        private Rectangle SetSource()
        {
            Point sourceSize = new Point(384);
            Point location = new Point(0, 0);

            switch (type)
            {
                case TileType.Building:
                    location = new Point(1, 1);
                    return new Rectangle(location, sourceSize);

                case TileType.Corner:
                    location = new Point(1, 385);
                    return new Rectangle(location, sourceSize);

                case TileType.FourWay:
                    location = new Point(769, 385);
                    return new Rectangle(location, sourceSize);

                case TileType.Grass:
                    location = new Point(385, 1);
                    return new Rectangle(location, sourceSize);

                case TileType.Straight:
                    location = new Point(769, 1);
                    return new Rectangle(location, sourceSize);

                case TileType.ThreeWay:
                    location = new Point(385, 385);
                    return new Rectangle(location, sourceSize);

                default:
                    return new Rectangle(location, sourceSize);
            }
        }
        /// <summary>
        /// Sets up the size and position of the tile
        /// </summary>
        private Rectangle SetRect()
        {
            // Calculate the position
            Point position = new Point(index[0] * size.X, index[1] * size.Y);
            
            return new Rectangle(position, size);
        }
        /// <summary>
        /// Determines if the tile is on the screen
        /// </summary>
        private bool OnScreen()
        {
            return rect.X > -rect.Width ||
                rect.X < Game1.screenWidth ||
                rect.Y > -rect.Height ||
                rect.Y < Game1.screenHeight;
        }
        /// <summary>
        /// Scrolls the tiles so you can view more of the map
        /// </summary>
        private void Scroll()
        {
            if (Input.KeyHold(Keys.Up))
            {
                rect.Y += 5;
            }
            if (Input.KeyHold(Keys.Down))
            {
                rect.Y -= 5;
            }
            if (Input.KeyHold(Keys.Right))
            {
                rect.X -= 5;
            }
            if (Input.KeyHold(Keys.Left))
            {
                rect.X += 5;
            }
        }
    }
}
