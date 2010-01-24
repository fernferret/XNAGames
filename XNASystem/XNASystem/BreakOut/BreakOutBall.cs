using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;

namespace XNASystem.BreakOut
{
	class BreakOutBall : IGameObject
	{
		private float _xPosition;
		private float _yPosition;
		private int _xVelocity;
		private int _yVelocity;
		private int _spin;
		private const double Effectiveness = 0.075;

		public BreakOutBall(float xPosition, float yPosition, int xVelocity, int yVelocity)
		{
			_xPosition = xPosition;
			_yPosition = yPosition;
			_xVelocity = xVelocity;
			_yVelocity = yVelocity;
		}

		/*public void SetSpin(int spin)
		{
			_spin = spin;
		}

		public void ReflectXVelocity()
		{
			_xVelocity *= -1;
		}

		public void ReflectYVelocity()
		{
			_yVelocity *= -1;
		}

		//timer must be reset after each time the ball bounces, otherwise, spin will not act correctly
		public void UpdatePosition(int time)
		{
			_xPosition = (int)(_xVelocity*time + Effectiveness*2*_spin*time);
			_yPosition = (int)(_yVelocity*time - Effectiveness*_spin*(time ^ 2));
		}*/

		public void UpdatePostiion(float x, float y)
		{
			_xPosition += x;
			_yPosition += y;
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Draw(textures[6], new Vector2(_xPosition, _yPosition), Color.White);
		}
	}
}
