using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace XNASystem
{
    class MenuItem
    {
        private string _title;
        public MenuAction Action;
        public MenuItem(string t, MenuAction a)
        {
            Action = a;
            _title = t;
        }

        internal string GetTitle()
        {
            return _title;
        }
    }
}