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
		#region variables

		private float _xPosition;
		private const float YPosition = 550;

		#endregion

		#region constructor

		public BreakOutPaddle()
		{
			_xPosition = 300;
		}

		#endregion

		#region update

		public void UpdatePostion(float x, float y)
		{
			_xPosition += 2* x;
		}

		#endregion

		#region draw

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[3], new Vector2(_xPosition, YPosition), Color.White);
		}

		#endregion

		#region get/set methods

		public float GetX()
		{
			return _xPosition;
		}

		public float GetY()
		{
			return YPosition;
		}

		public void SetX(float i)
		{
			_xPosition = i;
		}

		#endregion
	}
}
