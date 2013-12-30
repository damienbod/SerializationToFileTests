using System;
using System.Diagnostics;
using System.IO;
using SerializationToFiles.Dtos;
using ServiceStack.Text;

namespace SerializationToFiles.Serializers
{
    public class FileAccessServiceStackJson : FileAccess
    {
        public static void WriteReadServiceStackJson(string filePath)
        {
            var stopwatchTotal = new Stopwatch();
            var stopwatchWrite = new Stopwatch();
            var stopwatchRead = new Stopwatch();
            stopwatchTotal.Start();
            stopwatchWrite.Start();
            long length = WriteServiceStackJson(filePath);
            stopwatchWrite.Stop();
            stopwatchRead.Start();
            ReadServiceStackJson(filePath);
            stopwatchRead.Stop();
            stopwatchTotal.Stop();

            ExcelResultsReadAndWrite.AppendFormat("{0},", stopwatchTotal.ElapsedMilliseconds);
            ExcelResultsRead.AppendFormat("{0},", stopwatchRead.ElapsedMilliseconds);
            ExcelResultsWrite.AppendFormat("{0},", stopwatchWrite.ElapsedMilliseconds);
            ExcelResultsSize.AppendFormat("{0},", length);

            Console.WriteLine("ServiceStackJson \t R/W:{0} \t R:{2} \t W:{1} \t Size:{3}",
                stopwatchTotal.ElapsedMilliseconds, stopwatchWrite.ElapsedMilliseconds,
                stopwatchRead.ElapsedMilliseconds,
                length);

            //File.Delete(filePath);
        }

        private static long WriteServiceStackJson(string path)
        {
            FileStream fs = File.OpenWrite(path);
            JsonSerializer.SerializeToStream(GetTestObjects(), fs);
            fs.Close();

            return new FileInfo(path).Length;
        }

        private static void ReadServiceStackJson(string path)
        {
            FileStream fs = File.OpenRead(path);
            SimpleTransferProtobuf simpleTransferProtobuf;
            simpleTransferProtobuf = ServiceStack.Text.JsonSerializer.DeserializeFromStream<SimpleTransferProtobuf>(fs);
        }
    }
}
