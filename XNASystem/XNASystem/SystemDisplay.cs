using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
///
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
		public SystemDisplay(Stack<IScreen> screens, SystemMain main)
		{
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

		private void StartSequence()
		{
			while(_inSession)
			{
				_scoreManager.AddScore(TakeQuiz());
				_booklet.AdvanceQuiz();
				if(_booklet.GetStatus() == Status.Completed)
				{
					_inSession = false;
				}
				_scoreManager.AddScore(PlayGame());
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
		public void Update(KeyboardState state)
		{
			var boole = _kc.EnterKeyIsPressed(state);
			if(state.IsKeyDown(Keys.Enter))
			{
				_menuStack.Push(new OptionsMenu(_menuStack, _systemMain));
				_systemMain.SetStack(_menuStack);
			}
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();
			spriteBatch.DrawString(fonts[0],"Ready to take your quiz?",new Vector2(100,100),Color.Black);
			spriteBatch.End();
		}
	}
}
