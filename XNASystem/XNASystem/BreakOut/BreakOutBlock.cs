using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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

	class BreakOutBlock
	{
		private readonly int _xPosition;
		private readonly int _yPosition;
		private readonly Blocktype _type;

		public BreakOutBlock(int xPosition, int yPosition, Blocktype type)
		{
			_xPosition = xPosition;
			_yPosition = yPosition;
			_type = type;
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
	}

	#endregion

}
