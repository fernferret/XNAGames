using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;

namespace XNASystem.BreakOut
{
	class BreakOutWall : IGameObject
	{
		private readonly int _side;

		#region constructor

		public BreakOutWall(int side)
		{
			_side = side;
		}

		#endregion

		#region update

		public void UpdatePostion(float x, float y)
		{
			//do nothing, they dont move
		}

		#endregion

		#region draw

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[4], new Vector2(_side * 790, 0), Color.White);
		}

		#endregion

		#region get methods

		public float GetX()
		{
			return _side * 790;
		}

		public float GetY()
		{
			return 0;
		}

		#endregion
	}
}
