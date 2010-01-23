using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNASystem.BreakOut
{
	class BreakOutBall
	{
		private int _xPosition;
		private int _yPosition;
		private int _xVelocity;
		private int _yVelocity;
		private int _spin;
		private const double Effectiveness = 0.075;

		public BreakOutBall(int xPosition, int yPosition, int xVelocity, int yVelocity)
		{
			_xPosition = xPosition;
			_yPosition = yPosition;
			_xVelocity = xVelocity;
			_yVelocity = yVelocity;
		}

		public void SetSpin(int spin)
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
		}
	}
}
