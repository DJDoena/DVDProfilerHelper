using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public class VersionInfos
    {
        public string DVDProfilerVersion;

        public VersionInfo[] VersionInfoList;

        public void Serialize(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var xmlWriter = new XmlTextWriter(fs, Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;

                    xmlWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                    xmlWriter.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"versions.xsl\"");

                    DVDProfilerSerializer<VersionInfos>.Serialize(xmlWriter, this);
                }
            }
        }
    }

    [DebuggerDisplay("Name={ProgramName}, Version={ProgramVersion}")]
    public class VersionInfo
    {
        public string ProgramName;

        public string ProgramVersion;

        public DateTime Date;
    }
}