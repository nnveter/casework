// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using App2;
using casework.SplashScreen;
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

namespace casework.Views.Company
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateCompanyPage : Page
    {
        public CreateCompanyPage()
        {
            this.InitializeComponent();

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue = localSettings.Values["JwtToken"] as string;

            CaseWork.Model.Company company = new CaseWork.Model.Company() { name = txt.Text };

            await new ReqService().Post($"{Constants.URL}Companies/create", company, localValue);
            String res3 = await new ReqService().Get($"{Constants.URL}Companies/get/company", localValue);
            if (!string.IsNullOrEmpty(res3))
            {
                SplashScreenPage.company = JsonSerializer.Deserialize<CaseWork.Model.Company>(res3);
                MainWindow.CreateCompanyItem1.Visibility = Visibility.Collapsed;
                MainWindow.CompanyItem1.Visibility = Visibility.Visible;
                MainWindow.CompanyItem1.Content = SplashScreenPage.company.name;
            }
            else
            {
                SplashScreenPage.company = null;
            }
        }
    }
}
