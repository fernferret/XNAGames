using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;

namespace XNASystem.BreakOut
{
	class BreakOut : IGame , IScreen
	{
		private readonly BreakOutPaddle _paddle;

		public BreakOut()
		{
			//here is where we can take in things like level
			_paddle = new BreakOutPaddle();
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
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			_paddle.Draw(spriteBatch, fonts, textures);

			spriteBatch.End();
			
		}
	}
}