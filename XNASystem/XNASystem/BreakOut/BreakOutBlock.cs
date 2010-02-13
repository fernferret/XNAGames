using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;
using System;

namespace XNASystem.BreakOut
{

	class BreakOutBlock : IGameObject
	{
		#region variables

		private readonly int _xPosition;
		private readonly int _yPosition;
		private Blocktype _type;
		private Color _color;
		private int _buffer;
		private int _height;
		private int _width;
		private readonly List<Rectangle> _rectSideList;

		#endregion

		#region constructor

		public BreakOutBlock(int xPosition, int yPosition, int width, int height, Blocktype type)
		{
			_height = height;
			_width = width;
			_buffer = (width % 78) / 2;
			_xPosition = (xPosition * 78) + _buffer;
			_yPosition = (yPosition * 36) + _buffer + 36;
			_type = type;
			_rectSideList = new List<Rectangle> { new Rectangle(_xPosition + 1, _yPosition, 76, 1),
												new Rectangle(_xPosition + 77, _yPosition + 1, 1, 34),
												new Rectangle(_xPosition + 1, _yPosition + 35, 76, 1),
												new Rectangle(_xPosition, _yPosition + 1, 1, 34)};
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
			switch (_type)
			{
				case Blocktype.Invincible:
					spriteBatch.Draw(textures[2], new Vector2(_xPosition, _yPosition), _color);
					break;
				case Blocktype.Ball:
					spriteBatch.Draw(textures[69], new Vector2(_xPosition, _yPosition), _color);
					break;
				default:
					spriteBatch.Draw(textures[5], new Vector2(_xPosition, _yPosition), _color);
					break;
			}
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
			return _rectSideList;
		}

		public int GetSide(Rectangle ball)
		{

			int hit = -1;

			int i;
			for (i = 0; i < 4; i++)
			{
				if (ball.Intersects(_rectSideList[i]))
				{
					hit = i;
				}
			}

			if (hit == -1)
			{
				return 0;
			}

			return hit;
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