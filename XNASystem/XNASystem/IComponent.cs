using System;

namespace XNASystem
{
    // Interface that defines Quiz and Booklet
    interface IComponent<T>
    {
        String GetTitle();
        void AddItem(T item);
        bool Reset();
        Status GetStatus();
    }
}