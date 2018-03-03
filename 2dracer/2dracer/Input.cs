using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace _2dracer
{
    public enum Axis { X, Y }
    public enum MouseButton { Left, Right, Middle }
    public enum Triggers { Left, Right }

    /// <summary>
    /// Handles input from the player
    /// </summary>
    public static class Input
    {
        // Fields
        private static GamePadState currGS, prevGS;
        private static KeyboardState currKS, prevKS;
        private static MouseState currMS, prevMS;

        // Methods
        /// <summary>
        /// Update the input states
        /// </summary>
        public static void Update()
        {
            // GamePad States
            prevGS = currGS;
            currGS = GamePad.GetState(PlayerIndex.One);

            // Keyboard States
            prevKS = currKS;
            currKS = Keyboard.GetState();

            // Mouse States
            prevMS = currMS;
            currMS = Mouse.GetState();
        }
        
        // Keyboard Input
        /// <summary>
        /// Detects if a key is held
        /// </summary>
        /// <param name="key">Key to check</param>
        public static bool KeyHold(Keys key)
        {
            return currKS.IsKeyDown(key);
        }
        /// <summary>
        /// Detects if a key is tapped
        /// </summary>
        /// <param name="key">Key to check</param>
        public static bool KeyTap(Keys key)
        {
            return KeyHold(key) && prevKS.IsKeyUp(key);
        }

        // Mouse Input
        /// <summary>
        /// Detects if the mouse is clicked
        /// </summary>
        /// <param name="button">Button to check</param>
        public static bool MouseClick(MouseButton button)
        {
            if (button == MouseButton.Left)
            {
                return currMS.LeftButton == ButtonState.Pressed && prevMS.LeftButton == ButtonState.Released;
            }
            else if (button == MouseButton.Right)
            {
                return currMS.RightButton == ButtonState.Pressed && prevMS.RightButton == ButtonState.Released;
            }
            else
            {
                return currMS.MiddleButton == ButtonState.Pressed;
            }
        }
        /// <summary>
        /// Detects if the mouse is clicked within an area
        /// </summary>
        /// <param name="button">Button to check</param>
        /// <param name="area">Rectangle to check</param>
        public static bool MouseClick(MouseButton button, Rectangle area)
        {
            if (area.Contains(currMS.Position))
            {
                return MouseClick(button);
            }

            return false;
        }
        /// <summary>
        /// Detects if the mouse is being held down
        /// </summary>
        /// <param name="button">Button to check</param>
        public static bool MouseHold(MouseButton button)
        {
            if (button == MouseButton.Left)
            {
                return currMS.LeftButton == ButtonState.Pressed;
            }
            else if (button == MouseButton.Right)
            {
                return currMS.RightButton == ButtonState.Pressed;
            }
            else
            {
                return currMS.MiddleButton == ButtonState.Pressed;
            }
        }
        /// <summary>
        /// Detects if the mouse is being held down within an area
        /// </summary>
        /// <param name="button">Button to check</param>
        /// <param name="area">Rectangle to check</param>
        public static bool MouseHold(MouseButton button, Rectangle area)
        {
            if (area.Contains(currMS.Position))
            {
                return MouseHold(button);
            }

            return false;
        }
        /// <summary>
        /// Gets the current mouse position
        /// </summary>
        public static Point MousePos()
        {
            return currMS.Position;
        }
        /// <summary>
        /// Returns an angle in degrees between the mouse cursor and a given game object
        /// </summary>
        /// <param name="obj">Object to determine angle with</param>
        public static float MouseAngle(GameObject obj)
        {
            // Get component displacement between cursor and object
            float xDis = obj.Position.X - currMS.Position.X;
            float yDis = obj.Position.Y - currMS.Position.Y;

            return MathHelper.ToDegrees((float)Math.Atan2(yDis, xDis));
        }

        // GamePad Input - WIP

        // Movement Helpers -- Will implement GamePad to GetAxisRaw and a GetAxis method
        /// <summary>
        /// Returns value of axis with no smoothing
        /// </summary>
        /// <param name="axis">Axis movement is aligned on</param>
        public static float GetAxisRaw(Axis axis)
        {
            if (axis == Axis.Y)
            {
                // Return Y Axis value
                if (KeyHold(Keys.W))
                {
                    return 1f;
                }
                else if (KeyHold(Keys.S))
                {
                    return -1f;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                // Return X Axis value
                if (KeyHold(Keys.D))
                {
                    return 1f;
                }
                else if (KeyHold(Keys.A))
                {
                    return -1f;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// Not implemented - Do not use!
        /// </summary>
        /// <param name="axis">The axis movement is aligned on</param>
        public static float GetAxis(Axis axis)
        {
            return 0;
        }
    }
}

// - Genoah Martinelli