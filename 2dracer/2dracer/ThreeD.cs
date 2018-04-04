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
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    float aspectRatio =
                        Game1.graphics.PreferredBackBufferWidth / (float)Game1.graphics.PreferredBackBufferHeight;

                    float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                    float nearClipPlane = 1;
                    float farClipPlane = 200;

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        fieldOfView, aspectRatio, nearClipPlane, farClipPlane);

                    effect.View = Matrix.CreateTranslation(-1 * Game1.camera.Position.X / 75, Game1.camera.Position.Y / 75, -10);

                    effect.World = Matrix.Identity;
                }

                mesh.Draw();
            }
        }
    }
}
