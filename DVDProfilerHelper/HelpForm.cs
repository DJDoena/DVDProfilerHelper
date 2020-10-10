using System;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public partial class HelpForm : Form
    {
        private string _file;

        public HelpForm()
        {
            InitializeComponent();
        }

        public HelpForm(String file)
        {
            _file = file;

            InitializeComponent();
        }

        private void OnHelpFormActivated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_file))
            {
                WebBrowser.Navigate(Application.StartupPath + @"\Readme\readme.html");
            }
            else
            {
                WebBrowser.Navigate(_file);
            }
        }
    }
}