using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public class WindowHandle : IWin32Window
    {
        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();

        #region IWin32Window Members

        public IntPtr Handle
        {
            get
            {
                var managed = GetForegroundWindow();

                var native = new IntPtr(managed);

                return native;
            }
        }

        #endregion
    }
}