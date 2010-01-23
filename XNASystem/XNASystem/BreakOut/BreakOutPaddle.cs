using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;

namespace XNASystem.BreakOut
{
	class BreakOutPaddle : IGameObject
	{
		private int _xPosition;
		private int _yPosition;
		public BreakOutPaddle(int x, int y)
		{
			_xPosition = x;
		}

		public void UpdatePostiion(int x, int y)
		{
			_xPosition = x;
			_yPosition = y;
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[3], new Vector2(_xPosition, 550), Color.White);
		}
	}
}
