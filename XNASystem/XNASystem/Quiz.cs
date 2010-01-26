using System;
using System.Collections.Generic;
/*
 
 * This class is currently undergoing an overhaul from List based, to Stack based question management
 
 */
namespace XNASystem
{
	public class Quiz : IComponent<Question>
	{
		#region Variables
		// The listing of the currently loaded questions
		//private readonly List<Question> _questionList;
		private readonly String _title;
		private Question _openItem;
		private Status _status = Status.NotStarted;
		private Stack<Question> _questionStack;
		private Stack<Question> _answeredQuestionStack;
		private Score _score;
		#endregion

		#region Constructors
		/// <summary>
		/// Create a quiz with no questions, but initialize a new question list
		/// </summary>
		public Quiz(String title)
		{
			_title = title;
			//_questionList = new List<Question>();
			_questionStack = new Stack<Question>();
			_answeredQuestionStack = new Stack<Question>();
			_status = Status.NotStarted;
			_score = new Score("Eric",ActivityType.Quiz,0,_title);
		}

		/// <summary>
		/// Create a quiz with a pre-defined list of questions
		/// </summary>
		public Quiz(String title, Stack<Question> questions)
		{
			_title = title;
			//_questionList = questions;
			_questionStack = questions;
		}
		#endregion 

		#region Getters
		/// <summary>
		/// Advances the quiz to the next item (basically question)
		/// </summary>
		private Question AdvanceItem()
		{
			//return _questionList[_questionList.IndexOf(_openItem) + 1];
			return _questionStack.Pop();
		}

		private bool HasQuizBeenCompleted()
		{
			if(_questionStack.Count != 0)
			{
				return false;
			}
			_status = Status.Completed;
			return true;
		}

		/// <summary>
		/// Returns the number of items left in the quiz
		/// </summary>
		public int GetQuestionsLeft()
		{
			return _questionStack.Count;
		}

		public void AnswerQuestion(Answer a)
		{
			 if(GetOpenQuestion().AnswerQuestion(a))
			{
				_score.Value += 1;
			}
		}

		/// <summary>
		/// Returns the number of items in the quiz
		/// </summary>
		public int GetTotalQuestionCount()
		{
			return _questionStack.Count+_answeredQuestionStack.Count;
		}

		/// <summary>
		/// Advances the quiz to the next item (basically question)
		/// </summary>
		public string GetTitle()
		{
			return _title;
		}

		/// <summary>
		/// Advances the quiz to the next item (basically question)
		/// </summary>
		public Question GetOpenQuestion()
		{
			if (_questionStack.Count == 0)
			{
				_status = Status.Completed;
				return null;
			}
			_status = Status.InProgress;
			
			return _questionStack.Peek();
		}

		public float GetPercentDone()
		{
			return (1 - (GetQuestionsLeft()/GetTotalQuestionCount()));
		}

		public Question GetNextQuestion()
		{
			if (_questionStack.Count == 0)
			{
				_status = Status.Completed;
				return null;
			}
			_status = Status.InProgress;
			return _questionStack.Pop();
		}

		/// <summary>
		/// Advances the quiz to the next item (basically question)
		/// </summary>
		public Status GetStatus()
		{
			HasQuizBeenCompleted();
			return _status;
		}

		#endregion

		#region Setters
		/// <summary>
		/// Advances the quiz to the next item (basically question)
		/// </summary>
		public void AddItem(Question item)
		{
			_questionStack.Push(item);
			//_questionList.Add(item);
		}

		/// <summary>
		/// Resets this quiz to question 1
		/// </summary>
		public bool Reset()
		{
			//_openItem = _questionList[0];
			foreach (var question in _answeredQuestionStack)
			{
				question.Reset();
				_questionStack.Push(question);
			}
			_openItem = _questionStack.Pop();
			_status = Status.NotStarted;
			return true;
		}
		#endregion
	}
}
