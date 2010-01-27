using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
public enum ButtonPressed
{
	Before, After
}
namespace XNASystem.Utils
{
	public class InputHandler
	{
		private KeyboardState _keyState;
		private GamePadState _gamePadState;
		private List<ButtonAlias> _buttonAliases;
		private List<ButtonAlias> _pressedButtons;
		private ButtonAlias _up;
		private ButtonAlias _down;
		private ButtonAlias _enter;
		private ButtonAlias _back;
		private ButtonAlias _space;
		public InputHandler()
		{
			//_keyState = k;
			//_gamePadState = g;
			_up = new ButtonAlias("UP", Buttons.DPadUp, Keys.Up);
			_down = new ButtonAlias("DOWN", Buttons.DPadDown, Keys.Down);
			_enter = new ButtonAlias("ENTER", Buttons.A, Keys.Enter);
			_back = new ButtonAlias("BACK", Buttons.Back, Keys.Delete);
			_space = new ButtonAlias("SPACE", Buttons.Y, Keys.Space);
			_buttonAliases = new List<ButtonAlias>
			                 	{
			                 		_up,
			                 		_down,
			                 		_enter,
			                 		_back,
			                 		_space
			                 	};
			_pressedButtons = new List<ButtonAlias>();
		}
		public void AddAlias(String n, Keys k, Buttons b)
		{
			_buttonAliases.Add(new ButtonAlias(n, b, k));
		}
		public int HandleMenuMovement(int items, int c)
		{
			var choice = c;
			var alias = CheckKeys(new List<ButtonAlias> { _up, _down });
			if (alias == _up)
			{
				choice--;
			}

			if (alias == _down)
			{
				choice++;
			}

			if (choice == -1)
			{
				choice = items - 1;
			}
			if (choice == items)
			{
				choice = 0;
			}
			return choice;
		}

		public int HandleMenuMovement(int items, int c, int force)
		{
			var choice = c;

			if (choice != force)
			{
				choice = force;
			}
			return choice;
		}

		public bool IfEnterPressed()
		{
			if(_enter == CheckKeys(new List<ButtonAlias> {_enter}))
			{
				return true;
			}
			return false;
		}

		public bool IfSpacePressed()
		{
			if (_space == CheckKeys(new List<ButtonAlias> { _space }))
			{
				return true;
			}
			return false;
		}

		private ButtonAlias CheckKeys(IEnumerable<ButtonAlias> validActions)
		{
			foreach (var action in validActions)
			{
				if (_keyState.IsKeyDown(action.GetKey()) && !_pressedButtons.Contains(action))
				{
					action.Pressed = PressType.Key;
					_pressedButtons.Add(action);
					return action;
				}
				if (_gamePadState.IsButtonDown(action.GetButton()) && !_pressedButtons.Contains(action))
				{
					action.Pressed = PressType.XboxController;
					_pressedButtons.Add(action);
					return action;
				}
				if ((_keyState.IsKeyUp(action.GetKey())) && _pressedButtons.Contains(action))
				{
					if (action.Pressed == PressType.Key)
					{
						_pressedButtons.Remove(action);
					}
				}
				if ((_gamePadState.IsButtonUp(action.GetButton())) && _pressedButtons.Contains(action))
				{
					if (action.Pressed == PressType.XboxController)
					{
						_pressedButtons.Remove(action);
					}
				}
			}
			return null;
		}
		

		internal void SetInputs(KeyboardState keyState, GamePadState padState)
		{
			_keyState = keyState;
			_gamePadState = padState;
		}

		internal GamePadState GetPadState()
		{
			return _gamePadState;
		}

		internal KeyboardState GetKeyState()
		{
			return _keyState;
		}
	}
}
