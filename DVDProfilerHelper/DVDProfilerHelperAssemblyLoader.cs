using System;
using System.IO;
using System.Reflection;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class DVDProfilerHelperAssemblyLoader
    {
        private static bool _isRegistered;

        static DVDProfilerHelperAssemblyLoader()
        {
            _isRegistered = false;
        }

        public static void Load()
        {
            if (!_isRegistered)
            {
                AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;

                _isRegistered = true;
            }
        }

        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            const string ExtensionAssemblyFileName = "System.Resources.Extensions";

            if (args?.Name.StartsWith($"{ExtensionAssemblyFileName},") == true)
            {
                var thisFile = new FileInfo(typeof(DVDProfilerHelperAssemblyLoader).Assembly.Location);

                var extensionsFile = new FileInfo(Path.Combine(thisFile.DirectoryName, $"{ExtensionAssemblyFileName}.dll"));

                if (extensionsFile.Exists)
                {
                    var extensionsAssembly = Assembly.LoadFrom(extensionsFile.FullName);

                    return extensionsAssembly;
                }
            }

            return null;
        }
    }
}