namespace XNASystem
{
    class NavItem : IMenuItem
    {
        private readonly string _title;
        private readonly MenuAction _action;

        public NavItem(string s, MenuAction a)
        {
            _title = s;
            _action = a;
        }

        // Returns the Title of the Navigation Item
        public string GetTitle()
        {
            return _title;
        }

        // Returns teh Action of the Navigation Item
        public MenuAction GetAction()
        {
            return _action;
        }
    }
}