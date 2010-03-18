using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.Utils;
using XNASystem;

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
		private ScreenMenu _menu;
		private readonly List<String> _menuText;

		protected Stack<IScreen> _menuStack;	// the stack of menus accumulated in the program so far
		protected SystemMain _systemMain;		// the instance of SystemMain that controls the whole system
		public static String Game;

		public OptionsMenu(Stack<IScreen> stack, SystemMain main)
		{
			_menuStack = stack;
			_systemMain = main;

			_menuText = new List<string> {"Breakout", "DeathSquid", "Back"};
			_menu = new ScreenMenu(_menuText, "Options");

		}

		public void Update()
		{
			_menu.Update();
			if(_menu.GetSelectedItem() == "Back")
			{
				//take this menu off the stack
				_menuStack.Pop();

				// return the new stack to main
				_systemMain.SetStack(_menuStack);
			}
			Game = _menu.GetSelectedItem();
		}

		public void Draw()
		{
			SystemMain.GameSpriteBatch.Begin();
			_menu.Draw();
			SystemMain.GameSpriteBatch.End();
		}
	}
}