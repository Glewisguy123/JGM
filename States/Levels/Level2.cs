using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Engine.Managers.Behaviour;
using Engine.Managers.Collision;
using Engine.Managers.Render;
using Engine.Managers.EntityRelated;
using Engine.Entities;
using ADS;

namespace Engine
{
    class Level2 : BaseScreen
    {
        #region Variables
        #endregion

        #region Constructor, Initialization & Unload
        public Level2()
        {
        }

        /// <summary>
        /// Run Initialization logic
        /// - Generate the TileMap
        /// - Add a player to the game
        /// /// </summary>
        public override void Initialize()
        {

            SoundTrack = "SoundTrack1";

           
            Locator.Instance.getService<EntityManager>().createEntityDrawable<pEntity>(new Vector2(200,100));
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<pEntity>(new Vector2(200, 100));
            Locator.Instance.getService<EntityManager>().createEntity<pEntity>(new Vector2(250, 100));


            base.Initialize();

        }

      
        public override void UnloadContent()
        {
                   

        }
        #endregion
        #region Update & Draw
        /// <summary>
        /// Draw the map
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        int hi = 0;
        /// <summary>
        /// Update activities within the level
        /// Our level currently is very standard therefore no updates are needed
        /// </summary>
        /// <param name="gameTime"></param>

        bool test1 = false;
        bool test2 = false;
        bool test3 = false;
        bool test4 = false;
        bool test5 = false;
        bool test6 = false;
        public override void Update(GameTime gameTime)
        {
            hi++;
            if(hi > 50 && !test1)
            {
                Locator.Instance.getService<RenderManager>().getD.Clear();
                test1 = true;
            }
            if (hi > 100 && !test2)
            {
                Locator.Instance.getService<RenderManager>().getD1.Clear();
                test2 = true;
            }
            if (hi > 150 && !test3)
            {
                Locator.Instance.getService<EntityManager>().clearList();
                test3 = true;
            }
            if (hi > 200 && !test4)
            {
                Locator.Instance.getService<EntityManager>().createEntityDrawable<pEntity>(new Vector2(200, 100));
                test4 = true;
            }
            if (hi > 250 && !test5)
            {
                Locator.Instance.getService<EntityManager>().createEntityCamDrawable<pEntity>(new Vector2(200, 100));
                test5 = true;
            }
            if (hi > 300 && !test6)
            {
                Locator.Instance.getService<EntityManager>().createEntity<pEntity>(new Vector2(250, 100));
                test6 = true;
            }
        }
        #endregion
    }
}

