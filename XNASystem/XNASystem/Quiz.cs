using System;
using System.Collections.Generic;

namespace XNASystem
{
    class Quiz
    {
        // The listing of the currently loaded questions
        List<Question> _qList;
        private String _title;
        private int _currentQuestion;
        public Quiz(String title)
        {
            _title = title;
            _currentQuestion = 0;
            _qList = new List<Question>();
        }
        public Quiz(String title, List<Question> qlist)
        {
            _title = title;
            _qList = qlist;
        }

        public Question ShowQuestion()
        {
            var q = _qList[_currentQuestion];
            _currentQuestion++;
            return q;
        }
        public void RestartQuiz()
        {
            _currentQuestion = 0;
        }
    }
}