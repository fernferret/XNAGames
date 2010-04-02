﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.Utils
{
	public class DrawHelper
	{
		private SpriteBatch _sb;
		private Double _heightPadding = .2;
		private Double _widthPadding = .15;
		private int _xmin, _xmax, _ymin, _ymax;
		private const int ButtonWidth = 95;
		private const int ButtonSideWidth = 24;
		private const int ButtonOffset = -35;
		private int _spacingDanger;
		private int _spacingConstant = 100;
		private List<HelpBox> _helpBoxes;
		private int _selectionCurrentX;
		private int _selectionCurrentY = -1;
		private int _selectionCurrentWidth;
		private int _selectionFinalX;
		private int _selectionFinalY;
		private int _selectionFinalWidth;
		private int _speed = 10;
		private int _hconst = 0;
		private const int StandardSpeed = 10;
		private const int GlobalOffset = 5;
		public static String Debug = "";
        private const float NoRotation = 0;
        private const float ZeroLayerDepth = 0;
        private const float InitialXPosition = 0;
        private const float InitailYPosition = 0;
        private const int ConvertTo90Degrees = 2;

		public DrawHelper(SpriteBatch sb)
		{
			_sb = sb;
			_xmin = (int)(Math.Floor(SystemMain.Width * _widthPadding));
			_xmax = SystemMain.Width - _xmin;
			_ymin = (int)(Math.Floor(SystemMain.Height * _heightPadding));
			_ymax = SystemMain.Height - _ymin;
			_helpBoxes = new List<HelpBox>();
		}
		public Rectangle SendSelectorTo(int x, int y, int width)
		{
			var r = new Rectangle();
			return r;
		}
		public void DrawRectangle(int x, int y, int h, int w, Texture2D[] t)
		{
			// Upper Left
			_sb.Draw(t[0], new Rectangle(x, y, ButtonSideWidth, ButtonSideWidth), Color.White);
			// Upper Right
            _sb.Draw(t[0], new Rectangle(x + w, y, ButtonSideWidth, ButtonSideWidth), null, Color.White, NoRotation, new Vector2(InitialXPosition, InitailYPosition), SpriteEffects.FlipHorizontally, ZeroLayerDepth);

			// Lower Left
            _sb.Draw(t[0], new Rectangle(x, y + h, ButtonSideWidth, ButtonSideWidth), null, Color.White, NoRotation, new Vector2(InitialXPosition, InitailYPosition), SpriteEffects.FlipVertically, ZeroLayerDepth);
			// Lower Right
            _sb.Draw(t[0], new Rectangle(x + ButtonSideWidth + w, y + ButtonSideWidth + h, ButtonSideWidth, ButtonSideWidth), null, Color.White, (float)Math.PI, new Vector2(InitialXPosition, InitailYPosition), SpriteEffects.None, ZeroLayerDepth);

			// Draw Fill
			_sb.Draw(t[2], new Rectangle(x + ButtonSideWidth, y + ButtonSideWidth, w - ButtonSideWidth, h - ButtonSideWidth), Color.White);

			// Top Border
			_sb.Draw(t[1], new Rectangle(x + ButtonSideWidth, y, w - ButtonSideWidth, ButtonSideWidth), Color.White);
			// Bottom Border
            _sb.Draw(t[1], new Rectangle(x + ButtonSideWidth, y + h, w - ButtonSideWidth, ButtonSideWidth), null, Color.White, NoRotation, new Vector2(InitialXPosition, InitailYPosition), SpriteEffects.FlipVertically, ZeroLayerDepth);

			// Right Border
            _sb.Draw(t[1], new Rectangle(x + ButtonSideWidth + w, y + ButtonSideWidth, h - ButtonSideWidth, ButtonSideWidth), null, Color.White, (float)(Math.PI / ConvertTo90Degrees), new Vector2(InitialXPosition, InitailYPosition), SpriteEffects.None, ZeroLayerDepth);
			// Left Border
            _sb.Draw(t[1], new Rectangle(x + ButtonSideWidth, y + ButtonSideWidth, h - ButtonSideWidth, ButtonSideWidth), null, Color.White, (float)(Math.PI / ConvertTo90Degrees), new Vector2(InitialXPosition, InitailYPosition), SpriteEffects.FlipVertically, ZeroLayerDepth);
		}
		public void SelectionGoTo(int x, int y, int width)
		{
			if (x > -1)
			{
				_selectionFinalX = x;
			}
			if (y > -1)
			{
				_selectionFinalY = y;
			}
			if (width > -1)
			{
				_selectionFinalWidth = width;
			}
		}
		public void UpdateSelection()
		{
			if (Math.Abs(_selectionCurrentWidth - _selectionFinalWidth) <= _speed)
			{
				_selectionCurrentWidth = _selectionFinalWidth;
			}
			else if (_selectionCurrentWidth > _selectionFinalWidth)
			{
				_selectionCurrentWidth -= _speed;
			}
			else if (_selectionCurrentWidth < _selectionFinalWidth)
			{
				_selectionCurrentWidth += _speed;
			}

			if (Math.Abs(_selectionCurrentY - _selectionFinalY) <= _speed)
			{
				_selectionCurrentY = _selectionFinalY;
			}
			else if (_selectionCurrentY > _selectionFinalY)
			{
				_selectionCurrentY -= _speed;
			}
			else if (_selectionCurrentY < _selectionFinalY)
			{
				_selectionCurrentY += _speed;
			}
		}
		public void DrawSelection(int height, int widthOfString)
		{
			_selectionFinalWidth = widthOfString;
			_selectionFinalY = height;
			if (_selectionCurrentY == -1)
			{
				_selectionCurrentY = height;
			}
			UpdateSelection();
			DrawSelectionBox();
		}
		public void DrawSelection2(int height, String s, SpriteFont font)
		{
			var widthOfString = (int)(Math.Ceiling(font.MeasureString(s).X));
			_selectionFinalWidth = widthOfString;
			_selectionFinalY = height;
			if (_selectionCurrentY == -1)
			{
				_selectionCurrentY = height;
			}
			UpdateSelection();
			DrawSelectionBox();
		}
		private void DrawSelectionBox()
		{
			_sb.Draw(SystemMain.TexturePackage["HilightLeft"], new Rectangle(_xmin - ButtonSideWidth, _selectionCurrentY + ButtonOffset, ButtonSideWidth, ButtonWidth), Color.White);
			_sb.Draw(SystemMain.TexturePackage["HilightCenter"], new Rectangle(_xmin, _selectionCurrentY + ButtonOffset, _selectionCurrentWidth, ButtonWidth), Color.White);
			_sb.Draw(SystemMain.TexturePackage["HilightRight"], new Rectangle(_xmin + _selectionCurrentWidth, _selectionCurrentY + ButtonOffset, ButtonSideWidth, ButtonWidth), Color.White);

			_sb.DrawString(SystemMain.FontPackage["Main"], "" + _speed, new Vector2(100, 300), Color.Aquamarine);
			_sb.DrawString(SystemMain.FontPackage["Main"], "" + (_selectionCurrentY + ButtonOffset), new Vector2(100, 400), Color.Aquamarine);
		}

		public void DrawTitleCentered(SpriteFont currentFont, string title)
		{
			var widthOfCurrentString = (int)(Math.Ceiling(currentFont.MeasureString(title).X));
			var heightOfCurrentstring = (int)(Math.Ceiling(currentFont.MeasureString(title).Y));
			_sb.DrawString(currentFont, title, new Vector2(SystemMain.Width / 2 - widthOfCurrentString / 2, 60 - heightOfCurrentstring), Color.White);
		}
		public void DrawSubTitleCentered(SpriteFont currentFont, string title)
		{
			var widthOfCurrentString = (int)(Math.Ceiling(currentFont.MeasureString(title).X));
			var heightOfCurrentstring = (int)(Math.Ceiling(currentFont.MeasureString(title).Y));
			_sb.DrawString(currentFont, title, new Vector2(SystemMain.Width / 2 - widthOfCurrentString / 2, 100 - heightOfCurrentstring), Color.White);
		}
		public int DrawMenu(List<String> strings, SpriteFont font, int choice, Texture2D[] t)
		{
			var drawLocations = GetDrawLocations(strings);

			var i = 0;
			if (Math.Abs(_selectionCurrentY - _selectionFinalY) > 2 * _hconst)
			{
				_speed = 2 * StandardSpeed;
			}
			else if (_speed != StandardSpeed && _selectionCurrentY == _selectionFinalY)
			{
				_speed = StandardSpeed;

			}
			DrawSelection2(drawLocations[choice] + GlobalOffset, strings[choice], font);
			foreach (var str in strings)
			{


				_sb.DrawString(font, str, new Vector2(_xmin, drawLocations[i]), Color.Aquamarine);
				i++;
			}
			_sb.DrawString(font, drawLocations[0] + "", new Vector2(500, drawLocations[0]), Color.Aquamarine);
			return _spacingDanger;
		}
		public void AddHelpBox(Texture2D[] t, int x, int y, int width, int height)
		{
			_helpBoxes.Add(new HelpBox(_sb, x, y, width, height, 1, t, "test", SystemMain.FontPackage["Main"]));
		}
		public void DrawHelpBox()
		{
			foreach (var box in _helpBoxes)
			{
				box.UpdatePosition();
				box.DrawRectangle();
			}
		}
		private void RecalculateScreenPadding()
		{
			_xmin = (int)(Math.Floor(SystemMain.Width * _widthPadding));
			_xmax = SystemMain.Width - _xmin;
			_ymin = (int)(Math.Floor(SystemMain.Height * _heightPadding));
			_ymax = SystemMain.Height - _ymin;
		}
		private void ResetPadding()
		{
			_heightPadding = .2;
			_widthPadding = .15;
		}
		private List<int> GetDrawLocations(List<String> strings)
		{
			ResetPadding();
			RecalculateScreenPadding();

			var drawLocations = new List<int>();


			_hconst = 0;
			var totalheight = 0;
			if (strings.Count == 1)
			{
				return new List<int> { SystemMain.Height - 200 };
			}
			if (strings.Count > 1)
			{
				_hconst = (int)Math.Floor(((Double)_ymax - _ymin) / (strings.Count - 1));
				_spacingDanger = _hconst < ButtonWidth / 2 ? 1 : 0;
			}

			// We have too much spacing, decrease the amount of space between
			// words until we hava an acceptable number
			while (_hconst > _spacingConstant)
			{
				_heightPadding = _heightPadding + .02;
				RecalculateScreenPadding();
				_hconst = (int)Math.Floor(((Double)_ymax - _ymin) / (strings.Count - 1));
				totalheight = (strings.Count - 1) * _hconst;
			}
			var h = _ymin;
			if (totalheight != 0)
			{
				h += (((_ymax - _ymin) - totalheight) / 2);
			}
			if (strings.Count == 1)
			{
				_hconst = 0;
				h = _ymax - _ymin;
			}

			drawLocations.Add(h);
			foreach (var str in strings)
			{
				h += _hconst;
				drawLocations.Add(h);
			}
			drawLocations.RemoveAt(drawLocations.Count - 1);
			return drawLocations;
		}


	}
}
