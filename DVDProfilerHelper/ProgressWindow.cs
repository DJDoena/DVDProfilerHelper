using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public partial class ProgressWindow : Form
    {
        public bool CanClose { get; set; }

        public ProgressWindow()
        {
            InitializeComponent();
        }

        private void OnProgressWindowFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CanClose;
        }
    }
}