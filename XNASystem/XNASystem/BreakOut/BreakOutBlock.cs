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
			_xPosition = xPosition;
			_yPosition = yPosition;
			_type = type;
			_color = color;
			_rectList = new List<Rectangle> { new Rectangle(xPosition * 78, yPosition * 36, 78, 1),
												new Rectangle((xPosition * 78) + 77, yPosition* 36, 1, 36),
												new Rectangle(xPosition * 78, (yPosition* 36) + 35, 78, 1),
												new Rectangle(xPosition * 78, yPosition* 36, 1, 36)};
		}

		#endregion

		#region useless update

		public void UpdatePostiion(float x, float y)
		{
			//do nothing
		}

		#endregion

		#region draw

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[5], new Vector2(10 + (_xPosition * 78), 10 + (_yPosition * 36)), _color);
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

			return 0;
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
