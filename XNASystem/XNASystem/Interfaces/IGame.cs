﻿using System.Collections.Generic;

namespace XNASystem.Interfaces
{
	interface IGame
	{
		void AdvanceLevel();
		void ResetGame();
		List<PowerUp> GetPowerUps();
		void FinishGame();
		void StartGame();
		int GetLevelScore();
		void ResetScore();
	}
}