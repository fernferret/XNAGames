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
		private bool _isDying = false;
		private bool _isHurt = false;

		//animation stuff
		private float timer = 0f;
		private float interval = 1000f / 7f;
		private int frameCount;
		private int currentFrame = 0;
		private int _currentSprite;
		private List<int> _standardSprites = new List<int> { 13, 14 };
		private List<int> _deadSprites = new List<int> { 15, 16, 17, 18, 19, 20, 21, 22 };
		private List <int> _painSprites = new List<int>{ 25 };
		private List<int> _currentSprites;

		#endregion

		public ShooterEnemy(float xPosition, float yPosition, Color color, int hitPoints)
		{
			_xPosition = (xPosition * Width) + 2;
			_yPosition = (yPosition * Height) + 2;
			_color = color;
			_hitPoints = hitPoints;
			_currentSprites = _standardSprites;
			_currentSprite = _currentSprites[0];
			frameCount = _currentSprites.Count;
			_collisionBox = new Rectangle((int)_xPosition, (int)_yPosition, Height - 5, Width - 5);
		}


		public void UpdatePostion(float x, float y)
		{
			_xPosition += x;
			_yPosition += y;

			_collisionBox.Location = new Point((int)_xPosition, (int)_yPosition);
		}

		public bool CollidesWith(Rectangle box)
		{
			return _collisionBox.Intersects(box) ; 
		}

		public void Damage()
		{
			if (_hitPoints <= 0)
			{
				_currentSprites = _deadSprites;
				frameCount = _currentSprites.Count + 1;
				currentFrame = 0;
				_isDying = true;
			}
			else
			{
				_hitPoints--;
				_currentSprites = _painSprites;
				currentFrame = 0;
				frameCount = _currentSprites.Count;
			}
		}

		public bool IsDying()
		{
			return _isDying;
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
			return new ShooterProjectile(_xPosition + Width - 5, _yPosition + 7, 10, 10, 0, -3, Color.Red);
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
