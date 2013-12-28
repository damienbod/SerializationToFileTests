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
            string test100Chars = "qwertzuiop1234567890qwertzuiop1234567890qwertzuiop1234567890qwertzuiop1234567890qwertzuiop12345667890";
            var simpleTransferProtobuf = new SimpleTransferProtobuf();
            simpleTransferProtobuf.String1 = test100Chars;
            simpleTransferProtobuf.String2 = test100Chars;
            simpleTransferProtobuf.String3 = test100Chars;
            simpleTransferProtobuf.String4 = test100Chars;
            simpleTransferProtobuf.String5 = test100Chars;
            simpleTransferProtobuf.String6 = test100Chars;
            simpleTransferProtobuf.String7 = test100Chars;

            simpleTransferProtobuf.Int1 = 1;
            simpleTransferProtobuf.Int2 = 22;
            simpleTransferProtobuf.Int3 = 333;
            simpleTransferProtobuf.Int4 = 4444;
            simpleTransferProtobuf.Int5 = 55555;
            simpleTransferProtobuf.Int7 = 666666;

            simpleTransferProtobuf.Double1 = 1.1;
            simpleTransferProtobuf.Double2 = 22.22;
            simpleTransferProtobuf.Double3 = 3333.33;
            simpleTransferProtobuf.Double4 = 4444.4444;
            simpleTransferProtobuf.Double5 = 55555.55;
            simpleTransferProtobuf.Double6 = 6666.666666;
            simpleTransferProtobuf.Double7 = 7.7777777;

            simpleTransferProtobuf.SimpleChildTransferProtobufObject = CreateSimpleChildTransferProtobuf();
            simpleTransferProtobuf.SimpleChildTransferProtobufList = new List<SimpleChildTransferProtobuf>();
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
