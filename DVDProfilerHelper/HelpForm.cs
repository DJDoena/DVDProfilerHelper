using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public partial class HelpForm : Form
    {
        private String File;

        public HelpForm()
        {
            InitializeComponent();
        }

        public HelpForm(String file)
        {
            File = file;
            InitializeComponent();
        }

        private void OnHelpFormActivated(Object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(File))
            {
                WebBrowser.Navigate(Application.StartupPath + @"\Readme\readme.html");
            }
            else
            {
                WebBrowser.Navigate(File);
            }
        }
    }
}