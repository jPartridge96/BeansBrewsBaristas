using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace BeansBrewsBaristas.Managers
{
    public sealed class AudioManager
    {
        private AudioManager()
        {
            _soundLibrary = new Dictionary<string, SoundEffect>()
            {
                {"MenuTheme", LoadSound("Theme1") },
                {"Theme2", LoadSound("Theme2") }
            };
            _songLibrary = new Dictionary<string, Song>()
            {
                {"MenuTheme", LoadSong("Theme1") },
                {"Theme2", LoadSong("Theme2") }
            }; // SongCollection, MediaLibrary exist - useful?

            MediaPlayer.IsRepeating = true;
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
        /// <exception cref="ContentLoadException">Thrown if no SoundEffect is found with name</exception>
        public static SoundEffect LoadSound(string filePath)
        {
            try
            {
                return Global.GameManager.Content.Load<SoundEffect>($"SoundEffects\\{filePath}");
            }
            catch
            {
                throw new ContentLoadException($"SoundEffect with name '{filePath}' not found.");
            }
        }

        /// <summary>
        /// Loads Song from file path into ContentManager
        /// </summary>
        /// <param name="filePath">Path to song file</param>
        /// <returns>Returns Song if loaded successfully</returns>
        /// <exception cref="ContentLoadException">Thrown if no Song is found with name</exception>
        public static Song LoadSong(string filePath)
        {
            try
            {
                return Global.GameManager.Content.Load<Song>($"Songs\\{filePath}");
            }
            catch
            {
                throw new ContentLoadException($"Song with name '{filePath}' not found.");
            }
        }

        /// <summary>
        /// Searches dictionary for SoundEffect using string; plays SoundEffect if found
        /// </summary>
        /// <param name="sound">Name of SoundEffect</param>
        /// <exception cref="Exception">Thrown if no SoundEffect is found with name</exception>
        public static void PlaySound(string sound)
        {
            if (!_soundLibrary.TryGetValue(sound, out SoundEffect soundEffect))
                throw new Exception($"SoundEffect with name '{sound}' not found.");
            soundEffect.Play();
        }

        /// <summary>
        /// Searches dictionary for Song using string; plays Song if found
        /// </summary>
        /// <param name="songName">Name of Song</param>
        /// <exception cref="Exception">Thrown if no Song is found with name</exception>
        public static void PlaySong(string songName)
        {
            if (!_songLibrary.TryGetValue(songName, out Song song))
                throw new Exception($"Song with name '{songName}' not found.");
            MediaPlayer.Play(song);
        }

        public static Dictionary<string, SoundEffect> _soundLibrary;
        public static Dictionary<string, Song> _songLibrary;
    }
}


