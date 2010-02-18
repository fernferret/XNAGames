using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;

namespace XNASystem.BreakOut
{
	class BreakOutPaddle : IGameObject
	{
		#region variables

		private float _xPosition;
		private float YPosition = SystemMain.Height - 15;
		private readonly int _width;
		private readonly int _height;
		private Rectangle[] _sideRect;

		#endregion

		#region constructor

		public BreakOutPaddle()
		{
			_xPosition = 300;
			
			_width = 200;
			_height = 15;
		}

		#endregion

		#region update

		public void UpdatePostion(float x, float y)
		{
			if (2 * x > 1)
			{
				_xPosition++;
			}
			else if (2 * x < -1)
			{
				_xPosition--;
			}
			else
			{
				_xPosition += 2 * x;
			}
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

		public int GetSide(Rectangle ball)
		{
			_sideRect = new[]{new Rectangle((int) _xPosition, (int) YPosition, _width, 1), 
										new Rectangle((int) (_xPosition + _width - 1), (int) (YPosition + 1), 1, _height - 1), 
										new Rectangle((int) _xPosition, (int) (YPosition + 1), 1, _height - 1)};

			if (ball.Intersects(_sideRect[1]))
			{
				return 1;
			}
			if (ball.Intersects(_sideRect[2]))
			{
				return 1;
			}
			return 0;
		}

		public int GetWidth()
		{
			return _width;
		}

		public int GetHeight()
		{
			return _height;
		}

		#endregion
	}
}