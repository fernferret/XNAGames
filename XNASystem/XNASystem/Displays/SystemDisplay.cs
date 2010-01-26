using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.QuizArch;
using XNASystem.Utils;


namespace XNASystem.Displays
{
	public class SystemDisplay : IScreen
	{
		private QuestionLoader _qLoad;
		private Booklet _booklet;
		private ScoreManager _scoreManager;
		private String _player;
		private Stack<Interfaces.IScreen> _menuStack;
		private SystemMain _systemMain;

		protected int _up;
		protected int _down;
		protected int _enter;
		protected int _choice;


		public SystemDisplay(Stack<IScreen> screens, SystemMain main)
		{
			_up = 1;
			_down = 1;
			_enter = 1;
			_choice = 0;
			_menuStack = screens;
			_systemMain = main;
			_qLoad = new QuestionLoader();

			//populate a booklet from xml files
			_booklet = _qLoad.PopulateSystem();
			_player = "Eric";
			_scoreManager = new ScoreManager(_player);
			
		}
		
		#region update
		public void Update(InputHandler handler)
		{
			_choice = handler.HandleMenuMovement(2, _choice);
			if (handler.IfEnterPressed())
			{
				// case system to perform appropriate action of the chosen menu item
				switch (_choice)
				{
					// take quiz
					case 0:
						_menuStack.Push(new QuizDisplay(_booklet.GetNextQuiz(), this));
						_systemMain.SetStack(_menuStack);

						break;
					// change options
					case 1:
						_menuStack.Pop();
						_systemMain.SetStack(_menuStack);
						break;
					default:
						break;
				}
			}

		}
		public void Update(KeyboardState state, GamePadState padkeyState)
		{
			#region arrow controls
			// up arrow control
			if (state.IsKeyDown(Keys.Up) && _up != 1)
			{
				_up = 1;
				_choice--;
			}
			if (state.IsKeyUp(Keys.Up))
			{
				_up = 0;
			}

			//down arrow control
			if (state.IsKeyDown(Keys.Down) && _down != 1)
			{
				_down = 1;
				_choice++;
			}
			if (state.IsKeyUp(Keys.Down))
			{
				_down = 0;
			}
			#endregion

			#region enter controls

			//enter key controls
			if (state.IsKeyDown(Keys.Enter) && _enter != 1)
			{
				_enter = 1;

				// case system to perform appropriate action of the chosen menu item
				switch (_choice)
				{
						// take quiz
					case 0:
						_menuStack.Push(new QuizDisplay(_booklet.GetNextQuiz(), this));
						_systemMain.SetStack(_menuStack);
						
						break;
						// change options
					case 1:
						_menuStack.Pop();
						_systemMain.SetStack(_menuStack);
						break;
					default:
						break;
				}
			}
			if (state.IsKeyUp(Keys.Enter))
			{
				_enter = 0;
			}

			#endregion

			#region set choice
			// make sure that choice is always on an actually menu choice
			if (_choice == -1)
			{
				_choice = 2;
			}
			if (_choice == 2)
			{
				_choice = 0;
			}

			#endregion
		}
		#endregion

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();
			// draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, 800, 600), Color.White);

			// draw the box whereever it may be
			spriteBatch.Draw(textures[0], new Vector2(75, 175 + (75 * _choice)), Color.White);

			// draw the menu title
			spriteBatch.DrawString(fonts[0], "Are you ready to take your quiz?", new Vector2(250, 100), Color.Black);

			//draw the menu options
			spriteBatch.DrawString(fonts[0], "Yes! (Start Quiz)", new Vector2(100, 200), Color.Black);
			spriteBatch.DrawString(fonts[0], "No! (Return to Menu)", new Vector2(100, 275), Color.Black);
			spriteBatch.End();
		}
		/* DO NOT DELETE THIS METHOD */
		/* IT WILL BE USED AS SOON AS THE GAME IS IMPLEMENTED*/
		internal void EndGameScoreReview()
		{
			_menuStack.Pop();
			_menuStack.Push(new QuizDisplay(_booklet.GetNextQuiz(), this));
			_systemMain.SetStack(_menuStack);
		}
		internal void EndQuizScoreReview()
		{
			_menuStack.Pop();
			_menuStack.Push(new BreakOut.BreakOut(this));
			_systemMain.SetStack(_menuStack);
		}
		public void EndGame(Score score)
		{
			_scoreManager.AddScore(score);
			_menuStack.Pop();
			if (_booklet.GetStatus() == Status.InProgress)
			{
				_menuStack.Push(new QuizDisplay(_booklet.GetNextQuiz(), this));
				_systemMain.SetStack(_menuStack);
			}
			else
			{
				// TODO: Need to put a done with quiz screen here.
				_menuStack.Pop();
			}
		}
		public void EndQuiz(Score score)
		{
			_scoreManager.AddScore(score);
			_menuStack.Pop();
			_menuStack.Push(new QuizResultsDisplay(this,_scoreManager.GetCumulativeQuizScore()));
			_systemMain.SetStack(_menuStack);
		}
	}
}