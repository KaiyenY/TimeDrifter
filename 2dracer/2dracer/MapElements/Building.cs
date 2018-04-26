using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using _2dracer.Managers;

namespace _2dracer.MapElements
{
    /// <summary>
    /// Holds everything necessary to render buildings/
    /// </summary>
    public class Building
    {
        #region Fields
        private BasicEffect effect;
        private Model model;
        private Tile parent;
        private Vector2 localPos;
        private Vector3 worldPos;
        #endregion

        #region Constructor
        public Building(Tile parent, Vector2 localPos)
        {
            this.parent = parent;

            // Grabs the building model (can be added as parameter if we add more)
            model = LoadManager.Models["Cube"];


            effect = (BasicEffect)model.Meshes[0].Effects[0];

            // Makes sure we always have the local position of this model
            this.localPos = localPos;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the position of this building in world space.
        /// </summary>
        public void Update()
        {
            worldPos = Options.Graphics.GraphicsDevice.Viewport.Unproject(
                new Vector3(Vector2.Subtract(localPos, Game1.camera.Position), 0.97f),
                effect.Projection,
                effect.View,
                Matrix.Identity);
        }

        /// <summary>
        /// Draws this building onto the screen.
        /// </summary>
        public void Draw(Texture2D texture)
        {
            if (parent.IsEnabled)
            {
                double fieldOfView = (3.14159 / 4) * Options.Graphics.GraphicsDevice.Viewport.Width / (Map.TileSize * 2.338f);

                effect.Projection = Matrix.CreatePerspectiveFieldOfView((float)fieldOfView, Options.Graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 200f);
                effect.View = Matrix.CreateLookAt(new Vector3(Vector2.Zero, 2.3f), Vector3.Zero, Vector3.Up);
                effect.World = Matrix.CreateTranslation(worldPos.X, worldPos.Y, 0f);

                effect.Texture = texture;
                model.Meshes[0].Draw();
            }
        }
        #endregion
    }
}
