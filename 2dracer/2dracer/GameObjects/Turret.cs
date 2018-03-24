using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2dracer
{
    class Turret : GameObject
    {
        private Texture2D bulletSprite;
        private Bullet[] bullets;

        // which index of array will be the "new" bullet
        private int bulletIndex = 0;

        public Turret(Texture2D sprite, Texture2D bulletSprite) : 
            base(new Vector2(0,0), 0, sprite, new Vector2(25, 50))
        {
            this.sprite = sprite;
            this.bulletSprite = bulletSprite;

            // using array so we dont need to
            // reallocate arrays (how List works)
            // saves processing
            bullets = new Bullet[100];

            for (int i = 0; i < 100; i++)
            {
                // throw bullets into the void
                // out of sight, out of mind

                bullets[i] = new Bullet(this.bulletSprite, new Vector2(-999, -999), 0);
            }
        }

        float timer = 0;
        public void Update(Vector2 pos)
        {
            // update timer
            timer += (float)Game1.gameTime.ElapsedGameTime.TotalMilliseconds;

            // change position of turret, depending on where car is
            position = pos;

            // get angle that the turret should be facing
            if (Input.ControlConnected())
            {
                // Use controller input (should add setting for this later)
                rotation = Input.ControlAngle();
            }
            else
            {
                // Use mouse input
                rotation = Input.MouseAngle();
            }

            // a bullet fires every 0.15 seconds
            if ((Input.MouseHold(MouseButton.Left) || Input.ControlHold(Buttons.LeftShoulder)) && timer >= 150)
            {
                // reset timer
                timer = 0;

                // set bullet position
                bullets[bulletIndex] = new Bullet(bulletSprite, position, rotation);

                // get index of next bullet to fire, 1-100
                bulletIndex++;

                if (bulletIndex == 100)
                    bulletIndex = 0;
            }

            // make all bullets move
            foreach (Bullet bullet in bullets)
                bullet.Update();
        }

        public override void Draw()
        {
            rotation += (float)Math.PI / 2;
            base.Draw();
            rotation -= (float)Math.PI / 2;

            // draw all the bullets
            foreach (Bullet x in bullets)
                x.Draw();
        }
    }
}

// Niko Procopi
