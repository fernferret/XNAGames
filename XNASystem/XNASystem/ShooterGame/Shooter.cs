using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using XNASystem.Displays;
using XNASystem.Interfaces;

namespace XNASystem.Shooter
{
	class Shooter : IGame
	{
		private readonly SystemDisplay _main;

		private readonly ShooterShip _ship;
		private Rectangle _shipRect;
		private readonly List<List<ShooterEnemy>> _enemies;
		//projectile list

		public Shooter(SystemDisplay main)
		{
			_main = main;
			_ship = new ShooterShip();
			//_shipRect = new Rectangle(_ship.GetX(), _ship.GetY(), _ship.GetWidth(), _ship.GetHeight());

			#region sample level - delete when xml works

			_enemies = new List<List<ShooterEnemy>>
			           	{
			           		new List<ShooterEnemy>
			           			{
			           				new ShooterEnemy(0, 0, 10, Color.ForestGreen),
			           				new ShooterEnemy(1, 0, 10, Color.Gainsboro),
			           				new ShooterEnemy(2, 0, 10, Color.Goldenrod)
			           			}
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
/*
		#region update

		public void Update(KeyboardState keyState, GamePadState padState)
		{
			// update the paddles position by adding or subtracing according to the thumb stick
			_objectRect = new Rectangle((int)_paddle.GetX(), (int)_paddle.GetY(), 199, 17);

			#region paddle with wall collision

			if (_paddle.GetX() == 10)
			{
				if (padState.ThumbSticks.Left.X > 0)
				{
					_paddle.UpdatePosition(padState.ThumbSticks.Left.X, 0);
				}

				if (keyState.IsKeyDown(Keys.Left))
				{
					_paddle.UpdatePosition(10, 0);
				}

			}
			else if (_paddle.GetX() == 790 - 199)
			{
				if (padState.ThumbSticks.Left.X < 0)
				{
					_paddle.UpdatePosition(padState.ThumbSticks.Left.X, 0);
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
				_paddle.UpdatePosition(padState.ThumbSticks.Left.X, 0);
			}

			#endregion

			#region  wall collision testing and movement

			// check for collisions between the ball and any other objects
			int i;
			for (i = 0; i < _ballList.Count; i++)
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
						_objectRect = new Rectangle((int)_blockList[j][k].GetX() * 78, (int)_blockList[j][k].GetY() * 36, 78, 36);

						if (_ballRect.Intersects(_objectRect)) //if a ball intersects with the block...
						{
							if (_blockList[j][k].GetType() != Blocktype.Dead)//...and the block is not dead...
							{
								switch (_blockList[j][k].GetSide(_ballRect))//..than find out which side it hit and act accordingly.
								{
									case 1:
										_ballList[i].SwitchX();
										break;
									case 2:
										_ballList[i].SwitchY();
										break;
									case 3:
										_ballList[i].SwitchX();
										break;
									case 4:
										_ballList[i].SwitchY();
										break;
									default:
										break;
								}
							}

							// change the block type with this method.
							DecrementType(_blockList[j][k]);
						}
					}
				}

				#endregion

				_ballList[i].UpdatePosition(_ballList[i].GetVx(), _ballList[i].GetVy());
			}

			#endregion

			if (padState.Buttons.A == ButtonState.Pressed && _a == 0)
			{
				_ballList.Add(new BreakOutBall(400, 550, -5, -5));
				_a = 1;
			}
			if (padState.Buttons.A == ButtonState.Released)
			{
				_a = 0;
			}
		}

		

		#endregion
*/
		/*
		#region draw

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			//draw the ship
			_ship.Draw(spriteBatch, fonts, textures);

			//draw the enemies
			foreach(ShooterEnemy e in _enemies[0])
			{
				e.Draw(spriteBatch, fonts, textures);
			}

		}

		#endregion
		 */

	}
}
