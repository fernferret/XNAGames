using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.QuizArch;
using XNASystem.Utils;

namespace XNASystem.Displays
{
	public class QuizDisplay: IScreen
	{
		public Color Black = Color.Black;
		public Color Red = Color.Red;
		private Quiz _currentQuiz;
		private Question _currentQuestion;
		private Score _currentQuizScore;
		private List<Answer> _currentQuestionAnswers;
		private ScreenMenu _menu;

		private SystemDisplay _display;

		public QuizDisplay(Quiz q, SystemDisplay sysDisplay)
		{
			_display = sysDisplay;
			
			_currentQuiz = q;
			_currentQuestion = _currentQuiz.GetNextQuestion();
			_currentQuestionAnswers = _currentQuestion.GetRandomAnswers();
			_currentQuizScore = new Score("Eric", ActivityType.Quiz, 0, _currentQuiz.GetTitle());
			_menu = new ScreenMenu(_currentQuestionAnswers,_currentQuiz.GetTitle(),_currentQuestion.Title);
			SystemMain.Drawing.DestroyTips();
			SystemMain.Drawing.DrawInstruction(40, 660, " to confirm your answer!", SystemMain.TexturePackage["A"], -1);
		}


		#region update
		public void Update()
		{
			foreach (var sound in SystemMain.SoundsBackgroundInstance)
			{
				if (sound.State == SoundState.Playing)
				{
					sound.Stop();
				}
			}
			if (SystemMain.SoundQuizBgInstance.State != SoundState.Playing)
			{
				if (!SystemMain.SoundQuizBgInstance.IsLooped)
				{ SystemMain.SoundQuizBgInstance.IsLooped = true; }
				
				SystemMain.SoundQuizBgInstance.Volume = .4f;
				SystemMain.SoundQuizBgInstance.Play();
			}
			_menu.Update();
			
				// Answer the question with the current selection obtained from _menu
			//if(SystemMain.GetInput.IsButtonPressed(ButtonAction.MenuAccept))
			//{
			if (_menu.GetSelectedItem(false) != "")
			{
				AnswerTheQuestion(_currentQuestion.GetAnswer(_menu.GetSelectedItem(true)));
			}
				
			//}
			
		}

		private void AnswerTheQuestion(Answer answer)
		{
			if(_currentQuestion.AnswerQuestion(answer))
			{
				_currentQuizScore.Value += 1;
			}
			AdvanceQuestion();
		}

		private void AdvanceQuestion()
		{
			_currentQuizScore.SetPercentage(_currentQuiz.GetTotalQuestionCount());
			_currentQuestion = _currentQuiz.GetNextQuestion();
			if(_currentQuestion == null)
			{
				_display.EndQuiz(_currentQuizScore);
			}
			else
			{
				_currentQuestionAnswers = _currentQuestion.GetRandomAnswers();
				_menu.SetText(_currentQuestionAnswers);
			}
    		
		}

		#endregion

		public void Draw()
		{
			SystemMain.GameSpriteBatch.Begin();
			_menu.Draw();
			//spriteBatch.Draw(textures[1], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);

			// draw the box
			//var widthOfCurrentString = (int)(Math.Ceiling(_currentFont.MeasureString(_menuText[_choice]).X));
			//var menuText = new List<String>();
			//foreach (var q in _currentQuestionAnswers)
			//{
			//	menuText.Add(q.ToString());
			//}
			//SystemMain.DrawHelper.DrawSelection(new[] { textures[0], textures[64], textures[65] }, SystemMain.DrawHelper.GetDrawLocations(menuText)[_choice], (int)(Math.Ceiling(fonts[1].MeasureString(menuText[_choice]).X)));

			// draw the menu title
			//SystemMain.DrawHelper.DrawTitleCentered(fonts[2], _currentQuiz.GetTitle());
			//SystemMain.DrawHelper.DrawSubTitleCentered(fonts[1], _currentQuestion.GetTitle());

			//draw the menu options
			//SystemMain.DrawHelper.DrawMenu(menuText, fonts[1]);
			var horizontalOffset = 800;
			SystemMain.GameSpriteBatch.DrawString(SystemMain.FontPackage["Main"], "% Done: " + _currentQuiz.GetPercentDone(), new Vector2(horizontalOffset, 400), Color.White);
			SystemMain.GameSpriteBatch.DrawString(SystemMain.FontPackage["Main"], "Questions Left: " + _currentQuiz.GetQuestionsLeft(), new Vector2(horizontalOffset, 450), Color.White);
			SystemMain.GameSpriteBatch.DrawString(SystemMain.FontPackage["Main"], "Total Questions: " + _currentQuiz.GetTotalQuestionCount(), new Vector2(horizontalOffset, 500), Color.White);
			SystemMain.GameSpriteBatch.DrawString(SystemMain.FontPackage["Main"], "Current Score: " + _currentQuizScore.Percentage, new Vector2(horizontalOffset, 550), Color.White);
			SystemMain.Drawing.DrawHelpers();
			SystemMain.GameSpriteBatch.End();
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