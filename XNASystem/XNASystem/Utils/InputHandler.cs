using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
public enum UserAction
{
	Move,
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
		public InputHandler()
		{
			//_keyState = k;
			//_gamePadState = g;
			_up = new ButtonAlias("UP", Buttons.DPadUp, Keys.Up);
			_down = new ButtonAlias("DOWN", Buttons.DPadDown, Keys.Down);
			_enter = new ButtonAlias("ENTER", Buttons.A, Keys.Enter);
			_buttonAliases = new List<ButtonAlias>
			                 	{
			                 		_up, _down, _enter
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
			var alias = CheckKeys(new List<ButtonAlias> {_up, _down}, choice);
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
		private ButtonAlias CheckKeys(List<ButtonAlias> validActions, int value)
		{
			foreach (var action in validActions)
			{
				if((_keyState.IsKeyDown(action.GetKey()) || _gamePadState.IsButtonDown(action.GetButton())) && !_pressedButtons.Contains(action) )
				{
					_pressedButtons.Add(action);
					return action;
				}
				if ((_keyState.IsKeyUp(action.GetKey()) || _gamePadState.IsButtonUp(action.GetButton())) && _pressedButtons.Contains(action) )
				{
					_pressedButtons.Remove(action);
				}
			}
			return null;
		}


		internal void SetInputs(KeyboardState keyState, GamePadState padState)
		{
			_keyState = keyState;
			_gamePadState = padState;
		}
	}
}
