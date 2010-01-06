using System;
using System.Collections.Generic;

namespace XNASystem
{
    interface IComponent<T>
    {
        int GetItemCount();
        String GetTitle();
        T GetOpenItem();
        void AddItem(T item);
        bool Reset();
        Status GetStatus();
    }
}
