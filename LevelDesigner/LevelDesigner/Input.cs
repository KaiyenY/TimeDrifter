using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LevelDesigner
{
    public enum MouseButton
    {
        Left,
        Right
    }

    /// <summary>
    /// Provides simple <see cref="Keyboard"/> and <see cref="Mouse"/> input for <see cref="Designer"/>.
    /// </summary>
    public static class Input
    {
        #region Fields
        private static KeyboardState ks, prevKS;

        private static MouseState ms, prevMS;
        #endregion

        #region Methods
        /// <summary>
        /// Updates all input states
        /// </summary>
        public static void Update()
        {
            prevKS = ks;
            ks = Keyboard.GetState();

            prevMS = ms;
            ms = Mouse.GetState();
        }

        /// <summary>
        /// Detects whether the <see cref="Mouse"/> was clicked.
        /// </summary>
        /// <param name="button"><see cref="MouseButton"/> to check for click.</param>
        /// <returns>True if mouse was clicked.</returns>
        public static bool MouseClick(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return ms.LeftButton == ButtonState.Pressed && prevMS.LeftButton == ButtonState.Released;

                default:
                    return ms.RightButton == ButtonState.Pressed && prevMS.RightButton == ButtonState.Released;
            }
        }

        /// <summary>
        /// Detects whether the <see cref="Mouse"/> was released.
        /// </summary>
        /// <param name="button"><see cref="MouseButton"/> to check for release.</param>
        /// <returns>True if mouse was released.</returns>
        public static bool MouseReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return ms.LeftButton == ButtonState.Released && prevMS.LeftButton == ButtonState.Pressed;

                default:
                    return ms.RightButton == ButtonState.Released && prevMS.RightButton == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// Detects whether the <see cref="Mouse"/> is being held.
        /// </summary>
        /// <param name="button"><see cref="MouseButton"/> to check for holding.</param>
        /// <returns>True while mouse is being held.</returns>
        public static bool MouseHold(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return ms.LeftButton == ButtonState.Pressed;

                default:
                    return ms.RightButton == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// Gets the current position of the <see cref="Mouse"/>.
        /// </summary>
        /// <returns>Position of the <see cref="Mouse"/>.</returns>
        public static Point MousePos()
        {
            return ms.Position;
        }

        /// <summary>
        /// Detects whether a <see cref="Keyboard"/> key is tapped.
        /// </summary>
        /// <param name="key">Key to check for tap.</param>
        /// <returns>True if key was tapped.</returns>
        public static bool KeyTap(Keys key)
        {
            return ks.IsKeyDown(key) && prevKS.IsKeyUp(key);
        }

        /// <summary>
        /// Detects whether a <see cref="Keyboard"/> key is released.
        /// </summary>
        /// <param name="key">Key to check for release.</param>
        /// <returns>True if key was released.</returns>
        public static bool KeyReleased(Keys key)
        {
            return ks.IsKeyUp(key) && prevKS.IsKeyDown(key);
        }

        /// <summary>
        /// Detects whether a <see cref="Keyboard"/> key is held down.
        /// </summary>
        /// <param name="key">Key to check for holding.</param>
        /// <returns>True while key is held down.</returns>
        public static bool KeyHold(Keys key)
        {
            return ks.IsKeyDown(key);
        }
        #endregion
    }
}

// -- Genoah Martinelli