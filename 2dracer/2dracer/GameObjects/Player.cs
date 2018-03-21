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
    class Player : Mover
    {
        private int health = 100;
        private float timeJuice = 0;
        private SpriteFont font;

        public Player(Texture2D tex, Vector2 v, SpriteFont s) :
            base(
                new GameObject(v, 0, tex, new Vector2(50, 50))
                )
        {
            font = s;
        }

        public void Update(GameTime gameTime)
        {
            // turn car
            rotation += Input.GetAxisRaw(Axis.X) * 0.04f;

            // move car
            float speed = Input.GetAxisRaw(Axis.Y) * 3;
            position.X += (float)Math.Cos(rotation) * speed;
            position.Y += (float)Math.Sin(rotation) * speed;

            if(timeJuice < 10)
                timeJuice += 
        }

        public void Draw()
        {
            rotation += (float)Math.PI/2;
            base.DrawRect(1);
            rotation -= (float)Math.PI / 2;
        }

        public void DrawHUD()
        {
            Game1.spriteBatch.DrawString(font, "Health: " + health, new Vector2(50, 100), Color.White);
            Game1.spriteBatch.DrawString(font, "Time Juice: " + (int)timeJuice, new Vector2(250, 100), Color.White);
        }
    }
}

// Niko Procopi
