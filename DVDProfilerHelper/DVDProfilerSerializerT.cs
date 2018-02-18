namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public static class DVDProfilerSerializer<T> where T : class, new()
    {
        private static XmlSerializer _XmlSerializer;

        private static readonly Encoding _DefaultEncoding;

        static DVDProfilerSerializer()
            => _DefaultEncoding = Encoding.UTF8;

        public static XmlSerializer XmlSerializer
        {
            get
            {
                if (_XmlSerializer == null)
                {
                    _XmlSerializer = new XmlSerializer(typeof(T));
                }

                return (_XmlSerializer);
            }
        }

        public static T Deserialize(String fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return (Deserialize(fs));
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
            using (XmlTextWriter xtw = CreateXmlTextWriter(stream, encoding))
            {
                Serialize(xtw, instance);
            }
        }

        public static void Serialize(XmlWriter xmlWriter
            , T instance)
            => XmlSerializer.Serialize(xmlWriter, instance, CreateXmlSerializerNamespaces());

        public static T FromString(String text
            , Encoding encoding = null)
        {
            using (Stream ms = new MemoryStream(EnsureEncoding(encoding).GetBytes(text)))
            {
                return (Deserialize(ms));
            }
        }

        public static String ToString(T instance
            , Encoding encoding = null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serialize(ms, instance, encoding);

                return (EnsureEncoding(encoding).GetString(ms.ToArray()));
            }
        }

        private static XmlTextWriter CreateXmlTextWriter(Stream stream, Encoding encoding)
            => new XmlTextWriter(stream, EnsureEncoding(encoding))
            {
                Formatting = Formatting.Indented,
                Namespaces = false
            };

        private static XmlSerializerNamespaces CreateXmlSerializerNamespaces()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            ns.Add(String.Empty, String.Empty);

            return (ns);
        }

        private static Encoding EnsureEncoding(Encoding encoding)
            => encoding ?? _DefaultEncoding;
    }
}