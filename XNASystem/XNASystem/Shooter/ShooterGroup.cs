using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNASystem.Interfaces;
using XNASystem.Shooter;

namespace XNASystem.ShooterGame
{
	class ShooterGroup : IGameObject
	{
		private List<ShooterEnemy> _enemies;
		public void UpdatePostion(float x, float y)
		{
			throw new NotImplementedException();
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			foreach (ShooterEnemy e in _enemies)
			{
				e.Draw(spriteBatch, fonts, textures);
			}
		}

		public float GetX()
		{
			throw new NotImplementedException();
		}

		public float GetY()
		{
			throw new NotImplementedException();
		}

		//maneuvers
		//public void Loop
	}
}
