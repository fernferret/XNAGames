using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.SystemMenus;
using System;

namespace XNASystem.BreakOut
{
	class BreakOutCeiling : IGameObject
	{
		public BreakOutCeiling()
		{
		}

		public void UpdatePostiion(float x, float y)
		{
			//do nothing, they dont move
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[7], new Vector2(0, 0), Color.White);
		}

		public float GetX()
		{
			return 0;
		}

		public float GetY()
		{
			return 0;
		}
	}
}
