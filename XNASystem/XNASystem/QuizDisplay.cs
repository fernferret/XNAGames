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
		public QuizDisplay(Quiz q)
		{
			_currentQuizScore.PlayerName = "Eric";
			_currentQuizScore.Type = ActivityType.Quiz;
			_currentQuizScore.Value = 0;
			_currentQuiz = q;
			SetQuestion();
		}

    	private void SetQuestion()
    	{
    		_currentQuestion = _currentQuiz.GetNextQuestion();
    	}

    	public void Update(KeyboardState state)
    	{
    		throw new NotImplementedException();
    	}

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