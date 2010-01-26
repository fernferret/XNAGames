using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNASystem
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
					case 4:
						_display.EndQuizScoreReview();
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
			// Force user to accept choice 5
			if (_choice != 4)
			{
				_choice = 4;
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
			spriteBatch.DrawString(fonts[0], "Quiz Score:", new Vector2(250, 100), Color.Black);

			//draw the menu options
			spriteBatch.DrawString(fonts[0], "Quiz Questions Correct: " + _score.Value, new Vector2(100, 200), Color.Black);
			spriteBatch.DrawString(fonts[0], "Quiz Percentage: " + _score.Percentage, new Vector2(100, 275), Color.Black);
			spriteBatch.DrawString(fonts[0], "Start Game!", new Vector2(100, 500), Color.Black);

			spriteBatch.End();
    	}
    }
}