// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using ABI.System;
using App2;
using casework.Model;
using casework.Views;
using casework.Views.Autorization;
using casework.Views.DialogTask;
using CaseWork.Models;
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
using System.Threading.Tasks;
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
        public static CaseWork.Models.Task task;
        public SplashScreenPage()
        {
            this.InitializeComponent();
        }

        public static async void Pro()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // load a setting that is local to the device
            String localValue = localSettings.Values["JwtToken"] as string;
            String res = await new ReqService().Get($"{Constants.URL}Auth/check", localValue);
            if (localValue != null && localValue[0] != '|' && String.IsNullOrEmpty(res) && String.IsNullOrWhiteSpace(res)) {
                NavigateNextPage("Home");
            }
            else
            {
                NavigateNextPage("Login");
            }

        }


        /// <summary>
        /// Метод прехода между страницами;
        /// page = "Home";"Login";"Registration";"Registration2";"OpenTaskPage";
        /// </summary>
        public static bool NavigateNextPage(String page, String Header = null, CaseWork.Models.Task Task = null) {
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
            else if (page == "OpenTaskPage")
            {
                task = Task;
                MainWindow.ContentFrame1.Navigate(typeof(OpenTaskPage), Header);
                MainWindow.NavigationView1.Header = null;
                MainWindow.NavigationView1.SelectedItem = null;
                return true;
            }
            else { return false; }
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static async Task<NewTask> ShowCreateTaskDialog(XamlRoot root) {
            CreateTask dialog = new CreateTask();
            dialog.XamlRoot = root;
            var result = await dialog.ShowAsync();
            return dialog.Task;
        }
    }
}
