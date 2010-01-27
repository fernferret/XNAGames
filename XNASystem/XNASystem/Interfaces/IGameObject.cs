using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.Interfaces
{
	interface IGameObject
	{
		void UpdatePostion(float x, float y);

		void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures);

		float GetX();

		float GetY();
	}
}