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
		public QuizResultsDisplay(SystemDisplay display, Score s)
		{
			_up = 1;
			_down = 1;
			_enter = 1;
			_choice = 0;
			_display = display;
			_score = s;
		}

		#region update
		public void Update(InputHandler handler)
		{
			_choice = handler.HandleMenuMovement(1, _choice, 4);
			if(handler.IfEnterPressed())
			{
				switch (_choice)
				{
					// take quiz
					case 4:
						_display.EndQuizScoreReview();
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

			// draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, 800, 600), Color.White);

			// draw the box whereever it may be
			spriteBatch.Draw(textures[0], new Vector2(75, 175 + (75 * _choice)), Color.White);

			// draw the menu title
			spriteBatch.DrawString(fonts[0], "Quiz Score:", new Vector2(250, 100), Color.Black);

			//draw the menu options
			spriteBatch.DrawString(fonts[0], "Quiz Questions Correct: " + _score.Value, new Vector2(100, 200), Color.Black);
			spriteBatch.DrawString(fonts[0], "Quiz Percentage: " + _score.Percentage, new Vector2(100, 275), Color.Black);
			spriteBatch.DrawString(fonts[0], "Start Game!", new Vector2(100, 500), Color.Black);

			spriteBatch.End();
		}
	}
}