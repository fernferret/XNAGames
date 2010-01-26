using System;

namespace XNASystem.Interfaces
{
	interface IComponent<T>
	{
		int GetItemCount();
		String GetTitle();
		T GetOpenItem(bool advance);
		void AddItem(T item);
		bool Reset();
		Status GetStatus();
	}
}