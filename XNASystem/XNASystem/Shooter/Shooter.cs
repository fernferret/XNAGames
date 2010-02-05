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

namespace XNASystem.Shooter
{
	class Shooter : IGame, IScreen
	{
		private readonly SystemDisplay _main;

		private readonly ShooterShip _ship;
		private Rectangle _shipRect;
		private readonly List<ShooterEnemy> _enemies;
		//projectile list

		public Shooter(SystemDisplay main)
		{
			_main = main;
			_ship = new ShooterShip();
			//_shipRect = new Rectangle(_ship.GetX(), _ship.GetY(), _ship.GetWidth(), _ship.GetHeight());

			#region sample level - delete when xml works

			_enemies = new List<ShooterEnemy>
			           			{
			           				new ShooterEnemy(0, 0, 10, Color.White),
			           				//new ShooterEnemy(1, 0, 10, Color.Gainsboro),
			           				//new ShooterEnemy(4, 0, 10, Color.Goldenrod)
			           			};
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

			foreach(ShooterEnemy e in _enemies)
			{
				/*if(e.IsDead())
				{
					_enemies.Remove(e);
				}*/
				e.AnimateSprite(gameTime);

				e.SetTarget(e.GetX() + 3, e.GetY() + 2);

				e.UpdatePostion(0, 0);
			}

			if(handler.IfLeftPressed())
			{
				_ship.UpdatePostion(-1, 0);
			}
			if(handler.IfRightPressed())
			{
				_ship.UpdatePostion(1, 0);
			}
			if(handler.IfSpacePressed())
			{
				_ship.Shoot();
				if (_enemies.Count > 0)
				{
					_enemies[0].Kill(); //remove later, this is just for testing!!!!!!!!!!!!!!!!!
				}
				
			}
			else
			{
				_ship.Reload();
			}

			//remove later as well, this is just for testing !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			if(_enemies.Count > 0)
			{
				if(_enemies[0].IsDead())
				{
					_enemies.Remove(_enemies[0]);
				}
			}

			/*
			// TODO: Need to update to use the new handler class!
			var padState = handler.GetPadState();
			var keyState = handler.GetKeyState();
			int x;
			for (x = 0; x < 10; x++)
			{
				#region paddle with wall collision

				// update the paddles position by adding or subtracing according to the thumb stick
				_objectRect = new Rectangle((int)_paddle.GetX(), (int)_paddle.GetY(), 199, 17);

				if (_paddle.GetX() == 10)
				{
					if (padState.ThumbSticks.Left.X > 0)
					{
						_paddle.UpdatePostion(padState.ThumbSticks.Left.X, 0);
					}
					if (keyState.IsKeyDown(Keys.Right))
					{
						_paddle.UpdatePostion(1, 0);
					}

				}
				else if (_paddle.GetX() == 790 - 199)
				{
					if (padState.ThumbSticks.Left.X < 0)
					{
						_paddle.UpdatePostion(padState.ThumbSticks.Left.X, 0);
					}
					if (keyState.IsKeyDown(Keys.Left))
					{
						_paddle.UpdatePostion(-1, 0);
					}
				}
				else if (_objectRect.Intersects(new Rectangle((int)_leftWall.GetX(), (int)_leftWall.GetY(), 10, 600)))
				{
					_paddle.SetX(10);
				}

				else if (_objectRect.Intersects(new Rectangle((int)_rightWall.GetX(), (int)_rightWall.GetY(), 10, 600)))
				{
					_paddle.SetX(790 - 199);
				}

				else
				{
					_paddle.UpdatePostion(padState.ThumbSticks.Left.X, 0);
				}

				#endregion

				#region  ball collision testing and movement

				// check for collisions between the ball and any other objects
				int i;
				for (i = 0; i < _ballList.Count; i++)
				{
					if (_ballList[i].IsAlive())
					{
						// create a rectangle around the balls current position
						_ballRect = new Rectangle((int)_ballList[i].GetX(), (int)_ballList[i].GetY(), 15, 15);

						#region paddle

						// create a rectangle around the paddle and check for intersections
						_objectRect = new Rectangle((int)_paddle.GetX(), (int)_paddle.GetY(), 199, 17);
						if (_ballRect.Intersects(_objectRect))
						{
							//simply switch the y velocity 
							_ballList[i].SwitchY();
							_ballList[i].IncrementX(padState.ThumbSticks.Left.X);
						}

						#endregion

						#region walls and ceiling

						// create a rectangle around the lef twall and check for intersections
						_objectRect = new Rectangle((int)_leftWall.GetX(), (int)_leftWall.GetY(), 10, 600);
						if (_ballRect.Intersects(_objectRect))
						{
							//simply change the x velocity
							_ballList[i].SwitchX();
						}

						// create a rectangle aroun the right wall and check for intersections
						_objectRect = new Rectangle((int)_rightWall.GetX(), (int)_rightWall.GetY(), 10, 600);
						if (_ballRect.Intersects(_objectRect))
						{
							//simple change the x velocty
							_ballList[i].SwitchX();
						}

						//create a rectangle around the ceiling and check for intersections
						_objectRect = new Rectangle((int)_ceiling.GetX(), (int)_ceiling.GetY(), 800, 10);
						if (_ballRect.Intersects(_objectRect))
						{
							//simpley change the y velocity
							_ballList[i].SwitchY();
						}

						#endregion

						#region blocks

						int j, k;
						for (j = 0; j < 10; j++)
						{
							for (k = 0; k < 10; k++)
							{
								//make a rectangle aroundt he current block
								_objectRect = new Rectangle((int)_blockList[j][k].GetX(), (int)_blockList[j][k].GetY(), 78, 36);

								if (_ballRect.Intersects(_objectRect)) //if a ball intersects with the block...
								{
									if (_blockList[j][k].GetType() != Blocktype.Dead) //...and the block is not dead...
									{
										switch (_blockList[j][k].GetSide(_ballRect)) //..than find out which side it hit and act accordingly.
										{
											case 0:
												_ballList[i].SwitchY();
												break;
											case 1:
												_ballList[i].SwitchX();
												break;
											case 2:
												_ballList[i].SwitchY();
												break;
											case 3:
												_ballList[i].SwitchX();
												break;
											default:
												_score = (_blockList[j][k].GetSide(_ballRect));
												break;
										}
									}

									// change the block type with this method.
									DecrementType(_blockList[j][k]);
								}
							}
						}

						#endregion

						#region deadspace

						_objectRect = new Rectangle(0, 615, 800, 1);
						if (_objectRect.Intersects(_ballRect))
						{
							_lives--;
							_mainBallIsAlive = false;
							_ballList[i].Kill();
							if (_lives == 0)
							{
								_main.EndGame(new Score("Name", ActivityType.Game, _score, "Breakout"));
							}
						}

						#endregion

						_ballList[i].UpdatePostion(_ballList[i].GetVx(), _ballList[i].GetVy());
					}
				}
				#endregion

				if (handler.IfEnterPressed())
				{
					if (_lives > 0 && !_mainBallIsAlive)
					{
						_ballList.Add(new BreakOutBall(400, 530, (float)-.5, (float)-.5));
					}
					_a = 1;
				}
			}*/
		}
	
		#endregion

		
		#region draw


		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			//draw the ship
			_ship.Draw(spriteBatch, fonts, textures);

			//draw the enemies
			foreach(ShooterEnemy e in _enemies)
			{
				e.Draw(spriteBatch, fonts, textures);
			}
			spriteBatch.End();

		}

		#endregion
		 

	}
}
