using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.QuizArch;
using XNASystem.SystemMenus;
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
		private List<String> _menuText = new List<string> { "Yes! (Start Quiz)", "No! (Return to Menu)" };

		protected int _up;
		protected int _down;
		protected int _enter;
		protected int _choice;
		protected int _level = 0;



		public SystemDisplay(Stack<IScreen> screens, SystemMain main)
		{
			_up = 1;
			_down = 1;
			_enter = 1;
			_choice = 0;
			_menuStack = screens;
			_systemMain = main;
			//_qLoad = new QuestionLoader();

			//populate a booklet from xml files
			//_booklet = _qLoad.PopulateSystem();
			_booklet = SystemMain.Booklets[0];
			
			_player = "Eric";
			_scoreManager = new ScoreManager(_player);
			
		}

		#region update
		public void Update(InputHandler handler, GameTime gameTime)
		{
			_choice = handler.HandleMenuMovement(2, _choice);
			if (handler.IfEnterPressed())
			{
				// case system to perform appropriate action of the chosen menu item
				switch (_choice)
				{
					// take quiz
					case 0:
						_booklet.Reset();
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
		#endregion

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			spriteBatch.Draw(textures[1], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);

			// draw the box
			//var widthOfCurrentString = (int)(Math.Ceiling(_currentFont.MeasureString(_menuText[_choice]).X));
			SystemMain.DrawHelper.DrawSelection(new[] { textures[0], textures[64], textures[65] }, SystemMain.DrawHelper.GetDrawLocations(_menuText)[_choice], (int)(Math.Ceiling(fonts[1].MeasureString(_menuText[_choice]).X)));

			// draw the menu title
			SystemMain.DrawHelper.DrawTitleCentered(fonts[2], "Are you ready to take your quiz?");

			//draw the menu options
			SystemMain.DrawHelper.DrawMenu(_menuText, fonts[1]);

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