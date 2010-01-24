using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.Interfaces
{
	interface IGameObject
	{
		void UpdatePostiion(float x, float y);

		void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures);
	}
}