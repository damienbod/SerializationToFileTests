using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using ProtoBuf;
using SerializationToFiles.Dtos;

namespace SerializationToFiles
{
    public static class FileAccess
    {
        public static StringBuilder ExcelResultsReadAndWrite = new StringBuilder();
        public static StringBuilder ExcelResultsRead = new StringBuilder();
        public static StringBuilder ExcelResultsWrite = new StringBuilder();
        public static StringBuilder ExcelResultsSize = new StringBuilder();
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

            ExcelResultsReadAndWrite.AppendFormat("{0},", stopwatchTotal.ElapsedMilliseconds);
            ExcelResultsRead.AppendFormat("{0},", stopwatchRead.ElapsedMilliseconds);
            ExcelResultsWrite.AppendFormat("{0},",stopwatchWrite.ElapsedMilliseconds);
            ExcelResultsSize.AppendFormat("{0},", length);

            Console.WriteLine("Protobuf \t\t R/W:{0} \t R:{2} \t W:{1} \t Size:{3}",
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

            ExcelResultsReadAndWrite.AppendFormat("{0},", stopwatchTotal.ElapsedMilliseconds);
            ExcelResultsRead.AppendFormat("{0},", stopwatchRead.ElapsedMilliseconds);
            ExcelResultsWrite.AppendFormat("{0},", stopwatchWrite.ElapsedMilliseconds);
            ExcelResultsSize.AppendFormat("{0},", length);

            Console.WriteLine("NewtonsoftBson \t\t R/W:{0} \t R:{2} \t W:{1} \t Size:{3}",
                stopwatchTotal.ElapsedMilliseconds, stopwatchWrite.ElapsedMilliseconds,
                stopwatchRead.ElapsedMilliseconds,
                length);

            //File.Delete(filePath);
        }

        public static void WriteReadServiceStackJson(string filePath)
        {
            Stopwatch stopwatchTotal = new Stopwatch();
            Stopwatch stopwatchWrite = new Stopwatch();
            Stopwatch stopwatchRead = new Stopwatch();
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

        public static void WriteReadDotNetXml(string filePath)
        {
            Stopwatch stopwatchTotal = new Stopwatch();
            Stopwatch stopwatchWrite = new Stopwatch();
            Stopwatch stopwatchRead = new Stopwatch();
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
            FileStream fs = File.OpenWrite(path);
            using (BsonWriter _bsonWriter = new BsonWriter(fs) { CloseOutput = false })
            {
                Newtonsoft.Json.JsonSerializer _jsonSerializer = new JsonSerializer();
                _jsonSerializer.Serialize(_bsonWriter, _simpleTransferProtobuf);
                _bsonWriter.Flush();
            }
            fs.Close();
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

        private static long WriteServiceStackJson(string path)
        {
            FileStream fs = File.OpenWrite(path);
            ServiceStack.Text.JsonSerializer.SerializeToStream(_simpleTransferProtobuf, fs);
            fs.Close();

            return new FileInfo(path).Length;
        }

        private static void ReadServiceStackJson(string path)
        {
            FileStream fs = File.OpenRead(path);
            SimpleTransferProtobuf simpleTransferProtobuf;
            simpleTransferProtobuf = ServiceStack.Text.JsonSerializer.DeserializeFromStream<SimpleTransferProtobuf>(fs);
        }


        private static long WriteDotNetXml(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SimpleTransferProtobuf));
            TextWriter tw = new StreamWriter(path);
            serializer.Serialize(tw, _simpleTransferProtobuf);
            tw.Close(); 

            return new FileInfo(path).Length;
        }

        private static void ReadDotNetXml(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SimpleTransferProtobuf));
            TextReader tr = new StreamReader(path);
            SimpleTransferProtobuf simpleTransferProtobuf = (SimpleTransferProtobuf)serializer.Deserialize(tr);
            tr.Close(); 

        }
    }
}
