using System;
using System.Collections.Generic;

namespace XNASystem
{
    [Serializable]
    public class Question
    {
        private readonly List<Answer> _answers = new List<Answer>();
        private bool _hasAnswer;
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

        internal Menu GetAsMenu()
        {
            var questionItems = new List<IMenuItem>
                                    {
                                        new QuestionItem(_answers[0]),
                                        new QuestionItem(_answers[1]),
                                        new QuestionItem(_answers[2]),
                                        new QuestionItem(_answers[3])
                                    };
            return new Menu(Title,questionItems);
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

        internal void Reset()
        {
            _hasAnswer = false;
        }
    }
}
