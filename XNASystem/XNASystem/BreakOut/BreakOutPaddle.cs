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
		private readonly Rectangle[] _sideRect;

		#endregion

		#region constructor

		public BreakOutPaddle()
		{
			_xPosition = 300;
			_sideRect = new Rectangle[]{new Rectangle((int) _xPosition, (int) YPosition, 199, 1), 
										new Rectangle((int) (_xPosition + 198), (int) (YPosition + 1), 1, 15), 
										new Rectangle((int) _xPosition, (int) (YPosition + 16), 199, 1), 
										new Rectangle((int) _xPosition, (int) (YPosition + 1), 1, 15)};
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

		public int GetSide(Rectangle ball)
		{
			if(ball.Intersects(_sideRect[1]))
			{
				return 1;
			}
			if(ball.Intersects(_sideRect[3]))
			{
				return 3;
			}
			return 0;
		}

		#endregion
	}
}
