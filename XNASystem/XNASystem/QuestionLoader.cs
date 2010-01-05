using System;
using System.Collections.Generic;
using System.Xml;
// This class will be used to load questions via xml in later revs
namespace XNASystem
{
    class QuestionLoader
    {
        private Booklet _booklet;
        private List<Booklet> _bList;
        public QuestionLoader(Booklet booklet)
        {
            _bList = new List<Booklet> {booklet};
        }

        public Quiz GetQuiz(int i)
        {
            return _booklet.GetQuiz(i);
        }
        public List<Booklet> GetBooklets()
        {
            return _bList;
        }
        public Booklet GetBooklet()
        {
            return _booklet;
        }

        internal Menu SelectBooklet()
        {
            //return new Menu("Select Booklet",new List<MenuItem>{new MenuItem()});
        }
    }
}
