using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.ShooterGame
{
	class ShooterProjectile
	{
		private float _xPosition;
		private float _yPosition;
		private readonly float _xVelocity;
		private readonly float _yVelocity;
		private int _width = 10;
		private int _height = 10;
		private Rectangle _collisionBox;
		private Color _color;

		//animation stuff
		private float timer = 0f;
		private float interval = 1000f / 7f;
		private int frameCount;
		private int currentFrame = 0;
		private int _currentSprite;
		private List<int> _standardSprites = new List<int> { 24 };
		private List<int> _currentSprites;

		public ShooterProjectile(float xPosition, float yPosition, int width, int height, float xVelocity, float yVelocity, Color color)
		{
			_xPosition = xPosition;
			_yVelocity = yVelocity;
			_xVelocity = xVelocity;
			_yPosition = yPosition;
			_width = width;
			_height = height;
			_currentSprite = _standardSprites[0];
			_color = color;
			_collisionBox.Height = _height;
			_collisionBox.Width = _width;
		}

		public void UpdatePostion(float x, float y)
		{
			_xPosition += _xVelocity;
			_yPosition -= _yVelocity;

			_collisionBox.Location = new Point((int)_xPosition, (int)_yPosition);

		}

		public Rectangle GetCollisionBox()
		{
			return _collisionBox;
		}

		public void AnimateSprite(GameTime gameTime)
		{
			timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

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
			spriteBatch.Draw(textures[_currentSprite], _collisionBox, _color);
		}

		public float GetY()
		{
			return _yPosition;
		}

	}
}
