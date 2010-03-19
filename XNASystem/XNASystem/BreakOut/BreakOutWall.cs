using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;

namespace XNASystem.BreakOut
{
	class BreakOutWall : IGameObject
	{
		private readonly int _side;
		private readonly int _height;
		private readonly int _width;
		private readonly int _buffer;

		#region constructor

		public BreakOutWall(int side, int width, int height)
		{
			_side = side;
			_height = height;
			_width = width;
			_buffer = (width % 78) / 2;
		}

		#endregion

		#region update

		public void UpdatePostion(float x, float y)
		{
			//do nothing, they dont move
		}

		#endregion

		#region draw

		public void Draw()
		{
			SystemMain.GameSpriteBatch.Draw(SystemMain.TexturePackage["Wall"], new Rectangle(_side * (_width - _buffer), 0, _buffer, _height), Color.White);
		}

		#endregion

		#region get methods

		public float GetX()
		{
			return _side * (_width - _buffer);
		}

		public float GetY()
		{
			return 0;
		}

		#endregion
	}
}