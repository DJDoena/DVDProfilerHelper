using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class Serializer<T> where T : class, new()
    {
        private static XmlSerializer s_XmlSerializer;

        private static readonly Encoding DefaultEncoding;

        static Serializer()
        {
            DefaultEncoding = Encoding.UTF8;
        }

        public static XmlSerializer XmlSerializer
        {
            get
            {
                if (s_XmlSerializer == null)
                {
                    s_XmlSerializer = new XmlSerializer(typeof(T));
                }

                return (s_XmlSerializer);
            }
        }

        public static T Deserialize(String fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                T instance = Deserialize(fs);

                return (instance);
            }
        }

        public static T Deserialize(TextReader textReader)
            => ((T)(XmlSerializer.Deserialize(textReader)));

        public static T Deserialize(Stream stream)
            => ((T)(XmlSerializer.Deserialize(stream)));

        public static void Serialize(String fileName
            , T instance
            , Encoding encoding = null)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                Serialize(fs, instance, encoding);
            }
        }

        public static void Serialize(Stream stream
            , T instance
            , Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;

            using (XmlTextWriter xtw = new XmlTextWriter(stream, encoding))
            {
                xtw.Formatting = Formatting.Indented;
                xtw.Namespaces = false;

                Serialize(xtw, instance);
            }
        }

        public static void Serialize(XmlWriter xmlWriter
            , T instance)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            ns.Add(String.Empty, String.Empty);

            XmlSerializer.Serialize(xmlWriter, instance, ns);
        }

        public static T FromString(String text
            , Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;

            using (Stream ms = new MemoryStream(encoding.GetBytes(text)))
            {
                T instance = Deserialize(ms);

                return (instance);
            }
        }

        public static String ToString(T instance
            , Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;

            using (MemoryStream ms = new MemoryStream())
            {
                Serialize(ms, instance, encoding);

                String text = encoding.GetString(ms.ToArray());

                return (text);
            }
        }
    }
}