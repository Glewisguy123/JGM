
using Engine.Managers.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ADS;

namespace Engine
{
    /// <summary>
    ///Engine V.05
    ///Jack Tomlinson - University of Worcester
    /// </summary>
    public class Game1 : Game
    {
        //List of Update Components
        //List of Draw Components
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Game1 Instance;

      


            public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Instance = this;
            this.Window.Title = "JGM Engine Alpha";


        }


        //Initialize;
        protected override void Initialize()
        {

            Constants.Debug = true;

  
            //Set the mous to visible
            this.IsMouseVisible = true;
            Constants.g = GraphicsDevice;
            Random random = new Random();
            Constants.r = random;
       
           ADS.Locator.Instance.InitializeServices(this.Content, spriteBatch);
                       

            base.Initialize();

        }




        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Locator.Instance.getService<RenderManager>().spriteBatch = spriteBatch;
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Not everything has been intergrated to the new input 
            InputManager.Instance.Update(gameTime);

            GraphicsDevice.Clear(Constants.colour);
            Locator.Instance.UpdateServices(gameTime);
         


            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Draw Everything
            Locator.Instance.getService<RenderManager>().Draw();


            base.Draw(gameTime);
     
        }

      


    }
}
