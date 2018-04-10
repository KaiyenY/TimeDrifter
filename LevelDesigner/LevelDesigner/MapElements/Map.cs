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
                List<byte> data = StartUp.Data;

                // Initialize Tiles size
                Tiles = new Tile[data[0], data[1]];

                // Remove MapSize in data
                data.RemoveRange(0, 2);

                // Create tiles from file
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    for (int x = 0; x < Tiles.GetLength(0); x++)
                    {
                        // Set up tile
                        Tiles[x, y] = new Tile(
                            new Point(x * 768, y * 768),
                            Designer.TileSprites[data[0]],
                            (TileType)data[0],
                            data[1],
                            x,
                            y);

                        // Determine the neighbors
                        if (data[2] == 1)
                        {
                            Tiles[x, y].HasNeighbor[0] = true;
                        }
                        if (data[3] == 1)
                        {
                            Tiles[x, y].HasNeighbor[1] = true;
                        }
                        if (data[4] == 1)
                        {
                            Tiles[x, y].HasNeighbor[2] = true;
                        }
                        if (data[5] == 1)
                        {
                            Tiles[x, y].HasNeighbor[3] = true;
                        }

                        // Remove data already read
                        data.RemoveRange(0, 6);
                    }
                }

                // Get neighbor indices
                foreach (Tile tile in Tiles)
                {
                    if (tile.HasNeighbor[0])
                    {
                        // Tile above
                        tile.NeighborIndices[0, 0] = Tiles[tile.Index[0], tile.Index[1] + 1].Index[0];
                        tile.NeighborIndices[0, 1] = Tiles[tile.Index[0], tile.Index[1] + 1].Index[1];
                    }
                    if (tile.HasNeighbor[1])
                    {
                        // Tile below
                        tile.NeighborIndices[1, 0] = Tiles[tile.Index[0], tile.Index[1] - 1].Index[0];
                        tile.NeighborIndices[1, 1] = Tiles[tile.Index[0], tile.Index[1] - 1].Index[1];
                    }
                    if (tile.HasNeighbor[2])
                    {
                        // Tile to right
                        tile.NeighborIndices[2, 0] = Tiles[tile.Index[0], tile.Index[1]].Index[0];
                        tile.NeighborIndices[2, 1] = Tiles[tile.Index[0], tile.Index[1]].Index[1];
                    }
                    if (tile.HasNeighbor[3])
                    {
                        // Tile to left
                        tile.NeighborIndices[3, 0] = Tiles[tile.Index[0], tile.Index[1]].Index[0];
                        tile.NeighborIndices[3, 1] = Tiles[tile.Index[0], tile.Index[1]].Index[1];
                    }
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
                    new Point(size[0] * 768, size[1] * 768));

                // Create default tiles
                for (int y = 0; y < size[1]; y++)
                {
                    for (int x = 0; x < size[0]; x++)
                    {
                        Tiles[x, y] = new Tile(
                            new Point(x * 768, y * 768),
                            Designer.TileSprites[5],
                            TileType.Grass,
                            0,
                            x,
                            y);
                    }
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