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

        private MouseState curr;
        private MouseState prev;

        private double posX;
        private double posY;

        private double dirX = 0;
        private double dirY = 0;
        private double angle = 0;

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

            for(int i = 0; i < 100; i++)
            {
                // throw bullets into the void
                // out of sight, out of mind

                bullets[i] = new Bullet(-999, -999, 0);
            }
        }

        public void SetPosition(double x, double y)
        {
            posX = x;
            posY = y;
        }
       
        public void CalcAngle()
        {
            // difference of position between turret and mouse pointer
            dirX = curr.Position.X - posX;
            dirY = curr.Position.Y - posY;

            // use trig to make turret point towards mouse
            angle = Math.Atan(dirY / dirX) * 180 / 3.14159;


            // this is just to keep numbers consistant, 0-360
            // rather than -90 - 270
            if (dirX < 0)
                angle += 180;

            if (angle < 0)
                angle += 360;
        }

        double timer = 0;
        public void Update(GameTime gameTime, double x, double y)
        {
            // update timer
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            curr = Mouse.GetState();

            // change position of turret, depending on where car is
            SetPosition(x, y);

            // get angle that the turret should be facing
            CalcAngle();

            // a bullet fires every 0.15 seconds
            if (curr.LeftButton == ButtonState.Pressed && timer >= 150)
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

            prev = curr;
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
            foreach(Bullet x in bullets)
                x.Draw(b);
        }

        public String Debug()
        {
            String text = "Turret Debug:" +
                "\nangle: " + angle;

            return text;
        }
    }
}
