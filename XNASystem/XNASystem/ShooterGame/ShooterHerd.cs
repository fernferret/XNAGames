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
					var enemy = _enemies[rndElement.Next(0, _enemies.Count)];
					_projectiles.Add(enemy.GetShot());
					rand = rndElement.Next(0, 30);
					//if (2 == rand)
					//{
						//SystemMain.SoundShootEnemyInstance.Volume = .70f;
						//SystemMain.SoundShootEnemyInstance.Play();
					SystemMain.SoundShootEnemy.Play(.7f, 0.0f, GetEnemySoundPos(enemy));
					//}
				}
			}
		}
		private float GetEnemySoundPos(ShooterGameObject e)
		{
			var x = e.GetX();
			var w = e.GetWidth();
			var halfwidth = ((float)SystemMain.Width/2);
			var finalfloat = 0.0f;
			x += ((float)w/2);
			var zeropoint = SystemMain.Width/2;
			if(x>((float)w/2))
			{
				x -= halfwidth;
				finalfloat = (x/halfwidth);
			}
			else
			{
				finalfloat = (x / halfwidth);
				finalfloat = Math.Abs(1 - finalfloat)*(-1);
			}
			return finalfloat;
		}
		public void UpdatePostion(float x, float y)
		{
			float yInc = 0, xInc = 0;

			xInc = (_speed) * _direction;

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
				
				if((e.GetX() + e.GetWidth()) >= _width)
				{
					_direction = -1;
					_yCounter = e.GetWidth()-2;
				}

				if(e.GetY() >= _height && !e.IsDying())  //modify to set yInc to negative or something similar
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
					if (p.GetY() >= _height + 7 || p.GetY() < 0)
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

		public void Draw()
		{
			foreach (ShooterGameObject e in _enemies)
			{
				e.Draw();
			}
			foreach(ShooterProjectile p in _projectiles)
			{
				p.Draw();
			}
		}

		public int CollidesWith(ShooterShip s)
		{
			int score = 0;
			foreach(ShooterGameObject e in _enemies)
			{
				if(e.GetCollisionBox().Intersects(s.GetCollisionBox()) && !s.IsDying())
				{
					s.Kill();
					score += e.Damage();
				}
				
				if(s.GetShot() != null)
				{
					if (!e.IsDying())
					{
						if (e.GetCollisionBox().Intersects((s.GetShotCollisionBox())))
						{
							score += e.Damage();
							s.KillProjectile();
						}
					}
				}
			}
			return score;
		}

		public void CollidesWithProjectiles(ShooterShip s)
		{
			foreach(ShooterProjectile p in _projectiles)
			{
				if(p.GetCollisionBox().Intersects(s.GetCollisionBox()) && !s.IsDying())
				{
					s.Kill();
					_projectiles.RemoveRange(0, _projectiles.Count);
					break;
				}
			}
		}

		public bool Empty()
		{
			return (_enemies.Count == 0);
		}

		public float GetX()
		{
			return _xPosition;
		}

		public float GetY()
		{
			return _yPosition;
		}
	}
}
