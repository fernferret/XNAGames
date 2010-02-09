using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;
using XNASystem.ShooterGame;
namespace XNASystem.ShooterGame
{
	class ShooterBoss : IGameObject
	{
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

		//animation stuff
		private float timer = 0f;
		private float interval = 1000f / 7f;
		private int frameCount;
		private int currentFrame = 0;
		private int _currentSprite;
		private List<int> _standardSprites = new List<int> { 26, 27 };
		private List<int> _deadSprites = new List<int> { 15, 16, 17, 18, 19, 20, 21, 22 };
		private List<int> _painSprites = new List<int>( 28 );
		private List<int> _currentSprites;

		public ShooterBoss(float xPosition, float yPosition, Color color, int hitPoints)
		{
			_xPosition = xPosition;
			_yPosition = yPosition;
			_color = color;
			_hitPoints = hitPoints;
			_currentSprites = _standardSprites;
			_currentSprite = _currentSprites[0];
			frameCount = _currentSprites.Count;
			_collisionBox = new Rectangle((int)_xPosition, (int)_yPosition, 45, 45);
		}

		public void UpdatePostion(float x, float y)
		{
			throw new NotImplementedException();
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[_currentSprite], new Vector2(_xPosition, _yPosition), _color);
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
			if (!IsDead())
			{

				_currentSprite = _currentSprites[currentFrame];
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


		public float GetX()
		{
			throw new NotImplementedException();
		}

		public float GetY()
		{
			throw new NotImplementedException();
		}
	}
}
