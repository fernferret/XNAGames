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
		private const int Width = 755;  //Approximately 16 positions across the screen
		private const int Height = 600;
		private float _speed = 1;
		private float _yCounter = 0;
		private int _total = 0;
		private int _direction = 1;
		private List<ShooterEnemy> _enemies;
		private List<ShooterProjectile> _projectiles;
		Random rndElement = new Random();


		public ShooterHerd()
		{
			_enemies = new List<ShooterEnemy>();
			_projectiles = new List<ShooterProjectile>();
		}

		public void AddEnemy(ShooterEnemy e)
		{
			_total++;
			_enemies.Add(e);
		}

		public void KillEnemy(ShooterEnemy e)
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

			foreach(ShooterEnemy e in _enemies)
			{

				if(e.GetX() <= 0)
				{
					_direction = 1;
					_yCounter = e.GetWidth();

				}
				
				if((e.GetX()) == Width)
				{
					_direction = -1;
					_yCounter = 100;
				}
			}

			foreach(ShooterEnemy e in _enemies)
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
					if (p.GetY() >= Height + 7)
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
			foreach(ShooterEnemy e in _enemies)
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
			foreach (ShooterEnemy e in _enemies)
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
			foreach(ShooterEnemy e in _enemies)
			{
				if(e.CollidesWith(s.GetCollisionBox()))
				{
					s.Kill();
					e.Kill();
					return true;
				}
				
				if(s.GetShot() != null)
				{
					if (e.CollidesWith(s.GetShotCollisionBox()))
					{
						e.Kill();
						s.KillProjectile();
						return true;
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
