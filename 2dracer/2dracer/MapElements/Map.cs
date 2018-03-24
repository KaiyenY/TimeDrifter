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
        private List<Node> nodes;               // Holds all the nodes for the map
        private Tile[,] tiles;                  // Holds all the tiles for the map
        private Vector2 position;               // Holds the current position of the map

        // Properties
        public Tile[,] Tiles { get { return tiles; } }
        public Vector2 Pos { get { return position; } }

        // Constructors
        /// <summary>
        /// Generates a map from a file
        /// </summary>
        public Map()
        {
            int[] mapSize = new int[2];
            int[] tileInfo;
            
            try
            {
                sr = new StreamReader(@"..\..\..\..\Content\Maps\Main.txt");
                
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

                // Set the starting position of the map
                position = new Vector2(Game1.screenWidth - (mapSize[0] * 768), Game1.screenHeight - (mapSize[1] * 768));
                
                tiles = new Tile[mapSize[0], mapSize[1]];           // Set up tile array
                nodes = new List<Node>();                           // Set up nodes list

                // Create all tiles; set up nodes and collision stuff as need be
                int j = 0;
                for (int y = 0; y < mapSize[1]; y++)
                {
                    for (int x = 0; x < mapSize[0]; x++)
                    {
                        Vector2 tilePos = new Vector2(x * 768, y * 768);

                        tiles[x, y] = new Tile(
                            tilePos, 
                            (TileType)tileInfo[j],
                            MathHelper.ToRadians(tileInfo[j + 1]));
                        /*
                        if ((TileType)tileInfo[j] != TileType.Building)
                        {
                            // Not a building, has a node
                            nodes.Add(new Node());
                        }
                        else
                        {
                            // Something about collisions
                        }
                        */

                        j += 2;
                    }
                }
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

        // Methods
        public void Draw()
        {
            foreach (Tile tile in tiles)
            {
                tile.Draw();
            }
        }
    }
}

// Genoah