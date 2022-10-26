using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainCalculation : ContentPage
    {
        #region
        double? _prefix = null;
        double? _rozdiel = null;
        string _NumOfHosts = null;
        #endregion

        public MainCalculation()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            infoFrame.BackgroundColor = Color.Black;
            infoFrame.BorderColor = Color.White;
            bool vlsmChecked = Convert.ToBoolean(Application.Current.Properties["CheckVal"]);
            try
            {
                if (vlsmChecked)
                {
                    DoVlsmMethod();
                }
                else
                {
                    DoSubnetMethod();
                }
            }
            catch
            {
                printWarning();
            }
        }

        public void DoVlsmMethod()
        {
            string ipAdress = Application.Current.Properties["IpAdress"].ToString();
            string hosts = Application.Current.Properties["AdressCount"].ToString();
            List<string> ipAdresses = new List<string>();

            ipAdresses = VlsmMethod.getVlsm(ipAdress, hosts);
           
            for (int i = 0; i < ipAdresses.Count; i++)
            {
                Label IpLabel = new Label();
                IpLabel.FontSize = 30;
                IpLabel.FontAttributes = FontAttributes.Italic;
                IpLabel.FontSize = 30;
                IpLabel.TextColor = Color.White;
                if (i > 8)
                {
                    Label etc = new Label();
                    etc.Text = " . . .";
                    FinalResults.Children.Add(etc);
                    IpLabel.Text = ($"{ipAdresses[ipAdresses.Count - 1]}");
                    FinalResults.Children.Add(IpLabel);
                    break;
                }
                IpLabel.Text = $" {ipAdresses[i]}";
                FinalResults.Children.Add(IpLabel);
            }
        }

        public void DoSubnetMethod()
        {
            SubnetMask.TextColor = Color.Blue;
            NumOfHosts.TextColor = Color.Blue;
            SubnetsSign.TextColor = Color.White;
            SubnetsSign.FontSize = 35;
            SubnetsSign.FontAttributes = FontAttributes.Italic;
            SubnetsSign.Text = " Subnets: ";

            List<byte> list = new List<byte>();
            List<string> ipAdresses = new List<string>();
            string ipAdress = Application.Current.Properties["IpAdress"].ToString();
            int prefix = 0;
            int subnets = 0;
            int BorrowedBits = 0;
            subnets = Convert.ToInt32(Application.Current.Properties["AdressCount"]);
            prefix = int.Parse(ipAdress.Substring(ipAdress.IndexOf("/") + 1));
            ipAdress = ipAdress.Substring(0, ipAdress.IndexOf("/"));

            if (prefix > 32)
            {
                printWarning();
            }
           
            list = IpCalculator.ParseIpAdress(ipAdress);
            double add = 0;

            for (int i = 0; i <= subnets; i++)
            {
                if (Math.Pow(2, i) >= subnets)
                {
                    BorrowedBits = i;
                    prefix += BorrowedBits;
                    break;
                }
            }
            _prefix = prefix;
            if (prefix < 8)
            {
                _rozdiel = prefix;
                add = Math.Pow(2, 8 - (prefix));
                ipAdresses.AddRange(IpCalculator.Result(BorrowedBits, add, list, 0));
            }
            if (prefix >= 8 & prefix < 16)
            {
                _rozdiel = prefix - 8;
                add = Math.Pow(2, 16 - (prefix));
                ipAdresses.AddRange(IpCalculator.Result(BorrowedBits, add, list, 1));
            }
            if (prefix >= 16 & prefix <= 23)
            {
                _rozdiel = prefix - 16;
                add = Math.Pow(2, 24 - (prefix));
                ipAdresses.AddRange(IpCalculator.Result(BorrowedBits, add, list, 2));
            }
            if (prefix > 23)
            {
                _rozdiel = prefix - 24;
                add = Math.Pow(2, 32 - (prefix));
                ipAdresses.AddRange(IpCalculator.Result(BorrowedBits, add, list, 3));
            }


            _NumOfHosts = " Počet hostov: " + (Math.Pow(2, 32 - prefix) - 2).ToString();
            PrintAdresses(ipAdresses, prefix);
        }
        private void PrintAdresses(List<string> ipAdresses, int prefix)
        {
            for (int i = 0; i < ipAdresses.Count; i++)
            {
                Label IpLabel = new Label();
                IpLabel.FontSize = 30;
                IpLabel.FontAttributes = FontAttributes.Italic;
                IpLabel.FontSize = 30;
                IpLabel.TextColor = Color.White;
                if (i > 8)
                {
                    Label etc = new Label();
                    etc.Text = " . . .";
                    FinalResults.Children.Add(etc);
                    IpLabel.Text = ($"{ipAdresses[ipAdresses.Count - 1]} /{prefix}");
                    FinalResults.Children.Add(IpLabel);
                    break;
                }
                IpLabel.Text = $" {ipAdresses[i]} /{prefix}";
                FinalResults.Children.Add(IpLabel);
            }
            int sucet = 0;
            for (int i = 7; i > 7 - _rozdiel; i--)
            {
                sucet += Convert.ToInt32(Math.Pow(2, i));
            }
            string subnetMask = "";
            if (_prefix < 8)
            {
                subnetMask = $"{sucet}.0.0.0";
            }
            if (_prefix > 8 & _prefix < 16)
            {
                subnetMask = $"255.{sucet}.0.0";
            }
            if (_prefix > 16 & _prefix < 24)
            {
                subnetMask = $"255.255.{sucet}.0";
            }
            if (_prefix > 24 & _prefix < 32)
            {
                subnetMask = $"255.255.255.{sucet}";
            }
            SubnetMask.TextColor = Color.White;
            NumOfHosts.TextColor = Color.White;
            SubnetMask.Text = (" Subnet Mask: " + subnetMask).ToString();
            NumOfHosts.Text = _NumOfHosts;
        }
        async void printWarning()
        {
            await DisplayAlert("Chyba", "Zadaj platnú adresu!", "OK");
        }
    }
}