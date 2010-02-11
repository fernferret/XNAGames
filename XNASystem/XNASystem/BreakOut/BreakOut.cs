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
	Dead,  //No longer affects the ball in any way and not displayed on the screen
	Ball  // creates 2 extra balls when hit
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
		private List<List<BreakOutBlock>> _blockList;
		private readonly List<BreakOutBall> _ballList;
		private Rectangle _ballRect;
		private Rectangle _objectRect;
		private int _a;
		private int _space;
		private int _score;
		private int _lives;
		private Boolean _mainBallIsAlive;
		private readonly SystemDisplay _main;
		public int Height = 720;
		public int Width = 1280;
		private readonly int _buffer;
		private Boolean _running;
		private List<Color> _colorScheme;

		#endregion

		#region constructor

		public BreakOut(SystemDisplay main)
		{
			//here is where we can take in things like level
			_paddle = new BreakOutPaddle();
			_leftWall = new BreakOutWall(0, Width, Height);
			_rightWall = new BreakOutWall(1, Width, Height);
			_ceiling = new BreakOutCeiling(Width);
			_main = main;
			_a = 0;
			_space = 0;
			_score = 0;
			_lives = 20;
			_mainBallIsAlive = true;
			_buffer = (Width%78)/2;
			_running = false;

			_ballList = new List<BreakOutBall>{new BreakOutBall(200, 400, (float) 0.5, (float) 0.5)};

			StartGame(3);

		}

		#endregion

		#region game methods

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
			_main.EndGame(new Score("Name", ActivityType.Game, _score, "Breakout"));
		}

		public void StartGame(int level)
		{
			switch(level)
			{
				case 1:
					_colorScheme = new List<Color>{Color.Beige, Color.Salmon, Color.Red, Color.Coral, Color.Gray};
					#region sample level 1

					_blockList = new List<List<BreakOutBlock>>
			             	{
								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(0, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 4, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(0, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(1, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(1, 3, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(1, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(1, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(1, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(1, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(1, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(2, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(2, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(2, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(2, 3, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(2, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(2, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(2, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(3, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(3, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(3, 2, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(3, 3, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(3, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(3, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(3, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(3, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(3, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(3, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(4, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(4, 3, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(4, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(4, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(4, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(4, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(4, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(5, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(5, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(5, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(5, 3, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(5, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(5, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(5, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(5, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(5, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(5, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(6, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(6, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(6, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 4, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(6, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(6, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(7, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(7, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(7, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 5, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(7, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(7, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(8, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(8, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(8, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 5, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(8, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(8, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(9, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(9, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(9, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 4, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(9, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(9, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(10, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(10, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(10, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(10, 3, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(10, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(10, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(10, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(10, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(10, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(10, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(11, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(11, 3, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(11, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(11, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(11, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(11, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(11, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(12, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(12, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(12, 2, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(12, 3, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(12, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(12, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(12, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(12, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(12, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(12, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(13, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(13, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(13, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(13, 3, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(13, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(13, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(13, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(14, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(14, 3, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(14, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(14, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(14, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(14, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(14, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(15, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 4, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(15, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 9, Width, Height, Blocktype.Dead)
			             			}
			             	};

					#endregion
					break;
				case 2:
					_colorScheme = new List<Color> {Color.Beige, Color.Gold, Color.DarkOrange, Color.Tomato, Color.Gray};
					#region sample level 2

					_blockList = new List<List<BreakOutBlock>>
			             	{
								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(0, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 2, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 3, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 4, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 5, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 6, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 7, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(1, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 2, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 3, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 4, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 5, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 6, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 7, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(2, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(2, 1, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(2, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(2, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(3, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(3, 1, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(3, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(3, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(3, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(3, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(3, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(3, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(3, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(3, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(4, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 1, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(4, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(4, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(4, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(4, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(4, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(4, 7, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(4, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(4, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(5, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(5, 1, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(5, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(5, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(5, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(5, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(5, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(5, 7, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(5, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(5, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(6, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(6, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(6, 2, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(6, 3, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(6, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 6, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(6, 7, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(6, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(6, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(7, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(7, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(7, 2, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(7, 3, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(7, 4, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(7, 5, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(7, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 7, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(7, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(7, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(8, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(8, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(8, 2, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(8, 3, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(8, 4, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(8, 5, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(8, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 7, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(8, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(8, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(9, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(9, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(9, 2, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(9, 3, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(9, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 6, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(9, 7, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(9, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(9, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(10, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(10, 1, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(10, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(10, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(10, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(10, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(10, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(10, 7, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(10, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(10, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(11, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 1, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(11, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(11, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(11, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(11, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(11, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(11, 7, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(11, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(11, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(12, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(12, 1, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(12, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(12, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(12, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(12, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(12, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(12, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(12, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(12, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(13, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(13, 1, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(13, 2, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(13, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(14, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 2, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 3, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 4, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 5, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 6, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 7, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(15, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 2, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 3, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 4, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 5, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 6, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 7, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 9, Width, Height, Blocktype.Dead)
			             			},
			             	};

					#endregion
					break;
				case 3:
					_colorScheme = new List<Color>{Color.Beige, Color.Turquoise, Color.Teal, Color.Thistle, Color.Gray};
					#region sample level 3

					_blockList = new List<List<BreakOutBlock>>
			             	{
								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(0, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(0, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(0, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(0, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(0, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(1, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(1, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(1, 2, Width, Height, Blocktype.Ball),
			             				new BreakOutBlock(1, 3, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(1, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(1, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(1, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(1, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(1, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(1, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(2, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(2, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(2, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(2, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(2, 9, Width, Height, Blocktype.Dead)
			             			},

			             		new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(3, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(3, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(3, 2, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(3, 3, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(3, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(3, 5, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(3, 6, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(3, 7, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(3, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(3, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(4, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 2, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 3, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 4, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 5, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 6, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 7, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(4, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(5, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(5, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(5, 2, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(5, 3, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(5, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(5, 5, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(5, 6, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(5, 7, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(5, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(5, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(6, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(6, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(6, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(6, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(6, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(7, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(7, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 2, Width, Height, Blocktype.Ball),
			             				new BreakOutBlock(7, 3, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(7, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(7, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(7, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(8, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(8, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 2, Width, Height, Blocktype.Ball),
			             				new BreakOutBlock(8, 3, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(8, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(8, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(8, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(9, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(9, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(9, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(9, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(9, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(10, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(10, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(10, 2, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(10, 3, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(10, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(10, 5, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(10, 6, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(10, 7, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(10, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(10, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(11, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 1, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 2, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 3, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 4, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 5, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 6, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 7, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 8, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(11, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(12, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(12, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(12, 2, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(12, 3, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(12, 4, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(12, 5, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(12, 6, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(12, 7, Width, Height, Blocktype.Strong2),
			             				new BreakOutBlock(12, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(12, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(13, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(13, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(13, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(13, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(13, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(14, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(14, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(14, 2, Width, Height, Blocktype.Ball),
			             				new BreakOutBlock(14, 3, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(14, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(14, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(14, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(14, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(14, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(14, 9, Width, Height, Blocktype.Dead)
			             			},

								new List<BreakOutBlock>
			             			{
			             				new BreakOutBlock(15, 0, Width, Height, Blocktype.Dead),
			             				new BreakOutBlock(15, 1, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 2, Width, Height, Blocktype.Invincible),
			             				new BreakOutBlock(15, 3, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 4, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 5, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 6, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 7, Width, Height, Blocktype.Standard),
			             				new BreakOutBlock(15, 8, Width, Height, Blocktype.Strong3),
			             				new BreakOutBlock(15, 9, Width, Height, Blocktype.Dead)
			             			}
			             	};

					#endregion
					break;
				default:
					break;
			}

			_running = true;
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

		#region update

		public void Update(InputHandler handler)
		{
			// TODO: Need to update to use the new handler class!
			var padState = handler.GetPadState();
			var keyState = handler.GetKeyState();
			
			//test all collisions between the ball and blocks, paddle, and walls.
			int z;
			for (z = 0; z < 10; z++)
			{
				#region paddle movement and collision with wall

				// create a rectangle around the paddle
				_objectRect = new Rectangle((int) _paddle.GetX(), (int) _paddle.GetY(), _paddle.GetWidth(), _paddle.GetHeight());

				#region walls

				//if the paddle is up against the left wall only allow it to move to the right
				if (_paddle.GetX() == _buffer)
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

				// if the paddle is up against the right wall only allow it to move to the left
				else if (_paddle.GetX() == _rightWall.GetX() - _paddle.GetWidth())
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

				//if the paddle is intersecting with the left wall than move it away
				else if (_objectRect.Intersects(new Rectangle((int) _leftWall.GetX(), (int) _leftWall.GetY(), _buffer, Height)))
				{
					_paddle.SetX(_buffer);
				}

				//if the paddle is intersecting with the right wall than move t away
				else if (_objectRect.Intersects(new Rectangle((int) _rightWall.GetX(), (int) _rightWall.GetY(), _buffer, Height)))
				{
					_paddle.SetX(_rightWall.GetX() - _paddle.GetWidth());
				}

				#endregion

				#region movement

				// move the paddle based on the thumbstick or arrow key input
				else
				{
					_paddle.UpdatePostion(padState.ThumbSticks.Left.X, 0);
					if(keyState.IsKeyDown(Keys.Left))
					{
						_paddle.UpdatePostion(-1, 0);
					}
					if (keyState.IsKeyDown(Keys.Right))
					{
						_paddle.UpdatePostion(1, 0);
					}
				}

				#endregion

				#region balls

				// test the collsion with the ball in the new paddle position
				int i;
				for (i = 0; i < _ballList.Count; i++)
				{
					if (_ballList[i].IsAlive() && !_ballList[i].IsConstantV())
					{
						// create a rectangle around the balls current position
						_ballRect = new Rectangle((int)_ballList[i].GetX(), (int)_ballList[i].GetY(), 15, 15);

						if (_ballRect.Intersects(_objectRect))
						{
							// if the ball hits the top than bounce up
							if (_paddle.GetSide(_ballRect) == 0)
							{
								_ballList[i].SwitchY();
								_ballList[i].IncrementX(padState.ThumbSticks.Left.X);
								if (keyState.IsKeyDown(Keys.Left))
								{
									_ballList[i].IncrementX((float) -0.2);
								}
								if(keyState.IsKeyDown(Keys.Right))
								{
									_ballList[i].IncrementX((float) 0.2);
								}
								_ballList[i].UpdatePostion(0, 1);
							}
							//if it hits one of the sides than bounce down
							else
							{
								_ballList[i].SwitchX();
								_ballList[i].MakeConstantV();
							}
						}
					}
				}

				#endregion

				#endregion

				#region  ball collision testing 

				// check for collisions between the ball and any other objects
				for (i = 0; i < _ballList.Count; i++)
				{
					if (_ballList[i].IsAlive())
					{
						_ballList[i].UpdatePostion(_ballList[i].GetVx(), _ballList[i].GetVy());

						// create a rectangle around the balls current position
						_ballRect = new Rectangle((int)_ballList[i].GetX(), (int)_ballList[i].GetY(), 15, 15);

						if(!_ballList[i].IsConstantV())
						{
							#region paddle

							// create a rectangle around the paddle and check for intersections
							_objectRect = new Rectangle((int)_paddle.GetX(), (int)_paddle.GetY(), _paddle.GetWidth(), _paddle.GetHeight());

							if (_ballRect.Intersects(_objectRect))
							{
								if (_paddle.GetSide(_ballRect) == 0)
								{
									_ballList[i].SwitchY();
									_ballList[i].IncrementX(padState.ThumbSticks.Left.X);
									if(keyState.IsKeyDown(Keys.Left))
									{
										_ballList[i].IncrementX((float) -0.2);
									}
									if(keyState.IsKeyDown(Keys.Right))
									{
										_ballList[i].IncrementX((float) 0.2);
									}
									_ballList[i].UpdatePostion(0, 1);
								}
								else
								{
									 _ballList[i].SwitchX();
									_ballList[i].MakeConstantV();
								}
							}

							#endregion

							#region blocks

							int j, k;
							var blocksHitX = new List<int>();
							var blocksHitY = new List<int>();

							//check every block and record all collisions
							for (j = 0; j < 16; j++)
							{
								for (k = 0; k < 10; k++)
								{
									//make a rectangle aroundt he current block
									_objectRect = new Rectangle((int)_blockList[j][k].GetX(), (int)_blockList[j][k].GetY(), 78, 36);

									//if a ball intersects with the block and that block is alive...
									if (_ballRect.Intersects(_objectRect) && _blockList[j][k].GetType() != Blocktype.Dead)
									{
										//reord the position of the block
										blocksHitX.Add(j);
										blocksHitY.Add(k);
									}
								}
							}

							#region single block hit

							//act on the collisions based on how many are hit
							if (blocksHitX.Count == 1)
							{
								switch (_blockList[blocksHitX[0]][blocksHitY[0]].GetSide(_ballRect))
								//..than find out which side it hit and act accordingly.
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
										break;
								}

								// change the block type with this method.
								DecrementType(_blockList[blocksHitX[0]][blocksHitY[0]]);
							}

							#endregion

							#region double block hit

							else if (blocksHitY.Count == 2)
							{

								if (blocksHitX[0] == blocksHitX[1])
								{
									_ballList[i].SwitchX();
								}

								if (blocksHitY[0] == blocksHitY[1])
								{
									_ballList[i].SwitchY();
								}

								// change the block type with this method.
								DecrementType(_blockList[blocksHitX[0]][blocksHitY[0]]);
								DecrementType(_blockList[blocksHitX[1]][blocksHitY[1]]);
							}

							#endregion

							#region triple block hit

							else if (blocksHitY.Count == 3)
							{

								if (blocksHitX[0] != blocksHitX[1])
								{
									if (blocksHitY[0] != blocksHitY[2])
									{
										// change the block type with this method.
										DecrementType(_blockList[blocksHitX[0]][blocksHitY[0]]);
										DecrementType(_blockList[blocksHitX[2]][blocksHitY[2]]);
									}
								}
								else if (blocksHitX[0] != blocksHitX[2])
								{
									// change the block type with this method.
									DecrementType(_blockList[blocksHitX[2]][blocksHitY[2]]);
									DecrementType(_blockList[blocksHitX[1]][blocksHitY[1]]);
								}

								if (blocksHitY[0] == blocksHitY[1])
								{
									_ballList[i].SwitchX();
								}

								//no matter what completely flip the ball
								_ballList[i].SwitchX();
								_ballList[i].SwitchY();

							}

							#endregion

							#endregion
						}

						#region walls and ceiling

						// create a rectangle around the lef twall and check for intersections
						_objectRect = new Rectangle((int)_leftWall.GetX(), (int)_leftWall.GetY(), _buffer, Height);
						if (_ballRect.Intersects(_objectRect))
						{
							//simply change the x velocity
							_ballList[i].SwitchX();
						}

						// create a rectangle aroun the right wall and check for intersections
						_objectRect = new Rectangle((int)_rightWall.GetX(), (int)_rightWall.GetY(), _buffer, Height);
						if (_ballRect.Intersects(_objectRect))
						{
							//simple change the x velocty
							_ballList[i].SwitchX();
						}

						//create a rectangle around the ceiling and check for intersections
						_objectRect = new Rectangle((int)_ceiling.GetX(), (int)_ceiling.GetY(), Width, _buffer);
						if (_ballRect.Intersects(_objectRect))
						{
							//simpley change the y velocity
							_ballList[i].SwitchY();
						}

						#endregion

						#region deadspace

						_objectRect = new Rectangle(0, Height + 15, Width, 1);
						if (_objectRect.Intersects(_ballRect))
						{
							_lives--;
							_mainBallIsAlive = false;
							_ballList[i].Kill();
							if (_lives == 0)
							{
								FinishGame();
							}
						}

						#endregion
					}
				}
				#endregion

				#region win condition

				int x, y;
				Boolean win = true;

				for (x = 0; x < 10; x++)
				{
					for (y = 0; y < 10; y++)
					{
						if (_blockList[x][y].GetType() == Blocktype.Standard || _blockList[x][y].GetType() == Blocktype.Strong2 || _blockList[x][y].GetType() == Blocktype.Strong3)
						{
							win = false;
						}
					}
				}

				if (win)
				{
					_score += 1000;
					FinishGame();
				}

				#endregion

				#region shooting balls

				if (padState.Buttons.A == ButtonState.Pressed && _a == 0)
				{
					if (_lives > 0 && !_mainBallIsAlive)
					{
						_ballList.Add(new BreakOutBall(400, 530, (float) -.5, (float) -.5));
					}
					_a = 1;
				}
				if (padState.Buttons.A == ButtonState.Released)
				{
					_a = 0;
				}
				if (keyState.IsKeyDown(Keys.Space) && _space == 0)
				{
					if (_lives > 0 && !_mainBallIsAlive)
					{
						_ballList.Add(new BreakOutBall(400, 530, (float)-.5, (float)-.5));
					}
					_space = 1;
				}
				if (keyState.IsKeyUp(Keys.Space))
				{
					_space = 0;
				}

				#endregion
			}
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
				case Blocktype.Ball:
					var rand = new Random();
					_ballList.Add(new BreakOutBall(block.GetX() + 39, block.GetY() + 18,(float) .2 * (rand.Next(4) + 1), (float) .5));
					_ballList.Add(new BreakOutBall(block.GetX() + 39, block.GetY() + 18,(float) .2 * (rand.Next(4) + 1), (float) .5));
					_lives += 2;
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
			if (_running)
			{
				spriteBatch.Begin();

				spriteBatch.Draw(textures[4], new Rectangle(0, 0, Width, Height), Color.Black);
				//draw the paddle wlls and ceiling
				_paddle.Draw(spriteBatch, fonts, textures);
				_leftWall.Draw(spriteBatch, fonts, textures);
				_rightWall.Draw(spriteBatch, fonts, textures);
				_ceiling.Draw(spriteBatch, fonts, textures);

				//draw the score
				spriteBatch.DrawString(fonts[0], "" + _score, new Vector2(2*_buffer, 2*_buffer), Color.White);

				//draw the lives
				spriteBatch.DrawString(fonts[0], "Lives: " + _lives, new Vector2(Width - 70 - (2*_buffer), 2*_buffer), Color.White);

				//draw the blocks
				int i, j;
				for (i = 0; i < 16; i++)
				{
					for (j = 0; j < 10; j++)
					{
						switch(_blockList[i][j].GetType())
						{
							case Blocktype.Standard:
								_blockList[i][j].SetColor(_colorScheme[0]);
								break;
							case Blocktype.Strong2:
								_blockList[i][j].SetColor(_colorScheme[1]);
								break;
							case Blocktype.Strong3:
								_blockList[i][j].SetColor(_colorScheme[2]);
								break;
							case Blocktype.Ball:
								_blockList[i][j].SetColor(_colorScheme[3]);
								break;
							case Blocktype.Invincible:
								_blockList[i][j].SetColor(_colorScheme[4]);
								break;
							default:
								break;
						}

						if (_blockList[i][j].GetType() != Blocktype.Dead)
						{
							_blockList[i][j].Draw(spriteBatch, fonts, textures);
						}
					}
				}

				//draw the balls
				int k;
				for (k = 0; k < _ballList.Count; k++)
				{
					if (_ballList[k].IsAlive())
						_ballList[k].Draw(spriteBatch, fonts, textures);
				}
				spriteBatch.End();
			}
		}

		#endregion
	}
}