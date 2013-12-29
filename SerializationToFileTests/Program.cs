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
                FileAccess.WriteReadNewtonsoftFileJson("./jsonNewtonsoft.txt");
                // DOES NOT WORK
                FileAccess.WriteReadNewtonsoftFileBson("./bsonNewtonsoft.txt");
                Console.ReadKey();
            }
            
        }
    }
}
