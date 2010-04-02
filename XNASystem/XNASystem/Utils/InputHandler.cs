﻿using System;
using System.Collections.Generic;
﻿using Microsoft.Xna.Framework.Input;
public enum ButtonPressed
{
	Before, After
}
public enum ButtonAction
{
	MenuAccept, MenuCancel, MenuUp, MenuDown, NONE, ShipShoot, ShipMoveRightSlow, ShipMoveLeftSlow
}
namespace XNASystem.Utils
{
	public class InputHandler
	{
		private int _advances = 0;
		private KeyboardState _keyState;
		private GamePadState _gamePadState;
		private static Dictionary<ButtonAction, List<ButtonAlias>> _superButton;
		private static Dictionary<String, double> _holdTimes;
		private readonly Dictionary<String, double> _repeatHoldTimes;
		private readonly Dictionary<String, ButtonAlias> _buttonLocks;
		public InputHandler()
		{
			_repeatHoldTimes = new Dictionary<String, double>();
			_buttonLocks = new Dictionary<string, ButtonAlias>();
			_holdTimes = new Dictionary<string, double>();
			_superButton = new Dictionary<ButtonAction, List<ButtonAlias>>();
			_superButton.Add(ButtonAction.MenuUp, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.DPadUp, -1,-1,"MenuUp"),
										new ButtonAlias(Buttons.LeftThumbstickUp, -1,-1,"MenuUp"),
										new ButtonAlias(Keys.NumPad8,-1,-1,"MenuUp"),
										new ButtonAlias(Keys.W,2,.5,"MenuUp"),
										new ButtonAlias(Keys.Up,-1,-1,"MenuUp")
			                     	});
			_superButton.Add(ButtonAction.MenuDown, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.DPadDown, -1,-1,"MenuDown"),
										new ButtonAlias(Buttons.LeftThumbstickDown, -1,-1,"MenuDown"),
										new ButtonAlias(Keys.Down,-1,-1,"MenuDown"),
										new ButtonAlias(Keys.NumPad2,-1,-1,"MenuDown"),
										new ButtonAlias(Keys.S,1,.25,"MenuDown")
			                     	});
			_superButton.Add(ButtonAction.MenuAccept, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.A, -1,-1,"MenuAccept"),
										new ButtonAlias(Buttons.Start, -1,-1,"MenuAccept"),
										new ButtonAlias(Keys.Enter, -1,-1,"MenuAccept")
			                     	});
			_superButton.Add(ButtonAction.MenuCancel, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.B, -1,-1,"MenuCancel"),
										new ButtonAlias(Buttons.Back, -1,-1,"MenuCancel"),
										new ButtonAlias(Keys.Delete, .5,.2,"MenuCancel")
			                     	});

			_superButton.Add(ButtonAction.ShipShoot, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.A, -1,-1,"ShipShoot"),
										new ButtonAlias(Buttons.Y, -1,-1,"ShipShoot"),
										new ButtonAlias(Keys.V,0,0,"ShipShoot"),
										new ButtonAlias(Keys.Space, .1,.1,"ShipShoot")
			                     	});
			_superButton.Add(ButtonAction.ShipMoveLeftSlow, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.DPadLeft, -1,0,"ShipMoveLeftSlow"),
										new ButtonAlias(Buttons.LeftThumbstickLeft, -1,0,"ShipMoveLeftSlow"),
										new ButtonAlias(Keys.Left, -1,0,"ShipMoveLeftSlow")
			                     	});
			_superButton.Add(ButtonAction.ShipMoveRightSlow, new List<ButtonAlias>
			                     	{
			                     		new ButtonAlias(Buttons.DPadRight, -1,0,"ShipMoveRightSlow"),
										new ButtonAlias(Buttons.LeftThumbstickRight, -1,0,"ShipMoveRightSlow"),
										new ButtonAlias(Keys.Right, -1,0,"ShipMoveRightSlow")
			                     	});
		}

		public bool IsButtonPressed(ButtonAction b)
		{
			if (!_superButton.ContainsKey(b))
			{
				return false;
			}
				foreach (var button in _superButton[b])
				{
					if (!button.GetButton().Equals(null) && _gamePadState.IsConnected)
					{
						if (_gamePadState.IsButtonDown(button.GetButton()) && !_buttonLocks.ContainsKey(button.GetAssociation()))
						{
							return SetTypeXbox(button);
						}
						if (_gamePadState.IsButtonDown(button.GetButton()) && _buttonLocks.ContainsValue(button))
						{
							return checkButtonDynamics(button);
						}
						if (!checkCommandButtonUp(button)) return false;
					}
					if (button.GetKey().Equals(Keys.None)) continue;

					if (setupNewKeyAssociation(button)) return true;

					if (_keyState.IsKeyDown(button.GetKey()) && _buttonLocks.ContainsValue(button))
					{
						return checkButtonDynamics(button);
					}
					if (!checkCommandKeyUp(button)) return false;
				}
				return false;
		}

		private bool checkButtonDynamics(ButtonAlias button)
		{
			if (button.IsHoldable())
			{
				if (isHoldable(button)) return true;

				if (!isHoldableButNotHeldEnough(button)) return false;

				if (!buttonNotHeldEnough(button)) return false;

				if (!allowOneButtonPress(button)) return false;

				if (buttonHeldEnoughRepeat(button)) return true;

				if (!buttonNotHeldLongEnough(button)) return false;
			}
			return false;
		}

		private bool checkCommandButtonUp(ButtonAlias button)
		{
			if (_gamePadState.IsButtonUp(button.GetButton()) && _buttonLocks.ContainsValue(button))
			{
				RemoveLocksAndHolds(button);
				return false;
			}
			return true;
		}

		private bool SetTypeXbox(ButtonAlias button)
		{
			button.Pressed = PressType.XboxController;
			//_pressedButtons.Add(button.GetAssociation());
			_buttonLocks.Add(button.GetAssociation(), button);
			return true;
		}

		private void RemoveLocksAndHolds(ButtonAlias button)
		{
			if (_holdTimes.ContainsKey(button.GetAssociation()) && button.GetHoldable() > 0 && _holdTimes[button.GetAssociation()].CompareTo(SystemMain.CurrentGameTime.TotalRealTime.TotalSeconds) < 0 && !_repeatHoldTimes.ContainsKey(button.GetAssociation()))
			{
				_holdTimes.Remove(button.GetAssociation());
				_buttonLocks.Remove(button.GetAssociation());
			}
			else if (_repeatHoldTimes.ContainsKey(button.GetAssociation()) && _repeatHoldTimes[button.GetAssociation()].CompareTo(SystemMain.CurrentGameTime.TotalRealTime.TotalSeconds) < 0)
			{
				_repeatHoldTimes.Remove(button.GetAssociation());
				_buttonLocks.Remove(button.GetAssociation());
			}
			else if (!_holdTimes.ContainsKey(button.GetAssociation()))
			{
				_buttonLocks.Remove(button.GetAssociation());
			}
		}

		private bool isHoldable(ButtonAlias button)
		{
			if (button.GetHoldable() == 0)
			{
				return true;
			}
			return false;
		}

		private bool isHoldableButNotHeldEnough(ButtonAlias button)
		{
			if (button.GetHoldable() > 0 && !_holdTimes.ContainsKey(button.GetAssociation()))
			{
				_holdTimes.Add(button.GetAssociation(), SystemMain.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldable());
				return false;
			}
			return true;
		}

		private bool buttonNotHeldEnough(ButtonAlias button)
		{
			if (button.GetHoldable() > 0 && _holdTimes.ContainsKey(button.GetAssociation()) && _holdTimes[button.GetAssociation()].CompareTo(SystemMain.CurrentGameTime.TotalRealTime.TotalSeconds) > 0 && !_repeatHoldTimes.ContainsKey(button.GetAssociation()))
			{
				return false;
			}
			return true;
		}

		private bool allowOneButtonPress(ButtonAlias button)
		{
			if (_holdTimes.ContainsKey(button.GetAssociation()) && _holdTimes[button.GetAssociation()].CompareTo(SystemMain.CurrentGameTime.TotalRealTime.TotalSeconds) < 0 && !_repeatHoldTimes.ContainsKey(button.GetAssociation()) && button.GetHoldableRepeat() >= 0)
			{
				// Remove, re-add, rinse, repeat!
				_repeatHoldTimes.Add(button.GetAssociation(), SystemMain.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldableRepeat());
				return false;
			}
			return true;
		}

		private bool buttonHeldEnoughRepeat(ButtonAlias button)
		{
			if (_repeatHoldTimes.ContainsKey(button.GetAssociation()) && _repeatHoldTimes[button.GetAssociation()].CompareTo(SystemMain.CurrentGameTime.TotalRealTime.TotalSeconds) < 0)
			{
				_repeatHoldTimes.Remove(button.GetAssociation());
				_advances++;
				return true;
			}
			return false;
		}

		private bool buttonNotHeldLongEnough(ButtonAlias button)
		{
			if (!_repeatHoldTimes.ContainsKey(button.GetAssociation()) && button.GetHoldableRepeat() >= 0)
			{
				_repeatHoldTimes.Add(button.GetAssociation(), SystemMain.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldableRepeat());
				return false;
			}
			return true;
		}

		private bool setupNewKeyAssociation(ButtonAlias button)
		{
			if (_keyState.IsKeyDown(button.GetKey()) && !_buttonLocks.ContainsKey(button.GetAssociation()))
			{
				button.Pressed = PressType.Key;
				_buttonLocks.Add(button.GetAssociation(), button);
				return true;
			}
			return false;
		}

		private bool checkCommandKeyUp(ButtonAlias button)
		{
			if (_keyState.IsKeyUp(button.GetKey()) && _buttonLocks.ContainsValue(button))
			{
				if (_holdTimes.ContainsKey(button.GetAssociation()) && button.GetHoldable() > 0 && _holdTimes[button.GetAssociation()].CompareTo(SystemMain.CurrentGameTime.TotalRealTime.TotalSeconds) < 0 && !_repeatHoldTimes.ContainsKey(button.GetAssociation()))
				{
					_holdTimes.Remove(button.GetAssociation());
					_buttonLocks.Remove(button.GetAssociation());
				}
				else if (_repeatHoldTimes.ContainsKey(button.GetAssociation()) && _repeatHoldTimes[button.GetAssociation()].CompareTo(SystemMain.CurrentGameTime.TotalRealTime.TotalSeconds) < 0)
				{
					_repeatHoldTimes.Remove(button.GetAssociation());
				}
				else if (!_holdTimes.ContainsKey(button.GetAssociation()))
				{
					_buttonLocks.Remove(button.GetAssociation());
				}
				return false;
			}
			return true;
		}

		internal void SetInputs(KeyboardState keyState, GamePadState padState)
		{
			_keyState = keyState;
			_gamePadState = padState;
		}
	}
}