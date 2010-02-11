using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.ShooterGame
{
	class ShooterBossObject : ShooterGameObject
	{
		private const int Width = 150;
		private const int Height = 50;

		private static readonly List<int> StandardSprites = new List<int> { 26, 27 };
		private static readonly List<int> DeadSprites = new List<int> { 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 31 };
		private static readonly List<int> PainSprites = new List<int> { 28 };

		public ShooterBossObject(float xPosition, float yPosition)
			: base(xPosition, yPosition, Width, Height, (Width / 2 - 10), Height + 7, 3, 20, 20, Color.White, 15, 50, StandardSprites, PainSprites, DeadSprites)
		{
		}

		public override void UpdatePostion(float x, float y)
		{
			_xPosition += x;

			_collisionBox.Location = new Point((int)_xPosition, (int)_yPosition);

			if (_spriteQueue.Count <= 1)
			{
				AddSpritesToDraw(_standardSprites);
			}
		}
	}
}
