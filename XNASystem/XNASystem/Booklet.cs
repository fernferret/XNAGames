using System;
using System.Collections.Generic;

namespace XNASystem
{
	public class Booklet : IComponent<Quiz>
	{
		#region Variables
		private readonly String _title;

		private Quiz _openItem;

		private Status _status = Status.NotStarted;
		private Stack<Quiz> _quizStack;
		private Stack<Quiz> _completedQuizStack;
		#endregion

		public Booklet(String title)
		{
			_title = title;
			_quizStack = new Stack<Quiz>();
			_completedQuizStack = new Stack<Quiz>();
			_status = Status.NotStarted;
		}

		public Booklet(String title, Stack<Quiz> quizzes)
		{
			_title = title;
			_quizStack = quizzes;
			_completedQuizStack = new Stack<Quiz>();
			_status = Status.NotStarted;
		}

		private Status CheckStatus()
		{
			HasBookletBeenCompleted();
			return _status;
		}

		private bool HasBookletBeenCompleted()
		{
			if(_quizStack.Count != 0)
			{
				return false;
			}
			_status = Status.Completed;
			return true;
		}

		public int GetQuizzesLeft()
		{
			return _quizStack.Count;
		}

		public int GetTotalQuizCount()
		{
			return _quizStack.Count + _completedQuizStack.Count;
		}

		public String GetTitle()
		{
			return _title;
		}

		public Quiz GetOpenQuiz()
		{
			if (_quizStack.Count == 0)
			{
				_status = Status.Completed;
				return null;
			}
			_status = Status.InProgress;

			return _quizStack.Peek();
		}

		public float GetPercentDone()
		{
			return (1 - (GetQuizzesLeft() / GetTotalQuizCount()));
		}

		public Quiz GetNextQuiz()
		{
			if (_quizStack.Count == 0)
			{
				_status = Status.Completed;
				return null;
			}
			_status = Status.InProgress;

			_completedQuizStack.Push(_quizStack.Peek());
			return _quizStack.Pop();
		}

		public Status GetStatus()
		{
			HasBookletBeenCompleted();
			return _status;
		}

		public void AddItem(Quiz item)
		{
			_quizStack.Push(item);
		}

		public bool Reset()
		{
			foreach (var quiz in _completedQuizStack)
			{
				quiz.Reset();
				_quizStack.Push(quiz);
			}
			//_openItem = _quizStack.Pop();
			_status = Status.NotStarted;
			return true;
		}

		private bool DoneWithQuiz()
		{
			if (GetOpenQuiz().GetStatus() == Status.Completed)
			{
				return true;
			}
			return false;
		}
	}
}
