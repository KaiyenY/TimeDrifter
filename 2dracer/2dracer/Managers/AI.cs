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
    public static class AI
    {
        // Fields
        
        public static List<Node> nodes = new List<Node>(); //List of nodes to test A* algorithm

        private static Queue<Node> pathToGive = new Queue<Node>(); //Field for the recursive algorithm to use

        public static Queue<Node> Pathfind(Node start, Node target) //Implementation of A* fingers crossed
        {
            
            pathToGive = new Queue<Node>(); //Reset the path property
            List<Node> closedSet = new List<Node>();
            List<Node> openSet = new List<Node>(); //List of discovered nodes
            openSet.Add(start);
           
            start.gScore = 0; //cost of going from start to itself is 0
            start.fScore = target.DistanceFrom(start); //straight-line distance to the target

            //TODO: Finish AI's Main Pathfind function
                while (openSet.Count > 0) //Iterate while the path is not empty
                {
                Node current = new Node();
                current.fScore = int.MaxValue; //will point to the node in the open set with the lowest fScore
                    foreach(Node toCheck in openSet) //Check for lowest node in the set of all 'discovered' nodes
                    {
                        if(toCheck.fScore < current.fScore) 
                        {
                        current = toCheck;
                        }
                    }

                if (current == target) //Found the Node!
                { 
                    Console.WriteLine("Reconstructing path...");

                    Queue<Node> path = new Queue<Node>(ReconstructPath(start, current).Reverse());
                    
                    foreach(Node toReset in closedSet) //Reset the properties of every node that's been altered
                    {
                        toReset.Reset();
                    }

                    foreach(Node toReset in openSet)
                    {
                        toReset.Reset();
                    }

                    return path;
                }

                //Move current to the closed set
                openSet.Remove(current);
                closedSet.Add(current);

                foreach(Node neighbor in current.Neighbors)
                {
                    if(!closedSet.Contains(neighbor))//if this neighbor hasn't been checked yet
                    {
                        if(!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor); //Discover a new node from the current node's neighbors
                        }

                        int tentgScore = current.gScore + neighbor.DistanceFrom(current); //Represents the individual path segment distance

                        if(!(tentgScore > neighbor.gScore)) //recording the best path
                        {
                            neighbor.Parent = current;
                            neighbor.gScore = tentgScore;
                            neighbor.fScore = neighbor.gScore + target.DistanceFrom(neighbor);
                        }
                    }
                }
                }
            return null;
        }

        /// <summary>
        /// Recursively rebuilds the path to give to the enemy that needs it
        /// </summary>
        private static Queue<Node> ReconstructPath(Node start, Node current)
        {
            if(current == start) //has been passed the start, end the algorithm
            {
                return pathToGive;
            }
            else //Recursive case: add current to the queue and then see whether to add current's parent
            {
                pathToGive.Enqueue(current);
                Console.WriteLine("ADDING " + current.ToString() + " to the path");
                return ReconstructPath(start, current.Parent);
            }
            
        }  

        
    }
}
//Ruben Young