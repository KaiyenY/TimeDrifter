using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using _2dracer.MapElements;
using _2dracer.Managers;

namespace _2dracer
{
    class ThreeD
    {
        private Model model;

        public ThreeD(Model m)
        {
            model = m;
        }

        public void Draw()
        {
            foreach (BasicEffect effect in model.Meshes[0].Effects)
            {
                float aspectRatio =
                    Game1.graphics.PreferredBackBufferWidth / (float)Game1.graphics.PreferredBackBufferHeight;

                float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                float nearClipPlane = 1;
                float farClipPlane = 200;

                effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, aspectRatio, nearClipPlane, farClipPlane);

                int z = 5;

                effect.View = Matrix.CreateTranslation(-1 * Game1.camera.Position.X / (59 * z), Game1.camera.Position.Y / (59*z), (float)-10/z);

                effect.World = Matrix.CreateTranslation(3, -1.5f, 0);
            }

            model.Meshes[0].Draw();
        }
    }
}
