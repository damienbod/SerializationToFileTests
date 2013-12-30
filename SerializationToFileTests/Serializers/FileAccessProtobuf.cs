using System;
using System.Diagnostics;
using System.IO;
using ProtoBuf;
using SerializationToFiles.Dtos;

namespace SerializationToFiles.Serializers
{
    public class FileAccessProtobuf : FileAccess
    {
        public static void WriteReadProtobufFile(string filePath)
        {
            var stopwatchTotal = new Stopwatch();
            var stopwatchWrite = new Stopwatch();
            var stopwatchRead = new Stopwatch();
            stopwatchTotal.Start();
            stopwatchWrite.Start();
            long length = WriteProtobuf(filePath);
            stopwatchWrite.Stop();
            stopwatchRead.Start();
            ReadProtobuf(filePath);
            stopwatchRead.Stop();
            stopwatchTotal.Stop();

            ExcelResultsReadAndWrite.AppendFormat("{0},", stopwatchTotal.ElapsedMilliseconds);
            ExcelResultsRead.AppendFormat("{0},", stopwatchRead.ElapsedMilliseconds);
            ExcelResultsWrite.AppendFormat("{0},", stopwatchWrite.ElapsedMilliseconds);
            ExcelResultsSize.AppendFormat("{0},", length);

            Console.WriteLine("Protobuf \t\t R/W:{0} \t R:{2} \t W:{1} \t Size:{3}",
                stopwatchTotal.ElapsedMilliseconds, stopwatchWrite.ElapsedMilliseconds,
                stopwatchRead.ElapsedMilliseconds,
                length);

            //File.Delete(filePath);
        }

        private static long WriteProtobuf(string path)
        {
            using (Stream file = File.Create(path))
            {
                Serializer.Serialize(file, GetTestObjects());
                file.Close();
            }

            return new FileInfo(path).Length;
        }

        private static void ReadProtobuf(string path)
        {
            SimpleTransferProtobuf simpleTransferProtobuf;
            using (Stream file = File.OpenRead(path))
            {
                simpleTransferProtobuf = Serializer.Deserialize<SimpleTransferProtobuf>(file);
            }
        }

    }
}
