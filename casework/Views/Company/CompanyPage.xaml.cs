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
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views.Company
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CompanyPage : Page
    {
        public CompanyPage()
        {
            this.InitializeComponent();
            CompanyName.Text = SplashScreenPage.company.name;
            Pro();
        }

        public async void Pro()
        {
            List<Project> Projects = new List<Project>();

            Project newProject = new Project();

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue = localSettings.Values["JwtToken"] as string;

            String a = await new ReqService().Get($"{Constants.URL}Companies/get/members/{SplashScreenPage.company.id}", localValue);
           List<User> rec =
                   JsonSerializer.Deserialize<List<User>>(a);
            foreach (var item in rec)
            {
                newProject.Activities.Add(item);
            }


           Projects.Add(newProject);
           Users.Source = Projects;
        }

        private void GridV_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button element = (Button)sender;

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue = localSettings.Values["JwtToken"] as string;
            CaseWork.Model.User user = (CaseWork.Model.User)element.DataContext;
        }


        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (Cancel.Visibility == Visibility.Collapsed) 
            {
                Cancel.Visibility = Visibility.Visible;
                GridV.SelectionMode = ListViewSelectionMode.Multiple;
                EditEmployee.Icon = symbol1.Icon;
            }
            else
            {
                //TODO
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Cancel.Visibility = Visibility.Collapsed;
            EditEmployee.Icon = symbol2.Icon;
            GridV.SelectionMode = ListViewSelectionMode.None;
        }
    }

    public class Project
    {
        public Project()
        {
            Activities = new ObservableCollection<User>();
        }
        public ObservableCollection<User> Activities { get; private set; }
    }
}
