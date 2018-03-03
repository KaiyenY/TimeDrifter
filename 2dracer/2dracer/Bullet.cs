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
    class Bullet : GameObject
    {
        public Bullet(Texture2D tex, Vector2 pos, float angle) :
            base (pos, angle, tex, new Vector2(0.5f, 0.5f))
        {
            //start bullet at tip of gun
            //rather than center of gun
            position.X += (float)Math.Cos(angle * (3.14159 / 180)) * 70;
            position.Y += (float)Math.Sin(angle * (3.14159 / 180)) * 70;
        }

        public void Update()
        {
            // only move bullet if it is close enough to matter
            if (Math.Abs(position.X) < 1000 || Math.Abs(position.Y) < 1000)
            {
                position.X += (float)Math.Cos(rotation * (3.14159 / 180)) * 10;
                position.Y += (float)Math.Sin(rotation * (3.14159 / 180)) * 10;
            }
        }

        public void Draw()
        {
            rotation += 90;
            base.DrawRect(20);
            rotation -= 90;
        }
    }
}
