using LevelDesigner.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LevelDesigner.MapElements
{
    /// <summary>
    /// Holds and controls all tiles
    /// </summary>
    public static class Map
    {
        #region Fields
        /// <summary>
        /// Defines the <see cref="Rectangle"/> the <see cref="Map"/> is contained in.
        /// </summary>
        public static Rectangle Rect;

        /// <summary>
        /// A 2D-array of tiles contained by this <see cref="Map"/>.
        /// </summary>
        public static Tile[,] Tiles;
        #endregion

        // Properties

        // Constructor
        static Map()
        {
            if (StartUp.Data != null)
            {
                // Grab data from the StartUp menu
                Queue<string> data = StartUp.Data;

                // Initialize Tiles size
                string[] input = data.Dequeue().Split(',');
                Tiles = new Tile[int.Parse(input[0]), int.Parse(input[1])];

                // Set up the map rectangle
                Rect = new Rectangle(
                    Point.Zero,
                    new Point(Tiles.GetLength(0) * 384, Tiles.GetLength(1) * 384));

                // Create tiles from file
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    for (int x = 0; x < Tiles.GetLength(0); x++)
                    {
                        // Split up the tile data
                        input = data.Dequeue().Split(',');

                        // Set up tile
                        Tile current = Tiles[x, y] = new Tile(
                            new Point(x * 384, y * 384),
                            Designer.TileSprites[int.Parse(input[0])],
                            (TileType)int.Parse(input[0]),
                            int.Parse(input[4]),
                            int.Parse(input[1]),
                            int.Parse(input[2]),
                            int.Parse(input[3]));

                        // Throw away neighboring indices, don't need them for the editor
                        for (int i = 0; i < current.NeighborIndices.Capacity; i++)
                        {
                            data.Dequeue();
                        }
                    }
                }

                foreach (Tile tile in Tiles)
                {
                    tile.GrabNeighbors();
                }
            }
            else
            {
                // Grab MapSize from the StartUp menu
                byte[] size = StartUp.MapSize;

                // Initialize Tiles size
                Tiles = new Tile[size[0], size[1]];

                // Set up map rect
                Rect = new Rectangle(
                    Point.Zero,
                    new Point(size[0] * 384, size[1] * 384));

                // Create default tiles
                for (int y = 0; y < size[1]; y++)
                {
                    for (int x = 0; x < size[0]; x++)
                    {
                        Tiles[x, y] = new Tile(
                            new Point(x * 384, y * 384),
                            Designer.TileSprites[5],
                            TileType.Grass,
                            0,
                            0,
                            x,
                            y);
                    }
                }

                foreach (Tile tile in Tiles)
                {
                    tile.GrabNeighbors();
                }
            }
        }

        // Methods
        /// <summary>
        /// Updates the <see cref="Map"/>.
        /// </summary>
        public static void Update()
        {
            foreach (Tile tile in Tiles)
            {
                tile.Update();
            }
        }

        /// <summary>
        /// Draws the <see cref="Map"/>.
        /// </summary>
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}

// -- Genoah Martinelli