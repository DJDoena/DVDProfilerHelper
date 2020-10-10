using System.Reflection;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public partial class AboutBox : Form
    {
        public AboutBox(Assembly assembly)
        {
            InitializeComponent();

            Text = string.Format("About {0}", GetAssemblyProduct(assembly));

            ProductNameLabel.Text = GetAssemblyProduct(assembly);
            VersionLabel.Text = string.Format("Version {0}", GetAssemblyVersion(assembly));
            CopyrightLabel.Text = GetAssemblyCopyright(assembly);
            CompanyNameLabel.Text = GetAssemblyCompany(assembly);
            DescriptionTextBox.Text = GetAssemblyDescription(assembly);
        }

        #region Assembly Attribute Accessors

        public string GetAssemblyTitle(Assembly assembly) => GetAttribute<AssemblyTitleAttribute>(assembly)?.Title ?? System.IO.Path.GetFileNameWithoutExtension(assembly.CodeBase);

        public string GetAssemblyVersion(Assembly assembly) => assembly.GetName().Version.ToString();

        public string GetAssemblyDescription(Assembly assembly) => GetAttribute<AssemblyDescriptionAttribute>(assembly)?.Description ?? string.Empty;

        public string GetAssemblyProduct(Assembly assembly) => GetAttribute<AssemblyProductAttribute>(assembly)?.Product ?? string.Empty;

        public string GetAssemblyCopyright(Assembly assembly) => GetAttribute<AssemblyCopyrightAttribute>(assembly)?.Copyright ?? string.Empty;

        public string GetAssemblyCompany(Assembly assembly) => GetAttribute<AssemblyCompanyAttribute>(assembly)?.Company ?? string.Empty;

        private static T GetAttribute<T>(Assembly assembly) where T : class
        {
            var attributes = assembly.GetCustomAttributes(typeof(T), false);

            if (attributes.Length == 0)
            {
                return null;
            }

            return (T)attributes[0];
        }

        #endregion
    }
}