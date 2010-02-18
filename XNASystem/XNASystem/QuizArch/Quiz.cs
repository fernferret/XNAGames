using System;
using System.Collections.Generic;
using XNASystem.Interfaces;

/*
 
 * This class is currently undergoing an overhaul from List based, to Stack based question management
 
 */
namespace XNASystem.QuizArch
{
    [Serializable]
	public class Quiz : IComponent<Question>
	{
		#region Variables
		private readonly String _title;

		private Question _openItem;

		private Status _status = Status.NotStarted;
		private Stack<Question> _questionStack;
		private Stack<Question> _answeredQuestionStack;
		private Score _score;
		#endregion

		/// <summary>
		/// Create a quiz with no questions, but initialize a new question list
		/// </summary>
		public Quiz(String title)
		{
			_title = title;
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
			_questionStack = questions;
			_answeredQuestionStack = new Stack<Question>();
			_status = Status.NotStarted;
			_score = new Score("Eric", ActivityType.Quiz, 0, _title);
		}

		public Quiz(byte[] bytes, ref int position)
		{
			_title = DataManager.ReadStringFromByteArray(bytes, ref position);

			_questionStack = new Stack<Question>();
			int numberOfQuestions = DataManager.ReadLengthForNextSection(bytes, ref position);
			int index = 0;
			for (index = 0; index < numberOfQuestions; index++)
			{
				Question question = new Question(bytes, ref position);
				_questionStack.Push(question);
			}

			_answeredQuestionStack = new Stack<Question>();
			_status = Status.NotStarted;
			_score = new Score("Eric", ActivityType.Quiz, 0, _title);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
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
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
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

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public double GetPercentDone()
		{
			return 100*(1.0 - ((GetQuestionsLeft()+1.0)/GetTotalQuestionCount()));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public Question GetNextQuestion()
		{
			if (_questionStack.Count == 0)
			{
				_status = Status.Completed;
				return null;
			}
			_status = Status.InProgress;

			// Place the current question on the answered stack
			_answeredQuestionStack.Push(_questionStack.Peek());
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

		/// <summary>
		/// Advances the quiz to the next item (basically question)
		/// </summary>
		public void AddItem(Question item)
		{
			_questionStack.Push(item);
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
			//_openItem = _questionStack.Pop();
			_status = Status.NotStarted;
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<Question> GetAllQuestions()
		{
			return new List<Question>(_questionStack.ToArray());

		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<Question> GetRandomQuestions()
		{
			var r = new Random();

			var oQuestion = new List<Question>(_questionStack.ToArray());
			var rQuestion = new List<Question>();

			int i;
			while (oQuestion.Count > 0)
			{
				i = r.Next(oQuestion.Count);
				rQuestion.Add(oQuestion[i]);
				oQuestion.RemoveAt(i);
			}
			return rQuestion;
		}
		public override string ToString()
		{
			return _title;
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

			//Serialize questions
			bytes.Add((byte)(this._questionStack.Count / byte.MaxValue));
			bytes.Add((byte)(this._questionStack.Count % byte.MaxValue));
			foreach (Question q in this._questionStack)
			{
				bytes.AddRange(q.ToByteArray());
			}

			return bytes.ToArray();
		}
	}
}
