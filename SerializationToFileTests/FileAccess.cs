using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProtoBuf;
using SerializationToFiles.Dtos;

namespace SerializationToFiles
{
    public static class FileAccess
    {
        private static readonly SimpleTransferProtobuf _simpleTransferProtobuf =
            TestObjects.CreateSimpleTransferProtobufWithNChildren(5000);

        public static void WriteReadProtobufFile(string filePath)
        {
            Stopwatch stopwatchTotal = new Stopwatch();
            Stopwatch stopwatchWrite = new Stopwatch();
            Stopwatch stopwatchRead = new Stopwatch();
            stopwatchTotal.Start();
            stopwatchWrite.Start();
            long length = WriteProtobuf(filePath);
            stopwatchWrite.Stop();
            stopwatchRead.Start();
            ReadProtobuf(filePath);
            stopwatchRead.Stop();
            stopwatchTotal.Stop();
            Console.WriteLine("Protobuf   R/W: {0}, R:{2}, W:{1}, Size:{3}",
                stopwatchTotal.ElapsedMilliseconds, stopwatchWrite.ElapsedMilliseconds,
                stopwatchRead.ElapsedMilliseconds,
                length);

            //File.Delete(filePath);
        }

        public static void WriteReadNewtonsoftFile(string filePath)
        {
            Stopwatch stopwatchTotal = new Stopwatch();
            Stopwatch stopwatchWrite = new Stopwatch();
            Stopwatch stopwatchRead = new Stopwatch();
            stopwatchTotal.Start();
            stopwatchWrite.Start();
            long length = WriteNewtonsoftJson(filePath);
            stopwatchWrite.Stop();
            stopwatchRead.Start();
            ReadNewtonsoftJson(filePath);
            stopwatchRead.Stop();
            stopwatchTotal.Stop();
            Console.WriteLine("Newtonsoft R/W: {0}, R:{2}, W:{1} Size:{3}",
                stopwatchTotal.ElapsedMilliseconds, stopwatchWrite.ElapsedMilliseconds,
                stopwatchRead.ElapsedMilliseconds,
                length);

            //File.Delete(filePath);
        }

        private static long WriteProtobuf(string path)
        {
            using (Stream file = File.Create(path))
            {
                Serializer.Serialize(file, _simpleTransferProtobuf);
                file.Close();
            }

            return  new FileInfo(path).Length;
        }

        private static void ReadProtobuf(string path)
        {
            SimpleTransferProtobuf simpleTransferProtobuf;
            using (Stream file = File.OpenRead(path))
            {
                simpleTransferProtobuf = Serializer.Deserialize<SimpleTransferProtobuf>(file);
            }
        }

        private static long WriteNewtonsoftJson(string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, _simpleTransferProtobuf);
            }

            return new FileInfo(path).Length;
        }

        private static void ReadNewtonsoftJson(string path)
        {
            SimpleTransferProtobuf simpleTransferProtobuf;
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                simpleTransferProtobuf = (SimpleTransferProtobuf)serializer.Deserialize(file, typeof(SimpleTransferProtobuf));
            }
        }
    }
}
