using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.Utils
{
	class DrawHelper
	{
		private SpriteBatch _sb;
		public DrawHelper(SpriteBatch sb)
		{
			_sb = sb;
		}
		public Rectangle SendSelectorTo(int x, int y, int width)
		{
			var r = new Rectangle();
			return r;
		}
		public void DrawSelection(Texture2D[] t, int x, int widthOfString)
		{
			_sb.Draw(t[1], new Rectangle(76, x, 24, 95), Color.White);
			_sb.Draw(t[0], new Rectangle(100, x, widthOfString, 95), Color.White);
			_sb.Draw(t[2], new Rectangle(100 + widthOfString, x, 24, 95), Color.White);
		}
	}
}
