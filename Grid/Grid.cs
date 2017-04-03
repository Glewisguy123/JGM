using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Grid.Generation.New
{
    class Grid
    {
        Texture2D tex;
        Node[,] grid;
        int gridX;
        int gridY;
        private int xVar = 0;
        private int yVar = 0;
        public List<Node> path = new List<Node>();

        public Node[,] getGrid { get { return grid; } }

        public Grid(Texture2D tex)
        {
            this.tex = tex;
        }

        /// <summary>
        /// Create a grid and fill it with empty nodes 
        /// 
        /// Change the used node constructor
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void create(int X, int Y)
        {
            gridX = X;
            gridY = Y;
            grid = new Node[X, Y];
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    grid[x, y] = new Node(x, y, x + y, tex);
                }
            }
        }

     

        /// <summary>
        /// Test Method
        /// </summary>
        public void doStuff()
        {
            Node stuff = grid[9, 4];
            List<Node> neigh = getNeighbours(grid[9, 4]);
            for (int i = 0; i < neigh.Count; i++)
            {
                // Console.WriteLine("neighbour X " + neigh[i].getGrid.X + " " + " Neighbour Y" + neigh[i].getGrid.Y);

            }
        }





        /// <summary>
        /// Set up node visuals, needs to be improved
        /// </summary>
        public void setupVisual()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    int element = i * (grid.Length) + j;
                    grid[i, j].Position = new Vector2(xVar, yVar);
                    //Increase next X pos by texture width
                    xVar += tex.Width;

                }
                //set xVar to 0 because we are done setting x positions
                xVar = 0;
                //Same for Y
                yVar += tex.Height;
            }
        }


        public void resetVisual()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j].Color = Color.White;
                }
            }
        }

        /// <summary>
        /// Set node positions, this needs to be changed because it isnt accurate.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void setNodePositions(int width, int height)
        {

            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    grid[j, i].Position = new Vector2(xVar, yVar);
                    xVar += width;
                }

                xVar = 0;
                yVar += height;
            }

        }


        /// <summary>
        /// Draw all nodes
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            if (grid != null)
            {
                foreach (Node n in grid)
                {

                    if (path != null)
                        if (path.Contains(n))
                            n.Color = Color.Black;
                    n.Draw(sb);
                }


            }


        }



        public List<Node> getNeighbours(Node check)
        {

            List<Node> neigh = new List<Node>();
            for (int i = (int)check.getGrid.X - 1; i <= (int)check.getGrid.X + 1; i++)
            {
                for (int j = (int)check.getGrid.Y - 1; j <= (int)check.getGrid.Y + 1; j++)
                {
                    if (checkMap(i, j))
                    {
                        neigh.Add(grid[i, j]);
                    }
                }
            }

            return neigh;
        }

        /// <summary>
        /// Check 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool checkMap(int x, int y)
        {
            return x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1);
        }
    }
}
