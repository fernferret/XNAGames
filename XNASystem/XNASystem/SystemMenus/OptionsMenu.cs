using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.Utils;

namespace XNASystem.SystemMenus
{
	/// <summary>
	/// OptionsMenu
	/// 
	/// This is a screen that shows the different user changeable options for the system. The user can chooce to change the
	/// booklet their quiz will come from, the game they want to play and the color scheme of the whole system.
	/// 
	/// Constructor: MainMenu(Stack<IScreen> stack, SystemMain main)
	/// - stack is the list of menus that have stacked up so far
	/// - main is the instance of our main class that created this menu 
	/// 
	/// Created by: Andy Kruth
	/// Modified by:
	/// </summary>
	class OptionsMenu: IScreen
	{
		#region variables

		protected int _up;						// indicator that the up key is pressed or released
		protected int _down;					// indicator that the down key is pressed or released
		protected int _enter;					// indicator that the enter key is pressed or released
		protected int _choice;					// place holder for where the users choice icon is
		protected Stack<IScreen> _menuStack;	// the stack of menus accumulated in the program so far
		protected SystemMain _systemMain;		// the instance of SystemMain that controls the whole system

		#endregion

		#region constructor
		public OptionsMenu(Stack<IScreen> stack, SystemMain main)
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
		/// the keyboard keyState and performing the appropriate actions when keys are pressed and released.
		/// </summary>
		/// <param name="keyState"> the current keys that are pressed</param>
		public void Update(KeyboardState keyState, GamePadState padState)
		{
			#region arrow controls

			// UP arrow control

			// If the UP key is pressed and _up is 0 meaning that it hasnt been pressed already than move _choice up
			if (keyState.IsKeyDown(Keys.Up) && _up != 1)
			{
				//set _up to reflect that it has been pressed
				_up = 1;

				// move the _choice marker
				_choice--;
			}

			// If the UP key is released than set _up to 0
			if (keyState.IsKeyUp(Keys.Up))
			{
				//set _up to reflect that it is released
				_up = 0;
			}

			// DOWN arrow control

			// If the DOWN key is pressed and _down is 0 meaning that it hasnt been pressed already than move _choice down
			if (keyState.IsKeyDown(Keys.Down) && _down != 1)
			{
				//set _down to reflect tha tit is pressed
				_down = 1;

				//move the _choice marker
				_choice++;
			}

			//If the DOWN key is released than set -down to 0
			if (keyState.IsKeyUp(Keys.Down))
			{
				//set _down to refelct that it is released
				_down = 0;
			}

			#endregion

			#region enter controls

			//ENTER key controls

			//If the ENTER key is pressed and _enter is 0 meaning it hasnt pressed yet than do the appropriate action inside
			if (keyState.IsKeyDown(Keys.Enter) && _enter != 1)
			{
				_enter = 1;

				// case system to perform appropriate action of the chosen menu item based on _choice
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
						//take this menu off the stack
						_menuStack.Pop();

						// return the new stack to main
						_systemMain.SetStack(_menuStack);

						break;
					default:
						break;
				}
			}

			// If Enter is released than set _enter to 0 
			if (keyState.IsKeyUp(Keys.Enter))
			{
				_enter = 0;
			}

			#endregion

			#region set choice

			// if choice has gone up too far than force it to the bottom most option
			if (_choice == -1)
			{
				_choice = 4;
			}

			// if choice has gon down too far than force it to the upper most option
			if (_choice == 4)
			{
				_choice = 0;
			}

			#endregion
		}

		public void Update(InputHandler handler)
		{
			_choice = handler.HandleMenuMovement(4, _choice);
			if(handler.IfEnterPressed())
			{
				// case system to perform appropriate action of the chosen menu item based on _choice
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
						//take this menu off the stack
						_menuStack.Pop();

						// return the new stack to main
						_systemMain.SetStack(_menuStack);

						break;
					default:
						break;
				}
			}
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

			// draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, 800, 600), Color.White);

			// draw te selection box
			spriteBatch.Draw(textures[0], new Vector2(75, 175 + (100 * _choice)), Color.White);

			// draw the titl
			spriteBatch.DrawString(fonts[0], "Options Menu", new Vector2(250, 100), Color.Black);

			// draw the menu items
			spriteBatch.DrawString(fonts[0], "Select Color Scheme (NYI)", new Vector2(100, 200), Color.Black);
			spriteBatch.DrawString(fonts[0], "Select Booklet (NYI)", new Vector2(100, 300), Color.Black);
			spriteBatch.DrawString(fonts[0], "Select Game (NYI)", new Vector2(100, 400), Color.Black);
			spriteBatch.DrawString(fonts[0], "Back", new Vector2(100, 500), Color.Black);

			spriteBatch.End();
		}
		#endregion
	}
}