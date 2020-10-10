using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public partial class NewVersionAvailableForm : Form
    {
        private string _currentVersion;

        private string _newVersion;

        private string _linkAnchor;

        public NewVersionAvailableForm(string currentVersion, string newVersion, string linkAnchor)
        {
            _currentVersion = currentVersion;
            _newVersion = newVersion;
            _linkAnchor = linkAnchor;

            InitializeComponent();
        }

        private void OnLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start(LinkLabel.Text + "#" + _linkAnchor);

        private void OnNewVersionAvailableFormLoad(object sender, EventArgs e)
        {
            CurrentVersionTextBox.Text = _currentVersion;
            NewVersionTextBox.Text = _newVersion;
        }
    }
}