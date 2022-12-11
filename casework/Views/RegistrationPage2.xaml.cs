// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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
using App2;
using System.Text.Json;
using casework.Model;
using System.ComponentModel.DataAnnotations;
using casework.SplashScreen;
using Windows.ApplicationModel.ConversationalAgent;
using Windows.Storage;
using System.Xml.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage2 : Page
    {
        private API country;
        [Obsolete]
        public RegistrationPage2()
        {
            this.InitializeComponent();

            String host = System.Net.Dns.GetHostName();
            System.Net.IPAddress ip = System.Net.Dns.GetHostByName(host).AddressList[0];
            Pro(ip.ToString());
        }

        public async void Pro(String ip) {
            string result2 = await new ReqService().Get($"http://www.geoplugin.net/json.gp?ip={ip}");

            country =
                JsonSerializer.Deserialize<API>(result2);
            
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            var res = await new ReqService().Post<UserSignup>($"{Constants.URL}Auth/signup", new UserSignup {
               Email = RegistrationPage.Email1,
               Password = RegistrationPage.Password1,
               FirstName = FirstName.Text, 
               LastName = LastName.Text, 
               City = City.Text, 
               Country = country.geoplugin_countryName, 
               Horse = Horse.IsChecked
           });
            if (res[0] == '|')
            {
                res = res.TrimStart('|');
                infoBar.Message = res;
                infoBar.IsOpen = true;

            }
            else {
                ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["JwtToken"] = res;
                SplashScreenPage.NavigateNextPage("Home");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SplashScreenPage.NavigateNextPage("Registration");
        }
    }

    public class UserSignup
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public bool? Horse { get; set; } = false;
    }
}
