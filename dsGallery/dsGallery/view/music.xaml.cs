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
using Windows.Media.Core;
using Windows.Media.Playback;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Foundation.Metadata;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace dsGallery.view
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class music : Page
    {
        MusicCollection _storeditem;

        public music()
        {
            this.InitializeComponent();
            // Ensure that the MainPage is only created once, and cached during navigation.
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

        }

        private void collection_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Get the collection item corresponding to the clicked item.
            if (collection.ContainerFromItem(e.ClickedItem) is ListViewItem container)
            {
                // Stash the clicked item for use later. We'll need it when we connect back from the detailpage.
                _storeditem = container.Content as MusicCollection;

                mediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Resources/Music/neomini2.mp3"));
            }

        }
    }
}
