using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;
using XNASystem.QuizArch;
using XNASystem.SystemMenus;
using XNASystem.Utils;


namespace XNASystem.Displays
{
	public class SystemDisplay : IScreen
	{
		private ScreenMenu _menu;
		//private QuestionLoader _qLoad;
		private Booklet _booklet;
		private ScoreManager _scoreManager;
		private String _player;
		private Stack<IScreen> _menuStack;
		private SystemMain _systemMain;
		private List<String> _menuText = new List<string> { "Yes! (Start Quiz)", "No! (Return to Menu)" };

		//protected int _up;
		//protected int _down;
		//protected int _enter;
		//protected int _choice;
		// Start the game at level 0
		protected int _level = 0;



		public SystemDisplay(Stack<IScreen> screens, SystemMain main)
		{
			_menuStack = screens;
			_systemMain = main;
			//_qLoad = new QuestionLoader();

			//populate a booklet from file system
			_booklet = SystemMain.Booklets[SystemMain.SelectedBooklet];
			
			_player = "Eric";
			_scoreManager = new ScoreManager(_player);

			_menu = new ScreenMenu(_menuText, "Are you ready to take your quiz?");
			
		}

		#region update
		public void Update()
		{
			_menu.Update();
			if (_menu.GetSelectedItem() == "No! (Return to Menu)")
			{
				_menuStack.Pop();

				_systemMain.SetStack(_menuStack);
			}
			if (_menu.GetSelectedItem() == "Yes! (Start Quiz)")
			{
				_booklet.Reset();
				_menuStack.Push(new QuizDisplay(_booklet.GetNextQuiz(), this));
				_systemMain.SetStack(_menuStack);
			}
		}
		#endregion

		public void Draw()
		{
			SystemMain.GameSpriteBatch.Begin();
			_menu.Draw();
			SystemMain.GameSpriteBatch.End();
		}
		internal void ShowFinal()
		{
			_menuStack.Pop();
			_menuStack.Push(new QuizResultsDisplay(this, _scoreManager.GetCumulativeQuizScore(), true));
			//_menuStack.Push(new FinalResultsDisplay(this, _scoreManager));
			_systemMain.SetStack(_menuStack);
		}
		internal void EndQuizScoreReview()
		{
			_menuStack.Pop();
			//_menuStack.Push(new BreakOut.BreakOut(this));
			switch (OptionsMenu.Game)
			{
				case "Breakout":
					_menuStack.Push(new BreakOut.BreakOut(this, _level));
					break;
				case "DeathSquid":
					_menuStack.Push(new Shooter.Shooter(this, _level));
					break;
				default:
					_menuStack.Push(new Shooter.Shooter(this, _level));
					break;
			}
			_level++;
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
				// TODO: Need to put a done with game screen here.
				_menuStack.Pop();
				_menuStack.Push(new QuizResultsDisplay(this, _scoreManager.GetCumulativeQuizScore(), false));
				_systemMain.SetStack(_menuStack);

			}
		}
		public void EndQuiz(Score score)
		{
			_scoreManager.AddScore(score);
			_menuStack.Pop();
			_menuStack.Push(new QuizResultsDisplay(this,_scoreManager.GetCumulativeQuizScore(), false));
			_systemMain.SetStack(_menuStack);
		}
	}
}