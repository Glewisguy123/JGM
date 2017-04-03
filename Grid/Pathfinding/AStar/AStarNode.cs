using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Grid.Pathfinding.AStar
{
    class AStarNode
    {

        private int g;
        private int h;

        public int G { get { return g; } set { g = value; } }
        public int H { get { return h; } set { h = value; } }
        public int F { get { return g + h; } }

        public AStarNode(int element, string tex)
        {

        }

        public AStarNode(int element)
        {

        }


        public void Draw(SpriteBatch sp)
        {

        }


    }
}
