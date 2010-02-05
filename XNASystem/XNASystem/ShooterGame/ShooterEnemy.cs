using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;
using XNASystem.ShooterGame;

namespace XNASystem.Shooter
{
	class ShooterEnemy : IGameObject
	{
		#region variables

		private float _xPosition;
		private float _yPosition;
		private float _speed = 2;
		private int _direction = 1;
		private int _hitPoints;
		private Color _color;
		private Rectangle _collisionBox;
		private const int Width = 50;
		private const int Height = 50;

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

		public ShooterEnemy(float xPosition, float yPosition, Color color)
		{
			_xPosition = (xPosition * Width) + 2;
			_yPosition = (yPosition * Height) + 2;
			_color = color;
			_currentSprite = 13;
			_currentSprites = _standardSprites;
			frameCount = _currentSprites.Count;
			_collisionBox = new Rectangle((int)_xPosition, (int)_yPosition, 45, 45);
		}

/*
		public void SetTarget(float x, float y)
		{
			float angle;

			_xTarget = (x * 50) + 2; ;
			_yTarget = (y * 50) + 2;

			angle = (float) Math.Tan((y - _yPosition)/(x - _xPosition));
			_xVelocity = _speed*((float) Math.Cos(angle));
			_yVelocity = _speed*((float) Math.Sin(angle));
		}
*/

		public void UpdatePostion(float x, float y)
		{
			#region unused
			/*
			float xCheck = Math.Abs(_xPosition - _xTarget);
			float yCheck = Math.Abs(_yPosition - _yTarget);

			bool check = ((xCheck > (float) 0.1) || (yCheck > (float) 0.1));
			if (check)
			{
				_xPosition += _xVelocity + x;
				_yPosition += _yVelocity + y;
			}
			 * */
			#endregion
			/*if(_xPosition >= 750 || _xPosition <= 0)
			{
				_yPosition += 50;
				_direction *= -1;
			}
			_xPosition += _direction*_speed;*/
			//_yPosition += y;

			//new movement
			_xPosition += x;
			_yPosition += y;

			_collisionBox.Location = new Point((int)_xPosition, (int)_yPosition);
			//Console.WriteLine(_collisionBox.Center);

		}

		public bool CollidesWith(Rectangle box)
		{
			return _collisionBox.Intersects(box) ; 
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

		public ShooterProjectile GetShot()
		{
			return new ShooterProjectile(_xPosition + Width - 5, _yPosition + 7, 0, -3, Color.Red);
		}

		public float GetX()
		{
			return _xPosition;
		}

		public float GetY()
		{
			return _yPosition;
		}

		public void IncreaseSpeed(float s)
		{
			_speed += s;
		}

		public int GetWidth()
		{
			return Width;
		}

		public int GetHeight()
		{
			return Height;
		}

	}
}
