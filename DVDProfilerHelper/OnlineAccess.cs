﻿using System;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using DoenaSoft.DVDProfiler.DVDProfilerHelper.Properties;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class OnlineAccess
    {
        public static void Init(string company, string product) => RegistryAccess.Init(company, product);

        public static void CheckForNewVersion(string url, IWin32Window parent, string linkAnchor, Assembly assembly) => CheckForNewVersion(url, parent, linkAnchor, assembly, false);

        public static void CheckForNewVersion(string url, IWin32Window parent, string linkAnchor, Assembly assembly, Boolean silently)
        {
            if (parent == null)
            {
                parent = new WindowHandle();
            }

            WebResponse webResponse = null;
            try
            {
                webResponse = CreateSystemSettingsWebRequest(url);

                using (var stream = webResponse.GetResponseStream())
                {
                    var versionInfos = DVDProfilerSerializer<VersionInfos>.Deserialize(stream);

                    if ((versionInfos.VersionInfoList != null) && (versionInfos.VersionInfoList.Length > 0))
                    {
                        foreach (var versionInfo in versionInfos.VersionInfoList)
                        {
                            var currentName = ((AssemblyProductAttribute)((assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), true))[0])).Product;

                            if (versionInfo.ProgramName == currentName)
                            {
                                var currentVersion = ((AssemblyFileVersionAttribute)((assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true))[0])).Version;

                                if (versionInfo.ProgramVersion.CompareTo(currentVersion) == 1)
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
                    if (webResponse != null)
                    {
                        webResponse.Close();
                    }
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

        public static WebResponse CreateWebRequest(string targetUrl)
        {
            var webRequest = WebRequest.Create(targetUrl);

            if (RegistryAccess.Proxy)
            {
                webRequest.Proxy = new WebProxy(RegistryAccess.Server, (int)(RegistryAccess.Port));

                if (RegistryAccess.WindowsAuthentication)
                {
                    webRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                }
                else if (RegistryAccess.CustomAuthentication)
                {
                    if (string.IsNullOrEmpty(RegistryAccess.Domain))
                    {
                        webRequest.Proxy.Credentials = new NetworkCredential(RegistryAccess.Username, RegistryAccess.Password);
                    }
                    else
                    {
                        webRequest.Proxy.Credentials = new NetworkCredential(RegistryAccess.Username, RegistryAccess.Password, RegistryAccess.Domain);
                    }
                }
            }

            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (WebException)
            {
                throw;
            }

            return webResponse;
        }

        public static WebResponse CreateSystemSettingsWebRequest(string targetUrl, CultureInfo ci = null)
        {
            var webRequest = WebRequest.Create(targetUrl);

            if (ci != null)
            {
                webRequest.Headers.Add("Accept-Language: " + ci.Name);
            }

            webRequest.Proxy = WebRequest.GetSystemWebProxy();

            webRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (WebException)
            {
                throw;
            }

            return webResponse;
        }

        public static WebClient CreateWebClient()
        {
            var webClient = new WebClient();

            if (RegistryAccess.Proxy)
            {
                webClient.Proxy = new WebProxy(RegistryAccess.Server, (int)RegistryAccess.Port);

                if (RegistryAccess.WindowsAuthentication)
                {
                    webClient.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                }
                else if (RegistryAccess.CustomAuthentication)
                {
                    if (string.IsNullOrEmpty(RegistryAccess.Domain))
                    {
                        webClient.Proxy.Credentials = new NetworkCredential(RegistryAccess.Username, RegistryAccess.Password);
                    }
                    else
                    {
                        webClient.Proxy.Credentials = new NetworkCredential(RegistryAccess.Username, RegistryAccess.Password, RegistryAccess.Domain);
                    }
                }
            }

            return webClient;
        }

        public static WebClient CreateSystemSettingsWebClient()
        {
            var webClient = new WebClient
            {
                Proxy = WebRequest.GetSystemWebProxy(),
            };

            webClient.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            return webClient;
        }
    }
}