using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.QuizArch;
using XNASystem.Utils;

namespace XNASystem.MaterialEditor
{
	class CorrectAnswerMenu : IScreen
	{
		#region variables

		protected int _up;
		protected int _down;
		protected int _enter;
		protected int _choice;
		protected int _back;
		protected Stack<IScreen> _menuStack;
		protected SystemMain _systemMain;
		protected string _answer;
		protected EditorMenu _question;
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
		public CorrectAnswerMenu(Stack<IScreen> stack, SystemMain main, EditorMenu question)
		{
			_up = 1;
			_down = 1;
			_enter = 1;
			_back = 1;
			_choice = 0;
			_menuStack = stack;
			_systemMain = main;
			_question = question;
			_answer = "Enter Question Here";
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

		public void Update(InputHandler handler)
		{
			var keyState = handler.GetKeyState();
			#region arrow controls
			// up arrow control
			if (keyState.IsKeyDown(Keys.Up) && _up != 1)
			{
				_up = 1;
				_choice--;
			}
			if (keyState.IsKeyUp(Keys.Up))
			{
				_up = 0;
			}

			//down arrow control
			if (keyState.IsKeyDown(Keys.Down) && _down != 1)
			{
				_down = 1;
				_choice++;
			}
			if (keyState.IsKeyUp(Keys.Down))
			{
				_down = 0;
			}
			#endregion

			#region enter controls

			//enter key controls
			if (keyState.IsKeyDown(Keys.Enter) && _enter != 1)
			{
				_enter = 1;

				// case system to perform appropriate action of the chosen menu item
				switch (_choice)
				{
					//submit answer
					case 1:
						_question.AddAnswer(new Answer(_answer, true));
						_menuStack.Pop();
						_systemMain.SetStack(_menuStack);
						break;
					//back
					case 2:
						_menuStack.Pop();
						_systemMain.SetStack(_menuStack);
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.Enter))
			{
				_enter = 0;
			}

			#endregion

			#region backspace controls

			// backspace control control
			if (keyState.IsKeyDown(Keys.Back) && _back != 1)
			{
				_back = 1;
				switch (_choice)
				{
					case 0:
						if (_answer.Length != 0)
						{
							_answer = _answer.Remove(_answer.Length - 1);
						}
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.Back))
			{
				_back = 0;
			}

			#endregion

			#region space controls

			if (keyState.IsKeyDown(Keys.Space) && _space != 1)
			{
				_space = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, " ") : " ";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.Space))
			{
				_space = 0;
			}

			#endregion

			#region letters

