using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using XNASystem.Displays;
using XNASystem.Interfaces;
using XNASystem.ShooterGame;
using XNASystem.Utils;

namespace XNASystem.Shooter
{
	class Shooter : IGame, IScreen
	{
		private readonly SystemDisplay _main;

		private readonly int Width = 800;
		private readonly int Height = 600;

		private readonly ShooterShip _ship;
		private Rectangle _shipRect;
		private readonly List<ShooterEnemy> _enemies;
		private ShooterHerd _herd;
		private ShooterHerd _bossHerd;

		public Shooter(SystemDisplay main)
		{
			_main = main;
			_ship = new ShooterShip();

			#region sample level - delete when xml works

			_herd = new ShooterHerd((float)0.5);
			_bossHerd = new ShooterHerd(1);

			_bossHerd.AddEnemy(new ShooterBossObject(2, 0));
			
			/*_herd.AddEnemy(new ShooterEnemySuper(1, 1));
			_herd.AddEnemy(new ShooterEnemySuper(2, 1));
			_herd.AddEnemy(new ShooterEnemySuper(3, 1));
			_herd.AddEnemy(new ShooterEnemySuper(4, 1));
			_herd.AddEnemy(new ShooterEnemySuper(5, 1));
			_herd.AddEnemy(new ShooterEnemySuper(6, 1));
			_herd.AddEnemy(new ShooterEnemySuper(7, 1));
			_herd.AddEnemy(new ShooterEnemySuper(8, 1));
			_herd.AddEnemy(new ShooterEnemySuper(9, 1));
			_herd.AddEnemy(new ShooterEnemySuper(10, 1));

			_herd.AddEnemy(new ShooterEnemyAdvanced(1, 2));
			_herd.AddEnemy(new ShooterEnemyAdvanced(2, 2));
			_herd.AddEnemy(new ShooterEnemyAdvanced(3, 2));
			_herd.AddEnemy(new ShooterEnemyAdvanced(4, 2));
			_herd.AddEnemy(new ShooterEnemyAdvanced(5, 2));
			_herd.AddEnemy(new ShooterEnemyAdvanced(6, 2));
			_herd.AddEnemy(new ShooterEnemyAdvanced(7, 2));
			_herd.AddEnemy(new ShooterEnemyAdvanced(8, 2));
			_herd.AddEnemy(new ShooterEnemyAdvanced(9, 2));
			_herd.AddEnemy(new ShooterEnemyAdvanced(10, 2));*/

			
			_herd.AddEnemy(new ShooterEnemyBasic(1, 3));
			_herd.AddEnemy(new ShooterEnemyBasic(2, 3));
			_herd.AddEnemy(new ShooterEnemyBasic(3, 3));
			_herd.AddEnemy(new ShooterEnemyBasic(4, 3));
			_herd.AddEnemy(new ShooterEnemyBasic(5, 3));
			_herd.AddEnemy(new ShooterEnemyBasic(6, 3));
			_herd.AddEnemy(new ShooterEnemyBasic(7, 3));
			_herd.AddEnemy(new ShooterEnemyBasic(8, 3));
			_herd.AddEnemy(new ShooterEnemyBasic(9, 3));
			_herd.AddEnemy(new ShooterEnemyBasic(10, 3));

			/*
			_herd.AddEnemy(new ShooterEnemy(1, 3, Color.Tomato,2));
			_herd.AddEnemy(new ShooterEnemy(2, 3, Color.Tomato,2));
			_herd.AddEnemy(new ShooterEnemy(3, 3, Color.Tomato,2));
			_herd.AddEnemy(new ShooterEnemy(4, 3, Color.Tomato,2));
			_herd.AddEnemy(new ShooterEnemy(5, 3, Color.Tomato,2));
			_herd.AddEnemy(new ShooterEnemy(6, 3, Color.Tomato,2));
			_herd.AddEnemy(new ShooterEnemy(7, 3, Color.Tomato,2));
			_herd.AddEnemy(new ShooterEnemy(8, 3, Color.Tomato,2));
			_herd.AddEnemy(new ShooterEnemy(9, 3, Color.Tomato,2));
			_herd.AddEnemy(new ShooterEnemy(10, 3, Color.Tomato,2));

			_herd.AddEnemy(new ShooterEnemy(1, 4, Color.Yellow,1));
			_herd.AddEnemy(new ShooterEnemy(2, 4, Color.Yellow,1));
			_herd.AddEnemy(new ShooterEnemy(3, 4, Color.Yellow,1));
			_herd.AddEnemy(new ShooterEnemy(4, 4, Color.Yellow,1));
			_herd.AddEnemy(new ShooterEnemy(5, 4, Color.Yellow,1));
			_herd.AddEnemy(new ShooterEnemy(6, 4, Color.Yellow,1));
			_herd.AddEnemy(new ShooterEnemy(7, 4, Color.Yellow,1));
			_herd.AddEnemy(new ShooterEnemy(8, 4, Color.Yellow,1));
			_herd.AddEnemy(new ShooterEnemy(9, 4, Color.Yellow,1));
			_herd.AddEnemy(new ShooterEnemy(10, 4, Color.Yellow,1));

			_herd.AddEnemy(new ShooterEnemy(1, 5, Color.Orchid,0));
			_herd.AddEnemy(new ShooterEnemy(2, 5, Color.Orchid,0));
			_herd.AddEnemy(new ShooterEnemy(3, 5, Color.Orchid,0));
			_herd.AddEnemy(new ShooterEnemy(4, 5, Color.Orchid,0));
			_herd.AddEnemy(new ShooterEnemy(5, 5, Color.Orchid,0));
			_herd.AddEnemy(new ShooterEnemy(6, 5, Color.Orchid,0));
			_herd.AddEnemy(new ShooterEnemy(7, 5, Color.Orchid,0));
			_herd.AddEnemy(new ShooterEnemy(8, 5, Color.Orchid,0));
			_herd.AddEnemy(new ShooterEnemy(9, 5, Color.Orchid,0));
			_herd.AddEnemy(new ShooterEnemy(10, 5, Color.Orchid,0));*/

			#endregion
		}

