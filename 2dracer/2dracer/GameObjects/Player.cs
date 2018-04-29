using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using _2dracer.GameObjects;
using _2dracer.Managers;
using _2dracer.UI;

namespace _2dracer
{
    public class Player : Car
    {
        #region Fields
        private static TextElement scoreText;
        
        private Turret turret;
        
        public static bool slowMo = false;
        public static Vector2 PlayerPos;

        /// <summary>
        /// The node of the tile the player just stepped on
        /// </summary>
        public static Node playerNode;
        #endregion

        #region Properties
        public static double TimeJuice { get; private set; }
        public static int Health { get; private set; }
        public static double Score { get; private set; }
        public static Vector2 playerVelocity;
        #endregion

        #region Constructor
        public Player(Vector2 position) 
            : base (position, 0, LoadManager.Sprites["RedCar"], new Vector2(Options.ScreenWidth / 12, Options.ScreenHeight / 13.5f), 400, 100, 250, 1000)
        {
            Health = 100;
            TimeJuice = 0;
            Score = 0;
            GameMaster.Instantiate(turret = new Turret());

            if(MapElements.Map.Nodes[(int)this.Position.X / 768, (int)this.Position.X / 768] != null)
            {
                playerNode = MapElements.Map.Nodes[(int)this.Position.X / 768, (int)this.Position.X / 768];
                Console.WriteLine("PLAYERNODE: " + playerNode.ToString());
            }
            else
            {
                foreach(Node n in MapElements.Map.Nodes)
                {
                    if(n != null)
                    {
                        playerNode = n;
                        return;
                    }
                }
            }
        }
        #endregion

        #region Methods
        public override void Update()
        {
            float xAxis = Input.GetAxisRaw(Axis.X);
            float yAxis = Input.GetAxisRaw(Axis.Y);

            if (yAxis != 0)
            {
                Accelerate(yAxis);          // Accelerates player
            }
            else
            {
                Decelerate();               // Decelerates player
            }

            if (xAxis != 0)
            {
                Turn(xAxis);                // Turns player
            }
            else
            {
                angularVelocity *= 0.9f;
            }

            if (Input.KeyHold(Keys.Space))
            {
                Brake();
            }

            if (Input.KeyHold(Keys.R))
            {
                Health -= 1;
            }

            if (Health <= 0)
            {
                Health = 0;
                Game1.GameState = GameState.GameOver;
            }

            AdjustVelocity();               // Adjusts player velocity

            Juice();

            base.Update();

            int thisX = ((int)this.Position.X / 768);
            int thisY = ((int)this.Position.Y / 768);

            //Console.WriteLine("XINDEX: " + thisX + "|YINDEX: " + thisY);
            if (MapElements.Map.Nodes[thisX, thisY] != null)
            {
                playerNode = MapElements.Map.Nodes[thisX, thisY];
                //Console.WriteLine("PLAYERNODE: " + playerNode.ToString());
            }
            else
            {
                //Console.WriteLine("Nodes[" + thisX + "," + thisY+ "] is null!");
            }
            turret.MoveTurret(position);
            
            playerVelocity = velocity;
        }

        public static void Draw3D(Texture2D texture, float rotation)
        {
            double fieldOfView = (3.14159 / 4) * Options.Graphics.GraphicsDevice.Viewport.Width / 1500;

            effect.Projection = Matrix.CreatePerspectiveFieldOfView((float)fieldOfView, Options.Graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 200f);
            effect.View = Matrix.CreateLookAt(new Vector3(Vector2.Zero, 3.3f), Vector3.Zero, Vector3.Up);


            //scale * rotation * position
            effect.World =
                Matrix.CreateScale(0.1f, 0.1f, 0.09f) *
                Matrix.CreateRotationX(3.14159f/2) * 
                Matrix.CreateRotationZ(-3.14159f / 2 + rotation) *                  // Change this one to equal rotation of 2D car
                Matrix.CreateTranslation(worldPos.X, worldPos.Y, 0f);

            effect.Texture = LoadManager.Sprites["Wheels"];
            model.Meshes[0].Draw();


            BasicEffect effect2 = (BasicEffect)model.Meshes[1].Effects[0];
            effect2.Projection = effect.Projection;
            effect2.View = effect.View;
            effect2.World = effect.World;

            effect2.Texture = texture;
            model.Meshes[1].Draw();
        }

        /// <summary>
        /// Controls the timejuice mechanic
        /// </summary>
        private void Juice()
        {
            if (Input.KeyTap(Keys.E))
                slowMo = !slowMo;

            Score += Game1.gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

            if (!slowMo && TimeJuice < 10)
                TimeJuice += Game1.gameTime.ElapsedGameTime.TotalMilliseconds / 2000;

            if (slowMo)
                TimeJuice -= Game1.gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

            if (TimeJuice <= 0)
            {
                TimeJuice = 0;
                slowMo = false;
            }
        }

        /// <summary>
        /// Creates the HUD of the player.
        /// </summary>
        public static void CreateHUD()
        {
            UIManager.Add(scoreText = new TextElement(new Point(50, 450), true, 0.25f, "playerScore", "Score : " + Score));
        }
        #endregion
    }
}

// Niko Procopi
