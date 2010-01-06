
namespace XNASystem
{
    class NavItem: IMenuItem
    {
        private readonly string _title;
        private readonly MenuAction _action;
        public NavItem(string s, MenuAction a)
        {
            _title = s;
            _action = a;
        }
        public string GetTitle()
        {
            return _title;
        }

        public MenuAction GetAction()
        {
            return _action;
        }
    }
}
