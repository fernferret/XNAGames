using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using XNASystem.Displays;
using XNASystem.Interfaces;
using XNASystem.Utils;
using XNASystem.Shooter;

namespace XNASystem.ShooterGame
{
	class ShooterHerd : IGameObject
	{
		private const int Width = 755;  //Approximately 16 positions across the screen
		private const int Height = 600;
		private List<ShooterGroup> _groups;

		public ShooterHerd()
		{
		}

		public void AddGroup(ShooterGroup g)
		{
			_groups.Add(g);
		}

		public void KillGroup(ShooterGroup g)
		{
			_groups.Remove(g);
		}

		public void UpdatePostion(float x, float y)
		{
			throw new NotImplementedException();
		}

		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			foreach (ShooterGroup g in _groups)
			{
				g.Draw(spriteBatch, fonts, textures);
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
	}
}
