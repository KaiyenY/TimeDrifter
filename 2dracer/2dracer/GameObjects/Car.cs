using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using _2dracer.Managers;

namespace _2dracer.GameObjects
{
    /// <summary>
    /// Controls how car objects behave
    /// </summary>
    public class Car : Mover
    {
        #region Fields
        protected float bFriction;            // Magnitude of brake friction applied to car
        protected float friction;             // Magnitude of friction applied to car
        protected float horsePower;           // Magnitude of force from "engine" of car (LESS IN REVERSE)
        protected float prevDir;              // Car's previous direction state
        protected float prevRotation;         // Car's previous rotation state
        protected float topSpeed;             // The maximum speed the car can go
        #endregion

        #region Properties
        public float TopSpeed { get { return topSpeed; } }
        #endregion

        #region Constructors
        public Car(Vector2 position, float rotation, string spritePath, Vector2 size, float bFriction, float friction, float horsePower, float topSpeed)
            : base(new GameObject(position, rotation, spritePath, size))
        {
            this.bFriction = bFriction;
            this.friction = friction;
            this.horsePower = horsePower;
            this.topSpeed = topSpeed;
            prevRotation = rotation;
            prevDir = 0;
        }

        public Car(Vector2 position, string spritePath, Vector2 size)
            : this(position, 0, spritePath, size, 250, 100, 250, 500) { }
        #endregion

        #region Methods
        /// <summary>
        /// Accelerates the car in the current direction it is facing
        /// </summary>
        protected void Accelerate(float direction)
        {
            float xForce = (float)Math.Cos(rotation) * horsePower * direction;
            float yForce = (float)Math.Sin(rotation) * horsePower * direction;
            AddForce(new Vector2(xForce, yForce));

            CapVelocity();
        }

        /// <summary>
        /// Decelerates the car
        /// </summary>
        protected void Decelerate()
        {
            // Determine angle of deceleration (opposite of velocity)
            double theta = Math.Atan2(velocity.Y, velocity.X) - Math.PI;

            // Get component forces
            float xForce = (float)Math.Cos(theta) * friction;
            float yForce = (float)Math.Sin(theta) * friction;

            // Apply the force
            AddForce(new Vector2(xForce, yForce));
        }

        /// <summary>
        /// Applies brakes to the car
        /// </summary>
        protected void Brake()
        {
            // Determine angle of deceleration (opposite of velocity)
            double theta = Math.Atan2(velocity.Y, velocity.X) - Math.PI;

            // Get component forces
            float xForce = (float)Math.Cos(theta) * bFriction;
            float yForce = (float)Math.Sin(theta) * bFriction;

            // Apply the force
            AddForce(new Vector2(xForce, yForce));
        }

        /// <summary>
        /// Helps prevent slipping
        /// </summary>
        protected void AdjustVelocity()
        {
            // TODO: Add better physics
            float rotDiff = prevRotation - rotation;

            velocity = new Vector2(
                (float)(velocity.X * Math.Cos(-rotDiff) - velocity.Y * Math.Sin(-rotDiff)),
                (float)(velocity.X * Math.Sin(-rotDiff) + velocity.Y * Math.Cos(-rotDiff)));

            prevRotation = rotation;
        }

        /// <summary>
        /// Caps velocity
        /// </summary>
        protected void CapVelocity()
        {
            if (velocity.Length() >= topSpeed)
            {
                velocity = new Vector2(
                    topSpeed * (float)Math.Cos(rotation),
                    topSpeed * (float)Math.Sin(rotation));
            }
        }

        /// <summary>
        /// Turns the car according to a turning radius
        /// </summary>
        protected void Turn(float direction)
        {
            // TODO: Add better physics
            if (prevDir != direction)
            {
                angularVelocity *= 0;
            }
            
            if (Math.Abs(angularVelocity) <= 2)
            {
                AddTorque(direction * 1.25f);
            }

            prevDir = direction;
        }
        #endregion
    }
}
