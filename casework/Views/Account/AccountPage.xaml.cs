// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using App2;
using casework.SplashScreen;
using casework.Views.Autorization;
using CaseWork.Model;
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
using System.Text.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views.Account
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountPage : Page
    {
        public User ViewModel;
        public AccountPage()
        {
            this.InitializeComponent();
            ViewModel = SplashScreenPage.user;
        }


        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["JwtToken"] = null;
            localSettings.Values["Name"] = null;
            MainWindow.ContentFrame1.Navigate(typeof(LoginPage));
            MainWindow.NavigationView1.IsPaneVisible = false;
        }

        private async void GridV_ItemClick(object sender, ItemClickEventArgs e)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            User user = await SplashScreenPage.ShowOpenUserDataDialog(this.XamlRoot);
            if (user != SplashScreenPage.user && user != null) {
               await new ReqService().Post($"{Constants.URL}Users/update/profile", user, localSettings.Values["JwtToken"] as string);
            }
            SplashScreenPage.user = user;
            ViewModel = user;
        }
    }
}
