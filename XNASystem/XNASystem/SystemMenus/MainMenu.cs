using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Displays;
using XNASystem.Interfaces;
using XNASystem.MaterialEditor;
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

		protected int _up;						// indicator that the up key is pressed or released
		protected int _down;					// indicator that the down key is pressed or released
		protected int _enter;					// indicator that the enter key is pressed or released
		protected int _choice;					// place holder for where the users choice icon is
		protected Stack<IScreen> _menuStack;	// the stack of menus accumulated in the program so far
		protected SystemMain _systemMain;		// the instance of SystemMain that controls the whole system
		protected int _currentGameScore;
		private List<String> _menuText;
		private SpriteFont _currentFont;
		private bool _addHelpBox = true;
		#endregion

		#region constructor

		public MainMenu(Stack<IScreen> stack, SystemMain main)
		{
			_up = 0;
			_down = 0;
			_enter = 0;
			_choice = 0;
			_menuStack = stack;
			_systemMain = main;
			_menuText = new List<string> {"Take Quiz", "Options", "View Scores", "Edit Material", "Exit"};
			
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
		public void Update(InputHandler handler, GameTime gameTime)
		{
			_choice = handler.HandleMenuMovement(5, _choice);
			
			if (handler.IfEnterPressed())
			{
				// case system to perform appropriate action of the chosen menu item based on _choice
				switch (_choice)
				{
					// take quiz - run the quiz-game loop
					case 0:
						_menuStack.Push(new SystemDisplay(_menuStack, _systemMain));
						_systemMain.SetStack(_menuStack);
						break;
					// change options
					case 1:
						// create an options menu than push it onto the _menuStack
						_menuStack.Push(new OptionsMenu(_menuStack, _systemMain));
						// return the new _menuStack to main
						_systemMain.SetStack(_menuStack);
						break;
					// view scores
					case 2:
						// create a scores menu and add it to the stack
						_menuStack.Push(new ScoresMenu(_menuStack, _systemMain));
						//return the new stack to main
						_systemMain.SetStack(_menuStack);
						break;
					// write questions
					case 3:
						//create a editormainmenu menu and add it to the stack
						_menuStack.Push(new EditorMainMenu(_menuStack, _systemMain));
						//retrn the new stack to main
						_systemMain.SetStack(_menuStack);
						break;
					// exit
					case 4:
						// tell main to close the program
                        _systemMain.Close();
						break;
					default:
						break;
				}
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
		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();
			if(_addHelpBox)
			{
				SystemMain.DrawHelper.AddHelpBox(new[] { textures[29], textures[27], textures[28] }, 600, 300, 300, 200);
				//SystemMain.DrawHelper.AddHelpBox(new[] { textures[29], textures[27], textures[28] }, 500, 500, 100, 100);
				_addHelpBox = false;
			}
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);

			// draw the box
			//var widthOfCurrentString = (int)(Math.Ceiling(_currentFont.MeasureString(_menuText[_choice]).X));
			SystemMain.DrawHelper.DrawSelection(new[] { textures[0], textures[25], textures[26] }, SystemMain.DrawHelper.GetDrawLocations(_menuText)[_choice], (int)(Math.Ceiling(fonts[1].MeasureString(_menuText[_choice]).X)));

			// draw the menu title
			SystemMain.DrawHelper.DrawTitleCentered(fonts[2], "Welcome to the XNA Game System");
			SystemMain.DrawHelper.DrawHelpBox();
			//SystemMain.DrawHelper.DrawRectangle(new[] {textures[29],textures[27],textures[28]},300,300,300,200);
			
			//draw the menu options
			SystemMain.DrawHelper.DrawMenu(_menuText, fonts[1]);

			spriteBatch.End();
		}
		#endregion
	}
}