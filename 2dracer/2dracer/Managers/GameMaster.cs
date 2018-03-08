using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2dracer.Managers
{
    static class GameMaster
    {
        // fields
        private static List<GameObject> gameObjects;
        private static List<Mover> movers;
        private static List<Rigid> rigids;

        // properties
        public static List<GameObject> GameObjects { get { return gameObjects; } }
        public static List<Mover> Movers { get { return movers; } }
        public static List<Rigid> Rigids { get { return rigids; } }

        static GameMaster()
        {
            gameObjects = new List<GameObject>();
            movers = new List<Mover>();
            rigids = new List<Rigid>();

            // TODO: Add instatiation here instead of in Game1
            Instantiate(new Mover(new GameObject(new Vector2(50, 50))));
        }

        // methods
        /// <summary>
        /// Updates all game objects
        /// </summary>
        public static void Update(GameTime gameTime)
        {
            foreach(GameObject g in gameObjects)
            {
                g.Update(gameTime);
            }
        }
        
        /// <summary>
        /// Draws all game objects in correct ordering
        /// MUST call spriteBatch.Begin first
        /// </summary>
        public static void Draw()
        {
            // REPLACE THIS WITH SMARTER CODE TO DRAW OBJECTS IN LAYERS
            foreach (GameObject g in gameObjects)
            {
                g.Draw();
            }
        }

        /// <summary>
        /// Instantiatess a GameObject to be updated and drawn
        /// </summary>
        public static void Instantiate(GameObject g)
        {
            gameObjects.Add(g);

            if(g is Mover)
            {
                movers.Add((Mover)g);
            }
            if(g is Rigid)
            {
                rigids.Add((Rigid)g);
            }
        }

        /// <summary>
        /// Clears all GameObjects from the game
        /// </summary>
        public static void ClearAll()
        {
            gameObjects.Clear();
            movers.Clear();
            rigids.Clear();
        }
    }
}

// Matthew Soriano