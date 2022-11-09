using BeansBrewsBaristas.Content.scripts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BeansBrewsBaristas
{
    public class SceneManager
    {
        public List<DrawableGameComponent> ActiveScene { get; set; }

        private Dictionary<string, List<DrawableGameComponent>> Atlas;
        private bool TransitioningScene = false;

        private SceneManager()
        {
            Atlas = new Dictionary<string, List<DrawableGameComponent>>()
            {
                {
                    "Level1",
                    new List<DrawableGameComponent>()
                    {
                        new TextElement("Message", Color.LimeGreen, new Vector2(0, 0)),
                        new TextElement("Second Message", Color.White, new Vector2(100,0))
                    }
                },
                {
                    "Level2",
                    new List<DrawableGameComponent>()
                    {
                        new TextElement("SDASDASDSDASD", Color.LimeGreen, new Vector2(0, 0)),
                        new TextElement("ASDASDASDASDASD", Color.White, new Vector2(100,0))
                    }
                },
                {
                    "Level3",
                    new List<DrawableGameComponent>()
                    {
                        new TextElement("Message", Color.LimeGreen, new Vector2(0, 0)),
                        new TextElement("Second Message", Color.White, new Vector2(100,0))
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
            try
            {
                Task.Run(() =>
                {
                    if (ActiveScene != null)
                        UnloadScene(ActiveScene);

                    if (!Atlas.TryGetValue(sceneName, out var playableMap))
                        throw new Exception($"No scene found with name '{sceneName}'.");
                    else


                    if (ActiveScene != null)
                    {
                        if (!TransitioningScene)
                        {
                            TransitioningScene = true;
                            switch (ActiveScene.Equals(playableMap))
                            {
                                case true:
                                    throw new Exception($"Unable to load scene. '{sceneName}' is already the active scene.");
                                case false:
                                    Debug.WriteLine($"Loading scene {sceneName}");

                                    foreach (DrawableGameComponent comp in playableMap)
                                        Global.GameManager.Components.Add(comp);

                                    ActiveScene = playableMap;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (!TransitioningScene)
                        {
                            TransitioningScene = true;
                            Debug.WriteLine($"Loading scene {sceneName}");
                            foreach (DrawableGameComponent comp in playableMap)
                                Global.GameManager.Components.Add(comp);

                            ActiveScene = playableMap;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void UnloadScene(object scene)
        {
            foreach(DrawableGameComponent comp in (List<DrawableGameComponent>)scene)
            {
                comp.Visible = false;
                comp.Enabled = false;
            }
            TransitioningScene = false;
        }
    }
}
