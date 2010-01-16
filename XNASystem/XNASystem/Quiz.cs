using System;
using System.Collections.Generic;

namespace XNASystem
{
	class Quiz : IComponent<Question>
	{
		// The listing of the currently loaded questions
		private readonly List<Question> _questionList;
		private readonly String _title;
		private Question _openItem;
		private Status _status = Status.NotStarted;

		/// <summary>
		/// Create a quiz with no questions, but initialize a new question list
		/// </summary>
		public Quiz(String title)
		{
			_title = title;
			_questionList = new List<Question>();
		}

		/// <summary>
		/// Create a quiz with a pre-defined list of questions
		/// </summary>
		public Quiz(String title, List<Question> questions)
		{
			_title = title;
			_questionList = questions;
		}

		/// <summary>
		/// Advances the quiz to the next item (basically question)
		/// </summary>
		private Question AdvanceItem()
		{
			return _questionList[_questionList.IndexOf(_openItem) + 1];
		}

		private bool CheckStatus()
		{
			if (_questionList.IndexOf(_openItem) < GetItemCount() - 1)
			{
				return true;
			}
			_status = Status.Completed;
			return false;
		}

		/// <summary>
		/// Advances the quiz to the next item (basically question)
		/// </summary>
		public int GetItemCount()
		{
			return _questionList.Count;
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
		public Question GetOpenItem(bool advance)
		{
			if (_status == Status.NotStarted)
			{
				_status = Status.InProgress;
				_openItem = _questionList[0];
			}
			else if (advance)
			{
				// Set the _openItem to the next item in the quiz
				_openItem = CheckStatus() ? AdvanceItem() : null;
			}

			return _openItem;
		}

		/// <summary>
		/// Advances the quiz to the next item (basically question)
		/// </summary>
		public Status GetStatus()
		{
			CheckStatus();
			return _status;
		}

		/// <summary>
		/// Advances the quiz to the next item (basically question)
		/// </summary>
		public void AddItem(Question item)
		{
			_questionList.Add(item);
		}

		/// <summary>
		/// Resets this quiz to question 1
		/// </summary>
		public bool Reset()
		{
			_openItem = _questionList[0];
			foreach (var question in _questionList)
			{
				question.Reset();
			}
			_status = Status.NotStarted;
			return true;
		}

		/// <summary>
		/// Get the current item as a menu (in this case, just passes down to question)
		/// </summary>
		internal Menu MenuOf(bool advance)
		{
			return GetOpenItem(advance).GetAsMenu();
		}
	}
}
