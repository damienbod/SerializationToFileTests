using System.Collections.Generic;
using SerializationToFiles.Dtos;

namespace SerializationToFiles
{
    public static class TestObjects
    {
        public static SimpleTransferProtobuf CreateSimpleTransferProtobufWithNChildren(int amountOfChildren)
        {
            return CreateSimpleTransferProtobuf(amountOfChildren);
        }

        private static SimpleTransferProtobuf CreateSimpleTransferProtobuf(int amountOfChildren)
        {
            const string test100Chars = "qwertzuiop1234567890qwertzuiop1234567890qwertzuiop1234567890qwertzuiop1234567890qwertzuiop12345667890";
            var simpleTransferProtobuf = new SimpleTransferProtobuf
            {
                String1 = test100Chars,
                String2 = test100Chars,
                String3 = test100Chars,
                String4 = test100Chars,
                String5 = test100Chars,
                String6 = test100Chars,
                String7 = test100Chars,
                Int1 = 1,
                Int2 = 22,
                Int3 = 333,
                Int4 = 4444,
                Int5 = 55555,
                Int7 = 666666,
                Double1 = 1.1,
                Double2 = 22.22,
                Double3 = 3333.33,
                Double4 = 4444.4444,
                Double5 = 55555.55,
                Double6 = 6666.666666,
                Double7 = 7.7777777,
                SimpleChildTransferProtobufObject = CreateSimpleChildTransferProtobuf(),
                SimpleChildTransferProtobufList = new List<SimpleChildTransferProtobuf>()
            };

            for (int i = 0; i < amountOfChildren; i++)
            {
                simpleTransferProtobuf.SimpleChildTransferProtobufList.Add(CreateSimpleChildTransferProtobuf());
            }
            return simpleTransferProtobuf;
        }

        private static SimpleChildTransferProtobuf CreateSimpleChildTransferProtobuf()
        {
            const string test100Chars = "qwertzuiop1234567890qwertzuiop1234567890qwertzuiop1234567890qwertzuiop1234567890qwertzuiop12345667890";
            var childTransferProtobuf = new SimpleChildTransferProtobuf
            {
                String1 = test100Chars,
                String2 = test100Chars,
                String3 = test100Chars,
                String4 = test100Chars,
                String5 = test100Chars,
                String6 = test100Chars,
                String7 = test100Chars,
                Int1 = 1,
                Int2 = 22,
                Int3 = 333,
                Int4 = 4444,
                Int5 = 55555,
                Int7 = 666666,
                Double1 = 1.1,
                Double2 = 22.22,
                Double3 = 3333.33,
                Double4 = 4444.4444,
                Double5 = 55555.55,
                Double6 = 6666.666666,
                Double7 = 7.7777777,
                Hello = "hello"
            };

            return childTransferProtobuf;
        }
    }
}
