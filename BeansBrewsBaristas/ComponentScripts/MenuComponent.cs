using BeansBrewsBaristas.BaseClassScripts;
using BeansBrewsBaristas.Content.scripts;
using BeansBrewsBaristas.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.ComponentScripts
{
    public class MenuComponent : DrawableGameComponent
    {
        private Vector2 position;
        private Color regularColor = Color.White;
        private Color highlightColor = Color.Red;
        private SpriteFont regularFont = Global.GameManager.Content.Load<SpriteFont>("fonts/Font");
        private SpriteFont highlightFont;
        private static List<string> menuItems;

        public static int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                if (selectedIndex == menuItems.Count)
                    selectedIndex = 0;
                else if (selectedIndex == -1)
                    selectedIndex = menuItems.Count - 1;
            }
        }
        private static int selectedIndex = 0;

        public MenuComponent(Game game, string[] menuArray, Vector2 position)
            : base(game)
        {
            this.position = position;
            menuItems = menuArray.ToList();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Global.SpriteBatch.Begin();

            Vector2 tempPos = position;
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    Global.SpriteBatch.DrawString(regularFont ?? Debug.Font, menuItems[i], tempPos, highlightColor); // hilight
                    tempPos.Y += Debug.Font.LineSpacing;
                }
                else
                {
                    Global.SpriteBatch.DrawString(regularFont ?? Debug.Font, menuItems[i], tempPos, regularColor); // regular
                    tempPos.Y += Debug.Font.LineSpacing;
                }
            }

            Global.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public static void Select()
        {
            switch (SelectedIndex)
            {
                case 0: // Play
                    SceneManager.LoadScene("Level1");
                    break;
                case 1: // Help
                    SceneManager.LoadScene("Help");
                    break;
                case 2: // Credits
                    SceneManager.LoadScene("Credits");
                    break;
                case 3:
                    SceneManager.LoadScene("Options");
                    break;
                case 4: // Quit
                    Global.GameManager.Exit();
                    break;
            }
        }
    }
}
