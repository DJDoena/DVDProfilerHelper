using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoenaSoft.DVDProfiler.DVDProfilerHelper.Properties;
using DoenaSoft.ToolBox.Generics;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class OnlineAccess
    {
        public static void Init(string company, string product) => RegistryAccess.Init(company, product);

        public static void CheckForNewVersion(string url, IWin32Window parent, string linkAnchor, Assembly assembly)
            => CheckForNewVersion(url, parent, linkAnchor, assembly, false);

        public static void CheckForNewVersion(string url, IWin32Window parent, string linkAnchor, Assembly assembly, bool silently)
            => CheckForNewVersionAsync(url, parent, linkAnchor, assembly, silently).GetAwaiter().GetResult();

        private static async Task CheckForNewVersionAsync(string url, IWin32Window parent, string linkAnchor, Assembly assembly, bool silently)
        {
            if (parent == null)
            {
                parent = new WindowHandle();
            }

            HttpResponseMessage webResponse = null;
            try
            {
                webResponse = await GetSystemSettingsHttpResponse(url);

                using (var stream = await webResponse.Content.ReadAsStreamAsync())
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

        public static async Task<HttpResponseMessage> GetHttpResponse(string targetUrl)
        {
            var handler = CreateHandler();

            try
            {
                using (var client = new HttpClient(handler))
                {
                    var httpResponse = await client.GetAsync(targetUrl);

                    return httpResponse;
                }
            }
            catch (WebException)
            {
                throw;
            }
        }

        public static async Task<HttpResponseMessage> GetSystemSettingsHttpResponse(string targetUrl, CultureInfo cultureInfo = null)
        {
            var handler = CreateSystemSettingsHandler();

            try
            {
                using (var client = new HttpClient(handler))
                {
                    if (cultureInfo != null)
                    {
                        var acceptLanguage = cultureInfo.TwoLetterISOLanguageName.ToLower();

                        client.DefaultRequestHeaders.Add("Accept-Language", acceptLanguage);
                    }

                    var httpResponse = await client.GetAsync(targetUrl);

                    return httpResponse;
                }
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

                        var httpResponse = await GetSystemSettingsHttpResponse(newTargetUrl, cultureInfo);

                        return httpResponse;
                    }
                }

                throw;
            }
        }

        public static HttpClient CreateHttpClient()
        {
            var handler = CreateHandler();

            return new HttpClient(handler);
        }

        public static HttpClient CreateSystemSettingsHttpClient()
        {
            var handler = CreateSystemSettingsHandler();

            return new HttpClient(handler);
        }

        private static HttpClientHandler CreateHandler()
        {
            var handler = new HttpClientHandler();

            if (RegistryAccess.Proxy)
            {
                handler.Proxy = new WebProxy(RegistryAccess.Server, RegistryAccess.Port);

                if (RegistryAccess.WindowsAuthentication)
                {
                    handler.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                }
                else if (RegistryAccess.CustomAuthentication)
                {
                    if (string.IsNullOrEmpty(RegistryAccess.Domain))
                    {
                        handler.Proxy.Credentials = new NetworkCredential(RegistryAccess.Username, RegistryAccess.Password);
                    }
                    else
                    {
                        handler.Proxy.Credentials = new NetworkCredential(RegistryAccess.Username, RegistryAccess.Password, RegistryAccess.Domain);
                    }
                }
            }

            return handler;
        }

        private static HttpClientHandler CreateSystemSettingsHandler()
        {
            var handler = new HttpClientHandler()
            {
                Proxy = WebRequest.GetSystemWebProxy(),
            };

            handler.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            return handler;
        }
    }
}