using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Utils;

namespace XNASystem.Interfaces
{
	public interface IScreen
	{
		void Update();

		void Draw();
	}
}