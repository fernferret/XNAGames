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

		private readonly ShooterShip _ship;
		private Rectangle _shipRect;
		private readonly List<ShooterEnemy> _enemies;
		private ShooterHerd _herd;
		//projectile list

		public Shooter(SystemDisplay main)
		{
			_main = main;
			_ship = new ShooterShip();
			//_shipRect = new Rectangle(_ship.GetX(), _ship.GetY(), _ship.GetWidth(), _ship.GetHeight());


			#region sample level - delete when xml works

			_herd = new ShooterHerd();

			_enemies = new List<ShooterEnemy>
			           			{
			           				//new ShooterEnemy(1, 1, 1, Color.White),
			           				//new ShooterEnemy(1, 0, 10, Color.Gainsboro),
			           				//new ShooterEnemy(4, 0, 10, Color.Goldenrod)
			           			};

			_herd.AddEnemy(new ShooterEnemy(1, 1, Color.White));
			_herd.AddEnemy(new ShooterEnemy(2, 1, Color.White));
			_herd.AddEnemy(new ShooterEnemy(3, 1, Color.White));
			_herd.AddEnemy(new ShooterEnemy(4, 1, Color.White));
			_herd.AddEnemy(new ShooterEnemy(5, 1, Color.White));
			_herd.AddEnemy(new ShooterEnemy(6, 1, Color.White));
			_herd.AddEnemy(new ShooterEnemy(7, 1, Color.White));
			_herd.AddEnemy(new ShooterEnemy(8, 1, Color.White));
			_herd.AddEnemy(new ShooterEnemy(9, 1, Color.White));
			_herd.AddEnemy(new ShooterEnemy(10, 1, Color.White));

			_herd.AddEnemy(new ShooterEnemy(1, 2, Color.MediumSeaGreen));
			_herd.AddEnemy(new ShooterEnemy(2, 2, Color.MediumSeaGreen));
			_herd.AddEnemy(new ShooterEnemy(3, 2, Color.MediumSeaGreen));
			_herd.AddEnemy(new ShooterEnemy(4, 2, Color.MediumSeaGreen));
			_herd.AddEnemy(new ShooterEnemy(5, 2, Color.MediumSeaGreen));
			_herd.AddEnemy(new ShooterEnemy(6, 2, Color.MediumSeaGreen));
			_herd.AddEnemy(new ShooterEnemy(7, 2, Color.MediumSeaGreen));
			_herd.AddEnemy(new ShooterEnemy(8, 2, Color.MediumSeaGreen));
			_herd.AddEnemy(new ShooterEnemy(9, 2, Color.MediumSeaGreen));
			_herd.AddEnemy(new ShooterEnemy(10, 2, Color.MediumSeaGreen));

			_herd.AddEnemy(new ShooterEnemy(1, 3, Color.Tomato));
			_herd.AddEnemy(new ShooterEnemy(2, 3, Color.Tomato));
			_herd.AddEnemy(new ShooterEnemy(3, 3, Color.Tomato));
			_herd.AddEnemy(new ShooterEnemy(4, 3, Color.Tomato));
			_herd.AddEnemy(new ShooterEnemy(5, 3, Color.Tomato));
			_herd.AddEnemy(new ShooterEnemy(6, 3, Color.Tomato));
			_herd.AddEnemy(new ShooterEnemy(7, 3, Color.Tomato));
			_herd.AddEnemy(new ShooterEnemy(8, 3, Color.Tomato));
			_herd.AddEnemy(new ShooterEnemy(9, 3, Color.Tomato));
			_herd.AddEnemy(new ShooterEnemy(10, 3, Color.Tomato));

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

			_ship.UpdateProjectile();

			if (!_ship.IsDying())
			{
				//collision stuff
				_herd.CollidesWith(_ship);
				_herd.CollidesWithProjectiles(_ship);

				//Herd update stuff
				_herd.UpdatePostion(0, 0);
				_herd.UpdateProjectiles();

				_herd.Shoot();

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

			// draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, SystemMain.Width, SystemMain.Height), Color.White);

			//draw the ship
			_ship.Draw(spriteBatch, fonts, textures);

			//draw the enemies
			_herd.Draw(spriteBatch, fonts, textures);

			spriteBatch.End();

		}

		#endregion
		 

	}
}
