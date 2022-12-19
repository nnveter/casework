// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using App2;
using casework.SplashScreen;
using casework.Views.DialogTask;
using CaseWork.Model;
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
            LoadingInviteTask();

        }

        private async void PopulateProjects()
        {
            List<Project> Projects = new List<Project>();

            Project newProject = new Project();

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue = localSettings.Values["JwtToken"] as string;


            String a = await new ReqService().GetTask($"{Constants.URL}Tasks/get/filter/{0}/{3}", localValue);
            List<CaseWork.Model.Task> rec =
                   JsonSerializer.Deserialize<List<CaseWork.Model.Task>>(a);
            int id = 0;
            foreach (var item in rec)
            {
                item.ListId = id;
                if (item.DeadLineGet < DateTime.Now)
                {
                    item.UrgencyColor = Constants.Red;
                }
                else if (SplashScreenPage.UnixTimeStampToDateTime(item.deadLine - 172800) < DateTime.Now)
                {
                    item.UrgencyColor = Constants.Yellow;
                }
                newProject.Activities.Add(item);
                id++;
            }
            


            Projects.Add(newProject);

            cvsProjects.Source = Projects;
        }

        public async void LoadingInviteTask()
        {

            List<Project> Projects = new List<Project>();

            Project inviteTasks = new Project();

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue = localSettings.Values["JwtToken"] as string;


            String a = await new ReqService().GetTask($"{Constants.URL}Tasks/get/nonaccepted", localValue);
            List<CaseWork.Model.Task> rec =
                   JsonSerializer.Deserialize<List<CaseWork.Model.Task>>(a);
            int id = 0;
            foreach (var item in rec)
            {
                item.ListId = id;
                if (item.DeadLineGet < DateTime.Now)
                {
                    item.UrgencyColor = Constants.Red;
                }
                else if (SplashScreenPage.UnixTimeStampToDateTime(item.deadLine - 172800) < DateTime.Now)
                {
                    item.UrgencyColor = Constants.Yellow;
                }
                inviteTasks.Activities.Add(item);
                id++;
            }



            Projects.Add(inviteTasks);

            inviteTask.Source = Projects;
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            CaseWork.Model.Task a = (CaseWork.Model.Task)e.ClickedItem;
            SplashScreenPage.NavigateNextPage("OpenTaskPage", a.title, a);
        }
        private void Temp() {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue = localSettings.Values["Name"] as string;
            if (DateTime.Now.Hour <= 24 && DateTime.Now.Hour >= 19)
            {
                Header.Text = "Прекрасный вечер для продуктивной работы";
            }
            else if (DateTime.Now.Hour < 19 && DateTime.Now.Hour >= 9)
            {
                Header.Text = "Хороший день чтобы поработать";
            }
            else if (DateTime.Now.Hour < 9 && DateTime.Now.Hour >= 6)
            {
                Header.Text = "Доброе утро, " + localValue;
            }
            else if (DateTime.Now.Hour < 6)
            {
                Header.Text = localValue + ", пора бы отдохнуть";
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

        private void TestView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private async void DismissButton_Click(object sender, RoutedEventArgs e)
        {
            Button element = (Button)sender;

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue2 = localSettings.Values["JwtToken"] as string;
            CaseWork.Model.Task task = (CaseWork.Model.Task)element.DataContext;
            await new ReqService().Get($"{Constants.URL}Invites/deny/{task.invite.id}", localValue2);
            LoadingInviteTask();
        }

        private async void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            Button element = (Button)sender;

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String localValue2 = localSettings.Values["JwtToken"] as string;
            CaseWork.Model.Task task = (CaseWork.Model.Task)element.DataContext;
            txt.Text = await new ReqService().Get($"{Constants.URL}Invites/accept/{task.invite.id}", localValue2);
            LoadingInviteTask();
            PopulateProjects();
        }
    }

    public class Project
    {
        public Project()
        {
            Activities = new ObservableCollection<CaseWork.Model.Task>();
        }
        public ObservableCollection<CaseWork.Model.Task> Activities { get; private set; }
    }



}
