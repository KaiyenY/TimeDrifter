using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _2dracer.MapElements;


namespace _2dracer.Managers
{
    public class Camera
    {
        private Vector2 position;

        public Vector2 Position { get { return position; } }

        public Matrix ViewMatrix { get; set; }

        public void Update()
        {
            Vector2 pos = GameMaster.GameObjects[1].Position;
            Vector2 mapSize = Map.Size;

            // Through complicated stuff, these two checks will make sure the
            // camera doesn't go off the map
            if (pos.X >= (Options.ScreenWidth / 2) - 384 &&
                pos.X <= -(Options.ScreenWidth / 2) + mapSize.X - 384)
            {
                position.X = pos.X - (Options.ScreenWidth / 2);
            }
            if (pos.Y >= (Options.ScreenHeight / 2) - 384 &&
                pos.Y <= -(Options.ScreenHeight / 2) + mapSize.Y - 384)
            {
                position.Y = pos.Y - (Options.ScreenHeight / 2);
            }
            
            ViewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}

// Niko Procopi