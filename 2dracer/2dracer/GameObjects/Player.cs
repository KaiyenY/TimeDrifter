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
        // Temporary elements, will be gauge needle soon
        private static TextElement healthText;
        private static TextElement timeText;
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
            : base (position, 0, LoadManager.Sprites["RedCar"], new Vector2(Options.ScreenWidth / 12, Options.ScreenHeight / 13.5f), 400, 100, 250, 1000)
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

            AdjustVelocity();               // Adjusts player velocity

            Juice();

            if (timeText != null && healthText != null)
            {
                timeText.Text = $"Time Juice : {TimeJuice:N0}";
                healthText.Text = $"Health : {Health:N0}";
                scoreText.Text = $"Score : {Score:N0}";
            }

            base.Update();

            int thisX = ((int)this.Position.X / 768);
            int thisY = ((int)this.Position.Y / 768);

            if(thisX != NodeIndex[0] || thisY != NodeIndex[1]) //If the current position does not represent the field position
                if(thisX < MapElements.Map.Nodes.GetLength(0) && thisX < MapElements.Map.Nodes.GetLength(1)) //If doesn't go out of bounds
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
            UIManager.Add(healthText = new TextElement(new Point(50, 250), true, 0.25f, "playerHealth", "Health : " + Health));
            UIManager.Add(timeText = new TextElement(new Point(50, 350), true, 0.25f, "playerJuice", "Time Juice : " + TimeJuice));
            UIManager.Add(scoreText = new TextElement(new Point(50, 450), true, 0.25f, "playerScore", "Score : " + Score));
        }
        #endregion
    }
}

// Niko Procopi