		#region does not use yet


		public void AdvanceLevel()
		{
			throw new NotImplementedException();
		}

		public void ResetGame()
		{
			throw new NotImplementedException();
		}

		public List<PowerUp> GetPowerUps()
		{
			throw new NotImplementedException();
		}

		public void FinishGame()
		{
			throw new NotImplementedException();
		}

		public void StartGame()
		{
			throw new NotImplementedException();
		}

		public int GetLevelScore()
		{
			throw new NotImplementedException();
		}

		public void ResetScore()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region update


		public void Update(InputHandler handler, GameTime gameTime)
		{
			//animation stuff
			_ship.AnimateSprite(gameTime);
			_herd.AnimateSprite(gameTime);
			_bossHerd.AnimateSprite(gameTime);

			//boss stuff
			//_boss.AnimateSprite(gameTime);

			_ship.UpdateProjectile();

			if (!_ship.IsDying())
			{
				//collision stuff
				_herd.CollidesWith(_ship);
				_herd.CollidesWithProjectiles(_ship);
				_bossHerd.CollidesWithProjectiles(_ship);
				_bossHerd.CollidesWith(_ship);

				//Herd update stuff
				_herd.UpdatePostion(0, 0);
				_herd.UpdateProjectiles();
				_bossHerd.UpdatePostion(0, 0);
				_bossHerd.UpdateProjectiles();

				_herd.Shoot();
				_bossHerd.Shoot();

				if (handler.IfLeftPressed())
				{
					_ship.UpdatePostion(-1, 0);
				}
				if (handler.IfRightPressed())
				{
					_ship.UpdatePostion(1, 0);
				}
				if (handler.IfSpacePressed())
				{
					_ship.Shoot();

				}
				else
				{
					_ship.Reload();
				}
			}
		}
	
		#endregion

		
		#region draw


		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			//draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, 800, 600), Color.Black);

			//draw the ship
			_ship.Draw(spriteBatch, fonts, textures);

			//draw the enemies
			_herd.Draw(spriteBatch, fonts, textures);

			//draw the boss
			_bossHerd.Draw(spriteBatch, fonts, textures);

			spriteBatch.End();

		}

		#endregion
		 

	}
}
