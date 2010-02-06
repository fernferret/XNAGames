using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Utils;

namespace XNASystem.Interfaces
{
	public interface IScreen
	{
		//void Update(KeyboardState keykeyState, GamePadState padkeyState);

		void Update(InputHandler handler, GameTime gameTime);

		void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures);
	}
}