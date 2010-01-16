using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace XNASystem
{
    class EditorMainMenu : IMenu
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
        public EditorMainMenu(Stack<IMenu> stack, SystemMain main)
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
                        //change booklet
                    case 0:
                        break;
                        // change quiz
                    case 1:
                        break;
                        //write question
                    case 2:
                        _menuStack.Push(new EditorMenu(_menuStack, _systemMain));
                        _systemMain.SetStack(_menuStack);
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
        public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
        {
            spriteBatch.Begin();

            //draw the background
            spriteBatch.Draw(textures[1], new Rectangle(0, 0, 800, 600), Color.White);

            // draw the selection box
            spriteBatch.Draw(textures[0], new Vector2(75, 175 + (100 * _choice)), Color.White);

            //draw the title
            spriteBatch.DrawString(fonts[0], "Question Editor Menu", new Vector2(250, 100), Color.Black);

            //draw the menu items
            spriteBatch.DrawString(fonts[0], "Select Booklet (NYI)", new Vector2(100, 200), Color.Black);
            spriteBatch.DrawString(fonts[0], "Select Quiz (NYI)", new Vector2(100, 300), Color.Black);
            spriteBatch.DrawString(fonts[0], "Write New Question Here", new Vector2(100, 400), Color.Black);
            spriteBatch.DrawString(fonts[0], "Back", new Vector2(100, 500), Color.Black);

            spriteBatch.End();
        }
        #endregion
    }
}
