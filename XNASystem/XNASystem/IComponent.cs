using System;
using System.Collections.Generic;

namespace XNASystem
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
