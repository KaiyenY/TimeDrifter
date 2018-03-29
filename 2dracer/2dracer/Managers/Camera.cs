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
    public class Camera
    {
        private Vector2 position;

        public Vector2 Position { get { return position; } }

        public Matrix ViewMatrix { get; set; }

        public void Update()
        {
            Vector2 pos = Game1.player.Position;
            Vector2 mapSize = Game1.map.Size;

            // Through complicated stuff, these two checks will make sure the
            // camera doesn't go off the map
            if (pos.X >= (Game1.screenWidth / 2) - 384 &&
                pos.X <= -(Game1.screenWidth / 2) + mapSize.X - 384)
            {
                position.X = pos.X - (Game1.screenWidth / 2);
            }
            if (pos.Y >= (Game1.screenHeight / 2) - 384 &&
                pos.Y <= -(Game1.screenHeight / 2) + mapSize.Y - 384)
            {
                position.Y = pos.Y - (Game1.screenHeight / 2);
            }
            
            ViewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}

// Niko Procopi