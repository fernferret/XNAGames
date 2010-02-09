using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.ShooterGame
{
	class ShooterBossObject : ShooterGameObject
	{
		private const int Width = 150;
		private const int Height = 50;

		private static readonly List<int> StandardSprites = new List<int> { 26, 27 };
		private static readonly List<int> DeadSprites = new List<int> { 15, 16, 17, 18, 19, 20, 21, 22, 29, 30, 31 };
		private static readonly List<int> PainSprites = new List<int> { 28 };

		public ShooterBossObject(float xPosition, float yPosition)
			: base(xPosition, yPosition, Width, Height, (Width / 2 - 10), Height + 7, 3, 20, 20, Color.White, 15, StandardSprites, PainSprites, DeadSprites)
		{
		}

		public override void UpdatePostion(float x, float y)
		{
			_xPosition += x;

			if (_spriteQueue.Count <= 1)
			{
				AddSpritesToDraw(_standardSprites);
			}
		}
	}
}
