using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using _2dracer.Managers;

namespace _2dracer
{
    public class Player : Mover
    {
        // Fields
        private float prevRotation;         // Keeps track of previous frame rotation
        private float topSpeed;             // The maximum speed the player can achieve

        public static bool slowMo = false;

        // Properties
        public Texture2D BulletSprite { get; set; }
        public double TimeJuice { get; private set; }
        public int Health { get; private set; }

        // Constructor
        public Player(Vector2 position) 
            : base(new GameObject(position, 0, "Textures/RedCar", new Vector2(64, 128)), 
                  new Vector2(0,0), new Vector2(0,0), 0, 0, 1)
        {
            Health = 100;
            TimeJuice = 0;
            prevRotation = rotation;
            topSpeed = 1000f;
        }

        // Methods
        public override void Update()
        {
            Movement();

            Turn();

            Juice();

            base.Update();
        }

        public override void Draw()
        {
            rotation += (float)Math.PI / 2;
            base.Draw();
            rotation -= (float)Math.PI / 2;
            #region Draw HUD
            // Will change this after UI class is updated
            Game1.spriteBatch.End();
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.DrawString(Game1.comicSans, $"Health : {Health}", new Vector2(100, 100), Color.White);
            Game1.spriteBatch.DrawString(Game1.comicSans, $"Time Juice : {TimeJuice:N0}", new Vector2(Game1.screenWidth - 300, 100), Color.White);
            Game1.spriteBatch.End();
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Game1.camera.ViewMatrix);
            #endregion
        }

        /// <summary>
        /// Moves the car
        /// </summary>
        private void Movement()
        {
            float yAxis = Input.GetAxisRaw(Axis.Y);
            float horsePower = yAxis * 200f;

            // Movement stuff here
            if (yAxis != 0)
            {
                float xForce = (float)Math.Cos(rotation) * horsePower;
                float yForce = (float)Math.Sin(rotation) * horsePower;
                AddForce(new Vector2(xForce, yForce));
            }
            else
            {
                AddForce(velocity / -2);
            }

            // E-Brake
            if (Input.KeyTap(Keys.Space))
            {
                velocity = Vector2.Zero;
            }

            // Controls the top speed of the car
            if (velocity.Length() > topSpeed || velocity.Length() < -topSpeed)
            {
                velocity = new Vector2(
                    topSpeed * (float)Math.Cos(rotation),
                    topSpeed * (float)Math.Sin(rotation));
            }
            
            // Makes sure the car doesn't fly off the map * needs fixed
            if (position.X <= -386 ||
                position.X >= Game1.map.Size.X - 386)
            {
                velocity.X = 0;
            }
            if (position.Y <= -386 ||
                position.Y >= Game1.map.Size.Y - 386)
            {
                velocity.Y = 0;
            }
        }

        /// <summary>
        /// Controls how the car turns
        /// </summary>
        private void Turn()
        {
            float xAxis = Input.GetAxisRaw(Axis.X);

            // Checks to see if the car is at least moving
            if (velocity.Length() >= 10f)
            {
                // Checks to see which direction player wants to turn
                if (xAxis > 0)
                {
                    // Checks angular velocity
                    if (angularVelocity >= 0)
                    {
                        AddTorque(xAxis * (topSpeed / velocity.Length()) * 0.25f);

                        if (angularVelocity >= 2f)
                        {
                            angularVelocity = 2f;
                        }
                    }
                    else
                    {
                        angularVelocity = 0;
                    }
                }
                else if (xAxis < 0)
                {
                    // Checks angular velocity
                    if (angularVelocity <= 0)
                    {
                        AddTorque(xAxis * (topSpeed / velocity.Length()) * 0.5f);
                        
                        if (angularVelocity <= -2f)
                        {
                            angularVelocity = -2f;
                        }
                    }
                    else
                    {
                        angularVelocity = 0;
                    }
                }
                else
                {
                    angularVelocity *= 0.9f;
                }
            }
            else
            {
                angularVelocity *= 0.9f;
            }


            AdjustVelocity();
        }

        /// <summary>
        /// Prevents the player from sliding when turning * needs velocity clamped *
        /// </summary>
        private void AdjustVelocity()
        {
            if (prevRotation != rotation)
            {
                float rotDiff = prevRotation - rotation;

                velocity = new Vector2(
                    (float)(velocity.X * Math.Cos(-rotDiff) - velocity.Y * Math.Sin(-rotDiff)),
                    (float)(velocity.X * Math.Sin(-rotDiff) + velocity.Y * Math.Cos(-rotDiff)));


                prevRotation = rotation;
            }
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
