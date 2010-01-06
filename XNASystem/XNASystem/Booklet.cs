using System;
using System.Collections.Generic;

namespace XNASystem
{
    class Booklet: IComponent<Quiz>
    {
        //
        private readonly List<Quiz> _quizList;
        private readonly String _title;
        private Quiz _openItem;
        private Status _status = Status.NotStarted;

        public Booklet(String title)
        {
            _title = title;
            _quizList = new List<Quiz>();
        }

        public Booklet(String title, List<Quiz> quizzes)
        {
            _title = title;
            _quizList = quizzes;
        }

        private Quiz AdvanceItem()
        {
            return _quizList[_quizList.IndexOf(_openItem) + 1];
        }

        private bool CheckStatus()
        {
            if (_quizList.IndexOf(_openItem) < GetItemCount())
            {
                return true;
            }
            _status = Status.Completed;
            return false;
        }

        public int GetItemCount()
        {
            return _quizList.Count;
        }

        public String GetTitle()
        {
            return _title;
        }

        public Quiz GetOpenItem()
        {
            if (_status == Status.NotStarted)
            {
                return _openItem = _quizList[0];
            }
            var currentItem = _openItem;
            // Set the _openItem to the next item in the booklet
            _openItem = CheckStatus() ? AdvanceItem() : null;
            return currentItem;
        }

        public Status GetStatus()
        {
            return _status;
        }

        public void AddItem(Quiz item)
        {
            _quizList.Add(item);
        }

        public bool Reset()
        {
            if (_status == Status.InProgress)
            {
                _openItem = _quizList[0];
                return true;
            }
            return false;
        }

        internal Menu AdvanceQuestion()
        {
            return GetOpenItem().MenuOf();
        }
    }
}
