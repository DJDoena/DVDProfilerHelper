using System;
using System.Text;
using System.Xml;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class XmlConvertHelper
    {
        public static String GetWindows1252Text(String decoded
            , out String base64Alternate)
        {
            String encoded;
            Encoding encoding;

            encoding = Encoding.GetEncoding(1252);
            encoded = encoding.GetString(encoding.GetBytes(decoded));
            base64Alternate = null;
            if (decoded.Equals(encoded, StringComparison.Ordinal) == false)
            {
                base64Alternate = Convert.ToBase64String(Encoding.UTF8.GetBytes(decoded));
            }
            encoded = EscapeXml(decoded);
            return (encoded);
        }

        internal static String EscapeXml(String decoded)
        {
            XmlDocument doc;
            XmlNode node;
            String encoded;

            doc = new XmlDocument();
            node = doc.CreateElement("root");
            node.InnerText = decoded;
            encoded = node.InnerXml;
            return (encoded);
        }
    }
}