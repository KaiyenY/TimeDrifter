using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _2dracer.Managers
{
    class Camera
    {
        private Vector2 position;
        public Vector2 Position { get { return position; } }

        private Matrix viewMatrix;
        public Matrix ViewMatrix { get { return viewMatrix;  } }

        public void Update(Vector2 pos)
        {
            position.X = pos.X - (Game1.screenWidth / 2) ;
            position.Y = pos.Y - (Game1.screenHeight / 2);

            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}

// Niko Procopi