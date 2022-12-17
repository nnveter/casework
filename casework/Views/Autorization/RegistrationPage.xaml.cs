// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views.Autorization
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {
        public static string Email1;
        public static string Password1;
        public RegistrationPage()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            Email1 = Email.Text;
            Password1 = passworBoxWithRevealmode.Password;

            if (Email1.Contains("@") && Email1.Contains(".") && Password1.Length >= 8 && Email1.Length <= 64 && Email1.Length >= 5)
            {
                SplashScreenPage.NavigateNextPage("Registration2");
            }
            else 
            {
                infoBar.Message = "Почта или пароль не соответсвуют минимальным требованиям или указаны неверно";
                infoBar.IsOpen = true; 
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            SplashScreenPage.NavigateNextPage("Login");
        }
    }


}
