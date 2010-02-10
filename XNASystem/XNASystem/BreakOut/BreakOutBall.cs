using System;
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
		private Boolean _alive;
		private Boolean _constantV;

		#endregion

		#region constructor

		public BreakOutBall(float xPosition, float yPosition, float xVelocity, float yVelocity)
		{
			_xPosition = xPosition;
			_yPosition = yPosition;
			_xVelocity = xVelocity;
			_yVelocity = yVelocity;
			_alive = true;
			_constantV = false;
		}

		#endregion

		#region update

		public void UpdatePostion(float x, float y)
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
			if(!_constantV)
			_xVelocity *= -1;
		}

		#endregion

		public void IncrementX(float f)
		{
			if (!_constantV)
			{
				_xVelocity += f/2;
				if (_xVelocity > 1)
				{
					_xVelocity = 1;
				}
				if (_xVelocity < -1)
				{
					_xVelocity = -1;
				}
			}
		}

		public void Kill()
		{
			_alive = false;
		}

		public Boolean IsAlive()
		{
			return _alive;
		}


		public void MakeConstantV()
		{
			_constantV = true;
		}

		public Boolean IsConstantV()
		{
			return _constantV;
		}

	}
}
