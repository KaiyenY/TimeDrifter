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
    class Turret
    {
        private Texture2D t;
        private Texture2D b;
        private Bullet[] bullets;
        
        private float posX;
        private float posY;

        private float angle = 0;

        // which index of array will be the "new" bullet
        private int bulletIndex = 0;

        public Turret(Texture2D tex, Texture2D bullet)
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

                bullets[i] = new Bullet(-999, -999, 0);
            }
        }

        public void CalcAngle()
        {
            Point mPos = Input.MousePos();

            GameObject go1;
            Vector2 v = new Vector2(posX, posY);
            go1 = new GameObject(v, 0.0f, t, v);
            angle = Input.MouseAngle(go1);
        }

        float timer = 0;
        public void Update(GameTime gameTime, float x, float y)
        {
            // update timer
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // change position of turret, depending on where car is
            posX = x;
            posY = y;

            // get angle that the turret should be facing
            CalcAngle();

            // a bullet fires every 0.15 seconds
            if (Input.MouseHold(MouseButton.Left) && timer >= 150)
            {
                // reset timer
                timer = 0;

                // set bullet position
                bullets[bulletIndex] = new Bullet(posX, posY, angle);

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
            // draw the turret
            Game1.spriteBatch.Draw(t,
                new Rectangle((int)posX, (int)posY, t.Width / 4, t.Height / 4),
                null,
                Color.White,
                (float)((angle + 90) * 3.14159 / 180),
                new Vector2(t.Width / 2, t.Height / 2),
                SpriteEffects.None, 0f);

            // draw all the bullets
            foreach (Bullet x in bullets)
                x.Draw(b);
        }
    }
}
