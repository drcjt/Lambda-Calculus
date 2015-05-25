using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;

using Windows.ApplicationModel.Resources;

using LambdaEngine;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Lambda_Calculus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IPrinter
    {
        Lambda _lambdaCalculator;

        public MainPage()
        {
            this.InitializeComponent();

            _inputPane.Showing += InputPaneShowing;
            _inputPane.Hiding += InputPaneHiding;

            _lambdaCalculator = new Lambda(this);

            PrintLn("Simple Lambda Calculus Interpreter");
            PrintLn();

            LoadPrelude();
        }

        public async void LoadPrelude()
        {
            PrintLn("Reading the prelude");
            PrintLn();
            Print("\t");

            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync("prelude.txt");
            var contents = await Windows.Storage.FileIO.ReadTextAsync(file);

            _lambdaCalculator.LoadPrelude(contents);
        }

        private void Expression_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                EvaluateExpression();
            }
        }

        private void EvaluateExpression()
        {
            PrintLn(string.Format("> {0}", Expression.Text));
            _lambdaCalculator.EvaluateExpression(Expression.Text);
            Expression.Text = "";
        }

        public void Print(string text)
        {
            ConsoleText.Text += text;
        }

        public void PrintLn(string text = null)
        {
            if (text != null)
            {
                Print(text);
            }
            Print("\n");
        }

        Windows.UI.ViewManagement.InputPane _inputPane = Windows.UI.ViewManagement.InputPane.GetForCurrentView();

        private void InputPaneShowing(InputPane sender, InputPaneVisibilityEventArgs e)
        {
            e.EnsuredFocusedElementInView = true;

            FooterPlaceHolder.Height = e.OccludedRect.Height;
            FooterPlaceHolder.Visibility = Visibility.Visible;

            BodyScroller.UpdateLayout();
            BodyScroller.ChangeView(null, BodyScroller.ExtentHeight, null);

            CommandBar.Visibility = Visibility.Collapsed;
        }

        private void InputPaneHiding(InputPane sender, InputPaneVisibilityEventArgs e)
        {
            FooterPlaceHolder.Height = 0;
            FooterPlaceHolder.Visibility = Visibility.Collapsed;

            BodyScroller.UpdateLayout();
            BodyScroller.ChangeView(null, BodyScroller.ExtentHeight, null);

            CommandBar.Visibility = Visibility.Visible;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Lambda_Calculus.Views.Settings));
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            this.DataContext = null;
            this.DataContext = App.Settings;

        }
    }
}
