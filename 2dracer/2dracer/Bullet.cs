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
    class Bullet
    {
        public double x;
        public double y;
        public double angle;

        public Bullet(double a, double b, double c)
        {
            x = a;
            y = b;
            angle = c;

            //start bullet at tip of gun
            //rather than center of gun
            x += Math.Cos(angle * (3.14159 / 180)) * 70;
            y += Math.Sin(angle * (3.14159 / 180)) * 70;
        }

        public void Draw(Texture2D b)
        {
            Game1.spriteBatch.Draw(b,
            new Rectangle((int)x, (int)y, b.Width / 20, b.Height / 20),
            null,
            Color.White,
            (float)((angle + 90) * 3.14159 / 180),
            new Vector2(b.Width / 2, b.Height / 2),
            SpriteEffects.None, 0f);
        }

        public void Update()
        {
            if (Math.Abs(x) < 1000 || Math.Abs(y) < 1000)
            {
                x += Math.Cos(angle * (3.14159 / 180)) * 10;
                y += Math.Sin(angle * (3.14159 / 180)) * 10;
            }
        }
    }
}
