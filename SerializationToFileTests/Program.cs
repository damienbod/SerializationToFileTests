using System;
using System.IO;
using System.Text;
using SerializationToFiles.Serializers;
using FileAccess = SerializationToFiles.Serializers.FileAccess;

namespace SerializationToFiles
{
    internal class Program
    {
        private static int _counter = 1;
        private static void Main(string[] args)
        {
            const int repeatTestNTimes = 10;
            DoTestForNObjects(50, repeatTestNTimes);
            DoTestForNObjects(500, repeatTestNTimes);
            DoTestForNObjects(5000, repeatTestNTimes);
            DoTestForNObjects(50000, repeatTestNTimes);
            //Console.ReadKey();
        }

        static void DoTestForNObjects(int amount, int repeatTest)
        {
            _counter = 1;
            FileAccess.ExcelResultsReadAndWrite = new StringBuilder();
            FileAccess.ExcelResultsRead = new StringBuilder();
            FileAccess.ExcelResultsWrite = new StringBuilder();
            FileAccess.ExcelResultsSize = new StringBuilder();

            FileAccess.AmountOfChildObjects = amount;
            FileAccess.ExcelResultsReadAndWrite.Append("Protobuf, Newtonsoft Json,Newtonsoft Bson, ServiceStack Json, .Net Xml" + Environment.NewLine);
            FileAccess.ExcelResultsRead.Append("Protobuf, Newtonsoft Json,Newtonsoft Bson, ServiceStack Json, .Net Xml" + Environment.NewLine);
            FileAccess.ExcelResultsWrite.Append("Protobuf, Newtonsoft Json,Newtonsoft Bson, ServiceStack Json, .Net Xml" + Environment.NewLine);
            FileAccess.ExcelResultsSize.Append("Protobuf, Newtonsoft Json,Newtonsoft Bson, ServiceStack Json, .Net Xml" + Environment.NewLine);

            while (_counter < repeatTest)
            {
                Console.WriteLine("Test {0}", _counter++);
                FileAccessProtobuf.WriteReadProtobufFile("./fileProtobuf.txt");
                FileAccessNewtonsoftJson.WriteReadNewtonsoftFileJson("./jsonNewtonsoft.txt");
                FileAccessNewtonsoftBson.WriteReadNewtonsoftFileBson("./bsonNewtonsoft.txt");
                FileAccessServiceStackJson.WriteReadServiceStackJson("./serviceStackJson.txt");
                FileAccessDotNetXml.WriteReadDotNetXml("./dotnetXml.txt");

                FileAccess.ExcelResultsReadAndWrite.Append(Environment.NewLine);
                FileAccess.ExcelResultsRead.Append(Environment.NewLine);
                FileAccess.ExcelResultsWrite.Append(Environment.NewLine);
                FileAccess.ExcelResultsSize.Append(Environment.NewLine);
                //Console.ReadKey();
            }

            WriteCsv("Results/ExcelResultsReadAndWrite" +amount + ".txt", FileAccess.ExcelResultsReadAndWrite);
            WriteCsv("Results/ExcelResultsRead" + amount + ".txt", FileAccess.ExcelResultsRead);
            WriteCsv("Results/ExcelResultsWrite" + amount + ".txt", FileAccess.ExcelResultsWrite);
            WriteCsv("Results/ExcelResultsSize" + amount + ".txt", FileAccess.ExcelResultsSize);
        }
        private static void WriteCsv(string path, StringBuilder results)
        {
            File.WriteAllText(path, results.ToString());
        }
    }
}
