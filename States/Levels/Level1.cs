using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Managers.Render;
using Engine.Managers.EntityRelated;
using Engine.Entities;
using Engine.Managers.CamManage;
using Engine.Events.KeyboardEvent;
using ADS.States.Levels;
using ADS.Grid.Generation.New;
using ADS.Grid.Pathfinding.AStar;
using ADS.Entities.Steering;
using Engine.Events.MouseEvent;

using ADS.Managers.AStar;
using ADS;

namespace Engine
{
    class Level1 : BaseScreen
    {
        #region Variables

        private int counter = 0;
   
        private Vector2[] test = new Vector2[7];
        private Random random;


        int max = 30;
        
        Grid grid;
        AStarGridSearch a;


        AstarPath search1;

        /// <summary>
        /// ASTAR STUFF HERE
        /// </summary>


        #endregion
        #region Constructor, Initialization & Unload
        public Level1()
        {
        }

        /// <summary>
        /// Run Initialization logic
        /// - Generate the TileMap
        /// - Add a player to the game
        /// /// </summary>
        public override void Initialize()
        {
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<pEntity>(Vector2.Zero);

            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(50, 50));
           /* Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(100, 50));
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(150, 50));
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(200, 50));
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(250, 50));
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(300, 50));
            */

            search1 = new AstarPath();

            random = new Random();
            Constants.colour = Color.Maroon;
            Locator.Instance.getService<CameraManager>().getCam().Zoom = 1f;

            Locator.Instance.getService<KeyHandler>().KeyDown += OnKeyDown;
            Locator.Instance.getService<MouseHandler>().MouseClick += OnMouseClick;

            this.SoundTrack = "SoundTrack1";


            grid = new Grid(Locator.Instance.getService<IResourceLoader>().GetTex("Tile3"));
            grid.create(max, max);
            grid.setNodePositions(max, max);
            grid.setupVisual();
            a = new AStarGridSearch(grid);

       


       
            base.Initialize();

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
            // search1.DebugPath();
            foreach (ADS.Grid.Generation.New.Node node in grid.getGrid)
            {
            }
        }


        public void OnKeyDown(object sender, KeyEventArgs e)
        {


            if (e.key == Microsoft.Xna.Framework.Input.Keys.E)
            {
                if (Locator.Instance.getService<CameraManager>().getCam().Zoom == 1f)
                {
                    Locator.Instance.getService<CameraManager>().getCam().Zoom = 0.1f;
                }

                else if (Locator.Instance.getService<CameraManager>().getCam().Zoom == 0.1f)
                {
                    Locator.Instance.getService<CameraManager>().getCam().Zoom = 1f;
                }
            }

            if (e.key == Microsoft.Xna.Framework.Input.Keys.Q)
            {
                Locator.Instance.getService<EntityManager>().clearList();
           
            }


        
        }

  

        public override void Unload()
        {
            Locator.Instance.getService<EntityManager>().tempCamClear();
            Locator.Instance.getService<EntityManager>().clearList();
            Console.WriteLine("Unloading");
            Constants.colour = Color.Maroon;
            Locator.Instance.getService<KeyHandler>().KeyDown -= OnKeyDown;


        }

        #endregion


        #region Update & Draw
        /// <summary>
        /// Draw the map
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //    Map.Draw(spriteBatch);

            grid.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        /// <summary>
        /// Update activities within the level
        /// Our level currently is very standard therefore no updates are needed
        /// </summary>
        /// <param name="gameTime"></param>
        /// 


        public override void Update(GameTime gameTime)
        {

       

            counter += 1;

            if(counter <=200)

            {
                Locator.Instance.getService<RenderManager>().addString(new ADS.Utilities.GameText(this.GetType().ToString().Split('.').Last(), "SnapTitle", new Vector2(300,0), Color.Yellow, 0.5f));
            }

            
    
            base.Update(gameTime);
        }
        #endregion


        #region TemporaryCollision


       
    }
    #endregion


}

 

