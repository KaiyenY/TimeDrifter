using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2dracer
{
    /// <summary>
    /// Handles input from the player
    /// </summary>
    public class Input
    {
        // Fields
        private static GamePadState currGS, prevGS;
        private static KeyboardState currKS, prevKS;
        private static MouseState currMS, prevMS;

        // Constructor

        // Methods
        /// <summary>
        /// Update the input states
        /// </summary>
        public void Update()
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

        /// <summary>
        /// Checks for key hold
        /// </summary>
        public static bool HoldKey(Keys key)
        {
            return currKS.IsKeyDown(key);
        }

        /// <summary>
        /// Checks for a key tap
        /// </summary>
        public static bool TapKey(Keys key)
        {
            return HoldKey(key) && prevKS.IsKeyUp(key);
        }
        
        /// <summary>
        /// Moves the player
        /// * UPDATE WITH PHYSICS LATER I GUESS *
        /// </summary>
        public void MovePlayer(Player player, float moveSpeed, float rotSpeed)
        {
            float angle = player.Angle;

            // Move player forwards
            if (currKS.IsKeyDown(Keys.W) || 
                currGS.ThumbSticks.Left.Y == 1.0f)
            {
                player.posX += (float)Math.Cos(MathHelper.ToRadians(angle)) * moveSpeed;
                player.posY += (float)Math.Sin(MathHelper.ToRadians(angle)) * moveSpeed;
            }

            // Move player backwards
            if (currKS.IsKeyDown(Keys.S))
            {
                player.posX -= (float)Math.Cos(MathHelper.ToRadians(angle)) * moveSpeed;
                player.posY -= (float)Math.Sin(MathHelper.ToRadians(angle)) * moveSpeed;
            }

            // Rotate player left
            if (currKS.IsKeyDown(Keys.A))
            {
                player.Angle -= rotSpeed;
            }

            // Rotate player right
            if (currKS.IsKeyDown(Keys.D))
            {
                player.Angle += rotSpeed;
            }
        }
    }
}
