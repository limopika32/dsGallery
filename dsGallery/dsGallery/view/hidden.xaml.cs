using Microsoft.UI.Xaml.Controls;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace dsGallery.view
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class hidden : Page
    {
        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSDisconnectSession(IntPtr hServer, int sessionId, bool bWait);
        const int WTS_CURRENT_SESSION = -1;
        static readonly IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

        public hidden()
        {
            this.InitializeComponent();
        }

        private void exec_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var selected = ((Button)sender).Tag;
            switch (selected.ToString())
            {
                case "execSignout":
                    if (!WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, false))
                        throw new Win32Exception();
                    break;
                case "execRemount":
                    MainWindow.SDreader();
                    break;
                default:
                    break;
            }

        }
    }
}
