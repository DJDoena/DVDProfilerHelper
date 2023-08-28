using System.Diagnostics;
using Microsoft.Win32;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class RegistryAccess
    {
        private static string _company;

        private static string _product;

        private static RegistryKey _regKey;

        private static RegistryKey RegKey
        {
            get
            {
                if (_regKey == null)
                {
                    _regKey = Registry.CurrentUser.OpenSubKey(@"Software\" + _company + @"\" + _product, true);
                }

                return _regKey;
            }
            [DebuggerStepThrough]
            set => _regKey = value;
        }

        public static void Init(string company, string product)
        {
            _company = company;
            _product = product;
        }

        private static void EnsureExistingRegKey()
        {
            if (_regKey == null)
            {
                RegKey = Registry.CurrentUser.CreateSubKey(@"Software\" + _company + @"\" + _product);
            }
        }

        public static bool NoProxy
        {
            get
            {
                if (RegKey != null)
                {
                    var noProxyString = (string)RegKey.GetValue("NoProxy", "true");

                    if (bool.TryParse(noProxyString, out var noProxy))
                    {
                        return noProxy;
                    }
                }

                return true;
            }
            set
            {
                EnsureExistingRegKey();

                RegKey.SetValue("NoProxy", value.ToString(), RegistryValueKind.String);
            }
        }

        public static bool Proxy
        {
            get
            {
                if (RegKey != null)
                {
                    var proxyString = (string)RegKey.GetValue("Proxy", "false");

                    if (bool.TryParse(proxyString, out var proxy))
                    {
                        return proxy;
                    }
                }

                return false;
            }
            set
            {
                EnsureExistingRegKey();

                RegKey.SetValue("Proxy", value.ToString(), RegistryValueKind.String);
            }
        }

        public static string Server
        {
            get
            {
                if (RegKey != null)
                {
                    var serverString = (string)RegKey.GetValue("Server", "");

                    return serverString;
                }

                return string.Empty;
            }
            set
            {
                EnsureExistingRegKey();

                RegKey.SetValue("Server", value, RegistryValueKind.String);
            }
        }

        public static ushort Port
        {
            get
            {
                if (RegKey != null)
                {
                    var portString = (string)RegKey.GetValue("Port", "80");

                    if (ushort.TryParse(portString, out var port))
                    {
                        return port;
                    }
                }

                return 80;
            }
            set
            {
                EnsureExistingRegKey();

                RegKey.SetValue("Port", value.ToString(), RegistryValueKind.String);
            }
        }

        public static bool NoAuthentication
        {
            get
            {
                if (RegKey != null)
                {
                    var noAuthenticationString = (string)RegKey.GetValue("NoAuthentication", "true");

                    if (bool.TryParse(noAuthenticationString, out var noAuthentication))
                    {
                        return noAuthentication;
                    }
                }

                return true;
            }
            set
            {
                EnsureExistingRegKey();

                RegKey.SetValue("NoAuthentication", value.ToString(), RegistryValueKind.String);
            }
        }

        public static bool WindowsAuthentication
        {
            get
            {
                if (RegKey != null)
                {
                    var windowsAuthenticationString = (string)RegKey.GetValue("WindowsAuthentication", "false");

                    if (bool.TryParse(windowsAuthenticationString, out var windowsAuthentication))
                    {
                        return windowsAuthentication;
                    }
                }

                return false;
            }
            set
            {
                EnsureExistingRegKey();

                RegKey.SetValue("WindowsAuthentication", value.ToString(), RegistryValueKind.String);
            }
        }

        public static bool CustomAuthentication
        {
            get
            {
                if (RegKey != null)
                {
                    var customAuthenticationString = (string)RegKey.GetValue("CustomAuthentication", "false");

                    if (bool.TryParse(customAuthenticationString, out var customAuthentication))
                    {
                        return customAuthentication;
                    }
                }

                return false;
            }
            set
            {
                EnsureExistingRegKey();

                RegKey.SetValue("CustomAuthentication", value.ToString(), RegistryValueKind.String);
            }
        }

        public static string Username
        {
            get
            {
                if (RegKey != null)
                {
                    var userNameString = (string)RegKey.GetValue("Username", "");

                    return userNameString;
                }

                return string.Empty;
            }
            set
            {
                EnsureExistingRegKey();

                RegKey.SetValue("Username", value, RegistryValueKind.String);
            }
        }

        public static string Password
        {
            get
            {
                if (RegKey != null)
                {
                    var passwordString = (string)RegKey.GetValue("Password", "");

                    if (string.IsNullOrEmpty(passwordString) == false)
                    {
                        var key = "{[Super]1}(Kali[2]){([Fr";//age]3)[(Listisch4)]}[(Expi5Aligetisch)]!";

                        var guidArray = typeof(RegistryAccess).Assembly.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);

                        var guid = ((System.Runtime.InteropServices.GuidAttribute)(guidArray[0])).Value;

                        var guidBytes = System.Text.Encoding.UTF8.GetBytes(guid.Substring(0, 24));

                        var keyBytes = System.Text.Encoding.UTF8.GetBytes(key);

                        var des = new TripleDES(keyBytes, guidBytes);

                        passwordString = des.Decrypt(passwordString);
                    }

                    return passwordString;
                }

                return string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                {
                    var key = "{[Super]1}(Kali[2]){([Fr";//age]3)[(Listisch4)]}[(Expi5Aligetisch)]!";

                    var guidArray = typeof(RegistryAccess).Assembly.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);

                    var guid = ((System.Runtime.InteropServices.GuidAttribute)(guidArray[0])).Value;

                    var guidBytes = System.Text.Encoding.UTF8.GetBytes(guid.Substring(0, 24));

                    var keyBytes = System.Text.Encoding.UTF8.GetBytes(key);

                    var des = new TripleDES(keyBytes, guidBytes);

                    value = des.Encrypt(value);
                }
                EnsureExistingRegKey();
                RegKey.SetValue("Password", value, RegistryValueKind.String);
            }
        }

        public static string Domain
        {
            get
            {
                if (RegKey != null)
                {
                    var domainString = (string)RegKey.GetValue("Domain", "");

                    return domainString;
                }

                return string.Empty;
            }
            set
            {
                EnsureExistingRegKey();

                RegKey.SetValue("Domain", value, RegistryValueKind.String);
            }
        }

        public static string DataRootPath
        {
            get
            {
                if (RegKey != null)
                {
                    var dataRoot = (string)RegKey.GetValue("DataRoot", "");

                    return dataRoot;
                }

                return string.Empty;
            }
            set
            {
                EnsureExistingRegKey();

                RegKey.SetValue("DataRoot", value, RegistryValueKind.String);
            }
        }
    }
}