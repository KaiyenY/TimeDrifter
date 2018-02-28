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
        public double posX;
        public double posY;

        private double dirX = 0;
        private double dirY = 0;
        private double angle = 0;

        public Car(Texture2D tex, double x, double y)
        {
            posX = x;
            posY = y;
            c = tex;
        }

        public void Update()
        {
            KeyboardState s = new KeyboardState();

            if (s.IsKeyDown(Keys.W))
                posY -= 10;

            if (s.IsKeyDown(Keys.S))
                posY += 10;

            if (s.IsKeyDown(Keys.A))
                posX -= 10;

            if (s.IsKeyDown(Keys.D))
                posX += 10;
        }

        public void Draw()
        {
            Game1.spriteBatch.Draw(c,
                new Rectangle((int)posX, (int)posY, c.Width / 8, c.Height / 8),
                null,
                Color.White,
                (float)((angle) * 3.14159 / 180),
                new Vector2(c.Width / 2, c.Height / 2),
                SpriteEffects.None, 0f);
        }
    }
}
