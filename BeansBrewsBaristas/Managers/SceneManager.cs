using BeansBrewsBaristas.Content.scripts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using SharpDX.Direct3D11;

namespace BeansBrewsBaristas.Managers
{
    public class SceneManager
    {
        public static List<DrawableGameComponent> ActiveScene { get; set; }
        private static Dictionary<string, List<DrawableGameComponent>> Atlas;

        private SceneManager()
        {
            Atlas = new Dictionary<string, List<DrawableGameComponent>>()
            {
                {
                    "Level1",
                    new List<DrawableGameComponent>()
                    {
                        new TextElement("Scene 1",  new Vector2 (25, Global.Stage.Y - 75), Color.LimeGreen),
                        new TextElement("Loaded Successfully",new Vector2(25, Global.Stage.Y - 50), Color.White)
                        
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
