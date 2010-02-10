using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using XNASystem.Displays;
using XNASystem.Interfaces;
using XNASystem.Utils;
using XNASystem.Shooter;

namespace XNASystem.ShooterGame
{
	class ShooterHerd : IGameObject
	{
		private float _width;  //Approximately 16 positions across the screen
		private float _height;
		private readonly float _speed;
		private float _yCounter = 0;
		private int _total = 0;
		private int _direction = 1;
		private float _xPosition;
		private float _yPosition;
		private List<ShooterGameObject> _enemies;
		private List<ShooterProjectile> _projectiles;
		Random rndElement = new Random();


		public ShooterHerd(float speed, float xPosition, float yPosition, float width, float height)
		{
			_speed = speed;
			_height = height;
			_width = width;
			_yPosition = yPosition;
			_xPosition = xPosition;
			_enemies = new List<ShooterGameObject>();
			_projectiles = new List<ShooterProjectile>();
		}

		public void AddEnemy(ShooterGameObject e)
		{
			_total++;
			_enemies.Add(e);
		}

		public void KillEnemy(ShooterGameObject e)
		{
			_enemies.Remove(e);
		}

		public void Shoot()
		{
			int rand;
			if (_enemies.Count > 0)
			{
				rand = rndElement.Next(0, 30);
				if (2 == rand)
				{
					_projectiles.Add(_enemies[rndElement.Next(0, _enemies.Count)].GetShot());
				}
			}
		}

		public void UpdatePostion(float x, float y)
		{
			float yInc = 0, xInc = 0, speedModifier = 0;

			speedModifier = 0;// (float)((_total - _enemies.Count));
			xInc = (_speed + speedModifier) * _direction;

			if (_yCounter > 0)
			{
				yInc = _speed;
				_yCounter -= _speed;
			}
			else
			{
				_yCounter = 0;
				yInc = 0;
			}

			foreach(ShooterGameObject e in _enemies)
			{

				if(e.GetX() <= _xPosition)
				{
					_direction = 1;
					_yCounter = e.GetWidth()-2;

				}
				
				if((e.GetX() + e.GetWidth()) == _width)
				{
					_direction = -1;
					_yCounter = e.GetWidth()-2;
				}

				if((e.GetY() + e.GetHeight()) >= _height)
				{
					e.Kill();
				}
			}

			foreach(ShooterGameObject e in _enemies)
			{
				e.UpdatePostion(xInc, yInc);
			}
		}

		public void UpdateProjectiles()
		{
			if (_projectiles.Count > 0)
			{
				foreach (ShooterProjectile p in _projectiles)
				{
					if (p.GetY() >= _height + 7)
					{
						_projectiles.Remove(p);
						break;
					}
					else
					{
						p.UpdatePostion(0, 0);
					}
				}
			}
		}

		public void AnimateSprite(GameTime gameTime)
		{
			foreach(ShooterGameObject e in _enemies)
			{
				e.AnimateSprite(gameTime);

				if (e.IsDead())
				{
					_enemies.Remove(e);
					break;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			foreach (ShooterGameObject e in _enemies)
			{
				e.Draw(spriteBatch, fonts, textures);
			}
			foreach(ShooterProjectile p in _projectiles)
			{
				p.Draw(spriteBatch, fonts, textures);
			}
		}

		public bool CollidesWith(ShooterShip s)
		{
			foreach(ShooterGameObject e in _enemies)
			{
				if(e.GetCollisionBox().Intersects(s.GetCollisionBox()))
				{
					s.Kill();
					e.Damage();
					return true;
				}
				
				if(s.GetShot() != null)
				{
					if (!e.IsDying())
					{
						if (e.GetCollisionBox().Intersects((s.GetShotCollisionBox())))
						{
							e.Damage();
							s.KillProjectile();
							return true;
						}
					}
				}
			}
			return false;
		}

		public void CollidesWithProjectiles(ShooterShip s)
		{
			foreach(ShooterProjectile p in _projectiles)
			{
				if(p.GetCollisionBox().Intersects(s.GetCollisionBox()))
				{
					s.Kill();
					_projectiles.RemoveRange(0, _projectiles.Count);
					break;
				}
			}
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
