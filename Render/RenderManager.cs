using ADS;
using ADS.Utilities;
using ADS.Utility;
using Engine.Managers.CamManage;
using Engine.Managers.EntityRelated;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Managers.Render
{
    public class RenderManager :  IUpdService
    {
        private Queue<string> LinesToBeDrawn = new Queue<string>();
        private Queue<GameText> TextToBeDrawn = new Queue<GameText>();
        private Queue<IDrawable> Shapes = new Queue<IDrawable>();


        

        private List<IEntity> entities = new List<IEntity>();
       //Reference to the kernels spritebatch in which all entities will be drawn
        public SpriteBatch spriteBatch { get; set; }

        //List containing items/entities that arent drawn within the cameras perams
        private List<IDrawable> Drawables = new List<IDrawable>();

        public List<IDrawable> getD { get { return Drawables; } }
        public List<IDrawable> getD1 { get { return CamDrawables; } }

        //List containing items/entities which will be drawn with cam perams
        private List<IDrawable> CamDrawables = new List<IDrawable>();

        private List<IEntity> CamDrawEntities = new List<IEntity>();


       

        public RenderManager()
        {

        }

        //An initialize method that is called every time a new
        //screen is ready to be drawn
        public void Initialize()
        {
            getEntityList();
        }

        public void getEntityList()
        {
            entities = Locator.Instance.getService<EntityManager>().getList();
        }

        public void getCamEntityList()
        {
            CamDrawEntities = Locator.Instance.getService<EntityManager>().getCamList();
        }

        //For items which dont wish to be drawn in regards to the cameras matrix translations (such as GUI)
        public void addDrawable(IDrawable d)
        {
            Drawables.Add(d);
            Console.WriteLine("Added to Drawable List");
        }

        //For Scenery/Entities which wish to be drawn in regards to the cameras matrix translations
        public void addCamDrawable(IDrawable d)
        {
            CamDrawables.Add(d);
            Console.WriteLine("Added to CamDrawable List");

        }

        //For Entities which wish to be drawn in regards to the cameras matrix translations
        public void addCamDrawEntity(IDrawable d)
        {
            CamDrawEntities.Add(d as IEntity);
            Console.WriteLine("Added to CamDrawEntities List");

        }



        //Change method name
        //Draws everything within the camera
        public void DrawCameraRelatedArtefacts()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        Locator.Instance.getService<CameraManager>().getCam().get_transformation(Game1.Instance.GraphicsDevice));
            DrawComponents();
            DrawCamDrawables();
            DrawCamDrawEntities();
            //Below method should be in noncamera
            DrawShapes();


            spriteBatch.End();
        }

       
        public void clearTempEntity()
        {
            CamDrawEntities.Clear();
        }

        //Change method name
        //Draws everything the camera doesnt need to know about
  public void DrawNonCameraRelatedArtefacts()
  {
      spriteBatch.Begin();
      DrawDrawables();
            DrawEntities();
            spriteBatch.End();

  }

        /// <summary>
        /// Queue text to be drawn
        /// </summary>
        /// <param name="gameText"></param>
        public void addString(GameText gameText)
        {
            TextToBeDrawn.Enqueue(gameText);
        }

        /// <summary>
        /// Add a shape to the render managers shape draw list
        /// </summary>
        /// <param name="shape"></param>
     public void addShape(IDrawable shape)
        {
           if(shape != null)
            Shapes.Enqueue(shape);

            Console.WriteLine("Shape Added!");
        }

        public void Update(GameTime gameTime)
  {
            //Update lists
            getEntityList();
            getCamEntityList();
  }

        #region DrawingMethods

        //Draw Items
        public void Draw()
        {
            DrawCameraRelatedArtefacts();
            DrawNonCameraRelatedArtefacts();


        }

        public void Draw(Texture2D texture, Rectangle rect, Color col)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, null);
            spriteBatch.Draw(texture, rect, col);
            spriteBatch.End();
        }

        /// <summary>
        /// Draws all entities that are currently waiting to be drawn
        /// </summary>
        /// <param name="spriteBatch"></param>

        public void DrawEntities()
        {
            foreach (IEntity i in entities)
            {
                if (i.isVisible)
                {
                    i.Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Draws any components that require a draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawComponents()
        {
            Locator.Instance.getService<IScreenManager>().Draw(spriteBatch);

        }

     
        /// <summary>
        /// Iterate through the shapes list and draw any shapes
        /// </summary>
        public void DrawShapes()
        {
            for(int i = 0; i < Shapes.Count; i++)
            {
                Shapes.Dequeue().Draw(spriteBatch);
                
            }
            //Shapes.Clear();
        }

        public void DrawDrawables()
        {
            for(int i = 0; i < Drawables.Count; i++)
            {
                Drawables[i].Draw(spriteBatch);
            }

            for(int i = 0; i < TextToBeDrawn.Count; i++)
            {
                GameText a = TextToBeDrawn.Dequeue();
                a.Draw(spriteBatch);
            }
        }

        public void DrawCamDrawables()
        {
            for (int i = 0; i < CamDrawables.Count; i++)
            {
                CamDrawables[i].Draw(spriteBatch);
            }
        }

        public void DrawCamDrawEntities()
        {
            for (int i = 0; i < CamDrawEntities.Count; i++)
            {
                CamDrawEntities[i].Draw(spriteBatch);
            }
        }

        #endregion




    }
}


