using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNASystem.Interfaces
{
	public interface IScreen
	{
		void Update(KeyboardState keykeyState, GamePadState padkeyState);

		void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures);
	}
}