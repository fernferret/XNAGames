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
	class ShooterShip : IGameObject
	{

		private float _xPosition;
		private float _yPosition;
		private int _hitPoints;
		private const int Width = 45;
		private const int Height = 45;
		private bool _isDying = false;
		private ShooterProjectile _shot;

		//animation stuff
		private float timer = 0f;
		private float interval = 1000f / 7f;
		private int frameCount;
		private int currentFrame = 0;
		private int _currentSprite;
		private List<int> _standardSprites = new List<int> {8};
		private List<int> _shootSprites = new List<int> {9};
		private List<int> _deadSprites = new List<int> { 10, 11, 12, 18, 19, 20, 21, 22, 29, 30, 31, 23 };
		//private List<int> _explodeSprites = new List<int> {18, 19, 20, 21, 22};
		private List<int> _currentSprites;
		private Rectangle _collisionBox;



		public ShooterShip()
		{
			_xPosition = 300;
			_yPosition = 550;
			_currentSprite = 8;
			_currentSprites = _standardSprites;
			frameCount = _currentSprites.Count;
			_collisionBox = new Rectangle((int)_xPosition, (int)_yPosition, 50, 50);
		}

		public void UpdatePostion(float x, float y)
		{
			_xPosition += 7*x;
			if(_xPosition <= 0)
			{
				_xPosition = (float) 0.1;
			}
			if(_xPosition >= 755)
			{
				_xPosition = (float) 754.9;
			}

			_collisionBox.Location = new Point((int)_xPosition, (int)_yPosition);
		}

		public void UpdateProjectile()
		{
			if (_shot != null)
			{
				_shot.UpdatePostion(0, 0);

				if (_shot.GetY() < 0)
				{
					_shot = null;
				}
			}
		}

		public Rectangle GetCollisionBox()
		{
			return _collisionBox;
		}

		public Rectangle GetShotCollisionBox()
		{
			return _shot.GetCollisionBox();
		}

		public void Shoot()
		{
			if (!IsDying() && (_shot == null))
			{
				_currentSprites = _shootSprites;
				frameCount = _currentSprites.Count;
				currentFrame = 0;

				_shot = new ShooterProjectile(_xPosition + Width/2*1 - 5, _yPosition - 7, 10, 10, 0, 15, Color.White);
			}
		}

		public void Reload()
		{
			if (!IsDying())
			{
				_currentSprites = _standardSprites;
				frameCount = _currentSprites.Count;
				currentFrame = 0;
			}
		}

		public ShooterProjectile GetShot()
		{
			return _shot;
		}

		public void KillProjectile()
		{
			_shot = null;
		}

		public void Kill()
		{
			_isDying = true;
			_currentSprites = _deadSprites;
			frameCount = _currentSprites.Count;
			currentFrame = 0;
		}

		public bool IsDead()
		{
			bool isSprites = _currentSprites.Equals(_deadSprites);
			bool isCount = (currentFrame >= _currentSprites.Count - 1);
			return isSprites && isCount;
		}

		public bool IsDying()
		{
			return _isDying;
		}

		public void AnimateSprite(GameTime gameTime)
		{
			if (!IsDead())
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

				_currentSprite = _currentSprites[currentFrame];
			}
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[_currentSprite], new Vector2(_xPosition, _yPosition), Color.White);

			if(_shot != null)
			{
				_shot.Draw(spriteBatch, fonts, textures);
			}
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
			return Width;
		}

		public int GetHeight()
		{
			return Height;
		}
	}
}
