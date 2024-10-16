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
using Microsoft.UI.Xaml.Media.Animation;
using System.Diagnostics;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace dsGallery.view
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ApplicationDetail : Page
    {
        public ApplicationCollection DetailedObject { get; set; }

        public ApplicationDetail()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Store the item to be used in binding to UI
            DetailedObject = e.Parameter as ApplicationCollection;

            bool isExecable = DetailedObject.Path is not "";

            execButton.IsEnabled = isExecable;
            noExecWarning.Visibility = isExecable ? Visibility.Collapsed : Visibility.Visible;

            ConnectedAnimation imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (imageAnimation != null)
            {
                // Connected animation + coordinated animation
                imageAnimation.TryStart(detailedImage, new UIElement[] { coordinatedPanel });

            }
        }

        // Create connected animation back to collection page.
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackConnectedAnimation", detailedImage);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void execButton_Click(object sender, RoutedEventArgs e)
        {
            execStatus.Visibility = Visibility.Visible;

            var psInfo = new ProcessStartInfo
            {
                WorkingDirectory = @"D:\system\",
                FileName = DetailedObject.Path.Replace("/", "\\"),
                UseShellExecute = true,
            };

            var p = Process.Start(psInfo);

            p.WaitForExit();

            execStatus.Visibility = Visibility.Collapsed;
        }

    }
}
