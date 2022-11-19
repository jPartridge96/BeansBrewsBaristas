using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.Managers
{
    public sealed class AudioManager
    {
        private AudioManager()
        {
            _soundLibrary = new Dictionary<string, SoundEffect>()
            {
                // {"PascalCase", LoadSound("camelCase") }
                {"MenuTheme", LoadSound("Theme1") },
                {"Theme2", LoadSound("Theme2") }
            };
        }

        private static AudioManager _instance;
        public static AudioManager GetInstance()
        {
            if (_instance == null)
                _instance = new AudioManager();
            return _instance;
        }

        /// <summary>
        /// Loads SoundEffect from file path into ContentManager
        /// </summary>
        /// <param name="filePath">Path to sound file</param>
        /// <returns>Returns SoundEffect if loaded successfully</returns>
        public static SoundEffect LoadSound(string filePath)
        {
            return Global.GameManager.Content.Load<SoundEffect>($"Music/{filePath}");
        }

        /// <summary>
        /// Searches dictionary for sound using string; plays sound if found
        /// </summary>
        /// <param name="sound">Name of SoundEffect</param>
        /// <exception cref="Exception">Thrown if no sound is found with name</exception>
        public static void PlaySound(string sound)
        {
            if (!_soundLibrary.TryGetValue(sound, out SoundEffect soundEffect))
                throw new Exception($"SoundEffect with name '{sound} not found.'");
            soundEffect.Play();
        }


        public static Dictionary<string, SoundEffect> _soundLibrary;
    }
}


