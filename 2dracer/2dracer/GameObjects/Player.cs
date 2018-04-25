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
        private static Element healthText;
        private static Element timeText;
        private static Element scoreText;
        
        private Turret turret;
        
        public static bool slowMo = false;
        public static Vector2 PlayerPos;
        #endregion

        #region Properties
        public static double TimeJuice { get; private set; }
        public static int Health { get; private set; }
        public static double Score { get; private set; }
        public static Vector2 playerVelocity;
        #endregion

        #region Constructor
        public Player(Vector2 position) 
            : base (position, 0, LoadManager.Sprites["RedCar"], new Vector2(128, 64), 400, 100, 250, 750)
        {
            Health = 100;
            TimeJuice = 0;
            Score = 0;
            GameMaster.Instantiate(turret = new Turret());
            PlayerPos = position;
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
            PlayerPos = position;
            turret.MoveTurret(position);

            playerVelocity = velocity;
        }

        /// <summary>
        /// Controls the timejuice mechanic
        /// </summary>
        private void Juice()
        {
            if (Input.KeyTap(Keys.P))
                slowMo = !slowMo;

            Score += Game1.gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

            if (!slowMo && TimeJuice < 10)
                TimeJuice += Game1.gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

            if (slowMo)
                TimeJuice -= Game1.gameTime.ElapsedGameTime.TotalMilliseconds / 500;

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
            UIManager.Add(healthText = new Element(new Vector2(50, 50), 0.25f, "playerHealth", "Health : " + Health));
            UIManager.Add(timeText = new Element(new Vector2(50, 150), 0.25f, "playerJuice", "Time Juice : " + TimeJuice));
            UIManager.Add(scoreText = new Element(new Vector2(50, 250), 0.25f, "playerScore", "Score : " + Score));
        }
        #endregion
    }
}

// Niko Procopi
