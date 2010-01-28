using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;

namespace XNASystem.Shooter
{
	class ShooterShip : IGameObject
	{

		private float _xPosition;
		private const float _yPosition = 500;
		private int _hitPoints;
		private int _width;
		private int _height;

		public ShooterShip()
		{
			_xPosition = 300;
		}

		public void UpdatePostion(float x, float y)
		{
			_xPosition += 20 * x;
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			throw new NotImplementedException();
		}

		public float GetX()
		{
			return _xPosition;
		}

		public float GetY()
		{
			return _yPosition;
		}

		public int GetWidth()
		{
			return _width;
		}

		public int GetHeight()
		{
			return _height;
		}
	}
}
