using System.IO;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public class Utf8StringWriter : StringWriter
    {
        public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
    }
}