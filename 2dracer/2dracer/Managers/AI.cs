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
    public class AI
    {
        // Fields
        private List<Enemy> enemies;

        public List<Node> nodes = new List<Node>(); //List of nodes to test A* algorithm

        private Queue<Node> testQueue = new Queue<Node>(); //TEMPORARY queue to test giving instructions to enemies

        private Queue<Node> pathToGive = new Queue<Node>();

        // Constructors
        public AI(List<Enemy> enemies)
        {
            this.enemies = enemies;

            #region node populating
            //Create nodes
            Node nodeStart = new Node(new Point(400, 200));
            Node node1 = new Node(new Point(600, 200));
            Node node2 = new Node(new Point(600, 400));
            Node node3 = new Node(new Point(800, 200));
            Node node4 = new Node(new Point(1000, 200));
            Node node5 = new Node(new Point(600, 600));
            Node node6 = new Node(new Point(800, 400));
            Node node7 = new Node(new Point(1000, 400));
            Node node8 = new Node(new Point(800, 600));
            Node node9 = new Node(new Point(1000, 600));
            Node node10 = new Node(new Point(600, 700));

            //Populate each node's list of neighbors sigh
            nodeStart.PopulateNeighborsList(node1);
            node1.PopulateNeighborsList(nodeStart, node3, node2);
            node2.PopulateNeighborsList(node1, node6, node5);
            node3.PopulateNeighborsList(node1, node6, node4);
            node4.PopulateNeighborsList(node3, node7);
            node5.PopulateNeighborsList(node2, node8, node10);
            node6.PopulateNeighborsList(node2, node3, node7, node8);
            node7.PopulateNeighborsList(node4, node6, node9);
            node8.PopulateNeighborsList(node6, node5, node9);
            node9.PopulateNeighborsList(node7, node8);
            node10.PopulateNeighborsList(node5);

            //Populate AI class' list of nodes
            nodes.Add(nodeStart);
            nodes.Add(node1);
            nodes.Add(node2);
            nodes.Add(node3);
            nodes.Add(node4);
            nodes.Add(node5);
            nodes.Add(node6);
            nodes.Add(node7);
            nodes.Add(node8);
            nodes.Add(node9);
            nodes.Add(node10);


            //Now for the Queue
            testQueue.Enqueue(new Node(new Point(200, 400)));
            testQueue.Enqueue(node1);
            testQueue.Enqueue(node2);
            testQueue.Enqueue(node3);
            testQueue.Enqueue(node4);
            testQueue.Enqueue(node5);
            testQueue.Enqueue(node6);
            testQueue.Enqueue(node7);
            testQueue.Enqueue(node8);
            testQueue.Enqueue(node9);
            testQueue.Enqueue(node10);
            #endregion

            foreach (Node colornode in nodes)
            {
                colornode.Color = Color.Red;
            }
        }

        // This uses the nodes generated from the map, have fun
        public AI(List<Enemy> enemies, List<Node> nodes)
        {
            this.nodes = nodes;

            this.enemies = enemies;
        }

        public void AssignNewPathsToEnemies(Node target)//TODO: Integrate with regards to enemies
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Route = Pathfind(nodes[0], target);
            }
        }

        public Queue<Node> Pathfind(Node start, Node target) //Implementation of A* fingers crossed
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
                    System.Diagnostics.Debug.Print("open set check: " + toCheck.ToString());
                        if(toCheck.fScore < current.fScore) 
                        {
                        System.Diagnostics.Debug.Print(toCheck.ToString() + " has the lowest fScore");
                        current = toCheck;
                        }
                    }

                if (current == target)
                { 
                    Console.WriteLine("Reconstructing path...");

                    Queue<Node> path = new Queue<Node>(ReconstructPath(start, current).Reverse());
                    
                    return path;
                }

                //Move current to the closed set
                openSet.Remove(current);
                closedSet.Add(current);
                Console.WriteLine("Added " + current.ToString() + " to the closed set");

                foreach(Node neighbor in current.Neighbors)
                {

                    Console.WriteLine("current: " + current.ToString() + "neighbor: " + neighbor.ToString());
                    if(!closedSet.Contains(neighbor))//if this neighbor hasn't been checked yet
                    {
                        if(!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor); //Discover a new node from the current node's neighbors
                        }

                        int tentgScore = current.gScore + neighbor.DistanceFrom(current);

                        if(!(tentgScore > neighbor.gScore)) //recording the best path
                        {
                            System.Diagnostics.Debug.Print("Recording the path of " + neighbor.ToString());
                            neighbor.Parent = current;
                            neighbor.gScore = tentgScore;
                            neighbor.fScore = neighbor.gScore + target.DistanceFrom(neighbor);
                            neighbor.Color = Color.Black;
                        }
                    }
                }
                }
            return null;
        }

        private Queue<Node> ReconstructPath(Node start, Node current) //Recursive algorithm to rebuild a path
        {
            Console.WriteLine(current.ToString());
            if(current == start)
            {
                Console.WriteLine("returning1");
                return pathToGive;
            }
            else
            {
                pathToGive.Enqueue(current);
                return ReconstructPath(start, current.Parent);
            }
            
        }  
    }
}
//Ruben Young