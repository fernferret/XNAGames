﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;

namespace XNASystem.BreakOut
{
	class BreakOutBall : IGameObject
	{
		#region variables

		private float _xPosition;
		private float _yPosition;
		private float _xVelocity;
		private float _yVelocity;
		private int _spin;
		private const double Effectiveness = 0.075;

		#endregion

		#region constructor

		public BreakOutBall(float xPosition, float yPosition, float xVelocity, float yVelocity)
		{
			_xPosition = xPosition;
			_yPosition = yPosition;
			_xVelocity = xVelocity;
			_yVelocity = yVelocity;
		}

		#endregion

		#region partially implemented, unused advanced movement methods
		/*public void SetSpin(int spin)
		{
			_spin = spin;
		}

		public void ReflectXVelocity()
		{
			_xVelocity *= -1;
		}

		public void ReflectYVelocity()
		{
			_yVelocity *= -1;
		}

		//timer must be reset after each time the ball bounces, otherwise, spin will not act correctly
		public void UpdatePosition(int time)
		{
			_xPosition = (int)(_xVelocity*time + Effectiveness*2*_spin*time);
			_yPosition = (int)(_yVelocity*time - Effectiveness*_spin*(time ^ 2));
		}*/

		#endregion

		#region update

		public void UpdatePostiion(float x, float y)
		{
			_xPosition += _xVelocity;
			_yPosition += _yVelocity;
		}

		#endregion

		#region draw

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[6], new Vector2(_xPosition, _yPosition), Color.White);
		}

		#endregion

		#region bouncing methods

		public float GetX()
		{
			return _xPosition;
		}

		public float GetY()
		{
			return _yPosition;
		}

		public float GetVx()
		{
			return _xVelocity;
		}

		public float GetVy()
		{
			return _yVelocity;
		}

		public void SwitchY()
		{
			_yVelocity *= -1;
		}

		public void SwitchX()
		{
			_xVelocity *= -1;
		}

		#endregion

		public void IncrementX(float f)
		{
			_xVelocity += f * 5;
			if(_xVelocity > 10)
			{
				_xVelocity = 10;
			}
			if(_xVelocity < -10)
			{
				_xVelocity = -10;
			}
		}
	}
}
