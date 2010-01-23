using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.Interfaces
{
	interface IGameObject
	{
		void UpdatePostiion(int x, int y);

		void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures);
	}
}