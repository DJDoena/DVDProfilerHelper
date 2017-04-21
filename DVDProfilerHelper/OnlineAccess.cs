using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using DoenaSoft.DVDProfiler.DVDProfilerHelper.Properties;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class OnlineAccess
    {
        public static void Init(String company, String product)
        {
            RegistryAccess.Init(company, product);
        }

        public static void CheckForNewVersion(String url, IWin32Window parent, String linkAnchor, Assembly assembly)
        {
            CheckForNewVersion(url, parent, linkAnchor, assembly, false);
        }

        public static void CheckForNewVersion(String url, IWin32Window parent, String linkAnchor, Assembly assembly, Boolean silently)
        {
            WebResponse webResponse;

            if(parent == null)
            {
                parent = new WindowHandle();
            }
            webResponse = null;
            try
            {
                webResponse = CreateSystemSettingsWebRequest(url);
                using(Stream stream = webResponse.GetResponseStream())
                {
                    VersionInfos versionInfos;

                    versionInfos = Serializer<VersionInfos>.Deserialize(stream);
                    if((versionInfos.VersionInfoList != null) && (versionInfos.VersionInfoList.Length > 0))
                    {
                        foreach(VersionInfo versionInfo in versionInfos.VersionInfoList)
                        {
                            String currentName;

                            currentName = ((AssemblyProductAttribute)((assembly
                                .GetCustomAttributes(typeof(AssemblyProductAttribute), true))[0])).Product;
                            if(versionInfo.ProgramName == currentName)
                            {
                                String currentVersion;

                                currentVersion = ((AssemblyFileVersionAttribute)((assembly
                                    .GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true))[0])).Version;
                                if(versionInfo.ProgramVersion.CompareTo(currentVersion) == 1)
                                {
                                    using(NewVersionAvailableForm form = new NewVersionAvailableForm(currentVersion
                                        , versionInfo.ProgramVersion, linkAnchor))
                                    {

                                        form.ShowDialog(parent);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                if(silently == false)
                {
                    MessageBox.Show(parent, Resources.NoNewVersionAvailable, Resources.InformationHeader, MessageBoxButtons.OK
                        , MessageBoxIcon.Information);
                }
            }
            catch
            {
                try
                {
                    if(webResponse != null)
                    {
                        webResponse.Close();
                    }
                }
                catch
                {
                }
                if(silently == false)
                {
                    MessageBox.Show(parent, Resources.OnlineConnectionCouldNotBeEstablished, Resources.ErrorHeader, MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
                }
            }
        }

        public static WebResponse CreateWebRequest(String targetUrl)
        {
            WebRequest webRequest;
            WebResponse webResponse;

            webRequest = WebRequest.Create(targetUrl);
            if(RegistryAccess.Proxy)
            {
                webRequest.Proxy = new WebProxy(RegistryAccess.Server, (Int32)(RegistryAccess.Port));
                if(RegistryAccess.WindowsAuthentication)
                {
                    webRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                }
                else if(RegistryAccess.CustomAuthentication)
                {
                    if(String.IsNullOrEmpty(RegistryAccess.Domain))
                    {
                        webRequest.Proxy.Credentials = new NetworkCredential(RegistryAccess.Username
                            , RegistryAccess.Password);
                    }
                    else
                    {
                        webRequest.Proxy.Credentials = new NetworkCredential(RegistryAccess.Username
                            , RegistryAccess.Password, RegistryAccess.Domain);
                    }
                }
            }
            webResponse = null;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch(WebException)
            {
                throw;
            }
            return (webResponse);
        }

        public static WebResponse CreateSystemSettingsWebRequest(String targetUrl
            , CultureInfo ci = null)
        {
            WebRequest webRequest;
            WebResponse webResponse;

            webRequest = WebRequest.Create(targetUrl);

            if (ci != null)
            {
                webRequest.Headers.Add("Accept-Language: " + ci.Name);
            }

            webRequest.Proxy = WebRequest.GetSystemWebProxy();

            webRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            webResponse = null;

            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (WebException)
            {
                throw;
            }

            return (webResponse);
        }

        public static WebClient CreateWebClient()
        {
            WebClient webClient;

            webClient = new WebClient();
            if(RegistryAccess.Proxy)
            {
                webClient.Proxy = new WebProxy(RegistryAccess.Server, (Int32)(RegistryAccess.Port));
                if(RegistryAccess.WindowsAuthentication)
                {
                    webClient.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                }
                else if(RegistryAccess.CustomAuthentication)
                {
                    if(String.IsNullOrEmpty(RegistryAccess.Domain))
                    {
                        webClient.Proxy.Credentials = new NetworkCredential(RegistryAccess.Username
                            , RegistryAccess.Password);
                    }
                    else
                    {
                        webClient.Proxy.Credentials = new NetworkCredential(RegistryAccess.Username
                            , RegistryAccess.Password, RegistryAccess.Domain);
                    }
                }
            }
            return (webClient);
        }

        public static WebClient CreateSystemSettingsWebClient()
        {
            WebClient webClient;

            webClient = new WebClient();
            webClient.Proxy = WebRequest.GetSystemWebProxy();
            webClient.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            return (webClient);
        }
    }
}