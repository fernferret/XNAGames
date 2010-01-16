using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNASystem
{
    class OptionsMenu: IMenu
    {
        #region variables

        protected int _up;
        protected int _down;
        protected int _enter;
        protected int _choice;
        protected Stack<IMenu> _menuStack;
        protected SystemMain _systemMain;

        #endregion

        #region constructor
        public OptionsMenu(Stack<IMenu> stack, SystemMain main)
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
                    // color option
                    case 0:
                        break;
                    // booklet option
                    case 1:
                        break;
                    // game option
                    case 2:
                        break;
                        // back
                    case 3:
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
                _choice = 4;
            }
            if (_choice == 4)
            {
                _choice = 0;
            }

            #endregion
        }
        #endregion

        #region draw
        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D box, Texture2D background)
        {
            spriteBatch.Begin();

            // draw the background
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 600), Color.White);

            // draw te selection box
            spriteBatch.Draw(box, new Vector2(75, 175 + (100 * _choice)), Color.White);

            // draw the titl
            spriteBatch.DrawString(font, "Options Menu", new Vector2(250, 100), Color.Black);

            // draw the menu items
            spriteBatch.DrawString(font, "Select Color Scheme (NYI)", new Vector2(100, 200), Color.Black);
            spriteBatch.DrawString(font, "Select Booklet (NYI)", new Vector2(100, 300), Color.Black);
            spriteBatch.DrawString(font, "Select Game (NYI)", new Vector2(100, 400), Color.Black);
            spriteBatch.DrawString(font, "Back", new Vector2(100, 500), Color.Black);

            spriteBatch.End();
        }
        #endregion
    }
}