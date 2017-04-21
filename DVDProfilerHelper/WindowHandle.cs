using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public class WindowHandle : IWin32Window
    {
        [DllImport("user32.dll")]
        static extern Int32 GetForegroundWindow();

        #region IWin32Window Members

        public IntPtr Handle
        {
            get
            {
                Int32 managed = GetForegroundWindow();

                IntPtr native = new IntPtr(managed);

                return (native);
            }
        }

        #endregion
    }
}