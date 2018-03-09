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
    class OBB : AABB
    {
        // fields
        private float rotOffset;        // the rotational offset from the +x axis

        private Vector2[] vertices;     // the four corners of this OBB in LOCAL space (used in getting support points)
        private Vector2[] normals;      // the two normals of this OBB in WORLD space  (used in SAT algorithm)

        // properties
        public float RotOffset { get { return rotOffset; } }
        public float WorldRot { get { return parent.Rotation + rotOffset; } }
        public Vector2[] Normals { get { return normals; } }

        // constructors
        public OBB(Vector2 posOffset, float rotOffset, float width, float height, GameObject parent)
            : base(posOffset, width, height, parent)
        {
            this.rotOffset = rotOffset;
        }

        public OBB(float width, float height, GameObject parent)
            : this(Vector2.Zero, 0f, width, height, parent) { }

        // methods
        public override float CalculateMoment(float mass)
        {
            throw new NotImplementedException();
        }

        public override AABB GetAABB()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns that maximum and minimum value of the projection of this OBB onto a given axis
        /// </summary>
        /// <param name="axis">The axis of projection in WORLD space</param>
        /// <param name="max">The max value of the projection relative to the center of this OBB</param>
        /// <param name="min">The min value of the projection relative to the center of this OBB</param>
        public void GetProjection(Vector2 axis, out float max, out float min)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the point on this OBB that is furthest in a given direction
        /// </summary>
        /// <param name="dir">The direction in LOCAL space</param>
        /// <returns>Returns the vertice in LOCAL space that is farthest in that direction</returns>
        private Vector2 GetSupportPoint(Vector2 dir)
        {
            throw new NotImplementedException();
        }
    }
}

// Matthew Soriano