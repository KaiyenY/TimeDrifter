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
        public Player(Texture2D tex, Vector2 v) :
            base(v, 0, tex, new Vector2(50, 50)) { }

        public void Update()
        {
            // turn car
            rotation += Input.GetAxisRaw(Axis.X) * 0.04f;

            // move car
            float speed = Input.GetAxisRaw(Axis.Y) * 3;
            position.X += (float)Math.Cos(rotation) * speed;
            position.Y += (float)Math.Sin(rotation) * speed;
        }

        public void Draw()
        {
            rotation += (float)Math.PI/2;
            base.DrawRect(1);
            rotation -= (float)Math.PI / 2;
        }
    }
}

// Niko Procopi
