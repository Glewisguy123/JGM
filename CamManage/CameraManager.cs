using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Engine.Managers.CamManage
{
    public class CameraManager : IUpdService
    {
        //reference to the camera
        Camera camera;

     

        public void Initialize()
        {
            camera = new Camera();
        }

        public Camera getCam()
        {
            return camera;
        }


        public void Update(GameTime gameTime)
        {
            camera.Update();
        }

        
        /// <summary>
        /// Returns a position relative to the world.
        /// 
        /// This can be used to retrieve the mouses current position 
        /// since mousestate returns the mouses x and y co-ords relative to the
        /// height and width and doesnt take the camera into consideration.
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public Vector2 getWorldPosition(Vector2 Position)
        {
            Vector2 worldPosition = Vector2.Transform(Position, Matrix.Invert(camera.get_transformation(Game1.Instance.GraphicsDevice)));

            return worldPosition;
        }
        


    }
}
