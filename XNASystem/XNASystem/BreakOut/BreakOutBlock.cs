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
		private readonly Blocktype _type;
		private readonly Color _color;
		private readonly List<Rectangle> _rectList;

		#endregion

		#region constructor

		public BreakOutBlock(int xPosition, int yPosition, Blocktype type, Color color)
		{
			_xPosition = xPosition;
			_yPosition = yPosition;
			_type = type;
			_color = color;
			_rectList = new List<Rectangle> { new Rectangle(xPosition, yPosition, 78, 1), 
												new Rectangle(xPosition, yPosition, 1, 36), 
												new Rectangle(xPosition, yPosition + 35, 78, 1), 
												new Rectangle(xPosition + 77, yPosition, 1, 36)};
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
			spriteBatch.Draw(textures[5], new Vector2(10 + (_xPosition * 76), 10 + (_yPosition * 36)), _color);
		}

		#endregion

		#region get methods

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

		#endregion
	}
}
