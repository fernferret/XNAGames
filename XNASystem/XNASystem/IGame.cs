using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNASystem
{
	interface IGame
	{
		void AdvanceLevel();
		void ResetGame();
		List<PowerUp> GetPowerUps();
		void Draw();
		void FinishGame();
		void StartGame();
		int GetLevelScore();
		void ResetScore();
	}
}
