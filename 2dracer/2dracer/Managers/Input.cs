using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace _2dracer
{
    public enum Axis { X, Y }
    public enum Control { Left, Right }
    public enum DPad { Up, Down, Left, Right }
    public enum MouseButton { Left, Right, Middle }

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
        /// Detects if mouse was just released
        /// </summary>
        /// <param name="button">Button to check</param>
        public static bool MouseReleased(MouseButton button)
        {
            if (button == MouseButton.Left)
            {
                return currMS.LeftButton == ButtonState.Released && prevMS.LeftButton == ButtonState.Pressed;
            }
            else if (button == MouseButton.Right)
            {
                return currMS.RightButton == ButtonState.Released && prevMS.RightButton == ButtonState.Pressed;
            }
            else
            {
                return currMS.MiddleButton == ButtonState.Released && prevMS.MiddleButton == ButtonState.Pressed;
            }
        }
        /// <summary>
        /// Detects if mouse was just released within an area
        /// </summary>
        /// <param name="button">Button to check</param>
        /// <param name="area">Area to check</param>
        public static bool MouseReleased(MouseButton button, Rectangle area)
        {
            if (area.Contains(currMS.Position)) //Check if within area
            {
                return MouseReleased(button);
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Gets the current mouse position
        /// </summary>
        public static Point MousePos()
        {
            return currMS.Position;
        }
        /// <summary>
        /// Returns an angle in radians between the mouse cursor and a given game object
        /// </summary>
        /// <param name="obj">Object to determine angle with</param>
        public static float MouseAngle(GameObject obj)
        {
            // Get component displacement between cursor and object
            float xDis = MousePos().X - obj.Position.X;
            float yDis = MousePos().Y - obj.Position.Y;

            return (float)Math.Atan2(yDis, xDis);
        }

        // GamePad Input
        /// <summary>
        /// Detects if a button on a controller is held down
        /// </summary>
        /// <param name="button">Button to check</param>
        public static bool ControlHold(Buttons button)
        {
            return currGS.IsButtonDown(button);
        }
        /// <summary>
        /// Detects if a button on a controller is pressed
        /// </summary>
        /// <param name="button">Button to check</param>
        public static bool ControlPress(Buttons button)
        {
            return ControlHold(button) && prevGS.IsButtonUp(button);
        }
        /// <summary>
        /// Detects if a button was released after being pressed
        /// </summary>
        /// <param name="button">Button to check</param>
        public static bool ControlRelease(Buttons button)
        {
            return currGS.IsButtonUp(button) && prevGS.IsButtonDown(button);
        }
        /// <summary>
        /// Returns analog value of trigger press
        /// </summary>
        /// <param name="trigger">Trigger to check</param>
        public static float ControlTrigger(Control trigger)
        {
            if (trigger == Control.Left)
            {
                return currGS.Triggers.Left;
            }
            else
            {
                return currGS.Triggers.Right;
            }
        }
        /// <summary>
        /// Grabs axis values from controller thumbsticks
        /// </summary>
        /// <param name="axis">Axis movement is aligned on</param>
        /// <param name="stick">Which thumbstick to check</param>
        public static float ControlSticks(Axis axis, Control stick)
        {
            if (stick == Control.Left && axis == Axis.X)
            {
                return currGS.ThumbSticks.Left.X;       // Left X axis
            }
            else if (stick == Control.Left && axis == Axis.Y)
            {
                return currGS.ThumbSticks.Left.Y;       // Left Y axis
            }
            else if (stick == Control.Right && axis == Axis.X)
            {
                return currGS.ThumbSticks.Right.X;      // Right X axis
            }
            else
            {
                return currGS.ThumbSticks.Right.Y;      // Right Y Axis
            }
        }
        /// <summary>
        /// Detects if a DPad button is held down
        /// </summary>
        /// <param name="direction">Direction on DPad to check</param>
        public static bool ControlDPadHold(DPad direction)
        {
            switch (direction)
            {
                case DPad.Up:
                    return currGS.DPad.Up == ButtonState.Pressed;

                case DPad.Down:
                    return currGS.DPad.Down == ButtonState.Pressed;

                case DPad.Left:
                    return currGS.DPad.Left == ButtonState.Pressed;

                default:
                    return currGS.DPad.Right == ButtonState.Pressed;
            }
        }
        /// <summary>
        /// Detects if a DPad button is pressed
        /// </summary>
        /// <param name="direction">Direction on DPad to check</param>
        public static bool ControlDPadPress(DPad direction)
        {
            switch (direction)
            {
                case DPad.Up:
                    return ControlDPadHold(direction) && prevGS.DPad.Up == ButtonState.Released;

                case DPad.Down:
                    return ControlDPadHold(direction) && prevGS.DPad.Down == ButtonState.Released;

                case DPad.Left:
                    return ControlDPadHold(direction) && prevGS.DPad.Left == ButtonState.Released;

                default:
                    return ControlDPadHold(direction) && prevGS.DPad.Right == ButtonState.Released;
            }
        }
        /// <summary>
        /// Returns whether a controller is connected or not
        /// </summary>
        public static bool ControlConnected()
        {
            return currGS.IsConnected;
        }
        /// <summary>
        /// Determines the angle given by the Right Thumbstick for shooting
        /// </summary>
        public static float ControlAngle()
        {
            float x = ControlSticks(Axis.X, Control.Right);
            float y = ControlSticks(Axis.Y, Control.Right);
            
            return (float)Math.Atan2(-y, x);
        }


        // Movement Helpers
        /// <summary>
        /// Returns value of axis with no smoothing
        /// </summary>
        /// <param name="axis">Axis movement is aligned on</param>
        public static float GetAxisRaw(Axis axis)
        {
            if (axis == Axis.Y)
            {
                // Return Y Axis value
                if (KeyHold(Keys.W) || ControlTrigger(Control.Right) >= 0.25f)
                {
                    return 1f;
                }
                else if (KeyHold(Keys.S) || ControlTrigger(Control.Left) >= 0.25f)
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
                if (KeyHold(Keys.D) || ControlSticks(Axis.X, Control.Left) >= 0.25f)
                {
                    return 1f;
                }
                else if (KeyHold(Keys.A) || ControlSticks(Axis.X, Control.Left) <= -0.25f)
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
        /// Returns analog value of an axis input
        /// </summary>
        /// <param name="axis">The axis movement is aligned on</param>
        public static float GetAxis(Axis axis)
        {
            return ControlSticks(axis, Control.Left);
        }
    }
}

// - Genoah Martinelli