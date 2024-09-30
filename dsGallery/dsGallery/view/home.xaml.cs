using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI;
using Microsoft.Web.WebView2.Core;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace dsGallery.view
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class home : Page
    {
        public home()
        {
            this.InitializeComponent();

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((AppBarButton)sender).Label;
            switch (selected.ToString())
            {
                case "戻る":
                    hpView.GoBack();
                    break;
                case "更新":
                    hpView.Reload();
                    break;
                case "停止":
                    hpView.CoreWebView2.Stop();
                    break;
            }
        }


        private void hpView_CoreWebView2Initialized(WebView2 sender, CoreWebView2InitializedEventArgs args)
        {
            // ダイアログ表示を抑止
            sender.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;

            // コンテキストメニューを抑止
            sender.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;

            // 開発者ツールを無効化
            sender.CoreWebView2.Settings.AreDevToolsEnabled = false;

            // ブラウザに組み込まれているエラーページを無効化
            sender.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = false;

            // ズームコントロールを無効化
            sender.CoreWebView2.Settings.IsZoomControlEnabled = false;

            // ステータスバーを非表示
            sender.CoreWebView2.Settings.IsStatusBarEnabled = false;

            // オートフィル無効化
            sender.CoreWebView2.Settings.IsGeneralAutofillEnabled = false;

            // イベントハンドラ追加設定
            sender.CoreWebView2.SourceChanged += hpView_SourceChanged;
            sender.CoreWebView2.NewWindowRequested += hpView_NewWindowRequested;
        }


        private void hpView_NavigationStarting(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs args)
        {
            hpReff.Icon = new SymbolIcon(Symbol.Cancel);
            hpReff.Label = "停止";
        }

        private void hpView_NavigationCompleted(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
        {
            hpReff.Icon = new SymbolIcon(Symbol.Refresh);
            hpReff.Label = "更新";
        }

        private void hpView_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            // enable/disable back button
            hpBack.IsEnabled = hpView.CanGoBack;

            hpStat.Text = hpView.Source.ToString();
        }

        private void hpView_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            // external window block
            e.Handled = true;
        }
    }
}
