using Engine;
using Engine.Events.KeyboardEvent;
using Engine.Events.MouseEvent;
using Engine.Managers;
using Engine.Managers.Behaviour;
using Engine.Managers.CamManage;
using Engine.Managers.Collision;
using Engine.Managers.EntityRelated;
using Engine.Managers.Render;
using Engine.Managers.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS
{
    class Locator 
    {

        //Hashmap of all available services
        private Dictionary<object, object> _services = new Dictionary<object, object>();
        //Hashmap of all services that need to be updated
        private Dictionary<object, object> _updservices = new Dictionary<object, object>();

        private List<IUpdService> updateList = new List<IUpdService>();
        //Private instance
        private static Locator instance;

        private Locator()
        {

        }

        //Singleton
        public static Locator Instance
        {
            get
            {
                if (instance == null)
                    instance = new Locator();
                return instance;
            }
        }

        public void InitializeServices(ContentManager c,SpriteBatch sb)
        {
        

            MouseHandler mouseHandle = new MouseHandler();
            registerUPDService<MouseHandler>(mouseHandle);
            KeyHandler keyHandle = new KeyHandler();
            registerUPDService<KeyHandler>(keyHandle);

            ////////////////////////////////////////////////////////////////////////
            CameraManager camManage = new CameraManager();
            camManage.Initialize();
            registerUPDService<CameraManager>(camManage);


            SoundManager soundManage = new SoundManager();
            registerService<SoundManager>(soundManage);


            IResourceLoader resource = new ResourceLoader();
            resource.Content = c;
            resource.Initialize();
            registerService<IResourceLoader>(resource);

            EntityManager entityManage = new EntityManager();
            registerService<EntityManager>(entityManage);


            BehaviourManager behaveManage = new BehaviourManager();
            registerUPDService<BehaviourManager>(behaveManage);

            DetectionManger detectManage = new DetectionManger();
            registerUPDService<DetectionManger>(detectManage);

            RenderManager render = new RenderManager();
            registerUPDService<RenderManager>(render);

      



            IScreenManager screenManage = new ScreenManager();
            screenManage.Initialize();
            registerService<IScreenManager>(screenManage);
            soundManage.Initialize();

    
            registerUPDService<IScreenManager>(screenManage);



        }

        //Gets and returns requested service
        public T getService<T>()
        {
            T t = default(T);
            //Try to return the service from the hashmap
            try
            {
                if (_services.ContainsKey(typeof(T)) && _services[typeof(T)] != null)
                    t= (T)_services[typeof(T)];
            } //TRY: to catch a null reference exception
            catch (NullReferenceException)
            {
                throw new Exception("SERVICE DOESNT EXIST OR HASNT BEEN INITIALIZED");

            }

            return t;
        }

        //Register the service and check whether it requires to be added to the update list.
        public void registerService<T>(T val)
        {
            try
            {
                if (val != null && !_services.ContainsKey(typeof(T)))
                {

                    Console.WriteLine("Added2 " + typeof(T));
                    _services.Add(typeof(T), val);
                }
                }catch (KeyNotFoundException)
            {
                throw new ArgumentException("ISNT VALID");
            }


        }
            

            public void registerUPDService<T>(T val)
        {
            registerService<T>(val);
            
                IUpdService val1 = val as IUpdService;
                updateList.Add(val1);
            
        }

        //Update all UPDSERVICES
        public void UpdateServices(GameTime gameTime)
        {
            foreach (IUpdService a in updateList)
            {
                a.Update(gameTime);
            }
       
        }
    }
}
