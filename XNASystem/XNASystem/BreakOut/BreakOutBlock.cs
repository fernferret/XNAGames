using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;

#region

enum Blocktype
{
	Standard,  //Dead after one hit from the ball
	Invincible,  //never dies
	Strong2,  //Dead after two hits
	Strong3,  //Dead after three hits
	Dead  //No longer affects the ball in any way and not displayed on the screen
}

#endregion

namespace XNASystem.BreakOut
{
	#region

	class BreakOutBlock : IGameObject
	{
		private readonly int _xPosition;
		private readonly int _yPosition;
		private readonly Blocktype _type;
		private readonly Color _color;

		public BreakOutBlock(int xPosition, int yPosition, Blocktype type, Color color)
		{
			_xPosition = xPosition;
			_yPosition = yPosition;
			_type = type;
			_color = color;
		}

		public Blocktype Type
		{
			get { return _type; }
		}

		public int YPosition
		{
			get { return _yPosition; }
		}

		public int XPosition
		{
			get { return _xPosition; }
		}

		public void UpdatePostiion(float x, float y)
		{
			//do nothing
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(textures[5], new Vector2(_xPosition, _yPosition), _color);
			spriteBatch.End();
		}
	}

	#endregion

}
