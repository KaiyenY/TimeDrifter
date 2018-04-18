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

        #region Methodss
        public static void Start()
        {
            GameObjects = new List<GameObject>();
            Movers = new List<Mover>();
            Rigids = new List<Rigid>();

            // Instantiate GameObjects here please
            Instantiate(new Player(new Vector2(Options.ScreenWidth / 2, Options.ScreenHeight / 2)));

            for (int i = 0; i < 5; i++)
            {
                Instantiate(new Enemy(LoadManager.Sprites["Cop"], new Vector2(200, 200 * i)));
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