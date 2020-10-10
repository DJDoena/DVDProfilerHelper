using System;
using System.Text;
using System.Xml;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class XmlConvertHelper
    {
        public static string GetWindows1252Text(string decoded, out string base64Alternate)
        {
            var encoding = Encoding.GetEncoding(1252);

            var encoded = encoding.GetString(encoding.GetBytes(decoded));

            base64Alternate = null;
            if (decoded.Equals(encoded, StringComparison.Ordinal) == false)
            {
                base64Alternate = Convert.ToBase64String(Encoding.UTF8.GetBytes(decoded));
            }

            encoded = EscapeXml(decoded);

            return encoded;
        }

        internal static string EscapeXml(string decoded)
        {
            var doc = new XmlDocument();

            var node = doc.CreateElement("root");

            node.InnerText = decoded;

            var encoded = node.InnerXml;

            return encoded;
        }
    }
}