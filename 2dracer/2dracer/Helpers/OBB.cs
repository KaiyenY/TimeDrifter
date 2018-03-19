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
        private Vector2[] normals;      // the two normals of this OBB in LOCAL space  (used in SAT algorithm)

        // properties
        public float RotOffset { get { return rotOffset; } }
        public float WorldRot { get { return parent.Rotation + rotOffset; } }
        public Vector2[] WorldVertices
        {
            get
            {
                Vector2[] worldVertices = new Vector2[vertices.Length];
                for (int i = 0; i < vertices.Length; i++)
                {
                    worldVertices[i] = RotateVector(normals[i], WorldRot);
                }
                return worldVertices;
            }
        }
        public Vector2[] WorldNormals
        {
            get
            {
                Vector2[] worldNormals = new Vector2[normals.Length];
                for(int i = 0; i < normals.Length; i++)
                {
                    worldNormals[i] = RotateVector(normals[i], WorldRot);
                }
                return worldNormals;
            }
        }

        // constructors
        public OBB(Vector2 posOffset, float rotOffset, float width, float height, GameObject parent)
            : base(posOffset, width, height, parent)
        {
            this.rotOffset = rotOffset;

            vertices = new Vector2[] { new Vector2(halfWidth,  halfHeight),
                                       new Vector2(-halfWidth, halfHeight),
                                       new Vector2(-halfWidth, -halfHeight),
                                       new Vector2(halfWidth,  -halfHeight) };

            normals = new Vector2[] { Vector2.UnitX,
                                      Vector2.UnitY,
                                      -Vector2.UnitX,
                                      -Vector2.UnitY };
        }

        public OBB(float width, float height, GameObject parent)
            : this(Vector2.Zero, 0f, width, height, parent) { }

        // methods
        public override AABB GetAABB()
        {
            float maxX, minX;
            float maxY, minY;

            // Get the projections of this OBB onto the X and Y axis
            GetProjection(Vector2.UnitX, out maxX, out minX);
            GetProjection(Vector2.UnitY, out maxY, out minY);
            // Return the AABB whose height and width both enclose this OBB's projections
            return new AABB(posOffset, maxX * 2, maxY * 2, parent);
        }

        /// <summary>
        /// Returns that maximum and minimum value of the projection of this OBB onto a given axis
        /// </summary>
        /// <param name="axis">The axis of projection in WORLD space</param>
        /// <param name="max">The max value of the projection relative to the center of this OBB</param>
        /// <param name="min">The min value of the projection relative to the center of this OBB</param>
        public void GetProjection(Vector2 axis, out float max, out float min)
        {
            axis.Normalize();
            // find how far the farthest vertex is in the given direction
            max = Vector2.Dot(GetSupportPoint(axis), axis);
            // find how far the farthest vertex is in the opposite direction
            min = Vector2.Dot(GetSupportPoint(-axis), axis);
        }

        /// <summary>
        /// Gets the point on this OBB that is furthest in a given direction
        /// </summary>
        /// <param name="dir">The direction in WORLD space</param>
        /// <returns>Returns the vertice in WORLD space that is farthest in that direction</returns>
        public Vector2 GetSupportPoint(Vector2 dir)
        {
            dir.Normalize();                        // just checking this is a unit vector
            float bestDist = float.NegativeInfinity;
            Vector2[] worldVertices = WorldVertices;
            Vector2 bestVertex = worldVertices[0];       // we assume the first vertex is the best

            // we start at 1 because we assumed te first was the best
            for(int i = 1; i < worldVertices.Length; i++)
            {
                float dist = Vector2.Dot(worldVertices[i], dir);
                if (dist > bestDist)
                {
                    bestDist = dist;
                    bestVertex = worldVertices[i];
                }
            }

            return bestVertex;
        }
    }
}

// Matthew Soriano