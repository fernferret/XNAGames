using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNASystem
{
    class MainMenu : IScreen
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

        public MainMenu(Stack<IScreen> stack, SystemMain main)
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
                    // take quiz
                    case 0:
                        break;
                    // change options
                    case 1:
                        _menuStack.Push(new OptionsMenu(_menuStack, _systemMain));
                        _systemMain.SetStack(_menuStack);
                        break;
                    // view scores
                    case 2:
                        _menuStack.Push(new ScoresMenu(_menuStack, _systemMain));
                        _systemMain.SetStack(_menuStack);
                        break;
                    // write questions
                    case 3:
                        _menuStack.Push(new EditorMainMenu(_menuStack, _systemMain));
                        _systemMain.SetStack(_menuStack);
                        break;
                    // exit
                    case 4:
                        _systemMain.Exit();
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
                _choice = 5;
            }
            if (_choice == 5)
            {
                _choice = 0;
            }

            #endregion
        }
        #endregion

        #region draw
        public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
        {
            spriteBatch.Begin();

            // draw the background
            spriteBatch.Draw(textures[1], new Rectangle(0, 0, 800, 600), Color.White);

            // draw the box whereever it may be
            spriteBatch.Draw(textures[0], new Vector2(75, 175 + (75 * _choice)), Color.White);

            // draw the menu title
            spriteBatch.DrawString(fonts[0], "Welcome to the XNA Game System", new Vector2(250, 100), Color.Black);

            //draw the menu options
            spriteBatch.DrawString(fonts[0], "Start Quiz (NYI)", new Vector2(100, 200), Color.Black);
            spriteBatch.DrawString(fonts[0], "Options", new Vector2(100, 275), Color.Black);
            spriteBatch.DrawString(fonts[0], "View Scores", new Vector2(100, 350), Color.Black);
            spriteBatch.DrawString(fonts[0], "Write Questions", new Vector2(100, 425), Color.Black);
            spriteBatch.DrawString(fonts[0], "Exit", new Vector2(100, 500), Color.Black);

            spriteBatch.End();
        }
        #endregion
    }
}
