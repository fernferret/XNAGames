using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNASystem.ShooterGame
{
	class ShooterLevel
	{
		private List<ShooterHerd> _herdList;

		public ShooterLevel()
		{
			_herdList = new List<ShooterHerd>();
		}

		public void AddHerd(ShooterHerd h)
		{
			_herdList.Add(h);
		}

		public List<ShooterHerd> GetHerdList()
		{
			return _herdList;
		}
	}
}
