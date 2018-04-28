using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _2dracer.Managers;

namespace _2dracer
{
    public class Turret : GameObject
    {
        // Fields
        private float timer;            // Fire rate
        private List<Bullet> bullets;

        // Properties

        // Constructor
        public Turret() : 
            base(new Vector2(0,0), 0, LoadManager.Sprites["Turret"], new Vector2(Options.ScreenWidth / 24, Options.ScreenHeight / 27), 0.5f)
        {
            timer = 0;
            bullets = new List<Bullet>(50);
        }
        
        public override void Update()
        {
            FireBullet();

            RotateTurret();

            foreach (Bullet bullet in bullets)
            {
                bullet.Update();
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].Delete)
                {
                    bullets.Remove(bullets[i]);
                }
            }
        }

        public override void Draw()
        {
            origin = new Vector2(sprite.Width / 2, sprite.Height) / 2;
            Vector2 appliedScale = new Vector2((size.X * scale.X) / sprite.Width, (size.Y * scale.Y) / sprite.Height);
            Game1.spriteBatch.Draw(sprite, position, null, color, rotation, origin, appliedScale, SpriteEffects.None, layerDepth);

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }
        }

        /// <summary>
        /// Fires a <see cref="Bullet"/>.
        /// </summary>
        private void FireBullet()
        {
            // Update the timer
            timer += (float)Game1.gameTime.ElapsedGameTime.TotalMilliseconds;

            // a bullet fires every 0.15 seconds
            if ((Input.MouseHold(MouseButton.Left) || Input.ControlHold(Buttons.LeftShoulder)) && timer >= 150)
            {
                // Reset timer
                timer = 0;

                bullets.Add(new Bullet(position, rotation));

                Audio.PlaySound("Gunshot", 0.25f);
            }
        }

        /// <summary>
        /// Rotates the <see cref="Turret"/>.
        /// </summary>
        private void RotateTurret()
        {
            // Get the angle that the turret should be facing.
            if (Input.ControlConnected())
            {
                // Use controller input (should add setting for this later)
                rotation = Input.ControlAngle();
            }
            else
            {
                // Use mouse input
                rotation = Input.MouseAngle(this);
            }
        }

        /// <summary>
        /// Moves the <see cref="Turret"/> with the <see cref="Player"/>.
        /// </summary>
        public void MoveTurret(Vector2 position)
        {
            this.position = position;
        }

        
    }
}

// Niko Procopi
