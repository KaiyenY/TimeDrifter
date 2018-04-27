using _2dracer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace _2dracer.MapElements
{
    public static class Map
    {
        #region Fields
        private static Queue<string> tileInfo;
        private static Random rng = new Random();
        private static StreamReader sr;
        #endregion

        #region Properties
        public static List<Building> Buildings { get; private set; }
        public static Node[,] Nodes { get; private set; }
        public static Tile[,] Tiles { get; private set; }
        public static Vector2 Size { get; private set; }
        public static float TileSize { get; set; }
        #endregion

        #region Constructor
        static Map()
        {
            UpdateTileSize();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates any necessary information about the map.
        /// </summary>
        public static void Update()
        {
            foreach(Building building in Buildings)
            {
                building.Update();
            }

            foreach(Tile tile in Tiles)
            {
                tile.Update();
            }
        }

        /// <summary>
        /// Draws tiles and nodes into the game.
        /// </summary>
        public static void Draw()
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw();
            }
            foreach (Node node in Nodes)
            {
                if (node != null)
                {
                    Game1.spriteBatch.Draw(LoadManager.Sprites["Square"], new Rectangle(node.Location, new Point(25, 25)), null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                }
            }
        }

        /// <summary>
        /// Draws buildings into the game.
        /// </summary>
        public static void DrawBuildings()
        {
            for (int i = 0; i < Buildings.Count; i++)
            {
                Buildings[i].Draw(LoadManager.Sprites[$"Building{(i % 4) + 1}"]);
            }
        }

        public static void LoadMap()
        {
            int[] mapSize = new int[2];

            try
            {
                // Set up the reader
                sr = new StreamReader(@"..\..\..\..\Content\Main.txt");

                string[] mapInfo = sr.ReadLine().Split(',');        // Split info from file into two
                mapSize[0] = int.Parse(mapInfo[0]);                 // Set map horizontal size
                mapSize[1] = int.Parse(mapInfo[1]);                 // Set map vertical size
                tileInfo = new Queue<string>();                     // Stores all tile data from the file

                // Loop through all data and read it.
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    tileInfo.Enqueue(line);
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

            Tiles = new Tile[mapSize[0], mapSize[1]];           // Set up tile array
            Nodes = new Node[mapSize[0], mapSize[1]];           // Set up nodes list
            Buildings = new List<Building>();                   // Set up buildings list

            Size = new Vector2(TileSize * Tiles.GetLength(0), TileSize * Tiles.GetLength(1));

            CreateTiles();
        }

        /// <summary>
        /// Populates the tiles array.
        /// </summary>
        public static void CreateTiles()
        {
            // Create all tiles; set up nodes and collision stuff as need be
            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                for (int x = 0; x < Tiles.GetLength(0); x++)
                {
                    // Grabs the basic information to create a tile.
                    string[] info = tileInfo.Dequeue().Split(',');

                    // Create the tile.
                    Tile current = Tiles[x, y] = new Tile(
                        (TileType)int.Parse(info[0]),
                        MathHelper.ToRadians(int.Parse(info[1])),
                        int.Parse(info[2]),
                        int.Parse(info[3]));
                    
                    if (current.Type != TileType.Building)
                    {
                        // Create the node
                        Node currentNode = Nodes[x, y] = new Node(current.Position.ToPoint())
                        {
                            Index = new int[2] { x, y }
                        };

                        // The tile info is wrong, just rid of the info for now
                        for (int i = 0; i < int.Parse(info[4]); i++)
                        {
                            tileInfo.Dequeue();
                        }
                    }
                    else
                    {
                        // Add building to the list of buildings
                        Buildings.Add(new Building(current, new Vector2(x * TileSize, y * TileSize)));

                        // Make sure there is no node in this position
                        Nodes[x, y] = null;
                    }
                }
            }

            foreach (Node node in Nodes)
            {
                if (node != null)
                AssignNeighbors(node);
            }
        }

        /// <summary>
        /// Grabs all of the nodes from the map.
        /// </summary>
        public static List<Node> GetNodes()
        {
            List<Node> nodes = new List<Node>();
            foreach (Node node in Nodes)
            {
                if (node != null)
                nodes.Add(node);
            }
            return nodes;
        }

        /// <summary>
        /// Assigns neighbors to each of the nodes
        /// </summary>
        public static void AssignNeighbors(Node n)
        {
            if (n.Index[0] > 0)
            {
                // Check the left side of the node.
                if (Nodes[n.Index[0] - 1, n.Index[1]] != null)
                {
                    n.Neighbors.Add(Nodes[n.Index[0] - 1, n.Index[1]]);
                }
            }
            if (n.Index[0] < Tiles.GetLength(0) - 1)
            {
                // Check the right side of the node.
                if (Nodes[n.Index[0] + 1, n.Index[1]] != null)
                {
                    n.Neighbors.Add(Nodes[n.Index[0] + 1, n.Index[1]]);
                }
            }
            if (n.Index[1] > 0)
            {
                // Check the top of the node.
                if (Nodes[n.Index[0], n.Index[1] - 1] != null)
                {
                    n.Neighbors.Add(Nodes[n.Index[0], n.Index[1] - 1]);
                }
            }
            if (n.Index[1] < Tiles.GetLength(1) - 1)
            {
                // Check the bottom of the node.
                if (Nodes[n.Index[0], n.Index[1] + 1] != null)
                {
                    n.Neighbors.Add(Nodes[n.Index[0], n.Index[1] + 1]);
                }
            }
        }

        /// <summary>
        /// Updates the tile sizes.
        /// </summary>
        public static void UpdateTileSize()
        {
            TileSize = Options.ScreenWidth * 4 / 10;
            
            if (Buildings != null)
            {
                Buildings.Clear();
            }

            LoadMap();
        }
        #endregion
    }
}

// -- Genoah Martinelli