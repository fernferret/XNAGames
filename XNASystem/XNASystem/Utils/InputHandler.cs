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
										new ButtonAlias(Keys.Enter, -1,.2,"MenuAccept")
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
			// If our epic superbutton Dictionary contains the action provided
			// ProTip: If you're doing it right, it will ;) aka It always will if you've defined everything correctly
			if (_superButton.ContainsKey(b))
			{
				// Check each ButtonAlias within our specific _superButton
				foreach (var button in _superButton[b])
				{
					// Handle GamePad buttons first, if and only if the gampead is plugged in!
					if (!button.GetButton().Equals(null) && _gamePadState.IsConnected)
					{
						// This _superButton Command Has a Key Associated with it, 
						// Is it DOWN?
						// Does this command NOT already have an associated press?
						//if (_keyState.IsKeyDown(button.GetKey()) && !_pressedButtons.Contains(button.GetAssociation()))
						// OR
						if (_gamePadState.IsButtonDown(button.GetButton()) && !_buttonLocks.ContainsKey(button.GetAssociation()))
						{
							//NO! Sweet, let's set the association.  This is the key that will be counted as pressed
							// UNTIL IT DIES (is unpressed)
							button.Pressed = PressType.XboxController;
							//_pressedButtons.Add(button.GetAssociation());
							_buttonLocks.Add(button.GetAssociation(), button);
							return true;
							// Return True, since the button is being held
							//return false;

						}
						//if (_keyState.IsKeyDown(button.GetKey()) && _pressedButtons.Contains(button.GetAssociation()))
						if (_gamePadState.IsButtonDown(button.GetButton()) && _buttonLocks.ContainsValue(button))
						{
							//We already have an association, are we holdable and still held?
							if (button.IsHoldable())
							{
								if (button.GetHoldable() == 0)
								{
									return true;
								}
								// If we have a holdable button whos delay is greater than 0 seconds, and we have NO association, create one and return
								// We have NOTHING
								if (button.GetHoldable() > 0 && !_holdTimes.ContainsKey(button.GetAssociation()))
								{
									_holdTimes.Add(button.GetAssociation(), DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldable());
									return false;
								}
								// If we have a holdable button whos delay is greater than 0 seconds, and we have AN association, and it's not done yet, we don't care, it's FALSE!
								// We DO Have:
								// A. HoldTime Assoc (Button HAS NOT been held long enough)
								// We DONT Have:
								// A. RepeatHoldTime Assoc
								if (button.GetHoldable() > 0 && _holdTimes.ContainsKey(button.GetAssociation()) && _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) > 0 && !_repeatHoldTimes.ContainsKey(button.GetAssociation()))
								{
									//_repeatHoldTimes.Add(button.GetAssociation(), DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldable());
									return false;
								}

								// HOldable has been held for a valid amount of time, allow one press in! 
								// We DO Have:
								// A. HoldTime Assoc (Button HAS been held long enough)
								// We DONT Have:
								// A. RepeatHoldTime Assoc
								if (_holdTimes.ContainsKey(button.GetAssociation()) && _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) < 0 && !_repeatHoldTimes.ContainsKey(button.GetAssociation()) && button.GetHoldableRepeat() >= 0)
								{
									// Remove, re-add, rinse, repeat!
									//_holdTimes.Remove(button.GetAssociation());
									_repeatHoldTimes.Add(button.GetAssociation(), DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldableRepeat());
									return false;
								}

								// We DO Have:
								// A. HoldTime Assoc (Button HAS been held long enough)
								// B. RepeatHoldTime Assoc (Button HAS been held long enough)
								// Should be all systems go here to return true
								if (_repeatHoldTimes.ContainsKey(button.GetAssociation()) && _repeatHoldTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) < 0)
								{
									_repeatHoldTimes.Remove(button.GetAssociation());
									_advances++;
									return true;
								}
								// We DO Have:
								// A. HoldTime Assoc (Button HAS been held long enough)
								// B. RepeatHoldTime Assoc (Button HAS NOT been held long enough)
								if (!_repeatHoldTimes.ContainsKey(button.GetAssociation()) && button.GetHoldableRepeat() >= 0)
								{
									_repeatHoldTimes.Add(button.GetAssociation(), DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldableRepeat());
									return false;
								}




							}
							// Otherwise, tell the system to only do it once!

							// REMEMBER BUTTON IS STILL HELD DOWN HERE
							return false;

						}
						// This _superButton Command Has a Key Associated with it
						// Is it UP?
						// Does this command already have an associated press?
						//if (_keyState.IsKeyUp(button.GetKey()) && _pressedButtons.Contains(button.GetAssociation()))
						if (_gamePadState.IsButtonUp(button.GetButton()) && _buttonLocks.ContainsValue(button))
						{
							//_pressedButtons.Remove(button.GetAssociation());
							//_buttonLocks.Remove(button.GetAssociation());

							// We NEED the check for the time, so users can't glitch and press the up button faster than we want the repeat to be pressed!

							// Now we check for:
							// A. We have an association on Hold Time
							// B. We DO NOT have an association on Hold Time REPEAT
							// C. The button IS HOLDABLE
							// D. The hold time is finished
							if (_holdTimes.ContainsKey(button.GetAssociation()) && button.GetHoldable() > 0 && _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) < 0 && !_repeatHoldTimes.ContainsKey(button.GetAssociation()))
							{
								//DrawHelper.Debug = "FINSHES:" + DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds;
								//DrawHelper.Debug = _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) + ",(" + _holdTimes[button.GetAssociation()] + ", " + button.GetHoldable() + ")";
								_holdTimes.Remove(button.GetAssociation());
								_buttonLocks.Remove(button.GetAssociation());
							}
							else if (_repeatHoldTimes.ContainsKey(button.GetAssociation()) && _repeatHoldTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) < 0)
							{
								//DrawHelper.Debug = "FINSHES2:" + _advances + DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds;
								//DrawHelper.Debug = _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) + ",(" + _holdTimes[button.GetAssociation()] + ", " + button.GetHoldable() + ")";
								_repeatHoldTimes.Remove(button.GetAssociation());
								//_holdTimes.Remove(button.GetAssociation());
								_buttonLocks.Remove(button.GetAssociation());
							}
							else if (!_holdTimes.ContainsKey(button.GetAssociation()))
							{
								_buttonLocks.Remove(button.GetAssociation());
							}
							// We already have an association
							// Which has just been removed (key finally pulled up, remove association)
							// Were we holdable?
							// Well I guess it doesnt matter... The association... HAS BEEN REVOKED!
							return false;
						}
					}
					// If the key is SOMETHING aka, not a button
					if (!button.GetKey().Equals(Keys.None))
					{
						// This _superButton Command Has a Key Associated with it, 
						// Is it DOWN?
						// Does this command NOT already have an associated press?
						//if (_keyState.IsKeyDown(button.GetKey()) && !_pressedButtons.Contains(button.GetAssociation()))
						// OR
						if (_keyState.IsKeyDown(button.GetKey()) && !_buttonLocks.ContainsKey(button.GetAssociation()))
						{
							//NO! Sweet, let's set the association.  This is the key that will be counted as pressed
							// UNTIL IT DIES (is unpressed)
							button.Pressed = PressType.Key;
							//_pressedButtons.Add(button.GetAssociation());
							_buttonLocks.Add(button.GetAssociation(), button);
							return true;
							// Return True, since the button is being held
							//return false;

						}
						//if (_keyState.IsKeyDown(button.GetKey()) && _pressedButtons.Contains(button.GetAssociation()))
						if (_keyState.IsKeyDown(button.GetKey()) && _buttonLocks.ContainsValue(button))
						{
							//We already have an association, are we holdable and still held?
							if (button.IsHoldable())
							{
								if (button.GetHoldable() == 0)
								{
									return true;
								}
								// If we have a holdable button whos delay is greater than 0 seconds, and we have NO association, create one and return
								// We have NOTHING
								if (button.GetHoldable() > 0 && !_holdTimes.ContainsKey(button.GetAssociation()))
								{
									_holdTimes.Add(button.GetAssociation(), DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldable());
									return false;
								}
								// If we have a holdable button whos delay is greater than 0 seconds, and we have AN association, and it's not done yet, we don't care, it's FALSE!
								// We DO Have:
								// A. HoldTime Assoc (Button HAS NOT been held long enough)
								// We DONT Have:
								// A. RepeatHoldTime Assoc
								if (button.GetHoldable() > 0 && _holdTimes.ContainsKey(button.GetAssociation()) && _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) > 0 && !_repeatHoldTimes.ContainsKey(button.GetAssociation()))
								{
									//_repeatHoldTimes.Add(button.GetAssociation(), DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldable());
									return false;
								}

								// HOldable has been held for a valid amount of time, allow one press in! 
								// We DO Have:
								// A. HoldTime Assoc (Button HAS been held long enough)
								// We DONT Have:
								// A. RepeatHoldTime Assoc
								if (_holdTimes.ContainsKey(button.GetAssociation()) && _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) < 0 && !_repeatHoldTimes.ContainsKey(button.GetAssociation()) && button.GetHoldableRepeat() >= 0)
								{
									// Remove, re-add, rinse, repeat!
									//_holdTimes.Remove(button.GetAssociation());
									_repeatHoldTimes.Add(button.GetAssociation(), DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldableRepeat());
									return false;
								}

								// We DO Have:
								// A. HoldTime Assoc (Button HAS been held long enough)
								// B. RepeatHoldTime Assoc (Button HAS been held long enough)
								// Should be all systems go here to return true
								if (_repeatHoldTimes.ContainsKey(button.GetAssociation()) && _repeatHoldTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) < 0)
								{
									_repeatHoldTimes.Remove(button.GetAssociation());
									_advances++;
									return true;
								}
								// We DO Have:
								// A. HoldTime Assoc (Button HAS been held long enough)
								// B. RepeatHoldTime Assoc (Button HAS NOT been held long enough)
								if (!_repeatHoldTimes.ContainsKey(button.GetAssociation()) && button.GetHoldableRepeat() >= 0)
								{
									_repeatHoldTimes.Add(button.GetAssociation(), DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds + button.GetHoldableRepeat());
									return false;
								}




							}
							// Otherwise, tell the system to only do it once!

							// REMEMBER BUTTON IS STILL HELD DOWN HERE
							return false;

						}
						// This _superButton Command Has a Key Associated with it
						// Is it UP?
						// Does this command already have an associated press?
						//if (_keyState.IsKeyUp(button.GetKey()) && _pressedButtons.Contains(button.GetAssociation()))
						if (_keyState.IsKeyUp(button.GetKey()) && _buttonLocks.ContainsValue(button))
						{
							//_pressedButtons.Remove(button.GetAssociation());
							//_buttonLocks.Remove(button.GetAssociation());

							// We NEED the check for the time, so users can't glitch and press the up button faster than we want the repeat to be pressed!

							// Now we check for:
							// A. We have an association on Hold Time
							// B. We DO NOT have an association on Hold Time REPEAT
							// C. The button IS HOLDABLE
							// D. The hold time is finished
							if (_holdTimes.ContainsKey(button.GetAssociation()) && button.GetHoldable() > 0 && _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) < 0 && !_repeatHoldTimes.ContainsKey(button.GetAssociation()))
							{
								//DrawHelper.Debug = "FINSHES:" + DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds;
								//DrawHelper.Debug = _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) + ",(" + _holdTimes[button.GetAssociation()] + ", " + button.GetHoldable() + ")";
								_holdTimes.Remove(button.GetAssociation());
								_buttonLocks.Remove(button.GetAssociation());
							}
							else if (_repeatHoldTimes.ContainsKey(button.GetAssociation()) && _repeatHoldTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) < 0)
							{
								//DrawHelper.Debug = "FINSHES2:" + _advances + DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds;
								//DrawHelper.Debug = _holdTimes[button.GetAssociation()].CompareTo(DeathSquid.DeathSquid.CurrentGameTime.TotalRealTime.TotalSeconds) + ",(" + _holdTimes[button.GetAssociation()] + ", " + button.GetHoldable() + ")";
								_repeatHoldTimes.Remove(button.GetAssociation());
								//_holdTimes.Remove(button.GetAssociation());
								//_buttonLocks.Remove(button.GetAssociation());
							}
							else if (!_holdTimes.ContainsKey(button.GetAssociation()))
							{
								_buttonLocks.Remove(button.GetAssociation());
							}
							// We already have an association
							// Which has just been removed (key finally pulled up, remove association)
							// Were we holdable?
							// Well I guess it doesnt matter... The association... HAS BEEN REVOKED!
							return false;
						}
					}
				}
				return false;
			}
			return false;
		}
		internal void SetInputs(KeyboardState keyState, GamePadState padState)
		{
			_keyState = keyState;
			_gamePadState = padState;
		}
	}
}