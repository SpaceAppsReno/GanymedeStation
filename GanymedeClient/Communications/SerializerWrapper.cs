using System.IO;
using System.Runtime.Serialization.Json;

namespace Ganymede.Communications
{
    internal static class SerializerWrapper
    {
        public static BaseStation DeserializeBaseStationFromJson(Stream stream)
        {
            var serializer = new DataContractJsonSerializer(typeof(BaseStation));

            return (BaseStation)serializer.ReadObject(stream);
        }

        public static Pod DeserializePodFromJson(Stream stream)
        {
            var serializer = new DataContractJsonSerializer(typeof(Pod));

            return (Pod)serializer.ReadObject(stream);
        }
        
        public static Stream SerializeBaseStationToJson(BaseStation objectToSerialize)
        {
            var serializer = new DataContractJsonSerializer(typeof(BaseStation));

            var stream = new MemoryStream();
            serializer.WriteObject(stream, objectToSerialize);

            return stream;
        }

        public static Stream SerializePodToJson(Pod objectToSerialize)
        {
            var serializer = new DataContractJsonSerializer(typeof(Pod));

            var stream = new MemoryStream();
            serializer.WriteObject(stream, objectToSerialize);

            return stream;
        }

        public static T DeserializeFromJson<T>(Stream stream)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            stream.Position = 0;
            return (T)serializer.ReadObject(stream);
        }

        public static Stream SerializeToJson<T>(T objectToSerialize)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            var stream = new MemoryStream();
            serializer.WriteObject(stream, objectToSerialize);

            return stream;
        }
    }
}
