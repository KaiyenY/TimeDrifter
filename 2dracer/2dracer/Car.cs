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
    class Car
    {
        private Texture2D c;
        public float posX;
        public float posY;

        private float dirX = 0;
        private float dirY = 0;
        private float angle = 0;

        public Car(Texture2D tex, float x, float y)
        {
            posX = x;
            posY = y;
            c = tex;
        }

        public void Update()
        {
            KeyboardState s = Keyboard.GetState();

            // move car forward
            if (s.IsKeyDown(Keys.W))
            {
                posX += (float)Math.Cos(angle * (3.14159 / 180)) * 3;
                posY += (float)Math.Sin(angle * (3.14159 / 180)) * 3;
            }

            // move car backward
            if (s.IsKeyDown(Keys.S))
            {
                posX -= (float)Math.Cos(angle * (3.14159 / 180)) * 3;
                posY -= (float)Math.Sin(angle * (3.14159 / 180)) * 3;
            }

            // turn left
            if (s.IsKeyDown(Keys.A))
                angle -= 2;

            // turn right
            if (s.IsKeyDown(Keys.D))
                angle += 2;
        }

        public void Draw()
        {
            // Draw car
            Game1.spriteBatch.Draw(c,
                new Rectangle((int)posX, (int)posY, c.Width / 8, c.Height / 8),
                null,
                Color.White,
                (float)((angle + 90) * 3.14159 / 180),
                new Vector2(c.Width / 2, c.Height / 2),
                SpriteEffects.None, 0f);
        }
    }
}
