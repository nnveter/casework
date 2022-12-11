// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using ABI.System;
using casework.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.SplashScreen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashScreenPage : Page
    {
        public SplashScreenPage()
        {
            this.InitializeComponent();
        }

        public static async void Pro()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // load a setting that is local to the device
            String localValue = localSettings.Values["JwtToken"] as string;
            if (localValue != null && localValue[0] != '|') {
                NavigateNextPage("Home");
            }
            else
            {
                NavigateNextPage("Login");
            }

        }


        /// <summary>
        /// Метод прехода между страницами;
        /// page = "Home";"Login";"Registration";
        /// </summary>
        public static bool NavigateNextPage(String page, String Header = "") {
            if (page == "Home")
            {
                MainWindow.ContentFrame1.Navigate(typeof(HomePage), Header);
                MainWindow.NavigationView1.Header = Header;
                MainWindow.NavigationView1.SelectedItem = MainWindow.HomeItem1;
                MainWindow.NavigationView1.IsPaneVisible = true;
                return true;
            }
            else if (page == "Login")
            {
                MainWindow.ContentFrame1.Navigate(typeof(LoginPage), Header);
                return true;
            }
            else if (page == "Registration")
            {
                MainWindow.ContentFrame1.Navigate(typeof(RegistrationPage), Header);
                return true;
            }
            else if (page == "Registration2")
            {
                MainWindow.ContentFrame1.Navigate(typeof(RegistrationPage2), Header);
                return true;
            }
            else { return false; }
        }
    }
}
