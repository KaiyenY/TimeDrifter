using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using _2dracer.GameObjects;
using _2dracer.Managers;
using _2dracer.UI;

namespace _2dracer
{
    public class Player : Car
    {
        #region Fields
        private static TextElement scoreText;
        
        private Turret turret;
        
        public static bool slowMo = false;
        public static Vector2 PlayerPos;

       

        public delegate void EnterEventHandler(Node node);
        public static event EnterEventHandler Enter;

        private int[] NodeIndex = new int[2];
        #endregion

        #region Properties
        public static double TimeJuice { get; private set; }
        public static int Health { get; private set; }
        public static double Score { get; private set; }
        public static Vector2 playerVelocity;
        #endregion

        #region Constructor
        public Player(Vector2 position) 
            : base (position, 0, LoadManager.Sprites["RedCar"], new Vector2(Options.ScreenWidth / 12, Options.ScreenHeight / 13.5f), 400, 100, 250, 750)
        {
            Health = 100;
            TimeJuice = 0;
            Score = 0;
            GameMaster.Instantiate(turret = new Turret());

            
        }
        #endregion

        #region Methods
        public override void Update()
        {
            float xAxis = Input.GetAxisRaw(Axis.X);
            float yAxis = Input.GetAxisRaw(Axis.Y);

            if (yAxis != 0)
            {
                Accelerate(yAxis);          // Accelerates player
            }
            else
            {
                Decelerate();               // Decelerates player
            }

            if (xAxis != 0)
            {
                Turn(xAxis);                // Turns player
            }
            else
            {
                angularVelocity *= 0.9f;
            }

            if (Input.KeyHold(Keys.Space))
            {
                Brake();
            }

            if (Input.KeyHold(Keys.R))
            {
                Health -= 1;
            }

            if (Health <= 0)
            {
                Health = 0;
                Game1.GameState = GameState.GameOver;
            }

            AdjustVelocity();               // Adjusts player velocity

            Juice();

            base.Update();

            int thisX = ((int)this.Position.X / 768) + 1;
            int thisY = ((int)this.Position.Y / 768) + 1;

            if(thisX != NodeIndex[0] || thisY != NodeIndex[1]) //If the current position does not represent the field position
                if(thisX < MapElements.Map.Nodes.GetLength(0) && thisY < MapElements.Map.Nodes.GetLength(1)) //If doesn't go out of bounds
                    if(MapElements.Map.Nodes[thisX, thisY] != null)
                    {
                        Enter.Invoke(MapElements.Map.Nodes[thisX, thisY]); //Call the event that recalculates AI
                        Console.WriteLine("Player's Node: " + MapElements.Map.Nodes[thisX, thisY].ToString());
                    }

            if (MapElements.Map.Nodes[thisX, thisY] == null)
            {
                Console.WriteLine("Nodes[" + thisX + "," + thisY + "] is null!");
            }
            else
            {
                Console.WriteLine("The Node at [" + thisX + ", " + thisY + "] is " + MapElements.Map.Nodes[thisX, thisY].ToString());
            }

            turret.MoveTurret(position);

            // THIS IS JUST SO CARS HAVE AWFUL COLLISIONS
            Rectangle rect = new Rectangle((position - size / 2).ToPoint(), size.ToPoint());
            foreach (MapElements.Tile t in MapElements.Map.Tiles)
            {
                if (t.Type == MapElements.TileType.Building && t.Rect.Intersects(rect))
                {
                    if (slowMo)
                    {
                        position -= velocity * (float)Game1.gameTime.ElapsedGameTime.TotalSeconds / 3;
                    }
                    else
                    {
                        position -= velocity * (float)Game1.gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    velocity = Vector2.Zero;
                    break;
                }
            }

            playerVelocity = velocity;

            //Update the permanent location of the player's node within the array
            NodeIndex[0] = thisX;
            NodeIndex[1] = thisY;
        }

        /// <summary>
        /// Controls the timejuice mechanic
        /// </summary>
        private void Juice()
        {
            if (Input.KeyTap(Keys.E))
                slowMo = !slowMo;

            Score += Game1.gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

            if (!slowMo && TimeJuice < 10)
                TimeJuice += Game1.gameTime.ElapsedGameTime.TotalMilliseconds / 2000;

            if (slowMo)
                TimeJuice -= Game1.gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

            if (TimeJuice <= 0)
            {
                TimeJuice = 0;
                slowMo = false;
            }
        }

        /// <summary>
        /// Creates the HUD of the player.
        /// </summary>
        public static void CreateHUD()
        {
            UIManager.Add(scoreText = new TextElement(new Point(50, 450), true, 0.25f, "playerScore", "Score : " + Score));
        }
        #endregion
    }
}

// Niko Procopi
