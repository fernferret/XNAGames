using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace XNASystem.Interfaces
{
	interface IGameObject
	{
		void UpdatePostion(float x, float y);

		void Draw();

		float GetX();

		float GetY();
	}
}