using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using XNASystem.Interfaces;

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
		private readonly List<BreakOutBall> _ballList;
		private Rectangle _ballRect;
		private Rectangle _objectRect;

		#endregion

		#region constructor

		public BreakOut()
		{
			//here is where we can take in things like level
			_paddle = new BreakOutPaddle();
			_leftWall = new BreakOutWall(0);
			_rightWall = new BreakOutWall(1);
			_ceiling = new BreakOutCeiling();
			_blockList = new List<List<BreakOutBlock>>{new List<BreakOutBlock>{new BreakOutBlock(0,0,Blocktype.Standard, Color.Red), 
																				new BreakOutBlock(0,1,Blocktype.Standard, Color.Violet),
																				new BreakOutBlock(0,2,Blocktype.Standard, Color.Thistle),
																				new BreakOutBlock(0,3,Blocktype.Standard, Color.Yellow),
																				new BreakOutBlock(0,4,Blocktype.Standard, Color.AliceBlue),
																				new BreakOutBlock(0,5,Blocktype.Standard, Color.Beige),
																				new BreakOutBlock(0,6,Blocktype.Standard, Color.Bisque),
																				new BreakOutBlock(0,7,Blocktype.Standard, Color.BlanchedAlmond),
																				new BreakOutBlock(0,8,Blocktype.Standard, Color.CadetBlue),
																				new BreakOutBlock(0,9,Blocktype.Strong3, Color.Red)},
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10)};
			_ballList = new List<BreakOutBall>{new BreakOutBall(200, 400, 5, 5)};

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

		public void Update(KeyboardState keyState, GamePadState padState)
		{
			// update the paddles position by adding or subtracing according to the thumb stick
			_objectRect = new Rectangle((int)_paddle.GetX(), (int)_paddle.GetY(), 199, 17);

			#region paddle with wall collision

			if (_paddle.GetX() == 10)
			{
				if(padState.ThumbSticks.Left.X > 0)
				{
					_paddle.UpdatePostiion(padState.ThumbSticks.Left.X, 0);
				}

			}
			else if ( _paddle.GetX() == 790 - 199)
			{
				if(padState.ThumbSticks.Left.X < 0)
				{
					_paddle.UpdatePostiion(padState.ThumbSticks.Left.X, 0);
				}
			}
			else if(_objectRect.Intersects(new Rectangle((int) _leftWall.GetX(), (int) _leftWall.GetY(), 10, 600)))
			{
				_paddle.SetX(10);
			}

			else if (_objectRect.Intersects(new Rectangle((int)_rightWall.GetX(), (int)_rightWall.GetY(), 10, 600)))
			{
				_paddle.SetX(790- 199);
			}

			else
			{
				_paddle.UpdatePostiion(padState.ThumbSticks.Left.X, 0);
			}

			#endregion

			#region  wall collision testing and movement

			// check for collisions between the ball and any other objects
			int i;
			for(i = 0; i < _ballList.Count; i++)
			{
				// create a rectangle around the balls current position
				_ballRect = new Rectangle((int) _ballList[i].GetX(), (int) _ballList[i].GetY(), 15, 15);

				#region paddle

				// create a rectangle around the paddle and check for intersections
				_objectRect = new Rectangle((int) _paddle.GetX(),(int) _paddle.GetY(), 199, 17);
				if(_ballRect.Intersects(_objectRect))
				{
					//simply switch the y velocity 
					_ballList[i].SwitchY();
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
				for(j = 0; j < 1; j++)
				{
					for(k = 0; k < 10; k++)
					{
						//make a rectangle aroundt he current block
						_objectRect = new Rectangle((int) _blockList[j][k].GetX() * 78, (int) _blockList[j][k].GetY() * 36, 78, 36);

						if(_ballRect.Intersects(_objectRect))
						{
							if (_blockList[j][k].GetType() != Blocktype.Dead)
							{
								switch (_blockList[j][k].GetSide(_ballRect))
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

				_ballList[i].UpdatePostiion(_ballList[i].GetVx(), _ballList[i].GetVy());
			}

			#endregion
		}

		private static void DecrementType(BreakOutBlock block)
		{
			switch(block.GetType())
			{
				case Blocktype.Strong3:
					block.SetType(Blocktype.Strong2);
					block.SetColor(Color.Salmon);
					break;
				case Blocktype.Strong2:
					block.SetType(Blocktype.Standard);
					block.SetColor(Color.White);
					break;
				case Blocktype.Standard:
					block.SetType(Blocktype.Dead);
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

			//draw the blocks
			int i, j;
			for (i = 0; i < 1; i++)
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