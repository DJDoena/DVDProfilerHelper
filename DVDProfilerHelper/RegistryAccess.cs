using System;
using Microsoft.Win32;
using System.Diagnostics;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public static class RegistryAccess
    {
        private static String s_Company;
        private static String s_Product;
        private static RegistryKey s_RegKey;

        private static RegistryKey RegKey
        {
            get
            {
                if(s_RegKey == null)
                {
                    s_RegKey = Registry.CurrentUser.OpenSubKey(@"Software\" + s_Company + @"\" + s_Product, true);
                }
                return (s_RegKey);
            }
            [DebuggerStepThrough()]
            set
            {
                s_RegKey = value;
            }
        }

        public static void Init(String company, String product)
        {
            s_Company = company;
            s_Product = product;
        }

        private static void EnsureExistingRegKey()
        {
            if(s_RegKey == null)
            {
                RegKey = Registry.CurrentUser.CreateSubKey(@"Software\" + s_Company + @"\" + s_Product);
            }
        }

        public static Boolean NoProxy
        {
            get
            {
                if(RegKey != null)
                {
                    String noproxyString;
                    Boolean noproxy;

                    noproxyString = (String)(RegKey.GetValue("NoProxy", "true"));
                    if(Boolean.TryParse(noproxyString, out noproxy))
                    {
                        return (noproxy);
                    }
                }
                return (true);
            }
            set
            {
                EnsureExistingRegKey();
                RegKey.SetValue("NoProxy", value.ToString(), RegistryValueKind.String);
            }
        }

        public static Boolean Proxy
        {
            get
            {
                if(RegKey != null)
                {
                    String proxyString;
                    Boolean proxy;

                    proxyString = (String)(RegKey.GetValue("Proxy", "false"));
                    if(Boolean.TryParse(proxyString, out proxy))
                    {
                        return (proxy);
                    }
                }
                return (false);
            }
            set
            {
                EnsureExistingRegKey();
                RegKey.SetValue("Proxy", value.ToString(), RegistryValueKind.String);
            }
        }

        public static String Server
        {
            get
            {
                if(RegKey != null)
                {
                    String serverString;

                    serverString = (String)(RegKey.GetValue("Server", ""));
                    return (serverString);
                }
                return (String.Empty);
            }
            set
            {
                EnsureExistingRegKey();
                RegKey.SetValue("Server", value, RegistryValueKind.String);
            }
        }

        public static Decimal Port
        {
            get
            {
                if(RegKey != null)
                {
                    String portString;
                    Decimal port;

                    portString = (String)(RegKey.GetValue("Port", "80"));
                    if(Decimal.TryParse(portString, out port))
                    {
                        return (port);
                    }
                }
                return (80);
            }
            set
            {
                EnsureExistingRegKey();
                RegKey.SetValue("Port", value.ToString(), RegistryValueKind.String);
            }
        }

        public static Boolean NoAuthentication
        {
            get
            {
                if(RegKey != null)
                {
                    String noauthenticationString;
                    Boolean noauthentication;

                    noauthenticationString = (String)(RegKey.GetValue("NoAuthentication", "true"));
                    if(Boolean.TryParse(noauthenticationString, out noauthentication))
                    {
                        return (noauthentication);
                    }
                }
                return (true);
            }
            set
            {
                EnsureExistingRegKey();
                RegKey.SetValue("NoAuthentication", value.ToString(), RegistryValueKind.String);
            }
        }

        public static Boolean WindowsAuthentication
        {
            get
            {
                if(RegKey != null)
                {
                    String windowsauthenticationString;
                    Boolean windowsauthentication;

                    windowsauthenticationString = (String)(RegKey.GetValue("WindowsAuthentication", "false"));
                    if(Boolean.TryParse(windowsauthenticationString, out windowsauthentication))
                    {
                        return (windowsauthentication);
                    }
                }
                return (false);
            }
            set
            {
                EnsureExistingRegKey();
                RegKey.SetValue("WindowsAuthentication", value.ToString(), RegistryValueKind.String);
            }
        }

        public static Boolean CustomAuthentication
        {
            get
            {
                if(RegKey != null)
                {
                    String customauthenticationString;
                    Boolean customauthentication;

                    customauthenticationString = (String)(RegKey.GetValue("CustomAuthentication", "false"));
                    if(Boolean.TryParse(customauthenticationString, out customauthentication))
                    {
                        return (customauthentication);
                    }
                }
                return (false);
            }
            set
            {
                EnsureExistingRegKey();
                RegKey.SetValue("CustomAuthentication", value.ToString(), RegistryValueKind.String);
            }
        }

        public static String Username
        {
            get
            {
                if(RegKey != null)
                {
                    String usernameString;

                    usernameString = (String)(RegKey.GetValue("Username", ""));
                    return (usernameString);
                }
                return (String.Empty);
            }
            set
            {
                EnsureExistingRegKey();
                RegKey.SetValue("Username", value, RegistryValueKind.String);
            }
        }

        public static String Password
        {
            get
            {
                if(RegKey != null)
                {
                    String passwordString;

                    passwordString = (String)(RegKey.GetValue("Password", ""));
                    if(String.IsNullOrEmpty(passwordString) == false)
                    {
                        Object[] guidArray;
                        String guid;
                        String key;
                        Byte[] guidBytes;
                        Byte[] keyBytes;
                        TripleDES des;

                        key = "{[Super]1}(Kali[2]){([Fr";//age]3)[(Listisch4)]}[(Expi5Aligetisch)]!";
                        guidArray = typeof(RegistryAccess).Assembly.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
                        guid = (String)(((System.Runtime.InteropServices.GuidAttribute)(guidArray[0])).Value);
                        guidBytes = System.Text.Encoding.UTF8.GetBytes(guid.Substring(0, 24));
                        //ivBytes = new Byte[64];
                        //guidBytes.CopyTo(ivBytes, 0);
                        //guidBytes.CopyTo(ivBytes, 28);
                        keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
                        des = new TripleDES(keyBytes, guidBytes);
                        passwordString = des.Decrypt(passwordString);
                    }
                    return (passwordString);
                }
                return (String.Empty);
            }
            set
            {
                if(String.IsNullOrEmpty(value) == false)
                {
                    Object[] guidArray;
                    String guid;
                    String key;
                    Byte[] guidBytes;
                    Byte[] keyBytes;
                    TripleDES des;

                    key = "{[Super]1}(Kali[2]){([Fr";//age]3)[(Listisch4)]}[(Expi5Aligetisch)]!";
                    guidArray = typeof(RegistryAccess).Assembly.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
                    guid = (String)(((System.Runtime.InteropServices.GuidAttribute)(guidArray[0])).Value);
                    guidBytes = System.Text.Encoding.UTF8.GetBytes(guid.Substring(0, 24));
                    //ivBytes = new Byte[64];
                    //guidBytes.CopyTo(ivBytes, 0);
                    //guidBytes.CopyTo(ivBytes, 28);
                    keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
                    des = new TripleDES(keyBytes, guidBytes);
                    value = des.Encrypt(value);
                }
                EnsureExistingRegKey();
                RegKey.SetValue("Password", value, RegistryValueKind.String);
            }
        }

        public static String Domain
        {
            get
            {
                if(RegKey != null)
                {
                    String domainString;

                    domainString = (String)(RegKey.GetValue("Domain", ""));
                    return (domainString);
                }
                return (String.Empty);
            }
            set
            {
                EnsureExistingRegKey();
                RegKey.SetValue("Domain", value, RegistryValueKind.String);
            }
        }

        public static String DataRootPath
        {
            get
            {
                if(RegKey != null)
                {
                    String dataRoot;

                    dataRoot = (String)(RegKey.GetValue("DataRoot", ""));
                    return (dataRoot);
                }
                return (String.Empty);
            }
            set
            {
                EnsureExistingRegKey();
                RegKey.SetValue("DataRoot", value, RegistryValueKind.String);
            }
        }
    }
}