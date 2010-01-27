using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;

namespace XNASystem.BreakOut
{

	class BreakOutBlock : IGameObject
	{
		#region variables

		private readonly int _xPosition;
		private readonly int _yPosition;
		private Blocktype _type;
		private Color _color;
		private readonly List<Rectangle> _rectList;

		#endregion

		#region constructor

		public BreakOutBlock(int xPosition, int yPosition, Blocktype type, Color color)
		{
			_xPosition = (xPosition * 78) + 10;
			_yPosition = (yPosition * 36) + 46;
			_type = type;
			_color = color;
			_rectList = new List<Rectangle> { new Rectangle(_xPosition, _yPosition, 78, 1),
												new Rectangle(_xPosition + 77, _yPosition, 1, 36),
												new Rectangle(_xPosition, _yPosition + 35, 78, 1),
												new Rectangle(_xPosition, _yPosition, 1, 36)};
		}

		#endregion

		#region useless update

		public void UpdatePostion(float x, float y)
		{
			//do nothing
		}

		#endregion

		#region draw

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[5], new Vector2(_xPosition, _yPosition), _color);
		}

		#endregion

		#region get/set methods

		public float GetX()
		{
			return _xPosition;
		}

		public float GetY()
		{
			return _yPosition;
		}

		public new Blocktype GetType()
		{
			return _type;
		}

		public List<Rectangle> GetRectList()
		{
			return _rectList;
		}

		public int GetSide(Rectangle ball)
		{
			int i;
			for(i = 0; i < 4; i++)
			{
				if(ball.Intersects(_rectList[i]))
				{
					return i;
				}
			}

			return 69696969;
		}

		public void SetType(Blocktype type)
		{
			_type = type;
		}

		public void SetColor(Color color)
		{
			_color = color;
		}
		#endregion
	}
}
