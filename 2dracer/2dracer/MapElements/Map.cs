using System;
using System.IO;

using Microsoft.Xna.Framework;

namespace _2dracer.MapElements
{
    /// <summary>
    /// Contains all information for the map
    /// </summary>
    public class Map
    {
        // Fields
        private BinaryReader br;            // Reads binary stuff
        private BinaryWriter bw;            // Writes binary stuff
        private Rectangle rect;             // The map's rectangle
        private Tile[,] tiles;              // 2D array of tiles

        // Properties

        // Constructor
        /// <summary>
        /// Creates a default map
        /// </summary>
        public Map(byte[] mapSize)
        {
            tiles = new Tile[mapSize[0], mapSize[1]];

            rect = SetRect();

            // Read information about each tile
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    tiles[x, y] = new Tile(Game1.tilespritesheet, new int[2] { x, y }, 6);
                }
            }
        }

        /// <summary>
        /// Creates a map from a file
        /// </summary>
        public Map(Stream stream)
        {
            // Load map
            Load((FileStream)stream);
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

        /// <summary>
        /// Loads a map from a file
        /// </summary>
        private void Load(FileStream stream)
        {
            try
            {
                br = new BinaryReader(stream);

                // Read map size, set it to the tile array size
                byte[] mapSize = br.ReadBytes(2);

                tiles = new Tile[mapSize[0], mapSize[1]];

                rect = SetRect();

                // Read information about each tile
                for (int y = 0; y < mapSize[1]; y++)
                {
                    for (int x = 0; x < mapSize[0]; x++)
                    {
                        tiles[x, y] = new Tile(Game1.tilespritesheet, new int[2] { x, y }, 6);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in loading file : " + e.Message);
            }
            finally
            {
                br.Close();
            }
        }

        /// <summary>
        /// Saves a map to a file
        /// </summary>
        public void Save(FileStream stream)
        {
            try
            {
                bw = new BinaryWriter(stream);

                // Write map size to file
                byte[] mapSize = { (byte)tiles.GetLength(0), (byte)tiles.GetLength(1) };
                bw.Write(mapSize);

                // Write information from each tile into the file
                foreach (Tile tile in tiles)
                {
                    byte tileType = (byte)tile.Type;                                // Store the type of tile
                    byte[] index = { (byte)tile.Index[0], (byte)tile.Index[1] };    // Store the location of the tile

                    bw.Write(tileType);
                    bw.Write(index);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while saving file : " + e.Message);
            }
            finally
            {
                bw.Close();
            }
        }
    }
}
