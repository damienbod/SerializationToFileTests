using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SerializationToFiles.Dtos;

namespace SerializationToFiles.Serializers
{
    public class FileAccessNewtonsoftJson : FileAccess
    {
        public static void WriteReadNewtonsoftFileJson(string filePath)
        {
            var stopwatchTotal = new Stopwatch();
            var stopwatchWrite = new Stopwatch();
            var stopwatchRead = new Stopwatch();
            stopwatchTotal.Start();
            stopwatchWrite.Start();
            long length = WriteNewtonsoftJson(filePath);
            stopwatchWrite.Stop();
            stopwatchRead.Start();
            ReadNewtonsoftJson(filePath);
            stopwatchRead.Stop();
            stopwatchTotal.Stop();

            ExcelResultsReadAndWrite.AppendFormat("{0},", stopwatchTotal.ElapsedMilliseconds);
            ExcelResultsRead.AppendFormat("{0},", stopwatchRead.ElapsedMilliseconds);
            ExcelResultsWrite.AppendFormat("{0},", stopwatchWrite.ElapsedMilliseconds);
            ExcelResultsSize.AppendFormat("{0},", length);

            Console.WriteLine("NewtonsoftJson \t\t R/W:{0} \t R:{2} \t W:{1} \t Size:{3}",
                stopwatchTotal.ElapsedMilliseconds, stopwatchWrite.ElapsedMilliseconds,
                stopwatchRead.ElapsedMilliseconds,
                length);

            //File.Delete(filePath);
        }

        private static long WriteNewtonsoftJson(string path)
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (var sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, GetTestObjects());
            }

            return new FileInfo(path).Length;
        }

        private static void ReadNewtonsoftJson(string path)
        {
            SimpleTransferProtobuf simpleTransferProtobuf;
            using (StreamReader file = File.OpenText(path))
            {
                var serializer = new JsonSerializer();
                simpleTransferProtobuf = (SimpleTransferProtobuf)serializer.Deserialize(file, typeof(SimpleTransferProtobuf));
            }
        }

    }
}
