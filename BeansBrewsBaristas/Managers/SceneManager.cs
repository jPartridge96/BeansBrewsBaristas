using BeansBrewsBaristas.Content.scripts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace BeansBrewsBaristas.Managers
{
    public class SceneManager
    {
        public static string ActiveScene { get; set; }
        public static List<DrawableGameComponent> SceneComponents { get; set; }
        private static Dictionary<string, List<DrawableGameComponent>> Atlas;

        Texture2D menu = Global.GameManager.Content.Load <Texture2D>("Images/Menu");
        Texture2D backgroundCafe = Global.GameManager.Content.Load<Texture2D>("Images/CafeBackground1");
        Texture2D cafeBar = Global.GameManager.Content.Load<Texture2D>("Images/CafeBar1");
        Texture2D backgroundCafe2 = Global.GameManager.Content.Load<Texture2D>("Images/CafeBackground2");
        Texture2D cafeBar2 = Global.GameManager.Content.Load<Texture2D>("Images/CafeBar2");

        public static Texture2D VolumeTex = Global.GameManager.Content.Load<Texture2D>("Images/VolumeBar");
        public static TextElement soundEffectTex = new TextElement($"Effects Volume: {GameManager.volume}", new Vector2(350, 250), Color.White);
        public static TextElement musicVolumeTex = new TextElement($"Music Volume {GameManager.volume}", new Vector2(350, 175), Color.White);
        public static Sprite soundEffectSprite = new Sprite(new Vector2(350, 300), VolumeTex, Color.White);
        public static Sprite volumeSprite = new Sprite(new Vector2(350, 225), VolumeTex, Color.White);



        private SceneManager()
        {
            Atlas = new Dictionary<string, List<DrawableGameComponent>>()
            {
                {
                    "Menu",
                    new List<DrawableGameComponent>()
                    {
                        new Sprite(Vector2.Zero, menu, Color.White),
                        new TextElement("Beans Brews Baristas", new Vector2(350, 125), Color.White),
                        new MenuComponent(Global.GameManager, new string[] {
                            "Play", "Help", "Credits","Options", "Quit"
                        }, new Vector2(350, 200))
                    }
                },
                {
                    "Help",
                    new List<DrawableGameComponent>()
                    {
                        new Sprite(Vector2.Zero, menu, Color.White),
                        new TextElement("Help", new Vector2(350, 125), Color.Black),
                        new TextElement(
                            "Prepare guests' drinks by\n" +
                            "pressing the prompted keys.\n\n" +
                            "Be careful to not assemble\n" +
                            "their order incorrectly!",
                            new Vector2(350, 160), Color.White),
                    }
                },
                {
                    "Credits",
                    new List<DrawableGameComponent>()
                    {
                        new Sprite(Vector2.Zero, menu, Color.White),
                        new TextElement("Developers",  new Vector2 (350, 125), Color.Black),
                        new TextElement("Jordan Partridge",new Vector2(350, 160), Color.White),
                        new TextElement("Thomas Heal",new Vector2(350, 195), Color.White),
                        new TextElement("Illustrators",new Vector2(350, 265), Color.Black),
                        new TextElement("Hannah Zhang",new Vector2(350, 300), Color.White),
                        new TextElement("Brooke Hardie",new Vector2(350, 335), Color.White),
                    }
                },
                {
                    "Options",
                    new List<DrawableGameComponent>()
                    {
                        //need to either add a sprite sheet to rotate through and animate the volume mixer OR just add different textures representing 
                        //the different volume levels 100% 75% 50% 25% 0%
                        new Sprite(Vector2.Zero, menu, Color.White),
                        new TextElement("Options", new Vector2(350, 125), Color.Black),
                        musicVolumeTex,
                        soundEffectTex,
                        soundEffectSprite,
                        volumeSprite
                    }
                },
                {
                    "Level1",
                    new List<DrawableGameComponent>()
                    {

                        new Sprite(Vector2.Zero, backgroundCafe, Color.White),
                        new Sprite(Vector2.Zero, cafeBar, Color.White)
                    }
                },
                {
                    "Level2",
                    new List<DrawableGameComponent>()
                    {
                        new Sprite(Vector2.Zero, backgroundCafe2, Color.White),
                        new Sprite(Vector2.Zero, cafeBar2, Color.White)
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
                        UnloadScene(SceneComponents);

                        foreach (DrawableGameComponent comp in playableMap)
                            Global.GameManager.Components.Add(comp);
                        //just testing the audiomanager instance here
                        if (sceneName == "Level1")
                        {
                            AudioManager.PlaySong("Level1Song");
                        }
                        if (sceneName == "Level2")
                        {
                            AudioManager.PlaySong("Level2Song");
                        }
                        if(sceneName == "Menu")
                        {
                            AudioManager.PlaySong("MenuTheme");
                        }
                        
                        SceneComponents = playableMap;
                        ActiveScene = sceneName;
                        break;
                }
            }
            else
            {
                foreach (DrawableGameComponent comp in playableMap)
                    Global.GameManager.Components.Add(comp);

                SceneComponents = playableMap;
                ActiveScene = sceneName;
                //this is to play the main menu music.
                AudioManager.PlaySong("MenuTheme");
            }
        }

        public static void UnloadScene(object scene)
        {
            CustomerManager.Customers.Clear();
            CustomerManager.Orders.Clear();
            CustomerManager.OrderQueue.Clear();
            CustomerManager.PickupQueue.Clear();

            foreach (DrawableGameComponent comp in (List<DrawableGameComponent>)scene)
                Global.GameManager.Components.Remove(comp);
        }
    }
}
