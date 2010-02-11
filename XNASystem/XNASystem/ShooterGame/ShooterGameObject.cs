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
	abstract class ShooterGameObject : IGameObject
	{
		#region variables

		protected float _xPosition;
		protected float _yPosition;
		private float _speed = 2;
		private int _hitPoints;
		private int _score;
		private readonly int _xGun;
		private readonly int _yGun;
		private int _shotSpeed;
		private readonly Color _color;
		protected Rectangle _collisionBox;
		private readonly int _width;
		private readonly int _height;
		private readonly int _shotWidth;
		private readonly int _shotHeight;
		private bool _isDying = false;
		private bool _isHurt = false;

		//animation stuff
		private float timer = 0f;
		private float interval = 1000f / 7f;
		protected readonly List<int> _standardSprites;
		private readonly List<int> _deadSprites;
		private readonly List<int> _painSprites;
		private readonly List<int> _blankSprite;
		protected readonly Queue<int> _spriteQueue;

		#endregion

		protected ShooterGameObject(float xPosition, float yPosition, int width, int height, int xGun, int yGun, int shotSpeed, int shotWidth, int shotHeight, Color color, int hitPoints, int score, List<int> standardSprites, List<int> painSprites, List<int> deadSprites)
		{
			_height = height;
			_width = width;
			_xPosition = (xPosition * _width) + 2;
			_yPosition = (yPosition * _height) + 2;
			_xGun = xGun;  //gun position relative to _xPosition
			_yGun = yGun;  //gun position relative to _yPosition
			_shotWidth = shotWidth;
			_shotHeight = shotHeight;
			_shotSpeed = shotSpeed;
			_color = color;
			_hitPoints = hitPoints;
			_score = score;
			_standardSprites = standardSprites;
			_painSprites = painSprites;
			_deadSprites = deadSprites;
			_blankSprite = new List<int>{23};
			_spriteQueue = new Queue<int>();
			AddSpritesToDraw(_standardSprites);
			_collisionBox = new Rectangle((int)_xPosition, (int)_yPosition, _width - 5, _height - 5);
			
		}

		public virtual void UpdatePostion(float x, float y)
		{
			_xPosition += x;
			_yPosition += y;

			_collisionBox.Location = new Point((int)_xPosition, (int)_yPosition);

			if(_spriteQueue.Count <= 1)
			{
				AddSpritesToDraw(_standardSprites);
			}
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			if (!IsDead())
			{
				//why do I need this?
				if(_spriteQueue.Count <= 0)
				{
					AddSpritesToDraw(_standardSprites);
				}

				spriteBatch.Draw(textures[_spriteQueue.Peek()], new Vector2(_xPosition, _yPosition), _color);
			}
		}

		public void AnimateSprite(GameTime gameTime)
		{
			
				timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;

				if (timer > interval)
				{
					if(_spriteQueue.Count <= 0)
					{
						AddSpritesToDraw(_blankSprite);
					}
					_spriteQueue.Dequeue();
					timer = 0f;
				}
		}

		public void AddSpritesToDraw(List<int> l)
		{
			int i;
			for(i=0; i<l.Count; i++)
			{
				_spriteQueue.Enqueue(l[i]);
			}
		}

		public void RemoveAllSpritesToDraw()
		{
			int i;
			for(i=0; i<_spriteQueue.Count; i++)
			{
				_spriteQueue.Dequeue();
			}
		}

		public Rectangle GetCollisionBox()
		{
			return _collisionBox;
		}

		public int Damage()
		{
			_hitPoints--;

			if (_hitPoints <= 0)
			{
				if(IsDead())
				{
					AddSpritesToDraw(_blankSprite);
				}
				else
				{
					Kill();
					return _score;
				}
			}
			else
			{
				Hurt();
			}
			return 0;
		}

		private void Hurt()
		{
			RemoveAllSpritesToDraw();
			AddSpritesToDraw(_painSprites);
		}

		public void Kill()
		{
			RemoveAllSpritesToDraw();
			AddSpritesToDraw(_deadSprites);
			_isDying = true;
		}

		public bool IsDying()
		{
			return _isDying;
		}

		public bool IsDead()
		{
			return(IsDying() && !_spriteQueue.Contains(_deadSprites.Last()));
		}

		public ShooterProjectile GetShot()
		{
			return new ShooterProjectile(_xPosition + _xGun, _yPosition + _yGun, _shotWidth, _shotHeight, 0, -1*_shotSpeed, _color);
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
			return _width;
		}

		public int GetHeight()
		{
			return _height;
		}

	}
}
