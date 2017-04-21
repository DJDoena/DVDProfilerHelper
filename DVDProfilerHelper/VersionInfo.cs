using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    [Serializable()]
    public class VersionInfos
    {
        public String DVDProfilerVersion;

        public VersionInfo[] VersionInfoList;

        public void Serialize(String fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(fs, Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;

                    xmlWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                    xmlWriter.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"versions.xsl\"");

                    Serializer<VersionInfos>.Serialize(xmlWriter, this);
                }
            }
        }
    }

    [Serializable()]
    public class VersionInfo
    {
        public String ProgramName;

        public String ProgramVersion;

        public DateTime Date;
    }
}
