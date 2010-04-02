using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Displays;
using XNASystem.Interfaces;
using XNASystem.Utils;

namespace XNASystem.SystemMenus
{
	/// <summary>
	/// MainMenu
	/// 
	/// This class is the menu screen that the users will see first. It gives user the option to choose between starting a quiz-game loop,
	/// setting the options such as what booklet and what game they will be using. They can also choose to look at their scores. The last 
	/// option is to edit such as booklets quizzes and questions. This Menu also has the exit command that will close the entire program.
	/// 
	/// Constructor: MainMenu(Stack<IScreen> stack, SystemMain main)
	/// - stack is the list of menus that have stacked up so far
	/// - main is the instance of our main class that created this menu 
	/// 
	/// Created by: Andy Kruth
	/// Modified by: 
	/// </summary>
	class MainMenu : IScreen
	{
		#region variables

		private ScreenMenu _menu;
		protected Stack<IScreen> _menuStack;	// the stack of menus accumulated in the program so far
		protected SystemMain _systemMain;		// the instance of SystemMain that controls the whole system
		protected int _currentGameScore;
		private List<String> _menuText;
		#endregion

		#region constructor

		public MainMenu(Stack<IScreen> stack, SystemMain main)
		{
			_menuStack = stack;
			_systemMain = main;
			_menuText = new List<string> {"Take Quiz", "Options", "View Scores"/*, "Edit Material"*/, "Exit"};
			_menu = new ScreenMenu(_menuText,"XNA GAMES!");
			
			//SystemMain.Drawing.DrawInstruction(40, 640, " to go back", SystemMain.TexturePackage["B"], 4);
			
		}

		#endregion

		#region update

		/// <summary>
		/// Update
		/// 
		/// This method is called in our system mains update which is called extremely frequently. This method is responsible for checking
		/// the keyboard keyState and performing the appropriate actions when keys are pressed and released.
		/// </summary>
		/// <param name="handler">the key and button handler</param>
		public void Update()
		{
			_menu.Update();

			if (_menu.GetSelectedItem("Take Quiz"))
			{
				_menuStack.Push(new SystemDisplay(_menuStack, _systemMain));
				_systemMain.SetStack(_menuStack);
			}
			if (_menu.GetSelectedItem("Options"))
			{
				// create an options menu than push it onto the _menuStack
				_menuStack.Push(new OptionsMenu(_menuStack, _systemMain));
				// return the new _menuStack to main
				_systemMain.SetStack(_menuStack);
			}
			if (_menu.GetSelectedItem("View Scores"))
			{
				// create a scores menu and add it to the stack
				_menuStack.Push(new ScoresMenu(_menuStack, _systemMain));
				//return the new stack to main
				_systemMain.SetStack(_menuStack);
			}
			if (_menu.GetSelectedItem("Exit"))
			{
				_systemMain.Close();
			}
		}

		#endregion

		/// <summary>
		/// Draw
		/// 
		/// This method is called in the main systems draw method. This method draws to the screen everything that makes up this screen.
		/// </summary>
		/// <param name="spriteBatch"> the object needed to draw things in XNA</param>
		/// <param name="fonts"> a list of fonts that cn be used in this screen</param>
		/// <param name="textures"> a list of textures that can be used to draw this screens</param>
		#region draw
		public void Draw()
		{
			SystemMain.GameSpriteBatch.Begin();
			_menu.Draw();
			SystemMain.Drawing.DrawHelpers();
			SystemMain.GameSpriteBatch.End();
		}
		#endregion
	}
}