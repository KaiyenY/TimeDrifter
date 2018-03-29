using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2dracer.MapElements
{
    public class Map
    {
        // Fields
        private StreamReader sr;                // Reads the map files

        // Properties
        public List<Node> Nodes { get; }
        public Tile[,] Tiles { get; }
        public Vector2 Size { get; }

        // Constructors
        /// <summary>
        /// Generates a map from file
        /// </summary>
        public Map()
        {
            int[] mapSize = new int[2];
            int[] tileInfo;
            
            try
            {
                sr = new StreamReader(@"..\..\..\..\Content\Map.txt");
                
                string[] mapInfo = sr.ReadLine().Split(',');        // Split info from file into two
                mapSize[0] = int.Parse(mapInfo[0]);                 // Set map horizontal size
                mapSize[1] = int.Parse(mapInfo[1]);                 // SEt map vertical size

                tileInfo = new int[mapSize[0] * mapSize[1] * 2];    // Sets up array to hold tile information

                // Read all information
                for (int i = 0; i < tileInfo.Length; i += 2)
                {
                    string[] info = sr.ReadLine().Split(',');       // Split info from file

                    tileInfo[i] = int.Parse(info[0]);               // Tile type
                    tileInfo[i + 1] = int.Parse(info[1]);           // Tile rotation
                }
                
                Tiles = new Tile[mapSize[0], mapSize[1]];           // Set up tile array
                Nodes = new List<Node>();                           // Set up nodes list

                // Create all tiles; set up nodes and collision stuff as need be
                int j = 0;
                for (int y = 0; y < mapSize[1]; y++)
                {
                    for (int x = 0; x < mapSize[0]; x++)
                    {
                        Vector2 tilePos = new Vector2(x * 768, y * 768);

                        Tiles[x, y] = new Tile(
                            tilePos, 
                            (TileType)tileInfo[j],
                            MathHelper.ToRadians(tileInfo[j + 1]));
                        
                        if ((TileType)tileInfo[j] != TileType.Building)
                        {
                            // Not a building, has a node
                            Nodes.Add(new Node(new Point(x * 768 + 384, y * 768 + 384)));
                        }
                        /*
                        else
                        {
                            // Something about collisions
                        }
                        */

                        j += 2;
                    }
                }

                Size = new Vector2(768 * Tiles.GetLength(0), 768 * Tiles.GetLength(1));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// Generates a map in the editor
        /// </summary>
        /// <param name="size">Size of tile array</param>
        public Map(int[] size)
        {
            Tiles = new Tile[size[0], size[1]];

            for (int y = 0; y < size[1]; y++)
            {
                for (int x = 0; x < size[0]; x++)
                {
                    Vector2 tilePos = new Vector2(768 * x, 768 * y);

                    Tiles[x, y] = new Tile(tilePos, TileType.Grass, 0);
                }
            }
        }

        // Methods
        public void Draw()
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw();
            }
        }
    }
}

// Genoah