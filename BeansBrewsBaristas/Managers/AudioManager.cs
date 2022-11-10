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
                //{"Brewing", LoadSound("brewing") }
                {"MenuTheme", LoadSound("menuTheme") }
            };
        }

        private static AudioManager _instance;
        public static AudioManager GetInstance()
        {
            if (_instance == null)
                _instance = new AudioManager();
            return _instance;
        }

        public SoundEffect LoadSound(string filePath)
        {
            return Global.GameManager.Content.Load<SoundEffect>($"Music/{filePath}");
        }

        public void PlaySound(string sound)
        {
            if (!_soundLibrary.TryGetValue(sound, out SoundEffect soundEffect))
                throw new Exception($"SoundEffect with name '{sound} not found.'");

            soundEffect.Play();
        }

        public Dictionary<string, SoundEffect> _soundLibrary;
    }
}


