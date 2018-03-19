using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2dracer.Helpers
{
    class Circle : Collider
    {
        // fields
        private float radius;

        // properties
        public float Radius { get { return radius; } }

        // constructors
        public Circle(Vector2 posOffset, float radius, GameObject parent)
               : base(posOffset, parent)
        {
            this.radius = radius;
        }

        public Circle(float radius, GameObject parent)
               : this(Vector2.Zero, radius, parent) { }

        // methods
        public override float CalculateMoment(float mass)
        {
            // lots of math to calculate the moment of inertia of a thin disk
            return (float)(0.5 * mass * Math.Pow(radius, 2) + mass * posOffset.LengthSquared());
        }

        public override AABB GetAABB()
        {
            return new AABB(posOffset, radius * 2, radius * 2, parent);
        }
    }
}

// Matthew Soriano