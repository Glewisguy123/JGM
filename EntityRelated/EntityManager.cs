using ADS;
using ADS.Managers.High_Tier.EntityRelated;
using Engine.Entities;
using Engine.Managers.Behaviour;
using Engine.Managers.CamManage;
using Engine.Managers.Render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Engine.Managers.EntityRelated
{
    public class EntityManager : IEntityManager,IUpdService
    {
        //Reference to the camera
        Camera cam = Locator.Instance.getService<CameraManager>().getCam();
        //List of entities that have been created
        private List<IEntity> eList = new List<IEntity>();
        //List of entities that have been created (That are taken into
        //cam draw calls)
        private List<IEntity> cdList = new List<IEntity>();


    

      
        public EntityManager()
        {

        }

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns></returns>
        public List<IEntity> getList()
        {
            return eList;
        }

        /// <summary>
        /// Returns all Cam draw Entities
        /// </summary>
        /// <returns></returns>
        public List<IEntity> getCamList()
        {
            return cdList;
        }

        /// <summary>
        /// * *REMOVE**
        /// </summary>
        /// <returns></returns>
        public IEntity getPlayer()
        {
            for(int i = 0; i < cdList.Count; i++)
            {
                if (cdList[i].GetType() == typeof(pEntity))
                    return  cdList[i];
            }
            return null;
        }

        /// <summary>
        /// Add an entity to the entity list
        /// </summary>
        /// <param name="e"></param>
       public void addEntity(IEntity e)
       {
          
           eList.Add(e);
            Locator.Instance.getService<RenderManager>().addDrawable(e as IDrawable);
           Console.WriteLine("Added Entity -  ID " + e.UniqueID);
       }

        /// <summary>
        /// Add an entity to the camdraw list
        /// </summary>
        /// <param name="e"></param>
       public void addCamEntity(IEntity e)
        {
            cdList.Add(e);
            Console.WriteLine("Added CamDrawEntity -  ID " + e.UniqueID );

        }

        //Create an entity
        public IEntity createEntity<T>(Vector2 Position) where T : IEntity, new()
       {
           IEntity a = new T();
           a.Initialize(Position);
           addEntity(a);       
           return a;
       }
        //Create an entity we want to draw within the camera
        public IEntity createEntityCamDrawable<T>(Vector2 Position) where T : IEntity, new()
     {
         IEntity a = new T();
         a.Initialize(Position);
            addCamEntity(a);
            Console.WriteLine("Created Entity Cam Drawable " + a. Name);

            return a;
     }

      
        public IEntity createEntityDrawable<T>(Vector2 Position) where T : IEntity, new()
     {
         IEntity a = new T();
         a.Initialize(Position);
        
         Locator.Instance.getService<RenderManager>().addCamDrawEntity(a as IDrawable);
         return a;
     }

        //Clear all camera entities
        public void tempCamClear()
        {
            Locator.Instance.getService<RenderManager>().clearTempEntity();
            Locator.Instance.getService<BehaviourManager>().clearList();

        }

        /// <summary>
        /// Return a reference to an entity based on their name
        /// </summary>
        /// <param name="name"></param>
        public void getEntity(string name)
        {
            IEntity e;
            for (int i = 0; i < eList.Count; i++)
            {


                string naem = eList[i].Name;
                Console.WriteLine(naem + " AAAAAAAAAAAAAA" );
                if (naem == name)
                {
                    Console.WriteLine(naem);
                    e = eList[i];
                }
            }
        

            
          

          
        }

        /// <summary>
        /// Return a reference to a Camera Draw entity based on their name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEntity getCamEntity(string name)
        {
            IEntity e = null;
            for (int i = 0; i < cdList.Count; i++)
            {


                string naem = cdList[i].Name;
                if (naem == name)
                {
                    e = cdList[i];
                }
                
            }
            return e;
       }

        public void getEntity(int entityID)
        {

        }

        /// <summary>
        /// Clear the non cam draw entity list
        /// </summary>
        public void clearList()
     {
            Console.WriteLine("LIST CLEARED" );
            eList.Clear();
            cdList.Clear();
         Locator.Instance.getService<BehaviourManager>().clearList();
     }

     
        /// <summary>
        /// Remove an entity based on the ID applied
        /// </summary>
        /// <param name="entityID"></param>
        public void removeEntity(int entityID)
       {
            for(int i = 0; i < eList.Count; i++)
            {
                if(eList[i].UniqueID == entityID)
                {
                    eList.Remove(eList[i]);
                    Locator.Instance.getService<BehaviourManager>().removeMind(entityID);
                    Console.WriteLine("Removed Entity - ID " + entityID);
                    
                }
            }
       }

       
        /// <summary>
        /// Remove a Cam Draw entity based on the ID applied
        /// </summary>
        /// <param name="entityID"></param>
        public void removeCamEntity(int entityID)
        {
            for (int i = 0; i < cdList.Count; i++)
            {
                if (cdList[i].UniqueID == entityID)
                {
                    cdList.Remove(cdList[i]);
                    Locator.Instance.getService<BehaviourManager>().removeMind(entityID);
                    Console.WriteLine("Removed Entity - ID " + entityID);

                }
            }
        }

        /// <summary>
        /// UPDATE
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < eList.Count; i++)
            {
                if (eList[i].Position.X < -100 || eList[i].Position.Y > 1000)
                {
                    removeEntity(eList[i].UniqueID);
                }
            }

            for (int i = 0; i < cdList.Count; i++)
            {
                Console.WriteLine(cdList[i].UniqueID);
            }
        }
    }
}
