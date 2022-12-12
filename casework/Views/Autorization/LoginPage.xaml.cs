// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using App2;
using casework.SplashScreen;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views.Autorization
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            var res = await new ReqService().Post<UserCred>($"{Constants.URL}Auth/login", new UserCred(Email.Text, passworBoxWithRevealmode.Password));
            if (res[0] == '|')
            {
                res = res.TrimStart('|');
                infoBar.Message = res;
                infoBar.IsOpen = true;

            }
            else
            {
                ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["JwtToken"] = res;
                SplashScreenPage.NavigateNextPage("Home");
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            SplashScreenPage.NavigateNextPage("Registration");
        }
    }
    public record UserCred(string Email, string Password);
}
