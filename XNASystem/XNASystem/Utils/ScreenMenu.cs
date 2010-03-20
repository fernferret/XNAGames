using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.QuizArch;

namespace XNASystem.Utils
{
	class ScreenMenu
	{
		private readonly List<String> _menuText;
		private int _choice;
		private readonly String _title;
		private ButtonAction _lastPressed = ButtonAction.NONE;
		private String _subtitle;

		/// <summary>
		/// Creates a new Menu of text that will be drawn on the screen
		/// </summary>
		/// <param name="text">List of items to show in the menu</param>
		/// <param name="t"></param>
		public ScreenMenu(List<String> text, String t)
		{
			_menuText = text;
			_title = t;
			_subtitle = "";
		}
		public ScreenMenu(List<Answer> text, String t, String st)
		{
			_menuText = new List<string>();
			foreach (var answer in text)
			{
				_menuText.Add(answer.TheAnswer);
			}
			_title = t;
			_subtitle = st;
		}
		public ScreenMenu(List<String> text)
		{
			_menuText = text;
			_title = "";
			_subtitle = "";
		}
		private void SetButtonPressed(ButtonAction a)
		{
			if (_lastPressed.Equals(ButtonAction.NONE))
				_lastPressed = a;
		}
		public void ClearButtonPressed()
		{
			_lastPressed = ButtonAction.NONE;
		}
		public void Update()
		{
			if (SystemMain.GetInput.IsButtonPressed(ButtonAction.MenuUp))
			{
				if (_choice == 0)
				{
					_choice = _menuText.Count - 1;
				}
				else
				{
					_choice--;
				}
			}
			if (SystemMain.GetInput.IsButtonPressed(ButtonAction.MenuDown))
			{
				if (_choice == _menuText.Count - 1)
				{
					_choice = 0;
				}
				else
				{
					_choice++;
				}
			}
			if (SystemMain.GetInput.IsButtonPressed(ButtonAction.MenuAccept))
			{
				SetButtonPressed(ButtonAction.MenuAccept);
			}
			if (SystemMain.GetInput.IsButtonPressed(ButtonAction.MenuCancel))
			{
				SetButtonPressed(ButtonAction.MenuCancel);
			}
		}
		public void Draw()
		{
			//DeathSquid.GameSpriteBatch.Begin();
			SystemMain.GameSpriteBatch.Draw(SystemMain.TexturePackage["Background"], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);
			SystemMain.Drawing.DrawTitleCentered(SystemMain.FontPackage["Title"], _title);
			
			SystemMain.Drawing.DrawMenu(_menuText, SystemMain.FontPackage["Main"], _choice, new[] { SystemMain.TexturePackage["HilightLeft"], SystemMain.TexturePackage["HilightCenter"], SystemMain.TexturePackage["HilightRight"] });
			var keystate = typeof(InputHandler).GetField("_keyState", BindingFlags.NonPublic | BindingFlags.Instance);
			var keystatev = keystate.GetValue(SystemMain.GetInput);
			var test = new List<String>();
			for (int i = 0; i <= 7; i++)
			{
				test.Add(
					((UInt32)typeof(KeyboardState).GetField("currentState"+i,
					                                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).GetValue(
						keystatev)).ToString());
			}


			SystemMain.Drawing.DrawSubTitleCentered(SystemMain.FontPackage["Title"], test[0] + ", " + test[1] + ", "+test[2] + ", "+test[3] + ", "+test[4] + ", "+test[5] + ", "+test[6]);
			//DeathSquid.GameSpriteBatch.End();
		}

		public bool GetSelectedItem(String s)
		{
			if (_lastPressed.Equals(ButtonAction.MenuAccept))
			{
				if (_menuText[_choice] == s)
				{
					ClearButtonPressed();
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Gets the selected item from the list NOW.  This does NOT wait for input.
		/// </summary>
		/// <returns></returns>
		public String GetSelectedItem(bool clear)
		{
			if (_lastPressed.Equals(ButtonAction.MenuAccept))
			{
				if (clear)
					ClearButtonPressed();
				return (_menuText[_choice]);
			}
			return "";
		}
		public String GetItemOnNewPress()
		{
			return "";
		}

		public void SetText(List<Answer> text)
		{
			_menuText.Clear();
			foreach (var answer in text)
			{
				_menuText.Add(answer.TheAnswer);
			}
		}
	}
}