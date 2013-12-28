using System.Collections.Generic;
using ProtoBuf;

namespace SerializationToFiles.Dtos
{
    [ProtoContract]
    public class SimpleTransferProtobuf
    {
        [ProtoMember(1)]
        public SimpleChildTransferProtobuf SimpleChildTransferProtobufObject { get; set; }

        [ProtoMember(2)]
        public List<SimpleChildTransferProtobuf> SimpleChildTransferProtobufList { get; set; }

        [ProtoMember(3)]
        public string String1 { get; set; }
        [ProtoMember(4)]
        public string String2 { get; set; }
        [ProtoMember(5)]
        public string String3 { get; set; }
        [ProtoMember(6)]
        public string String4 { get; set; }
        [ProtoMember(7)]
        public string String5 { get; set; }
        [ProtoMember(8)]
        public string String6 { get; set; }
        [ProtoMember(9)]
        public string String7 { get; set; }

        [ProtoMember(10)]
        public double Double1 { get; set; }
        [ProtoMember(11)]
        public double Double2 { get; set; }
        [ProtoMember(12)]
        public double Double3 { get; set; }
        [ProtoMember(13)]
        public double Double4 { get; set; }
        [ProtoMember(15)]
        public double Double5 { get; set; }
        [ProtoMember(16)]
        public double Double6 { get; set; }
        [ProtoMember(17)]
        public double Double7 { get; set; }

        [ProtoMember(18)]
        public int Int1 { get; set; }
        [ProtoMember(19)]
        public int Int2 { get; set; }
        [ProtoMember(20)]
        public int Int3 { get; set; }
        [ProtoMember(21)]
        public int Int4 { get; set; }
        [ProtoMember(22)]
        public int Int5 { get; set; }
        [ProtoMember(23)]
        public int Int6 { get; set; }
        [ProtoMember(24)]
        public int Int7 { get; set; }
    }
}

