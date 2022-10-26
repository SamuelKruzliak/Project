using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform;

namespace Project
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        #region
        double? selectedEntry = null;
        #endregion

        async void useVlsm(object sender, EventArgs e)
        {
            if (vlsmCheck.IsChecked)
            {
                Subnets.Placeholder = "Count of hosts (60,20)";
            }
            else
            {
                Subnets.Placeholder = "Count of subnets";
            }
        }

        public bool vlsmChecked()
        {
            if (vlsmCheck.IsChecked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        async void HandleEnterPress(object sender, EventArgs e)
        {
            Application.Current.Properties["IpAdress"] = MainEntry.Text;
            Application.Current.Properties["AdressCount"] = Subnets.Text;
            Application.Current.Properties["CheckVal"] = vlsmChecked();

            Navigation.PushAsync(new MainCalculation());
        }
        async void isFocused(object sender, EventArgs e)
        {
            selectedEntry = 1;
        }
        async void MainEntryFocused(object sender, EventArgs e)
        {
            selectedEntry = 0;
        }

        async void HandleLetter(object sender, EventArgs e)
        {
            var text = ((Button)sender).Text;
            if (selectedEntry>0)
            {
                Subnets.Text += text;
            }
            else
            {
                MainEntry.Text += text;
            }
        }
        async void HandleClearPress(object sender, EventArgs e)
        {
            string x = "";
             if(selectedEntry == 0)
            {
                x = MainEntry.Text;
            }
            else if ( selectedEntry == 1)
            {
                x = Subnets.Text;
            }
            if (x.Length == 0)
            {
            }
            else
            {
                x = x.Substring(0, x.Length - 1);
            }
            if (selectedEntry == 0)
            {
                MainEntry.Text = x;
            }
            else if (selectedEntry == 1)
            {
                Subnets.Text = x;
            }
        }
    }
}

