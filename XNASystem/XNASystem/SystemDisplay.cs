using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNASystem
{
	class SystemDisplay : IScreen
	{
		private QuestionLoader _qLoad;
		private Booklet _booklet;
		private ScoreManager _scoreManager;
		private String _player;
		private bool _inSession;
		private Stack<IScreen> _menuStack;
		private SystemMain _systemMain;
		private KeyChecker _kc;
		private int _currentState;
		private GameDisplay _gd;
		private QuizDisplay _qd;

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
			_kc = new KeyChecker();
			_menuStack = screens;
			_systemMain = main;
			_qLoad = new QuestionLoader();

			//populate a booklet from xml files
			_booklet = _qLoad.PopulateSystem();
			_player = "Eric";
			_scoreManager = new ScoreManager(_player);
			_inSession = true;
			//StartSequence();
		}

/*		private void StartSequence()
		{
			while(_inSession)
			{
				_scoreManager.AddScore(TakeQuiz());
				//_booklet.AdvanceQuiz();
				if(_booklet.GetStatus() == Status.Completed)
				{
					_inSession = false;
				}
				_scoreManager.AddScore(PlayGame());
			}
		}*/

		private void SwitchApplication()
		{
			var current = _menuStack.Pop();
			if (current.GetType() == typeof(GameDisplay))
			{
				_menuStack.Push(new QuizDisplay(_booklet.GetNextQuiz()));
			}
			if (current.GetType() == typeof(QuizDisplay))
			{
				_menuStack.Push(new GameDisplay());
			}
			
		}

		public Score TakeQuiz()
		{
			// This code is correct, but won't work yet!
			/*while(_booklet.GetOpenItem(false).GetStatus() != Status.Completed)
			{
				
			}*/
			return new Score(_player, ActivityType.Quiz, 100, "DummyQuiz");
		}
		public Score PlayGame()
		{
			return new Score(_player,ActivityType.Game,100,"DummyGame");
		}
		#region update
		public void Update(KeyboardState state)
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
						SwitchApplication();
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
	}
}
