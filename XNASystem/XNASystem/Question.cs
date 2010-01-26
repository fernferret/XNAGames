using System;
using System.Collections.Generic;

namespace XNASystem
{
    public class Question
	{
		#region Variables
		private readonly List<Answer> _answers = new List<Answer>();
        private bool _hasAnswer;
		bool _isCorrect;
		#endregion

		public Question(String q, List<Answer> a)
        {
            Title = q;
            _answers = a;
        }

        public string Title { get; private set; }

        public Answer GetCorrectAnswer()
        {
            foreach (var o in _answers)
            {
                if (o.IsCorrect())
                {
                    return o;
                }
            }
            return null;
        }

        public List<Answer> GetAllAnswers()
        {
            return _answers;
        }
		
		public List<Answer> GetRandomAnswers()
		{
			var r = new Random();
			
			var oAnswer = new List<Answer>(_answers);
			var rAnswer = new List<Answer>();
			int i;
			while(oAnswer.Count > 0)
			{
				i = r.Next(oAnswer.Count);
				rAnswer.Add(oAnswer[i]);
				oAnswer.RemoveAt(i);
			}
			return rAnswer;
		}

        public bool HasAnswer()
        {
            foreach (var answer in _answers)
            {
                if(answer.HasBeenAnswered())
                {
                    return true;
                }
            }
            return false;
        }
		
		public bool AnsweredCorrectly()
		{
			foreach (var answer in _answers)
			{
				if(answer.HasBeenAnswered() && answer.IsCorrect())
				{
					return true;
				}
			}
			return false;
		}
        
		internal void Reset()
        {
            _hasAnswer = false;
        }

		internal bool AnswerQuestion(Answer a)
		{
			a.Choose();
			_hasAnswer = true;

			if(a.IsCorrect())
			{
				_isCorrect = true;
				return true;
			}
			_isCorrect = false;
			return false;
		}

		internal String GetTitle()
		{
			return Title;
		}

		public override string ToString()
		{
			return Title;
		}
	}
}
