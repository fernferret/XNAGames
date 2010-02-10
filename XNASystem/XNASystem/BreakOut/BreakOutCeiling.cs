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
		private readonly int _width;
		private readonly int _buffer;

		public BreakOutCeiling(int width)
		{
			_width = width;
			_buffer = (width%78)/2;
		}

		#region update

		public void UpdatePostion(float x, float y)
		{
			//do nothing, they dont move
		}

		#endregion

		#region draw

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[7], new Rectangle(0, 0, _width, _buffer), Color.White);
		}

		#endregion

		#region get methods

		public float GetX()
		{
			return 0;
		}

		public float GetY()
		{
			return 0;
		}

		#endregion
	}
}
