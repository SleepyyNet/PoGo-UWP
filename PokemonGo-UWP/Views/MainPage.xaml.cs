using System;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace PokemonGo_UWP.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;

            // Handlers for virtual keyboard on or off
            InputPane.GetForCurrentView().Showing += _virtualKeyboardOn;
            InputPane.GetForCurrentView().Hiding += _virtualKeyboardOff;

            ViewModel.InvalidLogin += ViewModelOnInvalidLogin;
        }

        private void _virtualKeyboardOn(object sender, object e)
        {
            MainGrid.RowDefinitions[0].Height = new GridLength(0.0);
        }
        private void _virtualKeyboardOff(object sender, object e)
        {
            MainGrid.RowDefinitions[0].Height = new GridLength(1.0, GridUnitType.Star);
        }
        private void passwordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter) return;
            // If username contains @ we login with google
            if (LoginUsernameTextBox.Text.Contains("@"))
                GoogleLoginButton.Focus(FocusState.Programmatic);
            else
                PtcLoginButton.Focus(FocusState.Programmatic);
        }

        private void ViewModelOnInvalidLogin(object sender, string e)
        {
            LoginPasswordPasswordBox.Password = String.Empty;
            ErrorMessageText.Text = e;
            ErrorMessageText.Visibility = ErrorMessageBorder.Visibility = Visibility.Visible;

            ShowErrorMessageStoryboard.Completed += (ss, ee) => { HideErrorMessageStoryboard.Begin(); };
            ShowErrorMessageStoryboard.Begin();
        }
    }
}