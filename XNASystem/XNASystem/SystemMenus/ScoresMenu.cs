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

		private ScreenMenu _menu;
		protected Stack<IScreen> _menuStack;
		protected SystemMain _systemMain;
		private List<String> _menuText = new List<string>{"View Quiz Scores (NYI)","View Game Scores (NYI)","Back"};

		#endregion

		#region constructor
		public ScoresMenu(Stack<IScreen> stack, SystemMain main)
		{
			_menuStack = stack;
			_systemMain = main;
			_menu = new ScreenMenu(_menuText,"Scores Menu");
		}
		#endregion

		#region update
		public void Update()
		{
			_menu.Update();
			if (_menu.GetSelectedItem("Back"))
			{
				_menuStack.Pop();
				_systemMain.SetStack(_menuStack);
			}
		}

		#endregion

		#region draw
		public void Draw()
		{
			SystemMain.GameSpriteBatch.Begin();
			_menu.Draw();
			//spriteBatch.Draw(textures[1], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);
			//SystemMain.DrawHelper.DrawSelection(new[] { textures[0], textures[64], textures[65] }, SystemMain.DrawHelper.GetDrawLocations(_menuText)[_choice], (int)(Math.Ceiling(fonts[1].MeasureString(_menuText[_choice]).X)));

			// draw the menu title
			//SystemMain.DrawHelper.DrawTitleCentered(fonts[2], "Score Viewer");
			//SystemMain.DrawHelper.DrawHelpBox();
			//SystemMain.DrawHelper.DrawMenu(_menuText, fonts[1]);
			// draw the background
			

			//draw te selection box
			//spriteBatch.Draw(textures[0], new Vector2(75, 175 + (150 * _choice)), Color.White);

			//draw the title
			//spriteBatch.DrawString(fonts[0], "Score Viewer", new Vector2(250, 100), Color.Black);

			//draw the menu items
			//spriteBatch.DrawString(fonts[0], "View Quiz Scores (NYI)", new Vector2(100, 200), Color.Black);
			//spriteBatch.DrawString(fonts[0], "View Game Scores (NYI)", new Vector2(100, 350), Color.Black);
			//spriteBatch.DrawString(fonts[0], "Back", new Vector2(100, 500), Color.Black);

			SystemMain.GameSpriteBatch.End();
		}
		#endregion
	}
}