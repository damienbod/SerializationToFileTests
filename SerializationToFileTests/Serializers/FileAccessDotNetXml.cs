using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using SerializationToFiles.Dtos;

namespace SerializationToFiles.Serializers
{
    public class FileAccessDotNetXml : FileAccess
    {
        public static void WriteReadDotNetXml(string filePath)
        {
            var stopwatchTotal = new Stopwatch();
            var stopwatchWrite = new Stopwatch();
            var stopwatchRead = new Stopwatch();
            stopwatchTotal.Start();
            stopwatchWrite.Start();
            long length = WriteDotNetXml(filePath);
            stopwatchWrite.Stop();
            stopwatchRead.Start();
            ReadDotNetXml(filePath);
            stopwatchRead.Stop();
            stopwatchTotal.Stop();

            ExcelResultsReadAndWrite.AppendFormat("{0}", stopwatchTotal.ElapsedMilliseconds);
            ExcelResultsRead.AppendFormat("{0}", stopwatchRead.ElapsedMilliseconds);
            ExcelResultsWrite.AppendFormat("{0}", stopwatchWrite.ElapsedMilliseconds);
            ExcelResultsSize.AppendFormat("{0}", length);

            Console.WriteLine("DotNet Xml \t\t R/W:{0} \t R:{2} \t W:{1} \t Size:{3}",
                stopwatchTotal.ElapsedMilliseconds, stopwatchWrite.ElapsedMilliseconds,
                stopwatchRead.ElapsedMilliseconds,
                length);

            //File.Delete(filePath);
        }

        private static long WriteDotNetXml(string path)
        {
            var serializer = new XmlSerializer(typeof(SimpleTransferProtobuf));
            TextWriter tw = new StreamWriter(path);
            serializer.Serialize(tw, GetTestObjects());
            tw.Close();

            return new FileInfo(path).Length;
        }

        private static void ReadDotNetXml(string path)
        {
            var serializer = new XmlSerializer(typeof(SimpleTransferProtobuf));
            TextReader tr = new StreamReader(path);
            var simpleTransferProtobuf = (SimpleTransferProtobuf)serializer.Deserialize(tr);
            tr.Close();

        }
    }
}
