using System;

namespace XNASystem
{
    [Serializable]
    public class Answer
    {
        private readonly Boolean _correct;
        private bool _questionAnswered;
        public Answer(String a, Boolean c)
        {
            TheAnswer = a;
            _correct = c;
        }

        public string TheAnswer { get; private set; }

        public Boolean IsCorrect()
        {
            return _correct;
        }

        public new String ToString()
        {
            return TheAnswer;
        }

        public void Choose()
        {
            _questionAnswered = true;
        }

        public bool HasBeenAnswered()
        {
            return _questionAnswered;
        }
    }
}