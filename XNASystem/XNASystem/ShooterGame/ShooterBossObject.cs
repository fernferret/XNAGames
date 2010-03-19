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

		private static readonly List<String> StandardSprites = new List<String> { "Boss", "BossAlt" };
		private static readonly List<String> DeadSprites = new List<String> { "ShooterBossExplosion1A", "ShooterBossExplosion3A", "ShooterBossExplosion4A", "ShooterBossExplosion5A", 
			"ShooterBossExplosion6A", "ShooterBossExplosion7A", "ShooterBossExplosion8A", "ShooterBossExplosion9A", "ShooterBossExplosion10A", "ShooterBossExplosion11A", 
			"ShooterBossExplosion12A", "ShooterBossExplosion13A", "ShooterBossExplosion14A", "ShooterBossExplosion15A", "ShooterBossExplosion16A", "ShooterBossExplosion17A", 
			"ShooterBossExplosion18A", "ShooterBossExplosion19A", "ShooterBossExplosion20A", "ShooterBossExplosion21A", "ShooterBossExplosion22A", "ShooterBossExplosion23A", 
			"ShooterBossExplosion24A", "ShooterBossExplosion25A", "ShooterBossExplosion26A", "ShooterBossExplosion27A", "ShooterBossExplosion28A", "ShooterBossExplosion29A", 
			"ShooterBossExplosion30A", "ShooterBossExplosion31A", "ShooterBossExplosion32A", "ShooterBossExplosion33A"};
		private static readonly List<String> PainSprites = new List<String> { "BossPain" };

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
		public override void Hurt()
		{
			SystemMain.SoundOofInstance.Play();
			RemoveAllSpritesToDraw();
			AddSpritesToDraw(_painSprites);
		}

		public override void Kill()
		{
			SystemMain.SoundNooooInstance.Play();
			RemoveAllSpritesToDraw();
			AddSpritesToDraw(_deadSprites);
			_isDying = true;
		}
	}
}
