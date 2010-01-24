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
		private float _xPosition;
		private const float _yPosition = 550;

		public BreakOutPaddle()
		{
			_xPosition = 300;
		}

		public void UpdatePostiion(float x, float y)
		{
			_xPosition += 20 * x;
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[3], new Vector2(_xPosition, _yPosition), Color.White);
		}
	}
}
