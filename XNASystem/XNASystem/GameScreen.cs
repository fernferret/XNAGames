using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;

namespace XNASystem
{
	class GameScreen : IScreen
	{
		#region variables

		#endregion

		#region constructor
		public GameScreen()
		{

		}
		#endregion

		#region update
		public void Update(KeyboardState keyState, GamePadState padState)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region draw
		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			throw new NotImplementedException();
		}
#endregion
	}
}
