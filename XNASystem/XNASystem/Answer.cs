using System;

namespace XNASystem
{
    public class Answer
    {
        private readonly Boolean _correct;
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
    }
}