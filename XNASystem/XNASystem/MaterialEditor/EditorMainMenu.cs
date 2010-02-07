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
	/// <summary>
	/// EditorMainMenu
	/// 
	/// This screen is the starting point for enter new material to the system. it displays the currently selected 
	/// booklet and quiz. it allows the user to create a new booklet, new quiz or new quesiton. it allso allows the 
	/// user to select the booklet and quiz like a directory for adding new questions.
	/// 
	/// Constructor: MainMenu(Stack<IScreen> stack, SystemMain main)
	/// - stack is the list of menus that have stacked up so far
	/// - main is the instance of our main class that created this menu 
	/// 
	/// Created by: Andy Kruth
	/// Modified by: 
	/// </summary>
	class EditorMainMenu : IScreen
	{
		#region variables

		protected int _up;
		protected int _down;
		protected int _enter;
		protected int _choice;
		protected int _currentBooklet;
		protected int _currentQuiz;
		protected Stack<IScreen> _menuStack;
		protected SystemMain _systemMain;

		#endregion

		#region constructor
		public EditorMainMenu(Stack<IScreen> stack, SystemMain main)
		{
			_up = 1;
			_down = 1;
			_enter = 1;
			_choice = 0;
			_menuStack = stack;
			_systemMain = main;
			_currentBooklet = _systemMain.GetCurrentBooklet();
			_currentQuiz = _systemMain.GetCurrentQuiz();
		}
		#endregion

		#region update
		public void Update(InputHandler handler, GameTime gameTime)
		{
			_choice = handler.HandleMenuMovement(4, _choice);
			if(handler.IfEnterPressed())
			{
				// case system to perform appropriate action of the chosen menu item
				switch (_choice)
				{
					//change booklet
					case 0:
						_menuStack.Push(new SelectBookletMenu(_menuStack, _systemMain, this));
						_systemMain.SetStack(_menuStack);
						break;
					// change quiz
					case 1:
						_menuStack.Push(new SelectQuizMenu(_menuStack, _systemMain, this));
						_systemMain.SetStack(_menuStack);
						break;
					//write question
					case 2:
						_menuStack.Push(new EditorMenu(_menuStack, _systemMain, this));
						_systemMain.SetStack(_menuStack);
						break;
					// back
					case 3:
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
		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			// draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);

			// draw the selection box
			spriteBatch.Draw(textures[0], new Vector2(75, 175 + (100 * _choice)), Color.White);

			//draw the title
			spriteBatch.DrawString(fonts[0], "Question Editor Menu", new Vector2(250, 100), Color.Black);

			//draw the menu items
			spriteBatch.DrawString(fonts[0], "Select Booklet", new Vector2(100, 200), Color.Black);
			//spriteBatch.DrawString(fonts[0], _systemMain.GetBookletList()[_currentBooklet].GetTitle(), new Vector2(400, 200), Color.Black);

			spriteBatch.DrawString(fonts[0], "Select Quiz", new Vector2(100, 300), Color.Black);

			// if there are no quizzes in the current booklet than say so
			if (_systemMain.GetBookletList()[_systemMain.GetCurrentBooklet()].GetAsList().Count == 0)
			{
				spriteBatch.DrawString(fonts[0], "No Quizzes here", new Vector2(400, 300), Color.Red);
			}
			else
			{
				spriteBatch.DrawString(fonts[0], _systemMain.GetBookletList()[_systemMain.GetCurrentBooklet()].GetAsList()[_currentQuiz].GetTitle(), new Vector2(400, 300), Color.Black);
			}

			spriteBatch.DrawString(fonts[0], "Write New Question Here", new Vector2(100, 400), Color.Black);
			spriteBatch.DrawString(fonts[0], "Back", new Vector2(100, 500), Color.Black);
			

			spriteBatch.End();
		}
		#endregion

		public void SetCurrentBooklet(int index)
		{
			_currentBooklet = index;
			_currentQuiz = 0;
		}

		public void SetCurrentQuiz(int index)
		{
			_currentQuiz = index;
		}

		public int GetCurrentQuiz()
		{
			return _currentQuiz;
		}
		/*public Quiz GetCurrentQuizAsQuiz()
		{
			var list = _systemMain.GetBookletList();

			return list[_systemMain.GetCurrentBooklet()].GetSpecificQuiz(_currentQuiz);
		}*/
		public int GetCurrentBooklet()
		{
			return _currentBooklet;
		}
	}
}