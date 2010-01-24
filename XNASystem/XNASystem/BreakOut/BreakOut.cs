using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;

#region

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
		private readonly BreakOutPaddle _paddle;
		private readonly BreakOutWall _leftWall;
		private readonly BreakOutWall _rightWall;
		private readonly List<List<BreakOutBlock>> _blockList;
		private readonly List<BreakOutBall> _ballList;

		public BreakOut()
		{
			//here is where we can take in things like level
			_paddle = new BreakOutPaddle();
			_leftWall = new BreakOutWall(0);
			_rightWall = new BreakOutWall(1);
			_blockList = new List<List<BreakOutBlock>>{new List<BreakOutBlock>{new BreakOutBlock(0,0,Blocktype.Standard, Color.Red), 
																				new BreakOutBlock(0,1,Blocktype.Standard, Color.Violet),
																				new BreakOutBlock(0,2,Blocktype.Standard, Color.Thistle),
																				new BreakOutBlock(0,3,Blocktype.Standard, Color.Yellow),
																				new BreakOutBlock(0,4,Blocktype.Standard, Color.AliceBlue),
																				new BreakOutBlock(0,5,Blocktype.Standard, Color.Beige),
																				new BreakOutBlock(0,6,Blocktype.Standard, Color.Bisque),
																				new BreakOutBlock(0,7,Blocktype.Standard, Color.BlanchedAlmond),
																				new BreakOutBlock(0,8,Blocktype.Standard, Color.CadetBlue),
																				new BreakOutBlock(0,9,Blocktype.Standard, Color.Chartreuse)},
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10),
														new List<BreakOutBlock>(10)};
			_ballList = new List<BreakOutBall>{new BreakOutBall(200, 400, 1, 1)};

		}

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

		public void Update(KeyboardState keyState, GamePadState padState)
		{
			_paddle.UpdatePostiion(padState.ThumbSticks.Left.X, 0);
			int i;
			for(i = 0; i < _ballList.Count; i++)
			{
				_ballList[i].UpdatePostiion(1,1);
			}
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			_paddle.Draw(spriteBatch, fonts, textures);
			_leftWall.Draw(spriteBatch, fonts, textures);
			_rightWall.Draw(spriteBatch, fonts, textures);
			int i, j;
			for (i = 0; i < 1; i++)
			{
				for(j = 0; j < 10; j++)
				{
					if(_blockList[i][j] != null)
					_blockList[i][j].Draw(spriteBatch, fonts, textures);
				}
			}
			int k;
			for (k = 0; k < _ballList.Count; k++)
			{
				_ballList[k].Draw(spriteBatch, fonts, textures);
			}
				spriteBatch.End();
			
		}
	}
}