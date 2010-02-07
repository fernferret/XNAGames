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
		public DrawHelper(SpriteBatch sb)
		{
			_sb = sb;
			_xmin = (int)(Math.Floor(SystemMain.Width * _widthPadding));
			_xmax = SystemMain.Width - _xmin;
			_ymin = (int)(Math.Floor(SystemMain.Height * _heightPadding));
			_ymax = SystemMain.Height - _ymin;
		}
		public Rectangle SendSelectorTo(int x, int y, int width)
		{
			var r = new Rectangle();
			return r;
		}
		public void DrawSelection(Texture2D[] t, int height, int widthOfString)
		{
			_sb.Draw(t[1], new Rectangle(_xmin - ButtonSideWidth, height+ButtonOffset, ButtonSideWidth, ButtonWidth), Color.White);
			_sb.Draw(t[0], new Rectangle(_xmin, height + ButtonOffset, widthOfString, ButtonWidth), Color.White);
			_sb.Draw(t[2], new Rectangle(_xmin + widthOfString, height + ButtonOffset, ButtonSideWidth, ButtonWidth), Color.White);
		}

		internal void DrawTitleCentered(SpriteFont currentFont, string title)
		{
			var widthOfCurrentString = (int)(Math.Ceiling(currentFont.MeasureString(title).X));
			var heightOfCurrentstring = (int)(Math.Ceiling(currentFont.MeasureString(title).Y));
			_sb.DrawString(currentFont, title, new Vector2(SystemMain.Width/2 - widthOfCurrentString/2, 60 - heightOfCurrentstring), Color.White);
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

		public List<int> GetDrawLocations(List<String> strings)
		{
			var drawLocations = new List<int>();
			
			var h = _ymin;

			drawLocations.Add(h);

			var hconst = (int)Math.Floor(((Double)_ymax - _ymin) / (strings.Count - 1));
			_spacingDanger = hconst < ButtonWidth/2 ? 1 : 0;
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
