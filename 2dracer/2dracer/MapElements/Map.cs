using _2dracer.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2dracer.MapElements
{
    public static class Map
    {
        #region Fields
        private static StreamReader sr;                // Reads the map files
        #endregion

        #region Properties
        public static List<Building> Buildings { get; }
        public static Node[,] Nodes { get; }
        public static Tile[,] Tiles { get; }
        public static Vector2 Size { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Generates a map from file
        /// </summary>
        static Map()
        {
            int[] mapSize = new int[2];
            Queue<string> tileInfo;
            
            try
            {
                sr = new StreamReader(@"..\..\..\..\Content\Main.txt");
                
                string[] mapInfo = sr.ReadLine().Split(',');        // Split info from file into two
                mapSize[0] = int.Parse(mapInfo[0]);                 // Set map horizontal size
                mapSize[1] = int.Parse(mapInfo[1]);                 // Set map vertical size
                tileInfo = new Queue<string>();                     // Store all tile data from the file

                Size = new Vector2(mapSize[0] * 768, mapSize[1] * 768);

                string line = null;
                while((line = sr.ReadLine()) != null)
                {
                    tileInfo.Enqueue(line);
                }
                
                Tiles = new Tile[mapSize[0], mapSize[1]];           // Set up tile array
                Nodes = new Node[mapSize[0], mapSize[1]];                     // Set up nodes list
                Buildings = new List<Building>();                   // Set up buildings list

                // Create all tiles; set up nodes and collision stuff as need be
                for (int y = 0; y < mapSize[1]; y++)
                {
                    for (int x = 0; x < mapSize[0]; x++)
                    {
                        string[] info = tileInfo.Dequeue().Split(',');
                        
                        Tile current = Tiles[x, y] = new Tile(
                            (TileType)int.Parse(info[0]),
                            MathHelper.ToRadians(int.Parse(info[1])),
                            int.Parse(info[2]),
                            int.Parse(info[3]));

                        // If the last bit of data is > 0, this tile has neighbors
                        if (current.Type != TileType.Building)
                        {
                            List<string[]> neighborInfo = new List<string[]>();

                            // There are nodes attached to this tile, so grab the positions
                            // of where the nodes are and use them to populate it's list of neighbors
                            for (int i = 0; i < int.Parse(info[4]); i++)
                            {
                                // Loops through the amount of neighbors this tile has, and
                                // stores them into this list to create the node
                                neighborInfo.Add(tileInfo.Dequeue().Split(','));
                            }

                            // Add the node to the list
                            Nodes[x, y] = new Node(current.Position.ToPoint());
                        }
                        else
                        {
                            // There are no neighbors for this node so it is a building
                            Buildings.Add(new Building(new Vector2(x * 2.65f - 2.2f, -2.65f * (y - 0.47f))));
                            Nodes[x, y] = null;
                        }
                    }
                }

                // Populate the each node's list
                foreach(Node node in Nodes)
                {
                    node.PopulateNeighborsList();
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

            // Pass this list into the AI
            List<Node> nodeList = new List<Node>();
            foreach (Node node in Nodes)
            {
                nodeList.Add(node);
            }
        }
        #endregion

        #region Methods
        public static void DrawGround()
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw();
            }
            foreach (Node node in Nodes)
            {
                if (node != null)
                {
                    Game1.spriteBatch.Draw(LoadManager.Sprites["Square"], new Rectangle(node.Location, new Point(25, 25)), Color.Red);
                }
            }
        }

        public static void DrawBuildings()
        {
            for (int i = 0; i < Buildings.Count; i++)
            {
                if(i % 2 == 1) Buildings[i].Draw(LoadManager.Sprites["Building1"]);
                else Buildings[i].Draw(LoadManager.Sprites["Building2"]);
            }
        }

        /// <summary>
        /// (dirty) Method to assign neighbors to each node in the map.
        /// </summary>
        public static void AssignNeighbors()
        {
            for (int x = 0; x < Tiles.GetLength(0); x++) //Loops through the horizontal direction
            {
                for (int y = 0; y < Tiles.GetLength(1); y++) //Loops through the vertical direction
                {
                    #region checking adjacencies
                    Node current = Tiles[x, y].Node;
                    if(x == 0)
                    {
                        if(y == 0)
                        {
                            //If at corner of the array, check only the two tiles it has adjacency to
                            if(Tiles[x+1, y].Type != TileType.Building) //If it's not a building, it must have a node (right?)
                            {
                                current.Neighbors.Add(Tiles[x + 1, y].Node);
                            }
                            if(Tiles[x, y+1].Type != TileType.Building)
                            {
                                current.Neighbors.Add(Tiles[x, y + 1].Node);
                            }
                        }
                        else //Not at the corner of the array, but at the edge of it
                        {
                            //In that case, check the three adjacent tiles
                            if (Tiles[x + 1, y].Type != TileType.Building)
                            {
                                current.Neighbors.Add(Tiles[x + 1, y].Node);
                            }
                            if (Tiles[x, y + 1].Type != TileType.Building)
                            {
                                current.Neighbors.Add(Tiles[x, y + 1].Node);
                            }
                            if (Tiles[x, y - 1].Type != TileType.Building)
                            {
                                current.Neighbors.Add(Tiles[x, y - 1].Node);
                            }
                        }
                    }
                    else if(y == 0)
                    {
                        //At the edge of the array, but not at the corner, check the 3 again
                        if (Tiles[x + 1, y].Type != TileType.Building)
                        {
                            current.Neighbors.Add(Tiles[x + 1, y].Node);
                        }
                        if (Tiles[x, y + 1].Type != TileType.Building)
                        {
                            current.Neighbors.Add(Tiles[x, y + 1].Node);
                        }
                        if (Tiles[x - 1, y].Type != TileType.Building)
                        {
                            current.Neighbors.Add(Tiles[x - 1, y].Node);
                        }
                    }
                    else
                    {
                        //Tile is in the middle of the array, check all 4 adjacencies
                        if (Tiles[x + 1, y].Type != TileType.Building)
                        {
                            current.Neighbors.Add(Tiles[x + 1, y].Node);
                        }
                        if (Tiles[x, y + 1].Type != TileType.Building)
                        {
                            current.Neighbors.Add(Tiles[x, y + 1].Node);
                        }
                        if (Tiles[x - 1, y].Type != TileType.Building)
                        {
                            current.Neighbors.Add(Tiles[x - 1, y].Node);
                        }
                        if (Tiles[x, y - 1].Type != TileType.Building)
                        {
                            current.Neighbors.Add(Tiles[x, y - 1].Node);
                        }
                    }
                    #endregion
                }
            }
        }

        #endregion
    }
}

// -- Genoah Martinelli