using System;

namespace XNASystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SystemMain game = new SystemMain())
            {
                game.Run();
            }
        }
    }
}
