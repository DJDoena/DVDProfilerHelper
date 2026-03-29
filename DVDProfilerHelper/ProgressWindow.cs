using System.ComponentModel;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper;

public partial class ProgressWindow : Form
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanClose { get; set; }

    public ProgressWindow()
    {
        this.InitializeComponent();
    }

    private void OnProgressWindowFormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = !this.CanClose;
    }
}