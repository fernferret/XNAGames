using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using XNASystem.Displays;
using XNASystem.Interfaces;
using XNASystem.Utils;

#region block types

enum Blocktype
{
	Standard,  //Dead after one hit from the ball
	Invincible,  //never dies
	Strong2,  //Dead after two hits
	Strong3,  //Dead after three hits
	Dead  //No longer affects the ball in any way and not displayed on the screen
}

#endregion

namespace XNASystem.BreakOut
{
	class BreakOut : IGame , IScreen
	{
		#region variables

		private readonly BreakOutPaddle _paddle;
		private readonly BreakOutWall _leftWall;
		private readonly BreakOutWall _rightWall;
		private readonly BreakOutCeiling _ceiling;
		private readonly List<List<BreakOutBlock>> _blockList;
		private List<BreakOutBall> _ballList;
		private Rectangle _ballRect;
		private Rectangle _objectRect;
		private int _a;
		private int _score;
		private int _lives;
		private Boolean _mainBallIsAlive;
		private SystemDisplay _main;

		#endregion

		#region constructor

		public BreakOut(SystemDisplay main)
		{
			//here is where we can take in things like level
			_paddle = new BreakOutPaddle();
			_leftWall = new BreakOutWall(0);
			_rightWall = new BreakOutWall(1);
			_ceiling = new BreakOutCeiling();
			_main = main;
			_a = 0;
			_score = 0;
			_lives = 3;
			_mainBallIsAlive = true;


			#region sample level - delete when xml works
			_blockList = new List<List<BreakOutBlock>>
			             	{
			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(0, 0, Blocktype.Dead, Color.Red),
			             				new BreakOutBlock(0, 1, Blocktype.Dead, Color.Violet),
			             				new BreakOutBlock(0, 2, Blocktype.Dead, Color.Thistle),
			             				new BreakOutBlock(0, 3, Blocktype.Strong3, Color.Red),
			             				new BreakOutBlock(0, 4, Blocktype.Strong2, Color.Salmon),
			             				new BreakOutBlock(0, 5, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(0, 6, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(0, 7, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(0, 8, Blocktype.Dead, Color.CadetBlue),
			             				new BreakOutBlock(0, 9, Blocktype.Dead, Color.Red)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(1, 0, Blocktype.Dead, Color.Red),
			             				new BreakOutBlock(1, 1, Blocktype.Dead, Color.Violet),
			             				new BreakOutBlock(1, 2, Blocktype.Invincible, Color.Gray),
			             				new BreakOutBlock(1, 3, Blocktype.Strong3, Color.Red),
			             				new BreakOutBlock(1, 4, Blocktype.Strong2, Color.Salmon),
			             				new BreakOutBlock(1, 5, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(1, 6, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(1, 7, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(1, 8, Blocktype.Dead, Color.CadetBlue),
			             				new BreakOutBlock(1, 9, Blocktype.Dead, Color.Red)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(2, 0, Blocktype.Dead, Color.Red),
			             				new BreakOutBlock(2, 1, Blocktype.Dead, Color.Violet),
			             				new BreakOutBlock(2, 2, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(2, 3, Blocktype.Invincible, Color.Gray),
			             				new BreakOutBlock(2, 4, Blocktype.Strong2, Color.Salmon),
			             				new BreakOutBlock(2, 5, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(2, 6, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(2, 7, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(2, 8, Blocktype.Dead, Color.CadetBlue),
			             				new BreakOutBlock(2, 9, Blocktype.Dead, Color.Red)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(3, 0, Blocktype.Dead, Color.Red),
			             				new BreakOutBlock(3, 1, Blocktype.Dead, Color.Violet),
			             				new BreakOutBlock(3, 2, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(3, 3, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(3, 4, Blocktype.Invincible, Color.Gray),
			             				new BreakOutBlock(3, 5, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(3, 6, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(3, 7, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(3, 8, Blocktype.Dead, Color.CadetBlue),
			             				new BreakOutBlock(3, 9, Blocktype.Dead, Color.Red)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(4, 0, Blocktype.Dead, Color.Red),
			             				new BreakOutBlock(4, 1, Blocktype.Dead, Color.Violet),
			             				new BreakOutBlock(4, 2, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(4, 3, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(4, 4, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(4, 5, Blocktype.Invincible, Color.Gray),
			             				new BreakOutBlock(4, 6, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(4, 7, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(4, 8, Blocktype.Dead, Color.CadetBlue),
			             				new BreakOutBlock(4, 9, Blocktype.Dead, Color.Red)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(5, 0, Blocktype.Dead, Color.Red),
			             				new BreakOutBlock(5, 1, Blocktype.Dead, Color.Violet),
			             				new BreakOutBlock(5, 2, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(5, 3, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(5, 4, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(5, 5, Blocktype.Invincible, Color.Gray),
			             				new BreakOutBlock(5, 6, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(5, 7, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(5, 8, Blocktype.Dead, Color.CadetBlue),
			             				new BreakOutBlock(5, 9, Blocktype.Dead, Color.Red)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(6, 0, Blocktype.Dead, Color.Red),
			             				new BreakOutBlock(6, 1, Blocktype.Dead, Color.Violet),
			             				new BreakOutBlock(6, 2, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(6, 3, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(6, 4, Blocktype.Invincible, Color.Gray),
			             				new BreakOutBlock(6, 5, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(6, 6, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(6, 7, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(6, 8, Blocktype.Dead, Color.CadetBlue),
			             				new BreakOutBlock(6, 9, Blocktype.Dead, Color.Red)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(7, 0, Blocktype.Dead, Color.Red),
			             				new BreakOutBlock(7, 1, Blocktype.Dead, Color.Violet),
			             				new BreakOutBlock(7, 2, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(7, 3, Blocktype.Invincible, Color.Gray),
			             				new BreakOutBlock(7, 4, Blocktype.Strong2, Color.Salmon),
			             				new BreakOutBlock(7, 5, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(7, 6, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(7, 7, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(7, 8, Blocktype.Dead, Color.CadetBlue),
			             				new BreakOutBlock(7, 9, Blocktype.Dead, Color.Red)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(8, 0, Blocktype.Dead, Color.Red),
			             				new BreakOutBlock(8, 1, Blocktype.Dead, Color.Violet),
			             				new BreakOutBlock(8, 2, Blocktype.Invincible, Color.Gray),
			             				new BreakOutBlock(8, 3, Blocktype.Strong3, Color.Red),
			             				new BreakOutBlock(8, 4, Blocktype.Strong2, Color.Salmon),
			             				new BreakOutBlock(8, 5, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(8, 6, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(8, 7, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(8, 8, Blocktype.Dead, Color.CadetBlue),
			             				new BreakOutBlock(8, 9, Blocktype.Dead, Color.Red)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(9, 0, Blocktype.Dead, Color.Red),
			             				new BreakOutBlock(9, 1, Blocktype.Dead, Color.Violet),
			             				new BreakOutBlock(9, 2, Blocktype.Dead, Color.Thistle),
			             				new BreakOutBlock(9, 3, Blocktype.Strong3, Color.Red),
			             				new BreakOutBlock(9, 4, Blocktype.Strong2, Color.Salmon),
			             				new BreakOutBlock(9, 5, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(9, 6, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(9, 7, Blocktype.Standard, Color.Beige),
			             				new BreakOutBlock(9, 8, Blocktype.Dead, Color.CadetBlue),
			             				new BreakOutBlock(9, 9, Blocktype.Dead, Color.Red)
			             			}
			             	};

			#endregion

			_ballList = new List<BreakOutBall>{new BreakOutBall(200, 400, (float) 0.5, (float) 0.5)};

		}

		#endregion

		#region unimplemented methods

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

		public void Update(InputHandler handler)
		{
			// TODO: Need to update to use the new handler class!
			var padState = handler.GetPadState();
			var keyState = handler.GetKeyState();
			int x;
			for (x = 0; x < 10; x++)
			{
				#region paddle with wall collision

				// update the paddles position by adding or subtracing according to the thumb stick
				_objectRect = new Rectangle((int) _paddle.GetX(), (int) _paddle.GetY(), 199, 17);

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
				else if (_objectRect.Intersects(new Rectangle((int) _leftWall.GetX(), (int) _leftWall.GetY(), 10, 600)))
				{
					_paddle.SetX(10);
				}

				else if (_objectRect.Intersects(new Rectangle((int) _rightWall.GetX(), (int) _rightWall.GetY(), 10, 600)))
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
						_ballRect = new Rectangle((int) _ballList[i].GetX(), (int) _ballList[i].GetY(), 15, 15);

						#region paddle

						// create a rectangle around the paddle and check for intersections
						_objectRect = new Rectangle((int) _paddle.GetX(), (int) _paddle.GetY(), 199, 17);
						if (_ballRect.Intersects(_objectRect))
						{
							//simply switch the y velocity 
							_ballList[i].SwitchY();
							_ballList[i].IncrementX(padState.ThumbSticks.Left.X);
						}

						#endregion

						#region walls and ceiling

						// create a rectangle around the lef twall and check for intersections
						_objectRect = new Rectangle((int) _leftWall.GetX(), (int) _leftWall.GetY(), 10, 600);
						if (_ballRect.Intersects(_objectRect))
						{
							//simply change the x velocity
							_ballList[i].SwitchX();
						}

						// create a rectangle aroun the right wall and check for intersections
						_objectRect = new Rectangle((int) _rightWall.GetX(), (int) _rightWall.GetY(), 10, 600);
						if (_ballRect.Intersects(_objectRect))
						{
							//simple change the x velocty
							_ballList[i].SwitchX();
						}

						//create a rectangle around the ceiling and check for intersections
						_objectRect = new Rectangle((int) _ceiling.GetX(), (int) _ceiling.GetY(), 800, 10);
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
								_objectRect = new Rectangle((int) _blockList[j][k].GetX(), (int) _blockList[j][k].GetY(), 78, 36);

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
				if ((padState.Buttons.A == ButtonState.Pressed && _a == 0) || (keyState.IsKeyDown(Keys.Space) && _a == 0))
				{
					if (_lives > 0 && !_mainBallIsAlive)
					{
						_ballList.Add(new BreakOutBall(400, 530, (float) -.5, (float) -.5));
					}
					_a = 1;
				}
				if (padState.Buttons.A == ButtonState.Released || keyState.IsKeyUp(Keys.Space))
				{
					_a = 0;
				}
			}
		}

		private static List<BreakOutBall> DeleteBall(List<BreakOutBall> balls, int i)
		{
			var newList = new List<BreakOutBall>();
			int x;
			for(x = 0; x < balls.Count; x++)
			{
				if (x != i)
				{
					newList.Add(balls[x]);
				}
			}

			return newList;
		}

		private void DecrementType(BreakOutBlock block)
		{
			switch(block.GetType())
			{
				case Blocktype.Strong3:
					block.SetType(Blocktype.Strong2);
					block.SetColor(Color.Salmon);
					_score += 100;
					break;
				case Blocktype.Strong2:
					block.SetType(Blocktype.Standard);
					block.SetColor(Color.White);
					_score += 100;
					break;
				case Blocktype.Standard:
					block.SetType(Blocktype.Dead);
					_score += 100;
					break;
				default:
					break;
			}
		}

		#endregion

		#region draw

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			//draw the paddle wlls and ceiling
			_paddle.Draw(spriteBatch, fonts, textures);
			_leftWall.Draw(spriteBatch, fonts, textures);
			_rightWall.Draw(spriteBatch, fonts, textures);
			_ceiling.Draw(spriteBatch, fonts, textures);

			//draw the score
			spriteBatch.DrawString(fonts[0], "" + _score, new Vector2(30,30), Color.Black);

			//draw the lives
			spriteBatch.DrawString(fonts[0],"Lives: " + _lives, new Vector2(730, 30), Color.Black);

			//draw the blocks
			int i, j;
			for (i = 0; i < 10; i++)
			{
				for(j = 0; j < 10; j++)
				{
					// only draw the current block if it is not dead
					if(_blockList[i][j].GetType() != Blocktype.Dead)
					_blockList[i][j].Draw(spriteBatch, fonts, textures);
				}
			}

			//draw the balls
			int k;
			for (k = 0; k < _ballList.Count; k++)
			{
				_ballList[k].Draw(spriteBatch, fonts, textures);
			}
				spriteBatch.End();

		}

		#endregion
	}
}