using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		private int _speed = 5;
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
		public void DrawSelection(Texture2D[] t, int height, int widthOfString)
		{
			_selectionFinalWidth = widthOfString;
			_selectionFinalY = height;
			if (_selectionCurrentY == -1)
			{
				_selectionCurrentY = height;
			}
			UpdateSelection();
			DrawSelectionBox(t);
		}
		public void DrawSelectionBox(Texture2D[] t)
		{
			//_sb.Draw(t[1], new Rectangle(_xmin - ButtonSideWidth, height + ButtonOffset, ButtonSideWidth, ButtonWidth), Color.White);
			//_sb.Draw(t[0], new Rectangle(_xmin, height + ButtonOffset, widthOfString, ButtonWidth), Color.White);
			//_sb.Draw(t[2], new Rectangle(_xmin + widthOfString, height + ButtonOffset, ButtonSideWidth, ButtonWidth), Color.White);

			_sb.Draw(t[1], new Rectangle(_xmin - ButtonSideWidth, _selectionCurrentY + ButtonOffset, ButtonSideWidth, ButtonWidth), Color.White);
			_sb.Draw(t[0], new Rectangle(_xmin, _selectionCurrentY + ButtonOffset, _selectionCurrentWidth, ButtonWidth), Color.White);
			_sb.Draw(t[2], new Rectangle(_xmin + _selectionCurrentWidth, _selectionCurrentY + ButtonOffset, ButtonSideWidth, ButtonWidth), Color.White);
			_sb.DrawString(SystemMain.FontPackage[1], _selectionCurrentWidth + "," + _selectionFinalWidth,new Vector2(800,500), Color.White);
		}

		internal void DrawTitleCentered(SpriteFont currentFont, string title)
		{
			var widthOfCurrentString = (int)(Math.Ceiling(currentFont.MeasureString(title).X));
			var heightOfCurrentstring = (int)(Math.Ceiling(currentFont.MeasureString(title).Y));
			_sb.DrawString(currentFont, title, new Vector2(SystemMain.Width/2 - widthOfCurrentString/2, 60 - heightOfCurrentstring), Color.White);
		}
		internal void DrawSubTitleCentered(SpriteFont currentFont, string title)
		{
			var widthOfCurrentString = (int)(Math.Ceiling(currentFont.MeasureString(title).X));
			var heightOfCurrentstring = (int)(Math.Ceiling(currentFont.MeasureString(title).Y));
			_sb.DrawString(currentFont, title, new Vector2(SystemMain.Width / 2 - widthOfCurrentString / 2, 100 - heightOfCurrentstring), Color.White);
		}
		public int DrawMenu(List<String> strings, SpriteFont font)
		{
			var drawLocations = GetDrawLocations(strings);
			
			var i = 0;
			foreach (var str in strings)
			{
				_sb.DrawString(font, str, new Vector2(_xmin, drawLocations[i]), Color.Aquamarine);
				i++;
			}
			return _spacingDanger;
		}
		public void AddHelpBox(Texture2D[] t, int x, int y, int width, int height)
		{
			_helpBoxes.Add(new HelpBox(_sb,x,y,width, height,1,t,"test",SystemMain.FontPackage[1]));
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
		public List<int> GetDrawLocations(List<String> strings)
		{
			ResetPadding();
			RecalculateScreenPadding();
			
			var drawLocations = new List<int>();
			
			
			var hconst = 0;
			var totalheight = 0;
			
			if (strings.Count > 1)
			{
				hconst = (int) Math.Floor(((Double) _ymax - _ymin)/(strings.Count - 1));
				_spacingDanger = hconst < ButtonWidth/2 ? 1 : 0;
			}
			
			// We have too much spacing, decrease the amount of space between
			// words until we hava an acceptable number
			while (hconst > _spacingConstant)
			{
				_heightPadding = _heightPadding + .02;
				RecalculateScreenPadding();
				hconst = (int)Math.Floor(((Double)_ymax - _ymin) / (strings.Count - 1));
				totalheight = (strings.Count - 1) * hconst;
			}
			var h = _ymin;
			if(totalheight != 0)
			{
				h += (((_ymax-_ymin)-totalheight)/2);
			}
			if (strings.Count == 1)
			{
				hconst = 0;
				h = _ymax - _ymin;
			}

			drawLocations.Add(h);
			foreach (var str in strings)
			{
				h += hconst;
				drawLocations.Add(h);
			}
			drawLocations.RemoveAt(drawLocations.Count - 1);
			return drawLocations;
		}

		
	}
}
