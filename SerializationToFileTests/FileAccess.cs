using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using ProtoBuf;
using SerializationToFiles.Dtos;

namespace SerializationToFiles
{
    public static class FileAccess
    {
        private static readonly SimpleTransferProtobuf _simpleTransferProtobuf =
            TestObjects.CreateSimpleTransferProtobufWithNChildren(500);

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

            Console.WriteLine("Protobuf \t R/W:{0} \t R:{2} \t W:{1} \t Size:{3}",
                stopwatchTotal.ElapsedMilliseconds, stopwatchWrite.ElapsedMilliseconds,
                stopwatchRead.ElapsedMilliseconds,
                length);

            //File.Delete(filePath);
        }

        public static void WriteReadNewtonsoftFileJson(string filePath)
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
            Console.WriteLine("NewtonsoftJson \t R/W:{0} \t R:{2} \t W:{1} \t Size:{3}",
                stopwatchTotal.ElapsedMilliseconds, stopwatchWrite.ElapsedMilliseconds,
                stopwatchRead.ElapsedMilliseconds,
                length);

            //File.Delete(filePath);
        }

        public static void WriteReadNewtonsoftFileBson(string filePath)
        {
            Stopwatch stopwatchTotal = new Stopwatch();
            Stopwatch stopwatchWrite = new Stopwatch();
            Stopwatch stopwatchRead = new Stopwatch();
            stopwatchTotal.Start();
            stopwatchWrite.Start();
            long length = WriteNewtonsoftBson(filePath);
            stopwatchWrite.Stop();
            stopwatchRead.Start();
            ReadNewtonsoftBson(filePath);
            stopwatchRead.Stop();
            stopwatchTotal.Stop();
            Console.WriteLine("NewtonsoftBson \t R/W:{0} \t R:{2} \t W:{1} \t Size:{3}",
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

        private static long WriteNewtonsoftBson(string path)
        {
            FileStream _fs = File.OpenWrite(path);
            using (BsonWriter _bsonWriter = new BsonWriter(_fs) { CloseOutput = false })
            {
                Newtonsoft.Json.JsonSerializer _jsonSerializer = new JsonSerializer();
                _jsonSerializer.Serialize(_bsonWriter, _simpleTransferProtobuf);
                _bsonWriter.Flush();
            }
            _fs.Close();
            return new FileInfo(path).Length;
        }

        private static void ReadNewtonsoftBson(string path)
        {
            FileStream fs = File.OpenRead(path);
            SimpleTransferProtobuf simpleTransferProtobuf;
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (var reader = new BsonReader(fs))
            {               
                simpleTransferProtobuf = serializer.Deserialize <SimpleTransferProtobuf>(reader);
            }
        }
    }
}
