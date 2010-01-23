using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNASystem.BreakOut
{
	class BreakOutLevel
	{
		private readonly string _name;
		private readonly List<BreakOutBlock> _blocks;

		public BreakOutLevel(String name)
		{
			_name = name;
		}

		public string Name
		{
			get { return _name; }
		}

		public void AddBlock(BreakOutBlock block)
		{
			if (block != null)
			{
				//removes any blocks occupying the same location as the block being added
				foreach(b in _blocks)
				{
					if(block.XPosition == b.XPosition)
					{
						if(block.YPosition == b.YPosition)
						{
							_blocks.Remove(b);
						}
					}
				}

				_blocks.Add(block);
			}
		}
	}
}