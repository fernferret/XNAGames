using System;

namespace XNASystem
{
    class QuestionItem : IMenuItem
    {
        private readonly Answer _answer;
        private readonly String _title;
        public QuestionItem(Answer a)
        {
            _answer = a;
            _title = _answer.ToString();
        }
        public string GetTitle()
        {
            return _title;
        }

        internal void AnswerQuestion()
        {
            _answer.Choose();
        }
    }
}
