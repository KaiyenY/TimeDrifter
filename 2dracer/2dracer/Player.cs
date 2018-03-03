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
    class Player : GameObject
    {
        public Player(Texture2D tex, float x, float y) :
            base(new Vector2(x,y), 0, tex) { }

        public void Update()
        {
            float radians = (float)(rotation * (3.14159 / 180));

            // move car
            Axis axis = Axis.Y;
            position.X += Input.GetAxisRaw(axis) * (float)Math.Cos(radians) * 3;
            position.Y += Input.GetAxisRaw(axis) * (float)Math.Sin(radians) * 3;
            
            // turn car
            axis = Axis.X;
            rotation += Input.GetAxisRaw(axis) * 2;
        }

        public void Draw()
        {
            rotation += 90;
            base.DrawRect(8);
            rotation -= 90;
        }
    }
}
