using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNASystem
{
    class Menu
    {
        private readonly String _title;
        private readonly List<MenuItem> _items;
        private MenuItem _selected;

        public Menu(string title, List<MenuItem> items)
        {
            _title = title;
            _items = items;
            _selected = _items.First();
        }

        internal int GetNum()
        {
            return _items.Count;
        }

        internal string GetTitle()
        {
            return _title;
        }

        internal IEnumerable<MenuItem> GetItems()
        {
            return _items;
        }

        internal MenuItem GetSelectedItem()
        {
            return _selected;
        }

        internal void SetSelectedItem(int item)
        {
            _selected = _items[item];
        }
    }
}