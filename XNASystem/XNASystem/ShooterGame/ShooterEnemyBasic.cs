using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.ShooterGame
{
	class ShooterEnemyBasic : ShooterGameObject
	{

		private const int Width = 50;
		private const int Height = 50;

		private static readonly List<int> StandardSprites = new List<int> { 13, 14 };
		private static readonly List<int> DeadSprites = new List<int> { 15, 16, 17, 18, 19, 20, 21, 22, 29, 30, 31 };
		private static readonly List <int> PainSprites = new List<int>{ 25 };

		public ShooterEnemyBasic(float xPosition, float yPosition) 
			: base(xPosition, yPosition, Width, Height, (Width/2 + 5), Height + 7, 3, 10, 10, Color.White, 0, 1, StandardSprites, PainSprites, DeadSprites)
		{
		}
	}
}
