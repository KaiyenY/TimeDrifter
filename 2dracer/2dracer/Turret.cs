using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2dracer
{
    class Turret : GameObject
    {
        private Texture2D t;
        private Texture2D b;
        private Bullet[] bullets;

        // which index of array will be the "new" bullet
        private int bulletIndex = 0;

        public Turret(Texture2D tex, Texture2D bullet) : 
            base(new Vector2(0,0), 0, tex, new Vector2(1, 2))
        {
            t = tex;
            b = bullet;

            // using array so we dont need to
            // reallocate arrays (how List works)
            // saves processing
            bullets = new Bullet[100];

            for (int i = 0; i < 100; i++)
            {
                // throw bullets into the void
                // out of sight, out of mind

                bullets[i] = new Bullet(b, new Vector2(-999, -999), 0);
            }
        }

        float timer = 0;
        public void Update(GameTime gameTime, Vector2 pos)
        {
            // update timer
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

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
                rotation = Input.MouseAngle(this);
            }

            // a bullet fires every 0.15 seconds
            if ((Input.MouseHold(MouseButton.Left) || Input.ControlHold(Buttons.LeftShoulder)) && timer >= 150)
            {
                // reset timer
                timer = 0;

                // set bullet position
                bullets[bulletIndex] = new Bullet(b, position, rotation);

                // get index of next bullet to fire, 1-100
                bulletIndex++;

                if (bulletIndex == 100)
                    bulletIndex = 0;
            }

            // make all bullets move
            foreach (Bullet bullet in bullets)
                bullet.Update();
        }

        public void Draw()
        {
            rotation += 90;
            base.DrawRect(4);
            rotation -= 90;

            // draw all the bullets
            foreach (Bullet x in bullets)
                x.Draw();
        }
    }
}
