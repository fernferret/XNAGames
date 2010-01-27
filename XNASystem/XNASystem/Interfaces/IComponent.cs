using System;

namespace XNASystem.Interfaces
{
	interface IComponent<T>
	{
		String GetTitle();
		void AddItem(T item);
		bool Reset();
		Status GetStatus();
	}
}