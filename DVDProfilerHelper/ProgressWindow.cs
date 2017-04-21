using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public partial class ProgressWindow : Form
    {
        public Boolean CanClose;

        public ProgressWindow()
        {
            InitializeComponent();
        }

        private void OnProgressWindowFormClosing(Object sender, FormClosingEventArgs e)
        {
            e.Cancel = (CanClose == false);
        }
    }
}
