using System;
using Microsoft.Xna.Framework.Input;

namespace XNASystem.Utils
{
	class ButtonAlias
	{
		private Buttons _button;
		private Keys _key;
		private String _name;
		public bool Pressed;
		public ButtonAlias(String n, Buttons b, Keys k)
		{
			_name = n;
			_button = b;
			_key = k;
			Pressed = false;
		}
		public String GetName()
		{
			return _name;
		}
		public Buttons GetButton()
		{
			return _button;
		}
		public Keys GetKey()
		{
			return _key;
		}

	}
}
