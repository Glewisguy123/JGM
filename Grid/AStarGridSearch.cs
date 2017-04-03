using ADS.Grid.Generation.New;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Grid.Pathfinding.AStar
{
    class AStarGridSearch
    {

        //Has the goal been found
        bool found = false;
        Node start = null;
        Node goal = null;
        //List containing nodes that we want to search
        List<Node> OpenList = new List<Node>();
        //List containing nodes we have already searched.
        List<Node> ClosedList = new List<Node>();
        //A queue of waypoints that an entity will request for its pathfinding. The class does the work and just returns the open list to the entity. (for now)
        Queue<Node> Waypoints = new Queue<Node>();

        List<Node> Path = new List<Node>();


        //SearchSpace
        Node[,] searchSpace;
        Texture2D check;

        ADS.Grid.Generation.New.Grid grid;

        public AStarGridSearch(ADS.Grid.Generation.New.Grid grid)
        {
            this.grid = grid;
        }


        /// <summary>
        /// A* Search steps.
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pGoal"></param>
        /// <param name="pCheck"></param>
        /// <param name="pPath"></param>
        public void Search(Node pStart, Node pGoal, Texture2D pCheck)
        {

            OpenList.Clear();
            ClosedList.Clear();
            Waypoints.Clear();
             if (grid.path != null) 
             grid.path.Clear();
            //Assign variables
            start = pStart;
            goal = pGoal;
            searchSpace = grid.getGrid;
            check = pCheck;


            // start.G = getDistance(start, pGoal);
            //Add the start node to the list for checking
            OpenList.Add(start);

            //Dont stop the search whilst there are nodes available or if the goal hasnt been found
            while (OpenList.Count > 0 || !found)
            {
                //Assign the current node of interest to the first open index
                if(OpenList.Count == 0 && ClosedList.Count != 0)
                {
                    Console.WriteLine("No Path Found");
                    Show(pStart, pGoal);
                    return;
                }
               
                Node current = OpenList[0];
                //
                CheckNeighbours(current, pGoal);

                //Find the fittest node around the current node and assign it
                FindFittest(current);

                //The node has been checked, move it from the open into the closed list
                OpenList.Remove(current);
                ClosedList.Add(current);

                //Check if we've found the goal
                if (current == pGoal)
                {
                    //Stop the loop
                    found = true;
                    // ShowPath(goal);
                    // start = current;
                    Console.WriteLine("FOUND");
                    //Draw the path.
                    Show(start, goal);

                    return;
                }

            }
        }
        public void addBlocked(int x, int y)
        {
          

            grid.getGrid[x, y].Blocked = true;
        }

        public void resetWalls()
        {
            foreach(Node n in grid.getGrid)
            {
                n.Blocked = false;
            }
        }
        /// <summary>
        /// Check surrounding neighbours, assign costs and move into relative list
        /// </summary>
        /// <param name="current"></param>
        /// <param name="pGoal"></param>
        private void CheckNeighbours(Node current, Node pGoal)
        {

            List<Node> neighs = grid.getNeighbours(current);
            foreach (Node node in neighs)
            {
                //Ignore all nodes that have already been checked or aren't passable
                if (Closed(node) || node.Blocked)
                {
                    if (node.Blocked)
                        node.Color = Color.Blue;

                    continue;
                }


                //Calculate cost from the current node to the node of interest
                int Cost = current.G + getDistance(current, node);

                //If the nodes costs havent been assigned or created, then assign them and make a reference through .Parent of the tiles neighbour
                if (Cost < node.G || !OpenList.Contains(node))
                {
                    node.G = Cost;
                    node.H = getDistance(node, goal);
                    node.Parent = current;

                }

                //If this node is a neighbour, but hasnt been checked yet, add the node into the List
                if (!OpenList.Contains(node))
                {
                    OpenList.Add(node);
                }
                //    }
                //}
            }
        }

        /// <summary>
        /// Finds node with lowest F cost (lowest H if their F costs are the same)
        /// and adds it to the open list
        /// </summary>
        private void FindFittest(Node current)
        {
            for (int i = 1; i < OpenList.Count; i++)
            {
                //Check if the fCost of open tile index is smaller than the current F. or if its equal to it
                if (OpenList[i].F < current.F || OpenList[i].F == current.F)
                {
                    //If the index of interests Heuristic cost is less, then make it the new fittest node
                    if (OpenList[i].H < current.H)
                        current = OpenList[i];
                }
            }
        }


        /// <summary>
        /// Gets the distance between the current and goal node for costs. Fix this.
        /// </summary>
        /// <param name="curr"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        private int getDistance(Node curr, Node goal)
        {

            int dx = (int)Math.Abs(curr.Position.X - goal.Position.X);
            int dy = (int)Math.Abs(curr.Position.Y - goal.Position.Y);


            if (dx > dy)
                return 14 * dy + 10 * (dx - dy);
            return 14 * dx + 10 * (dy - dx);
        }


        /// <summary>
        /// Checks to see if the node is located within the closed list
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>

        private bool Closed(Node node)
        {
            if (ClosedList.Contains(node))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Checks to see if the node is located within the open list
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool Opened(Node node)
        {
            if (OpenList.Contains(node))
            {
                return true;
            }
            else

                return false;
        }



        /// <summary>
        /// Displays a path of discovered path
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void Show(Node start, Node end)
        {
            start.Color = Color.Green;
            goal.Color = Color.Red;
            //Set the last node to be checked first so we can retrace the path back
            Node currentNode = end;

            for (int i = 0; i < OpenList.Count; i++)
            {
                //Dont draw on the start node
                if (currentNode != start)
                {
                    //Add all nodes inbetween
                    Path.Add(currentNode);
                    //Set up the link to loop back with (The parent node is the previous closest node that was checked and added to the open list)
                    currentNode = currentNode.Parent;
                }
            }

            //Little method I added to remove goal node since I hvent had much luck
            for (int i = 0; i < Path.Count; i++)
            {
                if (Path[i] == goal)
                    Path.Remove(Path[i]);
            }
            Path.Reverse();

            //Let the grid know of the path to draw it << change
             grid.path = Path;
           
        }

        public Queue<Node> getPath()
        {
            Waypoints.Enqueue(start);

            for (int i = 0; i < grid.path.Count; i++)
                Waypoints.Enqueue(grid.path[i]);

            Waypoints.Enqueue(goal);

            return Waypoints;
        }

        public List<Node> getPathv()
        {
            return Path;
        }
    }
}
