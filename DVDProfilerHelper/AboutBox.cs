using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public partial class AboutBox : Form
    {
        public AboutBox(Assembly assembly)
        {
            InitializeComponent();
            Text = String.Format("About {0}", GetAssemblyProduct(assembly));
            labelProductName.Text = GetAssemblyProduct(assembly);
            labelVersion.Text = String.Format("Version {0}", GetAssemblyVersion(assembly));
            labelCopyright.Text = GetAssemblyCopyright(assembly);
            labelCompanyName.Text = GetAssemblyCompany(assembly);
            textBoxDescription.Text = GetAssemblyDescription(assembly);
        }

        #region Assembly Attribute Accessors

        public String GetAssemblyTitle(Assembly assembly)
        {
            Object[] attributes;

            attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if(attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if(String.IsNullOrEmpty(titleAttribute.Title) == false)
                {
                    return (titleAttribute.Title);
                }
            }
            return (System.IO.Path.GetFileNameWithoutExtension(assembly.CodeBase));
        }

        public String GetAssemblyVersion(Assembly assembly)
        {
            return (assembly.GetName().Version.ToString());
        }

        public String GetAssemblyDescription(Assembly assembly)
        {
            Object[] attributes;

            attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if(attributes.Length == 0)
            {
                return (String.Empty);
            }
            return (((AssemblyDescriptionAttribute)attributes[0]).Description);
        }

        public String GetAssemblyProduct(Assembly assembly)
        {
            Object[] attributes;

            attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if(attributes.Length == 0)
            {
                return (String.Empty);
            }
            return (((AssemblyProductAttribute)attributes[0]).Product);

        }

        public String GetAssemblyCopyright(Assembly assembly)
        {
            Object[] attributes;

            attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if(attributes.Length == 0)
            {
                return (String.Empty);
            }
            return (((AssemblyCopyrightAttribute)attributes[0]).Copyright);
        }

        public String GetAssemblyCompany(Assembly assembly)
        {
            Object[] attributes;

            attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if(attributes.Length == 0)
            {
                return (String.Empty);
            }
            return (((AssemblyCompanyAttribute)attributes[0]).Company);
        }
        #endregion
    }
}