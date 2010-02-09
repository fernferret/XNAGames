using System;
using System.Collections.Generic;
using XNASystem.Interfaces;

namespace XNASystem.QuizArch
{
    [Serializable]
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

        public Booklet(byte[] bytes)
        {
            int position = 0;
            _title = DataManager.ReadStringFromByteArray(bytes, ref position);

            //Initialize quizzes
            _quizStack = new Stack<Quiz>();
            int numberOfQuizzes = DataManager.ReadLengthForNextSection(bytes, ref position);
            int index = 0;
            for (index = 0; index < numberOfQuizzes; index++)
            {
                Quiz quiz = new Quiz(bytes, ref position);
                _quizStack.Push(quiz);
            }

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

		/*public List<Quiz> GetQuizList()
		{
			Reset();
			var quizzes = new List<Quiz>();
			while(_quizStack.Count > 0)
			{
				quizzes.Add(GetNextQuiz());
				
			}
			Reset();
			return quizzes;
		}*/
		public List<Quiz> GetAsList()
		{
			List<Quiz> tempQuizzes;
			if (_completedQuizStack.Count == 0)
			{
				tempQuizzes = new List<Quiz>(_quizStack);
				tempQuizzes.Reverse();
			}
			else
			{
				tempQuizzes = new List<Quiz>(_quizStack);
				tempQuizzes.AddRange(_completedQuizStack);
				if (_completedQuizStack.Count > 0)
				{
					tempQuizzes.Reverse(0,_quizStack.Count);
				}
			}
			return tempQuizzes;
		}
		public Quiz GetSpecificQuiz(int currentQuiz)
		{
			return GetAsList()[currentQuiz];
		}

        public byte[] ToByteArray()
        {
            List<byte> bytes = new List<byte>();
            
            //Serializing title
            bytes.Add((byte)(this._title.Length / byte.MaxValue));
            bytes.Add((byte)(this._title.Length % byte.MaxValue));
            foreach (char c in this._title)
            {
                bytes.Add((byte)c);
            }

            //Serialize quizzes
            bytes.Add((byte)(this._quizStack.Count / byte.MaxValue));
            bytes.Add((byte)(this._quizStack.Count % byte.MaxValue));
            foreach (Quiz q in this._quizStack)
            {
                bytes.AddRange(q.ToByteArray());
            }

            return bytes.ToArray();
        }
	}
}