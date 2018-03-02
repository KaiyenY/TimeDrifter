using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2dracer
{
    public class Mover : GameObject
    {
        // Fields
        protected Vector2 velocity;
        protected Vector2 accel;

        protected float angularVelocity;
        protected float angularAccel;

        protected float mass;
        protected float dragFactor;

        //protected Collider col;

        // Properties
        public Vector2 Velocity { get { return velocity; } }
        public Vector2 Accel { get { return accel; } }
        public float AngularVelocity { get { return angularVelocity; } }
        public float AngularAccel { get { return angularAccel; } }
        public float Mass { get { return mass; } }
        public float DragFactor { get { return dragFactor; } }

        // Constructors
        public Mover(GameObject g, Vector2 velocity, Vector2 accel, float angularVelocity, float angularAccel, float mass, float dragFactor)
              : base(g)
        {
            size = new Vector2(100, 100);

            this.velocity = velocity;
            this.accel = accel;

            this.angularVelocity = angularVelocity;
            this.angularAccel = angularAccel;

            this.mass = mass;
            this.dragFactor = dragFactor;
        }

        public Mover(GameObject g, Vector2 velocity, Vector2 accel, float angularVelocity, float angularAccel, float mass)
              : this(g, velocity, accel, angularVelocity, angularAccel, mass, 1) { }

        public Mover(GameObject g, Vector2 velocity, Vector2 accel, float angularVelocity, float angularAccel)
              : this(g, velocity, accel, angularVelocity, angularAccel, 1) { }

        public Mover(GameObject g, Vector2 velocity, float angularVelocity)
              : this(g, velocity, Vector2.Zero, angularVelocity, 0f) { }

        public Mover(GameObject g)
              : this(g, Vector2.Zero, 0f) { }
        
        public Mover()
              : this(new GameObject()) { }

        // Methods
        public override void Update(GameTime gameTime)
        {
            UpdatePhysics(gameTime);
        }

        public void UpdatePhysics(GameTime gameTime)
        {
            velocity += accel * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            accel *= 0;

            angularVelocity += angularAccel * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotation += angularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            angularAccel *= 0;
        }

        public void AddForce(Vector2 force)
        {
            accel += force / mass;
        }

        public void AddTorque(float torque)
        {
            angularAccel += torque / mass;    // REPLACE MASS WITH MOMENT
        }                                     // moment not implemented until Collider class is

        public void AddForceAtPos(Vector2 force, Vector2 posOffset)
        {
            // NOT FULLY IMPLEMENTED
            // THIS NEEDS TO BE FIXED
            // orthogonal projection onto the lever arm
            Vector2 linearForce = (Vector2.Dot(force, posOffset) / Vector2.Dot(posOffset, posOffset)) * posOffset;
            // area of the parrallelogram formed by the force and lever arm
            float torque = posOffset.X * force.Y - posOffset.Y * force.X;

            AddForce(linearForce);
            AddTorque(torque);
        }
    }
}

// Matthew Soriano