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

		private static readonly List<String> StandardSprites = new List<String> { "EnemyBasic", "EnemyBasicAlt" };
		private static readonly List<String> DeadSprites = new List<String> { "BeginExplode1", "BeginExplode2", "BeginExplode3", "Explode1", "Explode2", "Explode3", "Explode4", "Explode5", "Explode6", "Explode7", "Explode8" };
		private static readonly List<String> PainSprites = new List<String> { "EnemyBasicPain" };

		public ShooterEnemyBasic(float xPosition, float yPosition) 
			: base(xPosition, yPosition, Width, Height, (Width/2 + 5), Height + 7, 3, 10, 10, Color.White, 0, 1, StandardSprites, PainSprites, DeadSprites)
		{
		}
	}
}
