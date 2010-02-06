using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;

namespace XNASystem.Shooter
{
	class ShooterEnemy : IGameObject
	{
		#region variables

		private float _xPosition;
		private float _yPosition;
		private float _speed;
		private float _xVelocity;
		private float _yVelocity;
		private float _xTarget;
		private float _yTarget;
		private int _hitPoints;
		private Color _color;

		#endregion

		public ShooterEnemy(float xPosition, float yPosition, float speed, Color color)
		{
			_yPosition = yPosition;
			_xPosition = xPosition;
			_speed = speed;
			_color = color;
		}

		public void SetTarget(float x, float y)
		{
			float angle;

			_xTarget = x;
			_yTarget = y;

			angle = (float) Math.Tan((y - _yPosition)/(x - _xPosition));
			_xVelocity = _speed*((float) Math.Cos(angle));
			_yVelocity = _speed*((float) Math.Sin(angle));
		}

		public void UpdatePosition(float x, float y)
		{
			if (_xPosition != _xTarget) //&& (_yPosition != _yTarget)
			{
				_xPosition += _xVelocity + x;
				_yPosition += _yVelocity + y;
			}
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[5], new Vector2(10 + (_xPosition * 78), 10 + (_yPosition * 36)), _color);
		}

		//shoot method

		public float GetX()
		{
			return _xPosition;
		}

		public float GetY()
		{
			return _yPosition;
		}

	}
}
