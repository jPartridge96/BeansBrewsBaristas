using BeansBrewsBaristas.Content.scripts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace BeansBrewsBaristas.Managers
{
    public class SceneManager
    {
        public static List<DrawableGameComponent> ActiveScene { get; set; }
        private static Dictionary<string, List<DrawableGameComponent>> Atlas;
        Texture2D backgroundCafe = Global.GameManager.Content.Load<Texture2D>("Images/CafeBackground1");
        Texture2D cafeBar = Global.GameManager.Content.Load<Texture2D>("Images/CafeBar1");
        

        private SceneManager()
        {
            Atlas = new Dictionary<string, List<DrawableGameComponent>>()
            {
                {
                    "Level1",
                    new List<DrawableGameComponent>()
                    {

                        new TextElement("Scene 1",  new Vector2 (25, Global.Stage.Y - 75), Color.LimeGreen),
                        new TextElement("Loaded Successfully",new Vector2(25, Global.Stage.Y - 50), Color.White),
                        new Sprite(Vector2.Zero, backgroundCafe, Color.White),
                        new Sprite(Vector2.Zero, cafeBar, Color.White)
                        
                        
                    }
                },
                {
                    "Level2",
                    new List<DrawableGameComponent>()
                    {
                        new TextElement("Scene 2",new Vector2(25, Global.Stage.Y / 2 - 15), Color.LimeGreen),
                        new TextElement("Loaded Successfully",new Vector2(25, Global.Stage.Y / 2 + 10), Color.White)
                    }
                },
                {
                    "Level3",
                    new List<DrawableGameComponent>()
                    {
                        new TextElement("Scene 3",new Vector2(25, 25), Color.LimeGreen),
                        new TextElement("Loaded Successfully",new Vector2(25,50), Color.White)
                    }
                },
                
            };
        }

        private static SceneManager _instance;
        public static SceneManager GetInstance()
        {
            if (_instance == null)
                _instance = new SceneManager();
            return _instance;
        }

        public static void LoadScene(string sceneName)
        {
            if (!Atlas.TryGetValue(sceneName, out var playableMap))
                throw new Exception($"No scene found with name '{sceneName}'.");

            if (ActiveScene != null)
            {
                switch (ActiveScene.Equals(playableMap))
                {
                    case false:
                        UnloadScene(ActiveScene);

                        foreach (DrawableGameComponent comp in playableMap)
                            Global.GameManager.Components.Add(comp);
                        ////just testing the audiomanager instance here
                        //if (sceneName == "Level1")
                        //{
                        //    AudioManager.PlaySound("MenuTheme");
                        //}
                        //if(sceneName == "Level2")
                        //{
                        //    AudioManager.PlaySound("Theme2");
                        //}

                        ActiveScene = playableMap;
                        break;
                }
            }
            else
            {
                foreach (DrawableGameComponent comp in playableMap)
                    Global.GameManager.Components.Add(comp);

                ActiveScene = playableMap;
            }
        }

        public static void UnloadScene(object scene)
        {  
            foreach (DrawableGameComponent comp in (List<DrawableGameComponent>)scene)
                Global.GameManager.Components.Remove(comp);
        }
    }
}
