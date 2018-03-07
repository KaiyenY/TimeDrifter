using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2dracer.MapElements
{
    /// <summary>
    /// Contains all information for the map
    /// </summary>
    public class Map
    {
        // Fields
        private Rectangle rect;                             // The map's rectangle
        private Tile[,] tiles;                              // 2D array of tiles

        // Properties

        // Constructor
        public Map()
        {
            tiles = new Tile[5, 5];
            rect = SetRect();

            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    tiles[x, y] = new Tile(Game1.tilespritesheet, new int[2] { x, y }, 6);
                }
            }
        }

        // Methods
        public void Update()
        {
            foreach (Tile tile in tiles)
            {
                tile.Update();
            }
        }
        public void Draw()
        {
            foreach (Tile tile in tiles)
            {
                tile.Draw();
            }
        }

        /// <summary>
        /// Creates the map's rectangle
        /// </summary>
        private Rectangle SetRect()
        {
            Point position = new Point(0, 0);
            Point size = new Point(tiles.GetLength(0) * 768, tiles.GetLength(1) * 768);
            return new Rectangle(position, size);
        }
    }
}
