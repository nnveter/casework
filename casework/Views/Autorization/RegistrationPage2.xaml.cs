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
using CaseWork.Model;
using System.ComponentModel.DataAnnotations;
using casework.SplashScreen;
using Windows.ApplicationModel.ConversationalAgent;
using Windows.Storage;
using System.Xml.Linq;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views.Autorization
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage2 : Page
    {
        private String country;

        [Obsolete]
        public RegistrationPage2()
        {
            this.InitializeComponent();
            Pro();
        }

        public void Pro() 
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            // Create a RegionInfo object for the current culture
            RegionInfo currentRegion = new RegionInfo(currentCulture.Name);

            // Display the name of the region
            country = currentRegion.EnglishName;

        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string baseUrl = Constants.URL;
                string path = "Auth/signup";
                Uri url = new Uri(baseUrl + path);

                UserSignup signup = new UserSignup
                {
                    Email = RegistrationPage.Email1,
                    Password = RegistrationPage.Password1,
                    FirstName = FirstName.Text,
                    LastName = LastName.Text,
                    City = City.Text,
                    Country = country,
                    Horse = Horse.IsChecked
                };

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(url, signup);
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();

                    if (result[0] == '|')
                    {
                        result = result.TrimStart('|');
                        infoBar.Message = result;
                        infoBar.IsOpen = true;
                    }
                    else
                    {
                        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                        localSettings.Values["JwtToken"] = result;
                        localSettings.Values["Name"] = Name;
                        localSettings.Values["Email"] = RegistrationPage.Email1;

                        SplashScreenPage.NavigateNextPage("Home");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
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
