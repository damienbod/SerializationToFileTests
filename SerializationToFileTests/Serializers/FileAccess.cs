using System.Text;
using SerializationToFiles.Dtos;

namespace SerializationToFiles.Serializers
{
    public class FileAccess
    {
        public static StringBuilder ExcelResultsReadAndWrite;
        public static StringBuilder ExcelResultsRead;
        public static StringBuilder ExcelResultsWrite;
        public static StringBuilder ExcelResultsSize;

        public static int AmountOfChildObjects = 50;

        protected static SimpleTransferProtobuf GetTestObjects()
        {
            return TestObjects.CreateSimpleTransferProtobufWithNChildren(AmountOfChildObjects);
        }
        
    }
}
