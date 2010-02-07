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
		public void Update(InputHandler handler, GameTime gameTime)
		{
			_choice = handler.HandleMenuMovement(3, _choice);
			if(handler.IfEnterPressed())
			{
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
		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures, int height, int width)
		{
			spriteBatch.Begin();

			// draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, width, height), Color.White);

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