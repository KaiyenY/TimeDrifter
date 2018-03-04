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
    class Enemy : GameObject
    {
        public Enemy(Texture2D tex, Vector2 v) :
            base(v, 0, tex) { }

        public void Update(Vector2 PlayerPos)
        {
            // turn car
            //rotation += 0.04f;
            Vector2 toPlayer = PlayerPos - Position; //Get Vector to the player
            toPlayer.Normalize(); //Turn to unit Vector

            //Update Position
            position.X += toPlayer.X;
            position.Y += toPlayer.Y;
            // move car
            //float speed = 3;
            //position.X += (float)Math.Cos(rotation) * speed;
            //position.Y += (float)Math.Sin(rotation) * speed;
        }
        
        public void Draw()
        {
            rotation += (float)Math.PI / 2;
            base.DrawRect(8);
            rotation -= (float)Math.PI / 2;
        }
    }
}
