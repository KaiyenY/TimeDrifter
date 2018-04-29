using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _2dracer.GameObjects;

namespace _2dracer.Managers
{
    static class GameMaster
    {
        #region Properties
        public static List<GameObject> GameObjects { get; private set; }
        public static List<Mover> Movers { get; private set; }
        public static List<Rigid> Rigids { get; private set; }
        #endregion

        #region Methods
        public static void Start()
        {
            GameObjects = new List<GameObject>();
            Movers = new List<Mover>();
            Rigids = new List<Rigid>();

            SpawnManager.Initialize();

            // Instantiate GameObjects here please
            SpawnManager.SpawnPlayer();


            // Everything below here should be replaced with the spawn manager code
            Random rand = new Random();

            for (int i = 0; i < 5; i++)
            {
                int j = 0;
                int k = 0;
                int minDistance = 3;
                int maxDistance = 5;

                do
                {
                    // J is between 0 and map_size_x
                    j = rand.Next(0, MapElements.Map.Tiles.GetLength(0));

                    // k is between 0 and map_size_y
                    k = rand.Next(0, MapElements.Map.Tiles.GetLength(1));

                    // find new random tile if
                }
                while (MapElements.Map.Tiles[j, k].Type == MapElements.TileType.Building //  the current is a building
                        ||  
                        // To close to playerX (2 blocks)               AND            To close to playerY (2 blocks)
                        (Math.Abs(Player.PlayerPos.X - MapElements.Map.TileSize * j) < MapElements.Map.TileSize * minDistance && Math.Abs(Player.PlayerPos.Y - MapElements.Map.TileSize * k) < MapElements.Map.TileSize * minDistance)           
                        ||              // the current tile is close to PlayerY
                        // To far from playerX (4 blocks)               OR          To far from playerY (4 blocks)
                        (Math.Abs(Player.PlayerPos.X - MapElements.Map.TileSize * j) > MapElements.Map.TileSize * maxDistance || Math.Abs(Player.PlayerPos.Y - MapElements.Map.TileSize * k) > MapElements.Map.TileSize * maxDistance)
                        );                


                Console.WriteLine(j + " " + k);
            }

            

            foreach(MapElements.Tile t in MapElements.Map.Tiles)
            {
                if(t.Type != MapElements.TileType.Building)
                {
                    //temporarily changed this to only spawn one cop so i can test AI ~~ Ruben
                    Enemy e = new Enemy(LoadManager.Sprites["Cop"], t.Position, t.Node);
                    Instantiate(e);
                    e.PrintDebug();
                    return; //only spawn 1
                }
            }
            
        }


        /// <summary>
        /// Updates all game objects
        /// </summary>
        public static void Update()
        {
            foreach (GameObject g in GameObjects)
            {
                g.Update();
            }
            
        }
        
        /// <summary>
        /// Draws all game objects in correct ordering
        /// MUST call spriteBatch.Begin first
        /// </summary>
        public static void Draw()
        {
            // REPLACE THIS WITH SMARTER CODE TO DRAW OBJECTS IN LAYERS
            foreach (GameObject g in GameObjects)
            {
                g.Draw();
            }
        }

        /// <summary>
        /// Instantiatess a GameObject to be updated and drawn
        /// </summary>
        public static void Instantiate(GameObject g)
        {
            GameObjects.Add(g);

            if(g is Mover)
            {
                Movers.Add((Mover)g);
            }
            if(g is Rigid)
            {
                Rigids.Add((Rigid)g);
            }
        }

        /// <summary>
        /// Clears all GameObjects from the game
        /// </summary>
        public static void ClearAll()
        {
            if (GameObjects != null)
            {
                GameObjects.Clear();
                Movers.Clear();
                Rigids.Clear();
            }
        }

        
        #endregion

        //TODO: Create a method/event that is triggered each time the player steps into a new tile (to calculate pathfinding based on that)
    }
}

// Matthew Soriano