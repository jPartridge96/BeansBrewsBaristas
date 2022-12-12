using BeansBrewsBaristas.Content.scripts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using BeansBrewsBaristas.BaseClassScripts;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.Managers
{
    public class SceneManager
    {
        //currently active scene
        public static string ActiveScene { get; set; }
        //list of scene components to be added at run time.
        public static List<DrawableGameComponent> SceneComponents { get; set; }
        //dictionary to determine the current level loaded
        private static Dictionary<string, List<DrawableGameComponent>> Atlas;

        //Menu components textures
        Texture2D menu = Global.GameManager.Content.Load<Texture2D>("Images/Menu");

        
        //Level components textures (bar and background are separate for layering purposes
        Texture2D backgroundCafe = Global.GameManager.Content.Load<Texture2D>("Images/CafeBackground1");
        Texture2D cafeBar = Global.GameManager.Content.Load<Texture2D>("Images/CafeBar1");
        Texture2D backgroundCafe2 = Global.GameManager.Content.Load<Texture2D>("Images/CafeBackground2");
        Texture2D cafeBar2 = Global.GameManager.Content.Load<Texture2D>("Images/CafeBar2");
        Texture2D backgroundCafe3 = Global.GameManager.Content.Load<Texture2D>("Images/CafeBackground3");
        Texture2D cafeBar3 = Global.GameManager.Content.Load<Texture2D>("Images/CafeBar3");

        //Options menu components textures
        public static Texture2D VolumeTex = Global.GameManager.Content.Load<Texture2D>("Images/VolumeBar");
        public static TextElement soundEffectTex = new TextElement($"Effects Volume: {GameManager.volume}", new Vector2(350, 250), Color.White);
        public static TextElement musicVolumeTex = new TextElement($"Music Volume {GameManager.volume}", new Vector2(350, 175), Color.White);
        public static Sprite soundEffectSprite = new Sprite(new Vector2(350, 300), VolumeTex, Color.White);
        public static Sprite volumeSprite = new Sprite(new Vector2(350, 225), VolumeTex, Color.White);

        //score stuff
        static Texture2D scoreBackgroundTex = Global.GameManager.Content.Load<Texture2D>("Images/scoreBackground");
        static Sprite scoreBackground = new Sprite(new Vector2(Global.Stage.X - scoreBackgroundTex.Width, 0), scoreBackgroundTex, Color.White);
        public static TextElement sceneScore = new TextElement($"", new Vector2(scoreBackground.Position.X + 3, scoreBackground.Position.Y), Color.Black);

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
                        new ComponentScripts.MenuComponent(Global.GameManager, new string[] {
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
                            "Prepare guests' drinks: \n" +
                            "Step 1) Select a Cup\n" +
                            "Step 2) Add your mods\n" +
                            "Step 3) Serve Order\n" +
                            "Becareful not to add\n" +
                            "the wrong modifications!",
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
                        new Sprite(Vector2.Zero, cafeBar, Color.White),
                        scoreBackground,
                        sceneScore,
                    }
                },
                {
                    "Level2",
                    new List<DrawableGameComponent>()
                    {
                        new Sprite(Vector2.Zero, backgroundCafe2, Color.White),
                        new Sprite(Vector2.Zero, cafeBar2, Color.White),
                        scoreBackground,
                        sceneScore,
                    }
                },
                {
                    "Level3",
                    new List<DrawableGameComponent>()
                    {
                        new Sprite(Vector2.Zero, backgroundCafe3, Color.White),
                        new Sprite(Vector2.Zero, cafeBar3, Color.White),
                        scoreBackground,
                        sceneScore,
                    }
                },

                
            };
        }

        public static SceneManager GetInstance()
        {
            if (_instance == null)
                _instance = new SceneManager();
            return _instance;
        }

        public static void LoadScene(string sceneName)
        {
            //reset the score on loadscene
            Global.score = 0;
            //set the message for the scoreboard
            SceneManager.sceneScore.Text = $"Tips: ${Global.score}";
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
                        if(sceneName == "Level3")
                        {
                            AudioManager.PlaySong("Level3Song");
                        }
                        if(sceneName == "Menu")
                        {
                            AudioManager.PlaySong("MenuTheme");
                        }
                        
                        SceneComponents = playableMap;
                        ActiveScene = sceneName;
                        break;
                }
                CustomerManager.CreateCustomer();
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

        private static SceneManager _instance;

    }
}
