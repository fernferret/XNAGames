using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace XNASystem
{
    class EditorMenu : IMenu
    {
        #region variables

        protected int _up;
        protected int _down;
        protected int _enter;
        protected int _choice;
        protected int _back;
        protected Stack<IMenu> _menuStack;
        protected SystemMain _systemMain;
        protected string _title;
        protected string _question;
        protected int _a,
                      _b,
                      _c,
                      _d,
                      _e,
                      _f,
                      _g,
                      _h,
                      _i,
                      _j,
                      _k,
                      _l,
                      _m,
                      _n,
                      _o,
                      _p,
                      _q,
                      _r,
                      _s,
                      _t,
                      _u,
                      _v,
                      _w,
                      _x,
                      _y,
                      _z,
                      _space;

        #endregion

        #region constructor
        public EditorMenu(Stack<IMenu> stack, SystemMain main)
        {
            _up = 1;
            _down = 1;
            _enter = 1;
            _back = 1;
            _choice = 0;
            _menuStack = stack;
            _systemMain = main;
            _title = "Enter Title Here";
            _question = "Enter Question Here";
            _a = 1;
            _b = 1;
            _c = 1;
            _d = 1;
            _e = 1;
            _f = 1;
            _g = 1;
            _h = 1;
            _i = 1;
            _j = 1;
            _k = 1;
            _l = 1;
            _m = 1;
            _n = 1;
            _o = 1;
            _p = 1;
            _q = 1;
            _r = 1;
            _s = 1;
            _t = 1;
            _u = 1;
            _v = 1;
            _w = 1;
            _x = 1;
            _y = 1;
            _z = 1;
            _space = 1;
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
                    //add correct answer
                    case 2:
                        // implement correct answer menu
                        break;
                    //add incorrect answer
                    case 3:
                        // implement incorrect answer menu
                        break;
                    //submit question
                    case 4:
                        // save question right here
                        _menuStack.Pop();
                        _systemMain.SetStack(_menuStack);
                        break;
                    // back
                    case 5:
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

            #region backspace controls

            // backspace control control
            if (state.IsKeyDown(Keys.Back) && _back != 1)
            {
                _back = 1;
                switch(_choice)
                {
                    case 0:
                        if (_title.Length != 0)
                        {
                            _title = _title.Remove(_title.Length - 1);
                        }
                        break;
                    case 1:
                        if (_question.Length != 0)
                        {
                            _question = _question.Remove(_question.Length - 1);
                        }
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.Back))
            {
                _back = 0;
            }

            #endregion

            #region space controls

            if (state.IsKeyDown(Keys.Space) && _space != 1)
            {
                _space = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, " ") : " ";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, " ") : " ";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.Space))
            {
                _space = 0;
            }

            #endregion

            #region letters

            if (state.IsKeyDown(Keys.A) && _a != 1)
            {
                _a = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "a") : "a";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "a") : "a";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.A))
            {
                _a = 0;
            }
            if (state.IsKeyDown(Keys.B) && _b != 1)
            {
                _b = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "b") : "b";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "b") : "b";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.B))
            {
                _b = 0;
            }
            if (state.IsKeyDown(Keys.C) && _c != 1)
            {
                _c = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "c") : "c";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "c") : "c";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.C))
            {
                _c = 0;
            }
            if (state.IsKeyDown(Keys.D) && _d != 1)
            {
                _d = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "d") : "d";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "d") : "d";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.D))
            {
                _d = 0;
            }
            if (state.IsKeyDown(Keys.E) && _e != 1)
            {
                _e = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "e") : "e";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "e") : "e";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.E))
            {
                _e = 0;
            }
            if (state.IsKeyDown(Keys.F) && _f != 1)
            {
                _f = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "f") : "f";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "f") : "f";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.F))
            {
                _f = 0;
            }
            if (state.IsKeyDown(Keys.G) && _g != 1)
            {
                _g = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "g") : "g";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "g") : "g";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.G))
            {
                _g = 0;
            }
            if (state.IsKeyDown(Keys.H) && _h != 1)
            {
                _h = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "h") : "h";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "h") : "h";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.H))
            {
                _h = 0;
            }
            if (state.IsKeyDown(Keys.I) && _i != 1)
            {
                _i = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "i") : "i";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "i") : "i";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.I))
            {
                _i = 0;
            }
            if (state.IsKeyDown(Keys.J) && _j != 1)
            {
                _j = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "j") : "j";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "j") : "j";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.J))
            {
                _j = 0;
            }
            if (state.IsKeyDown(Keys.K) && _k != 1)
            {
                _k = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "k") : "k";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "k") : "k";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.K))
            {
                _k = 0;
            }
            if (state.IsKeyDown(Keys.L) && _l != 1)
            {
                _l = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "l") : "l";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "l") : "l";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.L))
            {
                _l = 0;
            }
            if (state.IsKeyDown(Keys.M) && _m != 1)
            {
                _m = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "m") : "m";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "m") : "m";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.M))
            {
                _m = 0;
            }
            if (state.IsKeyDown(Keys.N) && _n != 1)
            {
                _n = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "n") : "n";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "n") : "n";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.N))
            {
                _n = 0;
            }
            if (state.IsKeyDown(Keys.O) && _o != 1)
            {
                _o = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "o") : "o";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "o") : "o";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.O))
            {
                _o = 0;
            }
            if (state.IsKeyDown(Keys.P) && _p != 1)
            {
                _p = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "p") : "p";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "p") : "p";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.P))
            {
                _p = 0;
            }
            if (state.IsKeyDown(Keys.Q) && _q != 1)
            {
                _q = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "q") : "q";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "q") : "q";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.Q))
            {
                _q = 0;
            }
            if (state.IsKeyDown(Keys.R) && _r != 1)
            {
                _r = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "r") : "r";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "r") : "r";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.R))
            {
                _r = 0;
            }
            if (state.IsKeyDown(Keys.S) && _s != 1)
            {
                _s = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "s") : "s";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "s") : "s";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.S))
            {
                _s = 0;
            }
            if (state.IsKeyDown(Keys.T) && _t != 1)
            {
                _t = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "t") : "t";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "t") : "t";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.T))
            {
                _t = 0;
            }
            if (state.IsKeyDown(Keys.U) && _u != 1)
            {
                _u = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "u") : "u";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "u") : "u";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.U))
            {
                _u = 0;
            }
            if (state.IsKeyDown(Keys.V) && _v != 1)
            {
                _v = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "v") : "v";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "v") : "v";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.V))
            {
                _v = 0;
            }
            if (state.IsKeyDown(Keys.W) && _w != 1)
            {
                _w = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "w") : "w";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "w") : "w";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.W))
            {
                _w = 0;
            }
            if (state.IsKeyDown(Keys.X) && _x != 1)
            {
                _x = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "x") : "x";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "x") : "x";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.X))
            {
                _x = 0;
            }
            if (state.IsKeyDown(Keys.W) && _w != 1)
            {
                _w = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "w") : "w";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "w") : "w";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.W))
            {
                _w = 0;
            }
            if (state.IsKeyDown(Keys.X) && _x != 1)
            {
                _x = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "x") : "x";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "x") : "x";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.X))
            {
                _x = 0;
            }
            if (state.IsKeyDown(Keys.Y) && _y != 1)
            {
                _y = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "y") : "y";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "y") : "y";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.Y))
            {
                _y = 0;
            }
            if (state.IsKeyDown(Keys.Z) && _z != 1)
            {
                _z = 1;
                switch (_choice)
                {
                    case 0:
                        _title = _title.Length != 0 ? _title.Insert(_title.Length, "z") : "z";
                        break;
                    case 1:
                        _question = _question.Length != 0 ? _question.Insert(_question.Length, "z") : "z";
                        break;
                    default:
                        break;
                }
            }
            if (state.IsKeyUp(Keys.Z))
            {
                _z = 0;
            }

            #endregion

            #region set choice
            // make sure that choice is always on an actually menu choice
            if (_choice == -1)
            {
                _choice = 6;
            }
            if (_choice == 6)
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
            spriteBatch.Draw(textures[0], new Rectangle(50, 185 + (60 * _choice), 700, 55), Color.White);

            //draw the title
            spriteBatch.DrawString(fonts[0], "Question Editor Menu", new Vector2(250, 100), Color.Black);

            //draw two text boxes
            spriteBatch.Draw(textures[2], new Rectangle(55, 190, 695, 45),Color.White);
            spriteBatch.Draw(textures[2], new Rectangle(55, 250, 695, 45), Color.White);

            //draw menu items
            spriteBatch.DrawString(fonts[0], _title, new Vector2(60, 200),Color.White);
            spriteBatch.DrawString(fonts[0], _question, new Vector2(60, 260), Color.White);
            spriteBatch.DrawString(fonts[0], "Add a correct Answer", new Vector2(60, 320), Color.Black);
            spriteBatch.DrawString(fonts[0], "Add an incorrect Answer", new Vector2(60, 380), Color.Black);
            spriteBatch.DrawString(fonts[0], "Submit Question", new Vector2(60, 440), Color.Black);
            spriteBatch.DrawString(fonts[0], "Back", new Vector2(60, 500), Color.Black);

            spriteBatch.End();
        }
        #endregion
    }
}
