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
    class Player
    {
        private Texture2D c;
        public float posX;
        public float posY;

        private float dirX = 0;
        private float dirY = 0;
        private float angle = 0;

        public Player(Texture2D tex, float x, float y)
        {
            posX = x;
            posY = y;
            c = tex;
        }

        public void Update()
        {
            float radians = (float)(angle * (3.14159 / 180));

            // move car
            Axis axis = Axis.Y;
            posX += Input.GetAxisRaw(axis) * (float)Math.Cos(radians) * 3;
            posY += Input.GetAxisRaw(axis) * (float)Math.Sin(radians) * 3;
            
            // turn car
            axis = Axis.X;
            angle += 2 * Input.GetAxisRaw(axis);
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
