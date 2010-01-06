using System;
using System.Collections.Generic;

namespace XNASystem
{
    class Quiz: IComponent<Question>
    {
        // The listing of the currently loaded questions
        private readonly List<Question> _questionList;
        private readonly String _title;
        private Question _openItem;
        private Status _status = Status.NotStarted;

        public Quiz(String title)
        {
            _title = title;
            _questionList = new List<Question>();
        }

        public Quiz(String title, List<Question> questions)
        {
            _title = title;
            _questionList = questions;
        }

        private Question AdvanceItem()
        {
            return _questionList[_questionList.IndexOf(_openItem) + 1];
        }

        private bool CheckStatus()
        {
            if (_questionList.IndexOf(_openItem) < GetItemCount())
            {
                return true;
            }
            _status = Status.Completed;
            return false;
        }

        public int GetItemCount()
        {
            return _questionList.Count;
        }

        public string GetTitle()
        {
            return _title;
        }

        public Question GetOpenItem()
        {
            if (_status == Status.NotStarted)
            {
                return _openItem = _questionList[0];
            }
            var currentItem = _openItem;
            // Set the _openItem to the next item in the quiz
            _openItem = CheckStatus() ? AdvanceItem() : null;
            return currentItem;
        }

        public Status GetStatus()
        {
            return _status;
        }

        public void AddItem(Question item)
        {
            _questionList.Add(item);
        }

        public bool Reset()
        {
            if (_status == Status.InProgress)
            {
                _openItem = _questionList[0];
                return true;
            }
            return false;
        }

        internal Menu MenuOf()
        {
            return GetOpenItem().GetAsMenu();
        }
    }
}
