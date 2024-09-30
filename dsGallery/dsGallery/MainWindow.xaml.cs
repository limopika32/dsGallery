using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace dsGallery
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly AppWindow _appWindow;

        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            _appWindow = GetAppWindowForCurrentWindow();
            _appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);

            SDreader();
        }


        public static async void SDreader()
        {
            // Get the logical root folder for all external storage devices.
            StorageFolder externalDevices = KnownFolders.RemovableDevices;

            // Get the first child folder, which represents the SD card.
            StorageFolder sdCard = (await externalDevices.GetFoldersAsync()).FirstOrDefault();

            if (sdCard != null)
            {
                
                // An SD card is present and the sdCard variable now contains a reference to it.
            }
            else
            {
                
                // No SD card is present.
            }
        }
        

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void mainNV_Loaded(object sender, RoutedEventArgs e)
        {
            // Add handler for ContentFrame navigation.
            ContentFrame.Navigated += On_Navigated;

            // mainNV doesn't load any page by default, so load home page.
            mainNV.SelectedItem = mainNV.MenuItems[0];
            // If navigation occurs on SelectionChanged, this isn't needed.
            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the home page.
            Navigate(typeof(view.home), new EntranceNavigationTransitionInfo());
        }


        private void mainNV_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                Navigate(typeof(view.about), new EntranceNavigationTransitionInfo());
            }else {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavigationViewItem nitem = item;
                if (nitem != null)
                {
                    switch (nitem.Tag)
                    {
                        case "NavigationView.Home":
                            Navigate(typeof(view.home), new EntranceNavigationTransitionInfo());
                            break;
                        case "NavigationView.Application":
                            Navigate(typeof(view.appli), new EntranceNavigationTransitionInfo());
                            break;
                        case "NavigationView.Music":
                            Navigate(typeof(view.music), new EntranceNavigationTransitionInfo());
                            break;
                        case "NavigationView.Illust":
                            Navigate(typeof(view.illust), new EntranceNavigationTransitionInfo());
                            break;

                        default:
                           break;
                    }
                }
            }
        }

        private void Navigate(Type navPageType, NavigationTransitionInfo transitionInfo)
        {
            Type preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (navPageType is not null && !Type.Equals(preNavPageType, navPageType))
            {
                ContentFrame.Navigate(navPageType, null, transitionInfo);
            }
        }


        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            if (ContentFrame.SourcePageType != null)
            {
                mainNV.Header = ((NavigationViewItem)mainNV.SelectedItem)?.Content?.ToString();

            }
        }
    }
}
