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

		//animation stuff
		private float timer = 0f;
		private float interval = 1000f / 7f;
		private int frameCount;
		private int currentFrame = 0;
		private int _currentSprite;
		private List<int> _standardSprites = new List<int> { 13, 14 };
		private List<int> _deadSprites = new List<int> { 15, 16, 17, 18, 19, 20, 21, 22 };
		private List<int> _currentSprites;

		#endregion

		public ShooterEnemy(float xPosition, float yPosition, float speed, Color color)
		{
			_xPosition = (xPosition * 50) + 2;
			_yPosition = (yPosition * 50) + 2;
			_speed = speed;
			_color = color;
			_currentSprite = 13;
			_currentSprites = _standardSprites;
			frameCount = _currentSprites.Count;
		}

		public void SetTarget(float x, float y)
		{
			float angle;

			_xTarget = (x * 50) + 2; ;
			_yTarget = (y * 50) + 2;

			angle = (float) Math.Tan((y - _yPosition)/(x - _xPosition));
			_xVelocity = _speed*((float) Math.Cos(angle));
			_yVelocity = _speed*((float) Math.Sin(angle));
		}


		public void UpdatePostion(float x, float y)
		{
			float xCheck = Math.Abs(_xPosition - _xTarget);
			float yCheck = Math.Abs(_yPosition - _yTarget);

			bool check = ((xCheck > (float) 0.1) || (yCheck > (float) 0.1));
			if (check)
			{
				_xPosition += _xVelocity + x;
				_yPosition += _yVelocity + y;
			}
			
		}

		public void Kill()
		{
			_currentSprites = _deadSprites;
			frameCount = _currentSprites.Count + 1;
			currentFrame = 0;
		}

		public bool IsDead()
		{
			return (_currentSprites.Equals(_deadSprites) && (currentFrame == _currentSprites.Count));
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[_currentSprite], new Vector2(_xPosition, _yPosition), _color);
		}

		public void AnimateSprite(GameTime gameTime)
		{
			
				timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;

				if (timer > interval)
				{
					currentFrame++;
					if (currentFrame > (frameCount - 1))
					{
						currentFrame = 0;
					}
					timer = 0f;
				}
				if (!IsDead())
				{

					_currentSprite = _currentSprites[currentFrame];
				}
		}

		//shoot method

		public float GetX()
		{
			return (_xPosition - 2)/50;
		}

		public float GetY()
		{
			return (_yPosition - 2)/50;
		}

	}
}
