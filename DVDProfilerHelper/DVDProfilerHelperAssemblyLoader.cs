using System;
using System.IO;
using System.Reflection;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class DVDProfilerHelperAssemblyLoader
    {
        public static void Load()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
        }

        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            const string ExtensionAssemblyFileName = "System.Resources.Extensions";

            if (args?.Name.StartsWith($"{ExtensionAssemblyFileName},") == true)
            {
                var thisAssemblyFile = new FileInfo(typeof(DVDProfilerHelperAssemblyLoader).Assembly.Location);

                var extensionsAssemblyFile = new FileInfo(Path.Combine(thisAssemblyFile.DirectoryName, $"{ExtensionAssemblyFileName}.dll"));

                if (File.Exists(extensionsAssemblyFile.FullName))
                {
                    var extensionsAssembly = Assembly.LoadFrom(extensionsAssemblyFile.FullName);

                    return extensionsAssembly;
                }
            }

            return null;
        }
    }
}