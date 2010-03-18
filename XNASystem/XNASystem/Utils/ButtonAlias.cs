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
		private readonly Buttons _button;// = Buttons.BigButton;
		private readonly Keys _key;// = Keys.None;
		private readonly String _association;
		private readonly double _holdable;
		private readonly double _holdableRepeat;
		public PressType Pressed;
		public ButtonAlias(Buttons b, double holdable, double holdableRepeat, String a)
		{
			_association = a;
			_button = b;
			_holdable = holdable;

			// If they specified a repeated holdable, make sure we specify the initial to that
			// repeat by default
			if (_holdable == -1 && holdableRepeat >= 0)
			{
				_holdable = holdableRepeat;
			}
			_holdableRepeat = holdableRepeat;
			Pressed = PressType.None;
		}
		public ButtonAlias(Keys k, double holdable, double holdableRepeat, String a)
		{
			_association = a;
			_key = k;
			_holdable = holdable;

			// If they specified a repeated holdable, make sure we specify the initial to that
			// repeat by default
			if (_holdable == -1 && holdableRepeat >= 0)
			{
				_holdable = holdableRepeat;
			}
			_holdableRepeat = holdableRepeat;
			Pressed = PressType.None;
		}
		public String GetAssociation()
		{
			return _association;
		}
		public Buttons GetButton()
		{
			return _button;
		}
		public Keys GetKey()
		{
			return _key;
		}

		public bool IsHoldable()
		{
			if (_holdable > -1)
			{
				return true;
			}
			return false;
		}
		public double GetHoldable()
		{
			return _holdable;
		}
		public double GetHoldableRepeat()
		{
			return _holdableRepeat;
		}
	}
}