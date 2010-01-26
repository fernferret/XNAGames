using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Displays;
using XNASystem.Interfaces;
using XNASystem.MaterialEditor;

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
				//set _enter to reflect that its pressed
				_enter = 1;

				// case system to perform appropriate action of the chosen menu item based on _choice
				switch (_choice)
				{
						// take quiz - run the quiz-game loop
					case 0:
						_menuStack.Push(new SystemDisplay(_menuStack, _systemMain));
						_systemMain.SetStack(_menuStack);
						break;
						// play game - only for debugging will be deleted later
					case 1:	
						// Removed as it is no longer valid here.
						break;
						// change options
					case 2:
						// create an options menu than push it onto the _menuStack
						_menuStack.Push(new OptionsMenu(_menuStack, _systemMain));

						// return the new _menuStack to main
						_systemMain.SetStack(_menuStack);

						break;
						// view scores
					case 3:
						// create a scores menu and add it to the stack
						_menuStack.Push(new ScoresMenu(_menuStack, _systemMain));

						//return the new stack to main
						_systemMain.SetStack(_menuStack);

						break;
						// write questions
					case 4:
						//create a editormainmenu menu and add it to the stack
						_menuStack.Push(new EditorMainMenu(_menuStack, _systemMain));

						//retrn the new stack to main
						_systemMain.SetStack(_menuStack);

						break;
						// exit
					case 5:
						// tell main to close the program
						_systemMain.Exit();

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
				_choice = 6;
			}

			// if choice has gon down too far than force it to the upper most option
			if (_choice == 6)
			{
				_choice = 0;
			}

			#endregion
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

			// draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, 800, 600), Color.White);

			// draw the box whereever it may be
			spriteBatch.Draw(textures[0], new Vector2(75, 175 + (66 * _choice)), Color.White);

			// draw the menu title
			spriteBatch.DrawString(fonts[0], "Welcome to the XNA Game System", new Vector2(250, 100), Color.Black);

			//draw the menu options
			spriteBatch.DrawString(fonts[0], "Start Quiz", new Vector2(100, 200), Color.Black);
			spriteBatch.DrawString(fonts[0], "Debug Game (Disabled)", new Vector2(100, 266), Color.Black);
			spriteBatch.DrawString(fonts[0], "Options (Disabled)", new Vector2(100, 333), Color.Black);
			spriteBatch.DrawString(fonts[0], "View Scores (Disabled)", new Vector2(100, 400), Color.Black);
			spriteBatch.DrawString(fonts[0], "Edit Material (Disabled)", new Vector2(100, 466), Color.Black);
			spriteBatch.DrawString(fonts[0], "Exit", new Vector2(100, 500), Color.Black);

			spriteBatch.End();
		}
		#endregion
	}
}