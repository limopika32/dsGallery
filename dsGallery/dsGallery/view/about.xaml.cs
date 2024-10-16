using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace dsGallery.view
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class about : Page
    {
        private String input_display;
        private static short wrongTime = 0;

        public about()
        {
            this.InitializeComponent();
            if (wrongTime >= 3) btn12.IsEnabled = false;
        }

        private void Common_btn_Click(object sender, RoutedEventArgs e)
        {
            var inputed = ((Button)sender).Content;
            switch (inputed.ToString())
            {
                case "Ent":
                    if (input_display == "D367A648")
                    {
                        Frame.Navigate(typeof(hidden), null, new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        wrongTime++;
                        input_display = null;
                        if (wrongTime >= 3)
                        {
                            btn12.IsEnabled = false;
                        }
                    }
                    break;
                case "Clr":
                    if (input_display == "BCBCBCBC")
                    {
                        wrongTime--;
                        btn12.IsEnabled = true;
                    }
                    input_display = "";
                    break;
                default:
                    input_display += inputed.ToString();
                    break;
            }

            pinStatus.Text = input_display ?? "Wrong PIN. remain:" + (3 - wrongTime);
        }
    }
}
