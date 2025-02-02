﻿using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.Media.Core;

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

            mediaPlayerElement.MediaPlayer.IsLoopingEnabled = true;

        }

        private void collection_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Get the collection item corresponding to the clicked item.
            if (collection.ContainerFromItem(e.ClickedItem) is ListViewItem container)
            {
                // Stash the clicked item for use later. We'll need it when we connect back from the detailpage.
                _storeditem = container.Content as MusicCollection;

                MediaSource ms = MediaSource.CreateFromStorageFile(_storeditem.Path);

                mediaPlayerElement.Source = ms;


            }

        }
    }
}
