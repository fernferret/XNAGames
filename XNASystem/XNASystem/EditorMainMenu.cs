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
        #endregion

        #region constructor
        public EditorMainMenu()
        {
            _up = 1;
            _down = 1;
            _enter = 1;
            _choice = 0;
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
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
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
        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D box, Texture2D background)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, "Question Editor Menu", new Vector2(250, 100), Color.Black);
            spriteBatch.DrawString(font, "Select Booklet (NYI)", new Vector2(100, 200), Color.Black);
            spriteBatch.DrawString(font, "Select Quiz (NYI)", new Vector2(100, 300), Color.Black);
            spriteBatch.DrawString(font, "Write New Question Here (NYI)", new Vector2(100,400), Color.Black);
            spriteBatch.DrawString(font, "Back (NYI)", new Vector2(100, 500), Color.Black);

            spriteBatch.Draw(box, new Vector2(75, 175 + (100 * _choice)), Color.White);

            spriteBatch.End();
        }
        #endregion
    }
}
