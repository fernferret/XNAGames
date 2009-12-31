using System;
using System.Collections.Generic;

namespace XNASystem
{
    public class Question
    {
        private readonly List<Answer> _answers = new List<Answer>();
        public Question(String q, List<String> a)
        {
            Question1 = q;
            foreach (var o in a)
            {
                if (o.Contains("*Answer*"))
                {
                    _answers.Add(new Answer(o.Remove(0, 8), true));
                }
                else
                {
                    _answers.Add(new Answer(o, false));
                }
            }
        }

        public string Question1 { get; private set; }

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
    }
}