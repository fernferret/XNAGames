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

		public QuizDisplay(Quiz q)
		{
			_up = 1;
			_down = 1;
			_enter = 1;
			_choice = 0;
			_currentQuizScore.PlayerName = "Eric";
			_currentQuizScore.Type = ActivityType.Quiz;
			_currentQuizScore.Value = 0;
			_currentQuiz = q;
			SetQuestion();
		}

    	private void SetQuestion()
    	{
    		_currentQuestion = _currentQuiz.GetNextQuestion();
    		_currentQuestionAnswers = _currentQuestion.GetRandomAnswers();
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
				_choice = 5;
			}
			if (_choice == 5)
			{
				_choice = 0;
			}

			#endregion
		}
		#endregion

    	public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
    	{
    		spriteBatch.Begin();
			spriteBatch.DrawString(fonts[0], _currentQuiz.GetTitle(), new Vector2(10,10), Black);
			spriteBatch.DrawString(fonts[0], _currentQuestion.GetTitle(), new Vector2(100, 100), Black);
    		var i = 100;
			foreach (var q in _currentQuestion.GetAllAnswers())
			{
				spriteBatch.DrawString(fonts[0], q.ToString(), new Vector2(10, i), Black);
				i += 20;
			}
			spriteBatch.End();
    	}

		public void SetQuiz(Quiz q)
		{
			_currentQuiz = q;
		}

		public void AdvanceQuestion()
		{
			//_currentQuiz.Advance();
		}

		private void AnswerQuestion()
		{

		}

		public Score GetScore()
		{
			return _currentQuizScore;
		}
    }
}