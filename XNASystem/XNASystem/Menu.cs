using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNASystem
{
    class Menu
    {
        private readonly String _title;
        private readonly List<IMenuItem> _items;
        private IMenuItem _selected;

        public Menu(string title, List<IMenuItem> items)
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

        internal IEnumerable<IMenuItem> GetItems()
        {
            return _items;
        }

        internal IMenuItem GetSelectedItem()
        {
            return _selected;
        }

        internal void SetSelectedItem(int item)
        {
            _selected = _items[item];
        }
    }
}