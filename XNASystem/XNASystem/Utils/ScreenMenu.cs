using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNASystem.Utils
{
	class ScreenMenu
	{
		private readonly List<String> _menuText;
		private int _choice;
		private readonly String _title;
		private ButtonAction _lastPressed = ButtonAction.NONE;
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
			if (DeathSquid.GetInput.IsButtonPressed(ButtonAction.MenuUp))
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
			if (DeathSquid.GetInput.IsButtonPressed(ButtonAction.MenuDown))
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
			if (DeathSquid.GetInput.IsButtonPressed(ButtonAction.MenuAccept))
			{
				SetButtonPressed(ButtonAction.MenuAccept);
			}
			if (DeathSquid.GetInput.IsButtonPressed(ButtonAction.MenuCancel))
			{

			}
		}
		public void Draw()
		{
			//DeathSquid.GameSpriteBatch.Begin();
			DeathSquid.Drawing.DrawTitleCentered(DeathSquid.SystemFonts["Title"], _title);
			DeathSquid.Drawing.DrawMenu(_menuText, DeathSquid.SystemFonts["Main"], _choice, new[] { DeathSquid.GameGraphics["Hilight_left"], DeathSquid.GameGraphics["Hilight_center"], DeathSquid.GameGraphics["Hilight_right"] });
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