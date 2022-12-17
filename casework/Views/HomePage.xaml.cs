// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using App2;
using casework.Model;
using casework.SplashScreen;
using casework.Views.DialogTask;
using CaseWork.Models;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        DateTime startDate = DateTime.Now;
        public HomePage()
        {
            this.InitializeComponent();

            PopulateProjects();
            Temp();

        }

        private async void PopulateProjects()
        {
            List<Project> Projects = new List<Project>();

            Project newProject = new Project();

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue = localSettings.Values["JwtToken"] as string;


            String a = await new ReqService().GetTask($"{Constants.URL}Tasks/get/filter", localValue);
            List<CaseWork.Models.Task> rec =
                   JsonSerializer.Deserialize<List<CaseWork.Models.Task>>(a);
            foreach (var item in rec)
            {
                if (item.DeadLineGet < DateTime.Now)
                {
                    item.UrgencyColor = Constants.Red;
                }
                else if (SplashScreenPage.UnixTimeStampToDateTime(item.deadLine - 172800) < DateTime.Now)
                {
                    item.UrgencyColor = Constants.Yellow;
                }
                newProject.Activities.Add(item);
            }
            


            Projects.Add(newProject);

            cvsProjects.Source = Projects;


        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            CaseWork.Models.Task a = (CaseWork.Models.Task)e.ClickedItem;
            SplashScreenPage.NavigateNextPage("OpenTaskPage", a.title, a);
        }
        private void Temp() {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue = localSettings.Values["Name"] as string;
            if (DateTime.Now.Hour <= 24 && DateTime.Now.Hour >= 19)
            {
                Header.Text = "���������� ����� ��� ������������ ������";
            }
            else if (DateTime.Now.Hour < 19 && DateTime.Now.Hour >= 9)
            {
                Header.Text = "������� ���� ��� �� ����������";
            }
            else if (DateTime.Now.Hour < 9 && DateTime.Now.Hour >= 6)
            {
                Header.Text = "������ ����, " + localValue;
            }
            else if (DateTime.Now.Hour < 6)
            {
                Header.Text = localValue + ", ���� �� ���������";
            }
        }

        private async void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue = localSettings.Values["JwtToken"] as string;

            NewTask task = await SplashScreenPage.ShowCreateTaskDialog(XamlRoot) as NewTask;
            if (task != null)
            {
                if (task.User != null)
                {
                    await new ReqService().Post($"{Constants.URL}Tasks/create/{task.User}", task, localValue);
                }
                else 
                {
                    await new ReqService().Post($"{Constants.URL}Tasks/create/{localSettings.Values["Email"] as string}", task, localValue);
                }
                PopulateProjects();
            }
        }
    }

    public class Project
    {
        public Project()
        {
            Activities = new ObservableCollection<CaseWork.Models.Task>();
        }
        public ObservableCollection<CaseWork.Models.Task> Activities { get; private set; }
    }



}
