using System;
using Microsoft.Xna.Framework.Input;

public enum PressType
{
	None, XboxController, Key
}
namespace XNASystem.Utils
{
	public class ButtonAlias
	{
		private Buttons _button;
		private Keys _key;
		private String _name;
		private ButtonHoldable _holdable;
		public PressType Pressed;
		public ButtonAlias(String n, Buttons b, Keys k, ButtonHoldable holdable)
		{
			_name = n;
			_button = b;
			_key = k;
			_holdable = holdable;
			Pressed = PressType.None;
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

		public bool GetHoldable()
		{
			return _holdable == ButtonHoldable.Yes;
		}
	}
}
