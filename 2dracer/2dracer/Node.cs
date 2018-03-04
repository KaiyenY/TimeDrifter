using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dracer
{
    class Node
    {
        public Point Location { get; set; } //Location in space

        public List<Node> Neighbors = new List<Node>(); //Nodes that can be travelled to from this node

        private int DistanceToTarget { get; set; } //holds the distance to the target

        public int Heuristic { get; set; } //Holds the heuristic that the A* algorithm will use

        public Node Parent { get; set; } //The node from which the heuristic value is coming from

        //Constructors
        public Node(Point location, List<Node> neighbors)
        {
            Location = location;
            Neighbors = neighbors;
        }

        public Node(Point location)
        {
            Location = location; 
        }

        public Node(Node n)
        {
            this.Location = n.Location;
        }

        public void PopulateNeighborsList(params Node[] neighbors) //Fills the list of neighbors
        {
            foreach(Node n in neighbors)
            {
                Neighbors.Add(n);
            }
        }

        public void AssignHeuristics(Node target) //Gives all of this node's neighbors a heuristic for A* to use
        {
            foreach(Node n in Neighbors)
            {
                n.Heuristic = n.DistanceFrom(this) + target.DistanceFrom(n);
            }
        }

        public void CalcDistanceToTarget(Node target)//might be redundant with DistanceFrom() method will check later.
        {
            int xDiff = target.Location.X - Location.X;
            int yDiff = target.Location.Y - Location.Y;
            Vector2 vectorToTarget = new Vector2(xDiff, yDiff);
            DistanceToTarget = (int)vectorToTarget.Length();
        }

        public int DistanceFrom(Node otherNode) //utility method to return the distance from one point to another
        {
            return (int)(otherNode.Location - this.Location).ToVector2().Length();
        }

        public override string ToString() //to make things easier
        {
            return string.Format("({0}, {1})", this.Location.X, this.Location.Y);
        }
    }
}
