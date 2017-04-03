using ADS.Grid.Pathfinding.AStar;
using ADS.Managers.AStar;
using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADS.Grid.Generation.New;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Events.MouseEvent;
using ADS;
using Engine.Managers.CamManage;
using Engine.Events.KeyboardEvent;
using Engine.Managers.EntityRelated;
using Engine.Managers.Render;
using ADS.Entities.Steering;

namespace Engine
{
    class AStarDemo : BaseScreen
    {
        ADS.Grid.Generation.New.Grid grid;
        AstarPath search1;
        AStarGridSearch a;
        Random random;
        int max = 20;


        public override void Initialize()
        {

            Locator.Instance.getService<CameraManager>().getCam().Zoom = 1f;

            Locator.Instance.getService<KeyHandler>().KeyDown += OnKeyDown;
           
            Locator.Instance.getService<MouseHandler>().MouseClick += OnMouseClick;
            random = new Random();
            search1 = new AstarPath();
            grid = new ADS.Grid.Generation.New.Grid(Locator.Instance.getService<IResourceLoader>().GetTex("Tile3"));
            grid.create(max, max);
            grid.setNodePositions(max, max);
            grid.setupVisual();
            a = new AStarGridSearch(grid);

            Locator.Instance.getService<CameraManager>().getCam().Pos = grid.getGrid[0, 0].Position + new Vector2(0,100);

            base.Initialize();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.key == Microsoft.Xna.Framework.Input.Keys.E)
            {
                if (Locator.Instance.getService<CameraManager>().getCam().Zoom == 1f)
                {
                    Locator.Instance.getService<CameraManager>().getCam().Zoom = 0.2f;
                }

                else if (Locator.Instance.getService<CameraManager>().getCam().Zoom == 0.2f)
                {
                    Locator.Instance.getService<CameraManager>().getCam().Zoom = 1f;
                }
            }

           

        }

        public override void Unload()
        {

            Locator.Instance.getService<KeyHandler>().KeyDown -= OnKeyDown;

            Locator.Instance.getService<MouseHandler>().MouseClick -=OnMouseClick;

            Locator.Instance.getService<EntityManager>().tempCamClear();
            base.Unload();
            
        }

        private void doSearch()
        {
            for (int i = 0; i < max; i++)
            {

                a.addBlocked(5, i);
            }
            ADS.Grid.Generation.New.Node startnode = grid.getGrid[random.Next(1, max), random.Next(1, max)];
            ADS.Grid.Generation.New.Node endnode = grid.getGrid[random.Next(1, max), random.Next(1, max)];
            if (startnode.Blocked || endnode.Blocked)
            {
                doSearch();
            }
            else
            {
                grid.resetVisual();
                grid.path = null;
                search1.CommitPathSearch(grid, startnode, endnode);
            }
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            doSearch();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            grid.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

       

    }
}
