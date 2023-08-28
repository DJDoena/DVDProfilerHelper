using DoenaSoft.DVDProfiler.DVDProfilerHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //OnlineAccess.Init("DoenaSoft.", "CastCrewEdit2");
            OnlineAccess.CheckForNewVersion("http://doena-soft.de/dvdprofiler/3.9.0/versions.xml", null, "CheckForDuplicatesInCastCrewEdit2Cache"
                   , GetType().Assembly, true);
        }
    }

}