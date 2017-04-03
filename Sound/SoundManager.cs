using Engine.Events.KeyboardEvent;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using ADS;
using Microsoft.Xna.Framework;


namespace Engine.Managers.Sound
{
    public class SoundManager : IUpdService
    {

        public bool isMuted = false;

        float defaultVolume = 0.01f;
        //Singleton
     
        public void Initialize()
        {
            MediaPlayer.Volume = defaultVolume;
            ScreenManager a = Locator.Instance.getService<IScreenManager>() as ScreenManager;
             a.ScreenChange += onScreenChanged;

        }
        //Play a song that is located within the resource loaders song library
        public void Play(string name)
        {
           
            if(Locator.Instance.getService<IResourceLoader>().CheckLibrary(name) == false)
                Console.WriteLine("SONG DOESNT EXIST");
            else
            MediaPlayer.Play(Locator.Instance.getService<IResourceLoader>().GetSong(name));
        }

        /// <summary>
        /// Play a sound effect
        /// </summary>
        /// <param name="name"></param>
        public void PlayEffect(string name)
        {

        }

        /// <summary>
        /// Mute the Sound Manager
        /// </summary>
        public void Mute()
        {
            if (!isMuted)
            {
                isMuted = true;
                defaultVolume = 0;

            }
        }

        /// <summary>
        /// Unmute the sound manager
        /// </summary>
        public void unMute()
        {
            if (isMuted)
            {
                isMuted = false;
                defaultVolume = 0.05f;
            }
        }

        public void Volume(float Volume)
        {
            defaultVolume = Volume;
        }
        //Stop the media player
        public void Stop()
        {
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Plays screens songs.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if(MediaPlayer.Volume != defaultVolume)
            MediaPlayer.Volume = defaultVolume;
        }
      
        /// <summary>
        /// Turn the Volume Up
        /// </summary>
        public void volUp()
        {
            if (defaultVolume <= 1)
                defaultVolume += 0.02f;
        }

        /// <summary>
        /// Turn the Volume Down
        /// </summary>
        public void volDown()
        {
              if (defaultVolume >= 0.1f)
                defaultVolume -= 0.02f;
        }
        

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="screen"></param>
        public void onScreenChanged(BaseScreen screen)
         {
          if(screen.SoundTrack != null)
                 Play(screen.SoundTrack);
                 
         }
        
    }
} 
