// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using ABI.System;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views.Account
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OpenUserData : ContentDialog
    {
        public User user = new User() {email = SplashScreenPage.user.email, 
            firstName = SplashScreenPage.user.firstName, 
            lastName = SplashScreenPage.user.lastName, 
            country = SplashScreenPage.user.country, 
            city = SplashScreenPage.user.city };
        public OpenUserData()
        {
            this.InitializeComponent();
            Email.Text = SplashScreenPage.user.email;
            FirstName.Text = SplashScreenPage.user.firstName;
            LastName.Text = SplashScreenPage.user.lastName;
            Country.Text = SplashScreenPage.user.country;
            City.Text = SplashScreenPage.user.city;
            IsPrimaryButtonEnabled = false;
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        public void City_TextChanged(object sender, TextChangedEventArgs e)
        {
            user.city = City.Text;
            if (EqualityUser())
            {
                IsPrimaryButtonEnabled = false;
            }
            else { IsPrimaryButtonEnabled = true; }
        }

        private void Country_TextChanged(object sender, TextChangedEventArgs e)
        {
            user.country = Country.Text;
            if (EqualityUser())
            {
                IsPrimaryButtonEnabled = false;
            }
            else { IsPrimaryButtonEnabled = true; }
        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            user.lastName = LastName.Text;
            if (EqualityUser())
            {
                IsPrimaryButtonEnabled = false;
            }
            else { IsPrimaryButtonEnabled = true; }
        }

        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            user.firstName = FirstName.Text;
            if (EqualityUser())
            {
                IsPrimaryButtonEnabled = false;
            }
            else { IsPrimaryButtonEnabled = true; }
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            user.email = Email.Text;
            if (EqualityUser())
            {
                IsPrimaryButtonEnabled = false;
            }
            else { IsPrimaryButtonEnabled = true; }
        }

        public bool EqualityUser() 
        {
            if (user.email.Equals(SplashScreenPage.user.email) &&
                user.firstName.Equals(SplashScreenPage.user.firstName) &&
                user.lastName.Equals(SplashScreenPage.user.lastName) &&
                user.country.Equals(SplashScreenPage.user.country) &&
                user.city.Equals(SplashScreenPage.user.city))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            user = null;
        }

        private void ContentDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
        }
    }
}
