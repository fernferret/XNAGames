using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		}
		private void SetButtonPressed(ButtonAction a)
		{
			if (_lastPressed.Equals(ButtonAction.NONE))
				_lastPressed = a;
		}
		private void ClearButtonPressed()
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

			}
		}
		public void Draw()
		{
			//DeathSquid.GameSpriteBatch.Begin();
			SystemMain.Drawing.DrawTitleCentered(SystemMain.FontPackage["Title"], _title);
			SystemMain.Drawing.DrawSubTitleCentered(SystemMain.FontPackage["Title"], _subtitle);
			SystemMain.Drawing.DrawMenu(_menuText, SystemMain.FontPackage["Main"], _choice, new[] { SystemMain.TexturePackage["Hilight_left"], SystemMain.TexturePackage["Hilight_center"], SystemMain.TexturePackage["Hilight_right"] });
			//DeathSquid.GameSpriteBatch.End();
		}

		public String GetSelectedItem()
		{
			if (!_lastPressed.Equals(ButtonAction.NONE))
			{
				return _menuText[_choice];
			}
			return "";
		}
	}
}