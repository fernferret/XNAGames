using System;

namespace XNASystem
{
    // Interface that defines Quiz and Booklet
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