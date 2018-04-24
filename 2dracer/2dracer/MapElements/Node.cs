using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using _2dracer.MapElements;

namespace _2dracer
{
    public class Node
    { 
        //Fields
        public List<Node> Neighbors = new List<Node>(); //Nodes that can be travelled to from this node

        private int _fScore = int.MaxValue; //default value is infinity

        private int _gScore = int.MaxValue; //default value is infinity

        public Node Parent { get; set; } //Which node to come from in the most efficient route

        //Properties
        public Point Location { get; set; } //Location in space

        public int[] Index { get; set; }    // Index on map

        #region Defining G and F Score Properties
        public int gScore {
            get
            {
                return _gScore;
            }
            set
            {
                _gScore = value;
            }
        }

        public int fScore {
            get
            {
                return _fScore;
            }

            set
            {
                _fScore = value;
            }
        }
        #endregion

        public Color Color { get; set; }//TODO: delete property, just used to visualize pathfinding

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


        public Node(Node n)//Copies all of the passed Node's attributes
        {
            this.Location = n.Location;
            this.Neighbors = new List<Node>(n.Neighbors);
            this.fScore = n.fScore;
            this.gScore = n.gScore;
            this.Parent = n.Parent;
            this.Color = n.Color;
        }

        public Node() { } //Empty Constructor for an empty soul
        #endregion

        #region Methods
        public void PopulateNeighborsList()     // Populates list of neighbors using indices
        {
            if (Map.Nodes[Index[0] + 1, Index[1]] != null)
            {
                Neighbors.Add(Map.Nodes[Index[0] + 1, Index[1]]);
                Console.WriteLine("Added " + Map.Nodes[Index[0] + 1, Index[1]] + " to the neighbors list of " + this.ToString());
            }
            if (Map.Nodes[Index[0] - 1, Index[1]] != null)
            {
                Neighbors.Add(Map.Nodes[Index[0] - 1, Index[1]]);
            }
            if (Map.Nodes[Index[0], Index[1] + 1] != null)
            {
                Neighbors.Add(Map.Nodes[Index[0], Index[1] + 1]);
            }
            if (Map.Nodes[Index[0], Index[1] - 1] != null)
            {
                Neighbors.Add(Map.Nodes[Index[0], Index[1] - 1]);
            }
        }

        public void PopulateNeighborsList(params Node[] neighbors) //Fills the list of neighbors
        {
            foreach(Node n in neighbors)
            {
                Neighbors.Add(n);
            }
        }

        public int DistanceFrom(Node otherNode) //utility method to return the distance from one point to another
        {
            return (int)(otherNode.Location - this.Location).ToVector2().Length();
        }

        public override string ToString() //to make things easier
        {
            return Location.ToString();
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

        public void Reset() //resets the values of this node to make it ripe for searching again
        {
            this.fScore = int.MaxValue;
            this.gScore = int.MaxValue;
            this.Parent = null;
        }
        #endregion
    }
}
//Ruben Young