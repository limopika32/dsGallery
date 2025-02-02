using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace dsGallery.view
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IllustDetail : Page
    {
        public IllustCollection DetailedObject { get; set; }

        public IllustDetail()
        {
            this.InitializeComponent();
            detailedImage.Tapped += DetailedImage_Tapped;
        }

        private void DetailedImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            coordinatedPanel.Visibility = coordinatedPanel.Visibility.Equals(Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
            closeButton.Visibility = coordinatedPanel.Visibility;
        }

        private void GoBackButton_Loaded(object sender, RoutedEventArgs e)
        {
            // When we land in page, put focus on the back button
            detailedImage.Focus(FocusState.Programmatic);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Store the item to be used in binding to UI
            DetailedObject = e.Parameter as IllustCollection;

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

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
