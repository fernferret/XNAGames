using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.Utils;

namespace XNASystem.Displays
{
	public class QuizResultsDisplay:IScreen
	{

		protected int _up;
		protected int _down;
		protected int _enter;
		protected int _choice;
		private Score _score;
		private SystemDisplay _display;
		private List<String> _menuText = new List<String>{"Play Game!"};
		private bool _isdone;
		private String _menuTitle = "Score Viewer";
		private ScreenMenu _menu;

		public QuizResultsDisplay(SystemDisplay display, Score s, bool isdone)
		{
			_isdone = isdone;
			_up = 1;
			_down = 1;
			_enter = 1;
			_choice = 0;
			_display = display;
			_score = s;
		}

		#region update
		public void Update()
		{
			///_menu.Update();
			if(SystemMain.GetInput.IsButtonPressed(ButtonAction.MenuAccept))
			{
				_display.EndQuizScoreReview();
			}
		}
		#endregion

		public void Draw()
		{
			SystemMain.GameSpriteBatch.Begin();

			// draw the background
			SystemMain.GameSpriteBatch.Draw(SystemMain.TexturePackage["Background"], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);

			// draw the box whereever it may be
			//spriteBatch.Draw(textures[0], new Vector2(75, 175 + (75 * _choice)), Color.White);

			// draw the menu title
			//spriteBatch.DrawString(fonts[0], "Quiz Score:", new Vector2(250, 100), Color.Black);
			//SystemMain.GameSpriteBatch.Draw(textures[1], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);
			//SystemMain.Drawing.DrawSelection(new[] { textures[0], textures[64], textures[65] }, SystemMain.Height-200, (int)(Math.Ceiling(fonts[1].MeasureString(_menuText[0]).X)));

			// draw the menu title
			if(_isdone)
			{
				_menuTitle = "You're Done!";
			}
			SystemMain.Drawing.DrawTitleCentered(SystemMain.FontPackage["Main"], _menuTitle);
			//SystemMain.Drawing.DrawHelpBox();
			//SystemMain.Drawing.DrawMenu(_menuText, fonts[1]);

			//draw the menu options
			//SystemMain.Drawing.DrawRectangle(50, 150, 160, 300, new[] { textures[68], textures[66], textures[67] });
			SystemMain.GameSpriteBatch.DrawString(SystemMain.FontPackage["Main"], "Quiz Questions Correct: " + _score.Value, new Vector2(100, 200), Color.Black);
			SystemMain.GameSpriteBatch.DrawString(SystemMain.FontPackage["Main"], "Quiz Percentage: " + _score.Percentage, new Vector2(100, 275), Color.Black);
			//spriteBatch.DrawString(fonts[0], "Start Game!", new Vector2(100, 500), Color.Black);

			SystemMain.GameSpriteBatch.End();
		}
	}
}