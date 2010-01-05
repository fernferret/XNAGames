using System;
using System.Collections.Generic;

namespace XNASystem
{
    public class Question
    {
        private readonly List<Answer> _answers = new List<Answer>();
        public Question(String q, List<String> a, int correct)
        {
            Question1 = q;
            var i = 0;
            foreach (var o in a)
            {
                if (i == correct)
                {
                    _answers.Add(new Answer(o, true));
                }
                else
                {
                    _answers.Add(new Answer(o, false));
                }
                i++;
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