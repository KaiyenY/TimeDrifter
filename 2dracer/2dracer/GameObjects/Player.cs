using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using _2dracer.Managers;
using _2dracer.GameObjects;

namespace _2dracer
{
    public class Player : Car
    {
        // Fields
        public static bool slowMo = false;

        // Properties
        public Texture2D BulletSprite { get; set; }
        public double TimeJuice { get; private set; }
        public int Health { get; private set; }
        //public Node justSteppedOn { get; set; }


        // Constructor
        public Player(Vector2 position) 
            : base (position, 0, "Textures/RedCar", new Vector2(128, 64), 400, 100, 250, 1000)
        {
            Health = 100;
            TimeJuice = 0;
        }

        // Methods
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

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();

            #region Draw HUD
            // This is temporary
            Game1.spriteBatch.End();
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.DrawString(Game1.comicSans, $"Heath : {Health}", new Vector2(100, 100), Color.White);
            Game1.spriteBatch.DrawString(Game1.comicSans, $"Time Juice : {TimeJuice:N0}", new Vector2(100, 200), Color.White);
            Game1.spriteBatch.End();
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Game1.camera.ViewMatrix);
            #endregion
        }
        
        /// <summary>
        /// Controls the timejuice mechanic
        /// </summary>
        private void Juice()
        {
            if (Input.KeyTap(Keys.P))
                slowMo = !slowMo;

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
    }
}

// Niko Procopi
