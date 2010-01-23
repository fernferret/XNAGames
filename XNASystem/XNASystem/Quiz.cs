using System;
using System.Collections.Generic;
using XNASystem.Interfaces;

namespace XNASystem
{
	public class Quiz: IComponent<Question>
    {
        // The listing of the currently loaded questions
        private List<Question> _questionList;
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
            // If we're in progress, return the same.
            //if (_openItem == _questionList[0] && S)
            //{
            //    return _questionList[_questionList.IndexOf(_openItem)];
            //}
            // Else return the next one
            return _questionList[_questionList.IndexOf(_openItem) + 1];
        }

        private bool CheckStatus()
        {
            if (_questionList.IndexOf(_openItem) < GetItemCount()-1)
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

        public Question GetOpenItem(bool advance)
        {
            if (_status == Status.NotStarted)
            {
                _status = Status.InProgress;
                _openItem = _questionList[0];
            }
            else if(advance)
            {
                // Set the _openItem to the next item in the quiz
                _openItem = CheckStatus() ? AdvanceItem() : null;
            }
            
            return _openItem;
        }

        public Status GetStatus()
        {
            CheckStatus();
            return _status;
        }

        public void AddItem(Question item)
        {
            _questionList.Add(item);
        }

        public bool Reset()
        {
            //if (_status == Status.InProgress)
            //{
                _openItem = _questionList[0];
            foreach (var question in _questionList)
            {
                question.Reset();
            }
            _status = Status.NotStarted;
                return true;
            //}
            //return false;
        }

       /* internal Menu MenuOf(bool advance)
        {
            return GetOpenItem(advance).GetAsMenu();
        }*/
    }
}
