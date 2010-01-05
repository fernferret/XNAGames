namespace XNASystem
{
    static class EntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (var instance = new SystemMain())
            {
                instance.Run();
            }
        }
    }
}