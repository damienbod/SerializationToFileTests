using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using ServiceStack;

namespace SerializationToFiles
{
    internal class Program
    {
        private static int counter = 1;
        private static void Main(string[] args)
        {
            FileAccess.ExcelResultsReadAndWrite.Append("Protobuf, Newtonsoft Json,Newtonsoft Bson, ServiceStack Json, .Net Xml" +  Environment.NewLine);
            FileAccess.ExcelResultsRead.Append("Protobuf, Newtonsoft Json,Newtonsoft Bson, ServiceStack Json, .Net Xml" + Environment.NewLine);
            FileAccess.ExcelResultsWrite.Append("Protobuf, Newtonsoft Json,Newtonsoft Bson, ServiceStack Json, .Net Xml" + Environment.NewLine);
            FileAccess.ExcelResultsSize.Append("Protobuf, Newtonsoft Json,Newtonsoft Bson, ServiceStack Json, .Net Xml" + Environment.NewLine);

            while (counter < 50)
            {
                Console.WriteLine("Test {0}", counter++);
                FileAccess.WriteReadProtobufFile("./fileProtobuf.txt");
                FileAccess.WriteReadNewtonsoftFileJson("./jsonNewtonsoft.txt");
                FileAccess.WriteReadNewtonsoftFileBson("./bsonNewtonsoft.txt");
                FileAccess.WriteReadServiceStackJson("./serviceStackJson.txt");
                FileAccess.WriteReadDotNetXml("./dotnetXml.txt");

                FileAccess.ExcelResultsReadAndWrite.Append( Environment.NewLine);
                FileAccess.ExcelResultsRead.Append(Environment.NewLine);
                FileAccess.ExcelResultsWrite.Append(Environment.NewLine);
                FileAccess.ExcelResultsSize.Append(Environment.NewLine);
                //Console.ReadKey();
            }

            WriteCsv("ExcelResultsReadAndWrite.txt", FileAccess.ExcelResultsReadAndWrite);
            WriteCsv("ExcelResultsRead.txt", FileAccess.ExcelResultsRead);
            WriteCsv("ExcelResultsWrite.txt", FileAccess.ExcelResultsWrite);
            WriteCsv("ExcelResultsSize.txt", FileAccess.ExcelResultsSize);
        }

        private static void WriteCsv(string path, StringBuilder results)
        {
            File.WriteAllText(path, results.ToString());
        }
    }
}
