using System;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoenaSoft.AbstractionLayer.WebServices;
using DoenaSoft.DVDProfiler.DVDProfilerHelper.Properties;
using DoenaSoft.ToolBox.Generics;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class OnlineAccess
    {
        private static IWebServices _webServices;

        public static void Init(string company, string product, IWebServices webServices = null)
        {
            RegistryAccess.Init(company, product);

            _webServices = webServices ?? new WebServices();
        }

        public static void CheckForNewVersion(string url, IWin32Window parent, string linkAnchor, Assembly assembly) => CheckForNewVersion(url, parent, linkAnchor, assembly, false);

        public static void CheckForNewVersion(string url, IWin32Window parent, string linkAnchor, Assembly assembly, bool silently)
        {
            if (parent == null)
            {
                parent = new WindowHandle();
            }

            IWebResponse webResponse = null;
            try
            {
                webResponse = GetSystemSettingsWebResponseAsync(url).GetAwaiter().GetResult();

                using (var stream = webResponse.GetResponseStream())
                {
                    var versionInfos = Serializer<VersionInfos>.Deserialize(stream);

                    if ((versionInfos.VersionInfoList != null) && (versionInfos.VersionInfoList.Length > 0))
                    {
                        foreach (var versionInfo in versionInfos.VersionInfoList)
                        {
                            var currentName = ((AssemblyProductAttribute)assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), true)[0]).Product;

                            if (versionInfo.ProgramName == currentName)
                            {
                                var currentVersion = ((AssemblyFileVersionAttribute)assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true)[0]).Version;

                                if (new Version(versionInfo.ProgramVersion) > new Version(currentVersion))
                                {
                                    using (var form = new NewVersionAvailableForm(currentVersion, versionInfo.ProgramVersion, linkAnchor))
                                    {
                                        form.ShowDialog(parent);

                                        return;
                                    }
                                }
                            }
                        }
                    }
                }

                if (silently == false)
                {
                    MessageBox.Show(parent, Resources.NoNewVersionAvailable, Resources.InformationHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                try
                {
                    webResponse?.Close();
                    webResponse?.Dispose();
                }
                catch
                {
                }

                if (silently == false)
                {
                    MessageBox.Show(parent, Resources.OnlineConnectionCouldNotBeEstablished, Resources.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static async Task<IWebResponse> GetWebResponseAsync(string targetUrl)
        {
            var proxy = BuildWebProxy();

            var webRequest = _webServices.CreateWebRequest(targetUrl, proxy);

            IWebResponse webResponse;
            try
            {
                webResponse = await webRequest.GetResponseAsync();

                return webResponse;
            }
            catch (WebException)
            {
                throw;
            }
        }

        public static async Task<IWebResponse> GetSystemSettingsWebResponseAsync(string targetUrl, CultureInfo cultureInfo = null)
        {
            var proxy = BuildSystemWebProxy();

            var webRequest = _webServices.CreateWebRequest(targetUrl, proxy, cultureInfo);

            try
            {
                var webResponse = await webRequest.GetResponseAsync();

                return webResponse;
            }
            catch (WebException webEx)
            {
                if (webEx.Message?.Contains("308") == true)
                {
                    var redirectTo = webEx.Response?.Headers["Location"];

                    if (!string.IsNullOrEmpty(redirectTo))
                    {
                        var targetUri = new Uri(targetUrl);

                        var newTargetUrl = $"{targetUri.Scheme}://{targetUri.Host}{redirectTo}";

                        var webResponse = await GetSystemSettingsWebResponseAsync(newTargetUrl, cultureInfo);

                        return webResponse;
                    }
                }

                throw;
            }
        }

        public static IWebClient CreateWebClient()
        {
            var proxy = BuildWebProxy();

            var webClient = _webServices.CreateWebClient(proxy);

            return webClient;
        }

        public static IWebClient CreateSystemSettingsWebClient()
        {
            var proxy = BuildSystemWebProxy();

            var webClient = _webServices.CreateWebClient(proxy);

            return webClient;
        }

        private static IWebProxy BuildWebProxy()
        {
            IWebProxy proxy = null;
            if (RegistryAccess.Proxy)
            {
                proxy = new WebProxy(RegistryAccess.Server, RegistryAccess.Port);

                if (RegistryAccess.WindowsAuthentication)
                {
                    proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                }
                else if (RegistryAccess.CustomAuthentication)
                {
                    if (string.IsNullOrEmpty(RegistryAccess.Domain))
                    {
                        proxy.Credentials = new NetworkCredential(RegistryAccess.Username, RegistryAccess.Password);
                    }
                    else
                    {
                        proxy.Credentials = new NetworkCredential(RegistryAccess.Username, RegistryAccess.Password, RegistryAccess.Domain);
                    }
                }
            }

            return proxy;
        }

        private static IWebProxy BuildSystemWebProxy()
        {
            var proxy = WebRequest.GetSystemWebProxy();

            proxy.Credentials = CredentialCache.DefaultNetworkCredentials;

            return proxy;
        }
    }
}