using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using SerializationToFiles.Dtos;

namespace SerializationToFiles.Serializers
{
    public class FileAccessNewtonsoftBson :FileAccess
    {
        public static void WriteReadNewtonsoftFileBson(string filePath)
        {
            var stopwatchTotal = new Stopwatch();
            var stopwatchWrite = new Stopwatch();
            var stopwatchRead = new Stopwatch();
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

        private static long WriteNewtonsoftBson(string path)
        {
            FileStream fs = File.OpenWrite(path);
            using (var bsonWriter = new BsonWriter(fs) { CloseOutput = false })
            {
                var jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(bsonWriter, GetTestObjects());
                bsonWriter.Flush();
            }
            fs.Close();
            return new FileInfo(path).Length;
        }

        private static void ReadNewtonsoftBson(string path)
        {
            FileStream fs = File.OpenRead(path);
            SimpleTransferProtobuf simpleTransferProtobuf;
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (var reader = new BsonReader(fs))
            {
                simpleTransferProtobuf = serializer.Deserialize<SimpleTransferProtobuf>(reader);
            }
        }
    }
}
