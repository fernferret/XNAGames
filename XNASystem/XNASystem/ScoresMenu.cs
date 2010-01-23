using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace XNASystem
{
	/// <summary>
	/// ScoresMenu
	/// 
	/// This screen displays scores accordng to user. users will have the chance to select which game and which qui 
	/// they want to look at and their scores will be given to them.
	/// 
	/// Constructor: MainMenu(Stack<IScreen> stack, SystemMain main)
	/// - stack is the list of menus that have stacked up so far
	/// - main is the instance of our main class that created this menu 
	/// 
	/// Created by: Andy Kruth
	/// Modified by: 
	/// </summary>
    class ScoresMenu : IScreen
    {
        #region variables

        protected int _up;
        protected int _down;
        protected int _enter;
        protected int _choice;
        protected Stack<IScreen> _menuStack;
        protected SystemMain _systemMain;

        #endregion

        #region constructor
        public ScoresMenu(Stack<IScreen> stack, SystemMain main)
        {
            _up = 1;
            _down = 1;
            _enter = 1;
            _choice = 0;
            _menuStack = stack;
            _systemMain = main;
        }
        #endregion

        #region update

		/// <summary>
		/// Update
		/// 
		/// This method is called in our system mains update which is called extremely frequently. This method is responsible for checking
		/// the keyboard state and performing the appropriate actions when keys are pressed and released.
		/// </summary>
		/// <param name="state"> the current keys that are pressed</param>
        public void Update(KeyboardState state)
        {
            #region arrow controls
            // up arrow control
            if (state.IsKeyDown(Keys.Up) && _up != 1)
            {
                _up = 1;
                _choice--;
            }
            if (state.IsKeyUp(Keys.Up))
            {
                _up = 0;
            }

            //down arrow control
            if (state.IsKeyDown(Keys.Down) && _down != 1)
            {
                _down = 1;
                _choice++;
            }
            if (state.IsKeyUp(Keys.Down))
            {
                _down = 0;
            }
            #endregion

            #region enter controls

            //enter key controls
            if (state.IsKeyDown(Keys.Enter) && _enter != 1)
            {
                _enter = 1;

                // case system to perform appropriate action of the chosen menu item
                switch (_choice)
                {
                        // quiz scores
                    case 0:
                        break;
                        // game scores
                    case 1:
                        break;
                        // back
                    case 2:
						// remove this menu than return the list to main
                        _menuStack.Pop();
                        _systemMain.SetStack(_menuStack);
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.Enter))
            {
                _enter = 0;
            }

            #endregion

            #region set choice
            // make sure that choice is always on an actually menu choice
            if (_choice == -1)
            {
                _choice = 3;
            }
            if (_choice == 3)
            {
                _choice = 0;
            }

            #endregion
        }
        #endregion

        #region draw

		/// <summary>
		/// Draw
		/// 
		/// This method is called in the main systems draw method. This method draws to the screen everything that makes up this screen.
		/// </summary>
		/// <param name="spriteBatch"> the object needed to draw things in XNA</param>
		/// <param name="fonts"> a list of fonts that cn be used in this screen</param>
		/// <param name="textures"> a list of textures that can be used to draw this screens</param>
        public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
        {
            spriteBatch.Begin();

            //draw the background
            spriteBatch.Draw(textures[1], new Rectangle(0, 0, 800, 600), Color.White);

            //draw te selection box
            spriteBatch.Draw(textures[0], new Vector2(75, 175 + (150 * _choice)), Color.White);

            //draw the title
            spriteBatch.DrawString(fonts[0], "Score Viewer", new Vector2(250, 100), Color.Black);

            //draw the menu items
            spriteBatch.DrawString(fonts[0], "View Quiz Scores (NYI)", new Vector2(100, 200), Color.Black);
            spriteBatch.DrawString(fonts[0], "View Game Scores (NYI)", new Vector2(100, 350), Color.Black);
            spriteBatch.DrawString(fonts[0], "Back", new Vector2(100, 500), Color.Black);

            spriteBatch.End();
        }
        #endregion
    }
}