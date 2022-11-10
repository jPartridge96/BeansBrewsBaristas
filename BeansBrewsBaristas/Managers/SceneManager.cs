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
        public List<DrawableGameComponent> ActiveScene { get; set; }
        private Dictionary<string, List<DrawableGameComponent>> Atlas;

        private SceneManager()
        {
            Atlas = new Dictionary<string, List<DrawableGameComponent>>()
            {
                {
                    "Level1",
                    new List<DrawableGameComponent>()
                    {
                        new TextElement("Scene 1", Color.LimeGreen, new Vector2 (25, Global.Stage.Y - 75)),
                        new TextElement("Loaded Successfully", Color.White, new Vector2(25, Global.Stage.Y - 50))
                    }
                },
                {
                    "Level2",
                    new List<DrawableGameComponent>()
                    {
                        new TextElement("Scene 2", Color.LimeGreen, new Vector2(25, Global.Stage.Y / 2 - 15)),
                        new TextElement("Loaded Successfully", Color.White, new Vector2(25, Global.Stage.Y / 2 + 10))
                    }
                },
                {
                    "Level3",
                    new List<DrawableGameComponent>()
                    {
                        new TextElement("Scene 3", Color.LimeGreen, new Vector2(25, 25)),
                        new TextElement("Loaded Successfully", Color.White, new Vector2(25,50))
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

        public void LoadScene(string sceneName)
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

        public void UnloadScene(object scene)
        {  
            foreach (DrawableGameComponent comp in (List<DrawableGameComponent>)scene)
                Global.GameManager.Components.Remove(comp);
        }
    }
}
