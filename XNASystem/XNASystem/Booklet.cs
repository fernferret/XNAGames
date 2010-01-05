using System.Collections.Generic;

namespace XNASystem
{
    class Booklet
    {
        private List<Quiz> _qList;
        private string _title;
        public Booklet(string s, List<Quiz> quizzes)
        {
            _title = s;
            _qList = quizzes;
        }

        internal Quiz GetQuiz(int i)
        {
            return _qList[i];
        }
    }
}