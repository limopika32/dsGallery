﻿using System;
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
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Foundation.Metadata;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace dsGallery.view
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class appli : Page
    { 
        ApplicationCollection _storeditem;

        public appli()
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
                _storeditem = container.Content as ApplicationCollection;

                // Prepare the connected animation.
                // Notice that the stored item is passed in, as well as the name of the connected element.
                // The animation will actually start on the Detailed info page.
                collection.PrepareConnectedAnimation("ForwardConnectedAnimation", _storeditem, "mainContent");
            }

            // Navigate to the DetailedInfoPage.
            // Note that we suppress the default animation.
            Frame.Navigate(typeof(ApplicationDetail), _storeditem, new SuppressNavigationTransitionInfo());
        }


        private async void collection_Loaded(object sender, RoutedEventArgs e)
        {
            loadingView.Visibility = Visibility.Collapsed;

            if (_storeditem != null)
            {
                // If the connected item appears outside the viewport, scroll it into view.
                collection.ScrollIntoView(_storeditem, ScrollIntoViewAlignment.Default);
                collection.UpdateLayout();

                // Play the second connected animation.
                ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("BackConnectedAnimation");
                if (animation != null)
                {
                    // Setup the "back" configuration if the API is present.
                    if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7))
                    {
                        animation.Configuration = new DirectConnectedAnimationConfiguration();
                    }

                    await collection.TryStartConnectedAnimationAsync(animation, _storeditem, "mainContent");
                }

                // Set focus on the list
                collection.Focus(FocusState.Programmatic);
            }
        }

        private void collection_Loading(FrameworkElement sender, object args)
        {
            loadingView.Visibility = Visibility.Visible;
        }
    }
}
