using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNASystem
{
    public class QuizDisplay: IScreen
    {
		public Color Black = Color.Black;
		public Color Red = Color.Red;
    	private Quiz _currentQuiz;
    	private Question _currentQuestion;
    	private Score _currentQuizScore;
    	private List<Answer> _currentQuestionAnswers;

		protected int _up;
		protected int _down;
		protected int _enter;
		protected int _choice;

    	private SystemDisplay _display;

		public QuizDisplay(Quiz q, SystemDisplay sysDisplay)
		{
			_display = sysDisplay;
			_up = 1;
			_down = 1;
			_enter = 1;
			_choice = 0;
			
			_currentQuiz = q;
			_currentQuestion = _currentQuiz.GetNextQuestion();
			_currentQuestionAnswers = _currentQuestion.GetRandomAnswers();
			_currentQuizScore = new Score("Eric", ActivityType.Quiz, 0, _currentQuiz.GetTitle());
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
				_currentQuestion.AnswerQuestion(_currentQuestionAnswers[_choice]);
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
				_choice = _currentQuestionAnswers.Count;
			}
			if (_choice == _currentQuestionAnswers.Count)
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

			//draw the menu options
			spriteBatch.DrawString(fonts[0], _currentQuiz.GetTitle(), new Vector2(50, 50), Color.Black);
			spriteBatch.DrawString(fonts[0], _currentQuestion.GetTitle(), new Vector2(250, 100), Color.Black);
			var i = 200;
			foreach (var q in _currentQuestion.GetAllAnswers())
			{
				spriteBatch.DrawString(fonts[0], q.ToString(), new Vector2(100, i), Black);
				i += 75;
			}

			spriteBatch.End();
    	}

		public void SetQuiz(Quiz q)
		{
			_currentQuiz = q;
		}

		private void AnswerQuestion()
		{
			if(_currentQuiz.GetStatus() == Status.Completed)
			{
				_display.EndQuiz(_currentQuizScore);
			}
		}

		public Score GetScore()
		{
			return _currentQuizScore;
		}
    }
}