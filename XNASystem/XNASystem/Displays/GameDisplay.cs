using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNASystem.Displays
{
	public class GameDisplay: IScreen
	{
		public void Update(KeyboardState state)
		{
			//throw new NotImplementedException();
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();
			spriteBatch.DrawString(fonts[0], "Game", new Vector2(100, 200), Color.Black);
			spriteBatch.End();
		}
	}
}