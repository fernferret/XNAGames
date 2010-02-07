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

		private List<String> _menuText = new List<string> { "Select Color Scheme (NYI)", "Select Booklet (NYI)", "Select Game (NYI)", "Back"};
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
		public void Update(InputHandler handler, GameTime gameTime)
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
/*			spriteBatch.Begin();

			// draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);

			// draw the selection box
			spriteBatch.Draw(textures[0], new Vector2(75, 175 + (100 * _choice)), Color.White);

			// draw the titl
			spriteBatch.DrawString(fonts[0], "Options Menu", new Vector2(250, 100), Color.Black);

			// draw the menu items
			spriteBatch.DrawString(fonts[0], "Select Color Scheme (NYI)", new Vector2(100, 200), Color.Black);
			spriteBatch.DrawString(fonts[0], "Select Booklet (NYI)", new Vector2(100, 300), Color.Black);
			spriteBatch.DrawString(fonts[0], "Select Game (NYI)", new Vector2(100, 400), Color.Black);
			spriteBatch.DrawString(fonts[0], "Back", new Vector2(100, 500), Color.Black);

			spriteBatch.End();*/
			spriteBatch.Begin();

			spriteBatch.Draw(textures[1], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);

			// draw the box
			//var widthOfCurrentString = (int)(Math.Ceiling(_currentFont.MeasureString(_menuText[_choice]).X));
			SystemMain.DrawHelper.DrawSelection(new[] { textures[0], textures[25], textures[26] }, SystemMain.DrawHelper.GetDrawLocations(_menuText)[_choice], (int)(Math.Ceiling(fonts[1].MeasureString(_menuText[_choice]).X)));

			// draw the menu title
			SystemMain.DrawHelper.DrawTitleCentered(fonts[2], "Options Menu");

			//draw the menu options
			SystemMain.DrawHelper.DrawMenu(_menuText, fonts[1]);

			spriteBatch.End();
		}
		#endregion
	}
}