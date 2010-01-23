using System;
using System.Collections.Generic;
using XNASystem.Interfaces;

namespace XNASystem
{
	public class Booklet: IComponent<Quiz>
    {
        //
        private readonly List<Quiz> _quizList;
        private readonly String _title;
        private Quiz _openItem;
        private Status _status = Status.NotStarted;
        private bool _didAdvance;
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
            // If we're in progress, return the same.
            if(_status == Status.InProgress)
            {
                return _quizList[_quizList.IndexOf(_openItem)];
            }
            // Else return the next one
            _didAdvance = true;
            return _quizList[_quizList.IndexOf(_openItem)+1];
        }

        private bool CheckStatus()
        {
            if (_quizList.IndexOf(_openItem) < GetItemCount()-1)
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

        public Quiz GetOpenItem(bool advance)
        {
            if (_status == Status.NotStarted)
            {
                _status = Status.InProgress;
                _openItem = _quizList[0];
            }
            else if(advance)
            {
                // Set the _openItem to the next item in the booklet
                _openItem = CheckStatus() ? AdvanceItem() : null;
            }
            return _openItem;
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
            // For now, only allow reset if we're completed
            //if (_status == Status.Completed)
            //{
                _openItem = _quizList[0];
            foreach (var quiz in _quizList)
            {
                quiz.Reset();
            }
            _status = Status.NotStarted;
                return true;
            //}
            //return false;
        }

       /* internal Menu AdvanceQuestion()
        {
            return GetOpenItem(false).MenuOf(true);
        }
        internal bool AdvanceQuiz()
        {
            _status = Status.Completed;
            GetOpenItem(true);
            return _didAdvance;
        }*/

        internal bool DoneWithQuiz()
        {
            if(GetOpenItem(false).GetStatus() == Status.Completed)
            {
                return true;
            }
            return false;
        }

        internal void ResetQuizAdvance()
        {
            _didAdvance = false;
        }

		public List<Quiz> GetQuizList()
		{
			return _quizList;
		}

		public void AddQuestionToQuiz(int index, Question question)
		{
			_quizList[index].AddItem(question);
		}
    }
}
