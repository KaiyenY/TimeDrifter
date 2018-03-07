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

        public int Heuristic { get; set; } //Holds the heuristic that the A* algorithm will use

        public Node Parent { get; set; } //The node from which the heuristic value is coming from

        #region Constructors
        public Node(Point location, List<Node> neighbors)
        {
            Location = location;
            Neighbors = neighbors;
        }

        public Node(Point location)
        {
            Location = location; 
        }

        public Node(Node n, int Heuristic)
        {
            this.Location = n.Location;
            this.Heuristic = n.Heuristic;
        }

        public Node(Node n)
        {
            this.Location = n.Location;
        }
        #endregion

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

        public int DistanceFrom(Node otherNode) //utility method to return the distance from one point to another
        {
            return (int)(otherNode.Location - this.Location).ToVector2().Length();
        }

        public override string ToString() //to make things easier
        {
            return string.Format("({0}, {1})", this.Location.X, this.Location.Y);
        }

        public bool Equals(Node n) //Helper method to see whether a node equals another
        {
            if((n.Location.X == this.Location.X) && (n.Location.Y == this.Location.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
