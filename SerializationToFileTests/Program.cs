using System;

namespace SerializationToFiles
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                FileAccess.WriteReadProtobufFile("./fileProtobuf.txt");
                FileAccess.WriteReadNewtonsoftFile("./jsonNewtonsoft.txt");
                Console.ReadKey();
            }
            
        }
    }
}
