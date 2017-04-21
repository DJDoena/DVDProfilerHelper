using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public partial class NewVersionAvailableForm : Form
    {
        private String m_CurrentVersion;
        private String m_NewVersion;
        private String m_LinkAnchor;

        public NewVersionAvailableForm(String currentVersion, String newVersion, String linkAnchor)
        {
            m_CurrentVersion = currentVersion;
            m_NewVersion = newVersion;
            m_LinkAnchor = linkAnchor;
            InitializeComponent();
        }

        private void OnLinkLabelLinkClicked(Object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(LinkLabel.Text + "#" + m_LinkAnchor);
        }

        private void OnNewVersionAvailableFormLoad(Object sender, EventArgs e)
        {
            CurrentVersionTextBox.Text = m_CurrentVersion;
            NewVersionTextBox.Text = m_NewVersion;
        }
    }
}