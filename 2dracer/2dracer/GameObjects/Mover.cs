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
        protected Vector2 velocity;         // linear rate of change vector
        protected Vector2 accel;            // linear acceleration vector

        protected float angularVelocity;    // angular rate of change
        protected float angularAccel;       // angulaer acceleration

        protected float mass;               // mass of the mover
        protected float moment;             // moment of inertia calculated using the mass and collider

        protected float dragFactor;         // the scalar factor of how aerodynamic this mover is
        protected float restitution;        // how bouncy this mover's collisions are (should be between 0 and 1)

        //protected Collider col;

        // Properties
        public Vector2 Velocity { get { return velocity; } }
        public Vector2 Accel { get { return accel; } }
        public float AngularVelocity { get { return angularVelocity; } }
        public float AngularAccel { get { return angularAccel; } }
        public float Mass { get { return mass; } }
        public float Moment { get { return moment; } }
        public float DragFactor { get { return dragFactor; } }
        public float Restitution { get { return restitution; } }

        // Constructors
        public Mover(GameObject g, Vector2 velocity, Vector2 accel, float angularVelocity, float angularAccel, float mass, float dragFactor, float restitution)
              : base(g)
        {
            this.velocity = velocity;
            this.accel = accel;

            this.angularVelocity = angularVelocity;
            this.angularAccel = angularAccel;

            this.mass = mass;
            moment = mass;          // CHANGE THIS TO COLLIDER.CALCULATEMOMENT ONCE IT IS COMPLETED

            this.dragFactor = dragFactor;
            this.restitution = restitution;
        }

        public Mover(GameObject g, Vector2 velocity, Vector2 accel, float angularVelocity, float angularAccel, float mass)
              : this(g, velocity, accel, angularVelocity, angularAccel, mass, 1, 1) { }

        public Mover(GameObject g, Vector2 velocity, Vector2 accel, float angularVelocity, float angularAccel)
              : this(g, velocity, accel, angularVelocity, angularAccel, 1) { }

        public Mover(GameObject g, Vector2 velocity, float angularVelocity)
              : this(g, velocity, Vector2.Zero, angularVelocity, 0f) { }

        public Mover(GameObject g)
              : this(g, Vector2.Zero, 0f) { }

        // Methods
        public override void Update()
        {
            UpdatePhysics();
        }

        // integrates this movers physics (position, velocity, acceleration)
        public void UpdatePhysics()
        {
            velocity += accel * (float)Game1.gameTime.ElapsedGameTime.TotalSeconds;
            accel *= 0;

            angularVelocity += angularAccel * (float)Game1.gameTime.ElapsedGameTime.TotalSeconds;
            angularAccel *= 0;

            if (Player.slowMo)
            {
                position += velocity * (float)Game1.gameTime.ElapsedGameTime.TotalSeconds / 2;
                rotation += angularVelocity * (float)Game1.gameTime.ElapsedGameTime.TotalSeconds / 2;
            }

            else
            {
                position += velocity * (float)Game1.gameTime.ElapsedGameTime.TotalSeconds;
                rotation += angularVelocity * (float)Game1.gameTime.ElapsedGameTime.TotalSeconds;
            }

        }

        // adds force to the center of the mover
        public void AddForce(Vector2 force)
        {
            accel += force / mass;
        }

        // adds torque to the mover's angular acceleration
        public void AddTorque(float torque)
        {
            angularAccel += torque / moment;
        }

        // computes the torque induced and applies the linear force and torque
        public void AddForceAtPos(Vector2 force, Vector2 posOffset)
        {
            // area of the parrallelogram formed by the force and lever arm
            float torque = posOffset.X * force.Y - posOffset.Y * force.X;

            AddForce(force);
            AddTorque(torque);
        }

        // impulses are used in collision response
        // these work just like forces accept applying an instantaneous change to the velocity
        public void AddImpulse(Vector2 impulse)
        {
            velocity += impulse / mass;
        }

        public void AddRotationalImpulse(float impulse)
        {
            angularVelocity += impulse / moment;
        }

        public void AddImpulseAtPos(Vector2 force, Vector2 posOffset)
        {
            // area of the parrallelogram formed by the impulse and lever arm
            float rotationalImpulse = posOffset.X * force.Y - posOffset.Y * force.X;

            AddImpulse(force);
            AddRotationalImpulse(rotationalImpulse);
        }
    }
}

// Matthew Soriano