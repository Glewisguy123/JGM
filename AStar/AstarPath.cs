
using ADS.Grid.Generation.New;
using ADS.Grid.Pathfinding.AStar;
using Engine;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Managers.AStar
{
    class AstarPath
    {

        
        //The graph in which the Astar will 
        ADS.Grid.Generation.New.Grid aStarData;

        AStarGridSearch currentDisp;

        public void DebugPath()
        {
            if (currentDisp != null)
            {
                currentDisp.Show(currentDisp.getPathv().First(), currentDisp.getPathv().Last());
            }
        }

        public List<Node> CommitPathSearch(ADS.Grid.Generation.New.Grid grid, Node Start, Node Goal)
        {
            AStarGridSearch search = new AStarGridSearch(grid);
            search.Search(Start, Goal, Locator.Instance.getService<IResourceLoader>().GetTex("Tile1"));
            List<Node> result = search.getPathv();
            currentDisp = search;
            return result;
        }

        public Queue<Node> CommitWayPointSearch(ADS.Grid.Generation.New.Grid grid, Node Start, Node Goal)
        {
            AStarGridSearch search = new AStarGridSearch(grid);
            search.Search(Start, Goal, Locator.Instance.getService<IResourceLoader>().GetTex("Tile1"));
            Queue<Node> result = search.getPath();
            currentDisp = search;
            return result;
        }

    }
}