			if (keyState.IsKeyDown(Keys.A) && _a != 1)
			{
				_a = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "a") : "a";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.A))
			{
				_a = 0;
			}
			if (keyState.IsKeyDown(Keys.B) && _b != 1)
			{
				_b = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "b") : "b";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.B))
			{
				_b = 0;
			}
			if (keyState.IsKeyDown(Keys.C) && _c != 1)
			{
				_c = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "c") : "c";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.C))
			{
				_c = 0;
			}
			if (keyState.IsKeyDown(Keys.D) && _d != 1)
			{
				_d = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "d") : "d";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.D))
			{
				_d = 0;
			}
			if (keyState.IsKeyDown(Keys.E) && _e != 1)
			{
				_e = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "e") : "e";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.E))
			{
				_e = 0;
			}
			if (keyState.IsKeyDown(Keys.F) && _f != 1)
			{
				_f = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "f") : "f";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.F))
			{
				_f = 0;
			}
			if (keyState.IsKeyDown(Keys.G) && _g != 1)
			{
				_g = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "g") : "g";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.G))
			{
				_g = 0;
			}
			if (keyState.IsKeyDown(Keys.H) && _h != 1)
			{
				_h = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "h") : "h";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.H))
			{
				_h = 0;
			}
			if (keyState.IsKeyDown(Keys.I) && _i != 1)
			{
				_i = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "i") : "i";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.I))
			{
				_i = 0;
			}
			if (keyState.IsKeyDown(Keys.J) && _j != 1)
			{
				_j = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "j") : "j";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.J))
			{
				_j = 0;
			}
			if (keyState.IsKeyDown(Keys.K) && _k != 1)
			{
				_k = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "k") : "k";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.K))
			{
				_k = 0;
			}
			if (keyState.IsKeyDown(Keys.L) && _l != 1)
			{
				_l = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "l") : "l";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.L))
			{
				_l = 0;
			}
			if (keyState.IsKeyDown(Keys.M) && _m != 1)
			{
				_m = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "m") : "m";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.M))
			{
				_m = 0;
			}
			if (keyState.IsKeyDown(Keys.N) && _n != 1)
			{
				_n = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "n") : "n";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.N))
			{
				_n = 0;
			}
			if (keyState.IsKeyDown(Keys.O) && _o != 1)
			{
				_o = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "o") : "o";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.O))
			{
				_o = 0;
			}
			if (keyState.IsKeyDown(Keys.P) && _p != 1)
			{
				_p = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "p") : "p";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.P))
			{
				_p = 0;
			}
			if (keyState.IsKeyDown(Keys.Q) && _q != 1)
			{
				_q = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "q") : "q";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.Q))
			{
				_q = 0;
			}
			if (keyState.IsKeyDown(Keys.R) && _r != 1)
			{
				_r = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "r") : "r";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.R))
			{
				_r = 0;
			}
			if (keyState.IsKeyDown(Keys.S) && _s != 1)
			{
				_s = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "s") : "s";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.S))
			{
				_s = 0;
			}
			if (keyState.IsKeyDown(Keys.T) && _t != 1)
			{
				_t = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "t") : "t";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.T))
			{
				_t = 0;
			}
			if (keyState.IsKeyDown(Keys.U) && _u != 1)
			{
				_u = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "u") : "u";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.U))
			{
				_u = 0;
			}
			if (keyState.IsKeyDown(Keys.V) && _v != 1)
			{
				_v = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "v") : "v";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.V))
			{
				_v = 0;
			}
			if (keyState.IsKeyDown(Keys.W) && _w != 1)
			{
				_w = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "w") : "w";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.W))
			{
				_w = 0;
			}
			if (keyState.IsKeyDown(Keys.X) && _x != 1)
			{
				_x = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "x") : "x";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.X))
			{
				_x = 0;
			}
			if (keyState.IsKeyDown(Keys.W) && _w != 1)
			{
				_w = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "w") : "w";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.W))
			{
				_w = 0;
			}
			if (keyState.IsKeyDown(Keys.X) && _x != 1)
			{
				_x = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "x") : "x";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.X))
			{
				_x = 0;
			}
			if (keyState.IsKeyDown(Keys.Y) && _y != 1)
			{
				_y = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "y") : "y";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.Y))
			{
				_y = 0;
			}
			if (keyState.IsKeyDown(Keys.Z) && _z != 1)
			{
				_z = 1;
				switch (_choice)
				{
					case 0:
						_answer = _answer.Length != 0 ? _answer.Insert(_answer.Length, "z") : "z";
						break;
					default:
						break;
				}
			}
			if (keyState.IsKeyUp(Keys.Z))
			{
				_z = 0;
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

		#region draw
		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			//draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, 800, 600), Color.White);

			// draw the selection box
			spriteBatch.Draw(textures[0], new Rectangle(50, 185 + (150 * _choice), 700, 55), Color.White);

			//draw the title
			spriteBatch.DrawString(fonts[0], "Correct Answer Editor Menu", new Vector2(250, 100), Color.Black);

			//draw the text boxes
			spriteBatch.Draw(textures[2], new Rectangle(55, 190, 695, 45), Color.White);

			//draw menu items
			spriteBatch.DrawString(fonts[0], _answer, new Vector2(60, 200), Color.White);
			spriteBatch.DrawString(fonts[0], "Submit Answer", new Vector2(60, 350), Color.Black);
			spriteBatch.DrawString(fonts[0], "Back", new Vector2(60, 500), Color.Black);

			spriteBatch.End();
		}
		#endregion
	}
}