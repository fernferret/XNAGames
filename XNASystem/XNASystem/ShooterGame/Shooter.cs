using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
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

		private readonly int _width = SystemMain.Width;
		private readonly int _height = SystemMain.Height;
		private int _currentLevel = 0;
		private int _lives = 3;
		private int _score = 0;

		private ShooterShip _ship;
		private Rectangle _shipRect;
		//private readonly List<ShooterEnemy> _enemies;
		private ShooterHerd _herd;
		private ShooterHerd _bossHerd;
		private List<ShooterLevel> _levels;
		private List<ShooterHerd> _herdList;

		public Shooter(SystemDisplay main, int level)
		{
			_main = main;
			_ship = new ShooterShip(0, _width);

			_levels = new List<ShooterLevel>();

			_herdList = new List<ShooterHerd>();

			//remove later
			StartGame(level);

		}

		#region does not use yet


		public void AdvanceLevel()
		{
			if (_currentLevel < _levels.Count)
			{
				_currentLevel++;
				_herdList = _levels[_currentLevel].GetHerdList();
				ResetGame();
			}
			else
			{
				FinishGame();
			}
		}

		public void ResetGame()
		{
			_ship = new ShooterShip(0, _width);

		}

		public List<PowerUp> GetPowerUps()
		{
			throw new NotImplementedException();
		}

		public void FinishGame()
		{
			_main.EndGame(new Score("Player",ActivityType.Game,_score,"Shooter"));
		}

		public void StartGame(int level)
		{
			#region generate sample levels

			#region 1st level
			ShooterHerd enemies1 = new ShooterHerd((float)0.5, 0, 0, _width, _height);
			ShooterLevel level1 = new ShooterLevel();

			enemies1.AddEnemy(new ShooterEnemyAdvanced(3, 1));
			enemies1.AddEnemy(new ShooterEnemyAdvanced(8, 1));

			enemies1.AddEnemy(new ShooterEnemyBasic(1, 2));
			enemies1.AddEnemy(new ShooterEnemyBasic(2, 2));
			enemies1.AddEnemy(new ShooterEnemyBasic(3, 2));
			enemies1.AddEnemy(new ShooterEnemyBasic(4, 2));
			enemies1.AddEnemy(new ShooterEnemyBasic(5, 2));
			enemies1.AddEnemy(new ShooterEnemyBasic(6, 2));
			enemies1.AddEnemy(new ShooterEnemyBasic(7, 2));
			enemies1.AddEnemy(new ShooterEnemyBasic(8, 2));
			enemies1.AddEnemy(new ShooterEnemyBasic(9, 2));
			enemies1.AddEnemy(new ShooterEnemyBasic(10, 2));

			enemies1.AddEnemy(new ShooterEnemyBasic(1, 3));
			enemies1.AddEnemy(new ShooterEnemyBasic(2, 3));
			enemies1.AddEnemy(new ShooterEnemyBasic(3, 3));
			enemies1.AddEnemy(new ShooterEnemyBasic(4, 3));
			enemies1.AddEnemy(new ShooterEnemyBasic(5, 3));
			enemies1.AddEnemy(new ShooterEnemyBasic(6, 3));
			enemies1.AddEnemy(new ShooterEnemyBasic(7, 3));
			enemies1.AddEnemy(new ShooterEnemyBasic(8, 3));
			enemies1.AddEnemy(new ShooterEnemyBasic(9, 3));
			enemies1.AddEnemy(new ShooterEnemyBasic(10, 3));

			level1.AddHerd(enemies1);

			_levels.Add(level1);
			#endregion


			#region 2nd level
			ShooterHerd enemies2 = new ShooterHerd((float)0.5, 0, 0, _width, _height);
			ShooterLevel level2 = new ShooterLevel();

			enemies2.AddEnemy(new ShooterEnemyAdvanced(1, 1));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(2, 1));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(3, 1));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(4, 1));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(5, 1));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(6, 1));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(7, 1));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(8, 1));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(9, 1));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(10, 1));

			enemies2.AddEnemy(new ShooterEnemyBasic(1, 2));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(2, 2));
			enemies2.AddEnemy(new ShooterEnemyBasic(3, 2));
			enemies2.AddEnemy(new ShooterEnemyBasic(4, 2));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(5, 2));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(6, 2));
			enemies2.AddEnemy(new ShooterEnemyBasic(7, 2));
			enemies2.AddEnemy(new ShooterEnemyBasic(8, 2));
			enemies2.AddEnemy(new ShooterEnemyAdvanced(9, 2));
			enemies2.AddEnemy(new ShooterEnemyBasic(10, 2));

			enemies2.AddEnemy(new ShooterEnemyBasic(1, 3));
			enemies2.AddEnemy(new ShooterEnemyBasic(2, 3));
			enemies2.AddEnemy(new ShooterEnemyBasic(3, 3));
			enemies2.AddEnemy(new ShooterEnemyBasic(4, 3));
			enemies2.AddEnemy(new ShooterEnemyBasic(5, 3));
			enemies2.AddEnemy(new ShooterEnemyBasic(6, 3));
			enemies2.AddEnemy(new ShooterEnemyBasic(7, 3));
			enemies2.AddEnemy(new ShooterEnemyBasic(8, 3));
			enemies2.AddEnemy(new ShooterEnemyBasic(9, 3));
			enemies2.AddEnemy(new ShooterEnemyBasic(10, 3));

			level2.AddHerd(enemies2);

			_levels.Add(level2);
			#endregion

			#region 3rd level
			ShooterHerd enemies3 = new ShooterHerd((float)1, 0, 0, _width, _height);
			ShooterHerd boss3 = new ShooterHerd((float)1.5, 0, 0, _width, _height);
			ShooterLevel level3 = new ShooterLevel();

			enemies3.AddEnemy(new ShooterEnemyBasic(1, 1));
			enemies3.AddEnemy(new ShooterEnemyBasic(2, 1));
			enemies3.AddEnemy(new ShooterEnemyBasic(3, 1));
			enemies3.AddEnemy(new ShooterEnemyBasic(4, 1));
			enemies3.AddEnemy(new ShooterEnemyBasic(5, 1));
			enemies3.AddEnemy(new ShooterEnemyBasic(6, 1));
			enemies3.AddEnemy(new ShooterEnemyBasic(7, 1));
			enemies3.AddEnemy(new ShooterEnemyBasic(8, 1));
			enemies3.AddEnemy(new ShooterEnemyBasic(9, 1));
			enemies3.AddEnemy(new ShooterEnemyBasic(10, 1));


			boss3.AddEnemy(new ShooterBossObject(2, 0));

			level3.AddHerd(enemies3);
			level3.AddHerd(boss3);

			_levels.Add(level3);
			#endregion

			#region 4th level
			ShooterHerd enemies4 = new ShooterHerd((float)1, 0, 0, _width, _height);
			ShooterLevel level4 = new ShooterLevel();

			enemies4.AddEnemy(new ShooterEnemyAdvanced(1, 1));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(2, 1));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(3, 1));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(4, 1));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(5, 1));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(6, 1));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(7, 1));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(8, 1));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(9, 1));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(10, 1));

			enemies4.AddEnemy(new ShooterEnemyAdvanced(1, 2));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(2, 2));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(3, 2));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(4, 2));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(5, 2));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(6, 2));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(7, 2));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(8, 2));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(9, 2));
			enemies4.AddEnemy(new ShooterEnemyAdvanced(10, 2));

			enemies4.AddEnemy(new ShooterEnemyBasic(1, 3));
			enemies4.AddEnemy(new ShooterEnemyBasic(2, 3));
			enemies4.AddEnemy(new ShooterEnemyBasic(3, 3));
			enemies4.AddEnemy(new ShooterEnemyBasic(4, 3));
			enemies4.AddEnemy(new ShooterEnemyBasic(5, 3));
			enemies4.AddEnemy(new ShooterEnemyBasic(6, 3));
			enemies4.AddEnemy(new ShooterEnemyBasic(7, 3));
			enemies4.AddEnemy(new ShooterEnemyBasic(8, 3));
			enemies4.AddEnemy(new ShooterEnemyBasic(9, 3));
			enemies4.AddEnemy(new ShooterEnemyBasic(10, 3));

			level4.AddHerd(enemies4);

			_levels.Add(level4);
			#endregion

			#region 5th level
			ShooterHerd enemies5 = new ShooterHerd((float)1.5, 0, 0, _width, _height);
			ShooterLevel level5 = new ShooterLevel();


			enemies5.AddEnemy(new ShooterEnemyBasic(1, 1));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(2, 1));
			enemies5.AddEnemy(new ShooterEnemyBasic(3, 1));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(4, 1));
			enemies5.AddEnemy(new ShooterEnemyBasic(5, 1));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(6, 1));
			enemies5.AddEnemy(new ShooterEnemyBasic(7, 1));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(8, 1));
			enemies5.AddEnemy(new ShooterEnemyBasic(9, 1));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(10, 1));

			enemies5.AddEnemy(new ShooterEnemyAdvanced(1, 2));
			enemies5.AddEnemy(new ShooterEnemyBasic(2, 2));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(3, 2));
			enemies5.AddEnemy(new ShooterEnemyBasic(4, 2));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(5, 2));
			enemies5.AddEnemy(new ShooterEnemyBasic(6, 2));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(7, 2));
			enemies5.AddEnemy(new ShooterEnemyBasic(8, 2));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(9, 2));
			enemies5.AddEnemy(new ShooterEnemyBasic(10, 2));

			enemies5.AddEnemy(new ShooterEnemyBasic(1, 3));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(2, 3));
			enemies5.AddEnemy(new ShooterEnemyBasic(3, 3));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(4, 3));
			enemies5.AddEnemy(new ShooterEnemyBasic(5, 3));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(6, 3));
			enemies5.AddEnemy(new ShooterEnemyBasic(7, 3));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(8, 3));
			enemies5.AddEnemy(new ShooterEnemyBasic(9, 3));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(10, 3));

			enemies5.AddEnemy(new ShooterEnemyAdvanced(1, 4));
			enemies5.AddEnemy(new ShooterEnemyBasic(2, 4));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(3, 4));
			enemies5.AddEnemy(new ShooterEnemyBasic(4, 4));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(5, 4));
			enemies5.AddEnemy(new ShooterEnemyBasic(6, 4));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(7, 4));
			enemies5.AddEnemy(new ShooterEnemyBasic(8, 4));
			enemies5.AddEnemy(new ShooterEnemyAdvanced(9, 4));
			enemies5.AddEnemy(new ShooterEnemyBasic(10, 4));

			level5.AddHerd(enemies5);

			_levels.Add(level5);
			#endregion

			#region 6th level
			ShooterHerd enemies6 = new ShooterHerd((float)1, 0, 0, _width, _height);
			ShooterHerd enemies6ai = new ShooterHerd((float)2.5, 0, 0, _width / 2, _height);
			ShooterHerd enemies6aii = new ShooterHerd((float)2.5, _width / 2, 0, _width, _height);
			ShooterHerd boss6 = new ShooterHerd((float)2, 0, 0, _width, _height);
			ShooterLevel level6 = new ShooterLevel();

			enemies6.AddEnemy(new ShooterEnemyAdvanced(1, 1));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(2, 1));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(3, 1));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(4, 1));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(5, 1));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(6, 1));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(7, 1));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(8, 1));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(9, 1));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(10, 1));

			enemies6.AddEnemy(new ShooterEnemyAdvanced(1, 2));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(2, 2));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(3, 2));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(4, 2));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(5, 2));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(6, 2));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(7, 2));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(8, 2));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(9, 2));
			enemies6.AddEnemy(new ShooterEnemyAdvanced(10, 2));

			enemies6ai.AddEnemy(new ShooterEnemyAdvanced(3, 4));
			enemies6ai.AddEnemy(new ShooterEnemyBasic(3, 3));
			enemies6ai.AddEnemy(new ShooterEnemyBasic(2, 4));
			enemies6ai.AddEnemy(new ShooterEnemyBasic(4, 4));
			enemies6ai.AddEnemy(new ShooterEnemyBasic(3, 5));

			enemies6aii.AddEnemy(new ShooterEnemyAdvanced(8, 4));
			enemies6aii.AddEnemy(new ShooterEnemyBasic(8, 3));
			enemies6aii.AddEnemy(new ShooterEnemyBasic(7, 4));
			enemies6aii.AddEnemy(new ShooterEnemyBasic(9, 4));
			enemies6aii.AddEnemy(new ShooterEnemyBasic(8, 5));

			boss6.AddEnemy(new ShooterBossObject(2, 0));

			level6.AddHerd(enemies6);
			level6.AddHerd(enemies6ai);
			level6.AddHerd(enemies6aii);

			level6.AddHerd(boss6);

			_levels.Add(level6);
			#endregion

			#region 7th level
			ShooterHerd enemies7 = new ShooterHerd((float)1.5, 0, 0, _width, _height);
			ShooterHerd enemies7a = new ShooterHerd((float)3, 0, 0, _width, _height);
			ShooterLevel level7 = new ShooterLevel();


			enemies7.AddEnemy(new ShooterEnemyBasic(1, 1));
			enemies7.AddEnemy(new ShooterEnemyAdvanced(2, 1));
			enemies7.AddEnemy(new ShooterEnemyBasic(3, 1));
			enemies7.AddEnemy(new ShooterEnemyAdvanced(4, 1));
			enemies7.AddEnemy(new ShooterEnemyBasic(5, 1));
			enemies7.AddEnemy(new ShooterEnemyAdvanced(6, 1));
			enemies7.AddEnemy(new ShooterEnemyBasic(7, 1));
			enemies7.AddEnemy(new ShooterEnemyAdvanced(8, 1));
			enemies7.AddEnemy(new ShooterEnemyBasic(9, 1));
			enemies7.AddEnemy(new ShooterEnemyAdvanced(10, 1));

			enemies7.AddEnemy(new ShooterEnemyAdvanced(1, 2));
			enemies7.AddEnemy(new ShooterEnemyBasic(2, 2));
			enemies7.AddEnemy(new ShooterEnemyAdvanced(3, 2));
			enemies7.AddEnemy(new ShooterEnemyBasic(4, 2));
			enemies7.AddEnemy(new ShooterEnemyAdvanced(5, 2));
			enemies7.AddEnemy(new ShooterEnemyBasic(6, 2));
			enemies7.AddEnemy(new ShooterEnemyAdvanced(7, 2));
			enemies7.AddEnemy(new ShooterEnemyBasic(8, 2));
			enemies7.AddEnemy(new ShooterEnemyAdvanced(9, 2));
			enemies7.AddEnemy(new ShooterEnemyBasic(10, 2));

			enemies7a.AddEnemy(new ShooterEnemySuper(3, -10));
			enemies7a.AddEnemy(new ShooterEnemyBasic(2, -10));
			enemies7a.AddEnemy(new ShooterEnemyBasic(4, -10));
			enemies7a.AddEnemy(new ShooterEnemyBasic(3, -9));

			enemies7a.AddEnemy(new ShooterEnemySuper(8, -10));
			enemies7a.AddEnemy(new ShooterEnemyBasic(7, -10));
			enemies7a.AddEnemy(new ShooterEnemyBasic(9, -10));
			enemies7a.AddEnemy(new ShooterEnemyBasic(8, -9));

			level7.AddHerd(enemies7);
			level7.AddHerd(enemies7a);

			_levels.Add(level7);
			#endregion


			_herdList = _levels[0].GetHerdList();

			#endregion

			_herdList = _levels[level].GetHerdList();
		}

		public int GetLevelScore()
		{
			return _score;
		}

		public void ResetScore()
		{
			_score = 0;
		}

		#endregion

		public bool GameWon()
		{
			foreach(ShooterHerd l in _herdList)
			{
				if(!l.Empty())
				{
					return false;
				}
			}
			return true;
		}

		public bool GameLost()
		{
			if(_ship.IsDead())
			{
				_lives--;
				_ship = new ShooterShip(0, _width);
			}
			
			if(_lives < 0)
			{
				return true;
			}
			return false;
		}

		#region update


		public void Update(InputHandler handler, GameTime gameTime)
		{
			if (SystemMain.SoundQuizBgInstance.State == SoundState.Playing)
			{
				SystemMain.SoundQuizBgInstance.Stop();
			}
			var soundplaying = false;
			foreach (var sound in SystemMain.SoundsBackgroundInstance)
			{
				if(sound.State == SoundState.Playing)
				{
					soundplaying = true;
				}
			}
			if(!soundplaying)
			{
				Random r = new Random();
				var rn = r.Next(0, SystemMain.SoundsBackgroundInstance.Count);
				//SystemMain.SoundsBackgroundInstance[rn].IsLooped = false;
				SystemMain.SoundsBackgroundInstance[rn].Volume = .30f;
				//SystemMain.SoundsBackgroundInstance[rn].Pitch = -1.0f;
				SystemMain.SoundsBackgroundInstance[rn].Play();

			}
			if(GameWon() || GameLost())
			{
				//AdvanceLevel();//remove later
				//_lives += 3;//remove later
				FinishGame();
			}

			//animation stuff
			_ship.AnimateSprite(gameTime);

			foreach(ShooterHerd h in _herdList)
			{
				h.AnimateSprite(gameTime);
			}

			_ship.UpdateProjectile();

				//collision stuff
				foreach (ShooterHerd h in _herdList)
				{
					//increment score
					_score += h.CollidesWith(_ship);

					h.CollidesWithProjectiles(_ship);

					h.UpdatePostion(0, 0);
					h.UpdateProjectiles();

					h.Shoot();
				}

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
					//SystemMain.SoundBackgroundInstance.IsLooped = true;
					

				}
				else
				{
					_ship.Reload();
				}
			
		}
	
		#endregion

		
		#region draw


		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			//draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, _width, _height), Color.Black);

			//draw text
			spriteBatch.DrawString(fonts[0], "Lives: " + _lives, new Vector2(_width - 100, 0), Color.White);
			spriteBatch.DrawString(fonts[0], "Score: " + _score, new Vector2(0, 0), Color.White);

			//draw the ship
			_ship.Draw(spriteBatch, fonts, textures);

			foreach (ShooterHerd h in _herdList)
			{
				h.Draw(spriteBatch, fonts, textures);
			}

			spriteBatch.End();

		}

		#endregion
		 

	}
}
