using System;

namespace XNASystem
{
    static class SystemMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SystemMenu menu = new SystemMenu())
            {
                menu.Run();
            }
        }
    }
}