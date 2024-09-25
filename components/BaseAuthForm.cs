using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UniProject
{
    public class BaseAuthForm : Form
    {
        /***** For panel drag *****/
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        public void EnableDrag(Control panel)
        {
            panel.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                }
            };
        }
        /***** End of panel drag *****/
    }
}