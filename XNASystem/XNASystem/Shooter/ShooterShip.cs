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
		private const float _yPosition = 550;
		private int _hitPoints;
		private const int Width = 45;
		private const int Height = 45;

		//animation stuff
		private float timer = 0f;
		private float interval = 1000f / 7f;
		private int frameCount;
		private int currentFrame = 0;
		private int _currentSprite;
		private List<int> _standardSprites = new List<int> {8};
		private List<int> _shootSprites = new List<int> {9};
		private List<int> _deadSprites = new List<int> {10, 11, 12, 18, 19, 20, 21, 22};
		//private List<int> _explodeSprites = new List<int> {18, 19, 20, 21, 22};
		private List<int> _currentSprites;




		public ShooterShip()
		{
			_xPosition = 300;
			_currentSprite = 8;
			_currentSprites = _standardSprites;
			frameCount = _currentSprites.Count;
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
		}

		public void Shoot()
		{
			_currentSprites = _shootSprites;
			frameCount = _currentSprites.Count;
			currentFrame = 0;
		}

		public void Reload()
		{
			if (!IsDead())
			{
				_currentSprites = _standardSprites;
				frameCount = _currentSprites.Count;
				currentFrame = 0;
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

			_currentSprite = _currentSprites[currentFrame];
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[_currentSprite], new Vector2(_xPosition, _yPosition), Color.White);
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
