using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.SystemMenus;
using System;

namespace XNASystem.BreakOut
{
	class BreakOutWall : IGameObject
	{
		private readonly int _side;

		public BreakOutWall(int side)
		{
			_side = side;
		}

		public void UpdatePostiion(float x, float y)
		{
			//do nothing, they dont move
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[4], new Vector2(_side * 790, 0), Color.White);
		}
	}
}
