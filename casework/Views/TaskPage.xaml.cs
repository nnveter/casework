// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using App2;
using System.Text.Json;
using Windows.Storage;
using casework.SplashScreen;
using CaseWork.Models.Dto;
using System.Reflection;
using Microsoft.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskPage : Page
    {
        ObservableCollection<String> ItemsComboBox = new ObservableCollection<String>();
        public TaskPage()
        {
            this.InitializeComponent();

            ItemsComboBox.Add("DeadLine");
            ItemsComboBox.Add("Urgency");
            ItemsComboBox.Add("InComplate");
            ItemsComboBox.Add("Complate");
            ItemsComboBox.Add("All");

            Pro();
        }

        private async void Pro(TasksByFilterAr filter = null) {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            String localValue = localSettings.Values["JwtToken"] as string;
            String response;

            if (filter != null)
            {
                response = await new ReqService().GetTask($"{Constants.URL}Tasks/get/Filter", localValue, filter.TasksAccessFilter, filter.TasksTypeFilter);
            }
            else
            {
                response = await new ReqService().GetTask($"{Constants.URL}Tasks/get/Filter", localValue);
            }

            List<CaseWork.Models.Task> rec =
                   JsonSerializer.Deserialize<List<CaseWork.Models.Task>>(response);

            foreach (var item in rec)
            {
                item.ExecutorString = item.executor.firstName[0] + ". " + item.executor.lastName;
                if (item.DeadLineGet < DateTime.Now)
                {
                    item.UrgencyColor = Constants.Red;
                }
                else if (SplashScreenPage.UnixTimeStampToDateTime(item.deadLine - 172800) < DateTime.Now)
                {
                    item.UrgencyColor = Constants.Yellow;
                }
                TestView.Items.Add(item);
            }
        }

        private void TestView_ItemClick(object sender, ItemClickEventArgs e)
        {
            CaseWork.Models.Task a = (CaseWork.Models.Task)e.ClickedItem;
            SplashScreenPage.NavigateNextPage("OpenTaskPage", null, a);
        }

        private void ComboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TestView.Items.Clear();

            string Filters = e.AddedItems[0].ToString();
            TasksByFilterAr Filter = null;

            switch (Filters)
            {
                case "DeadLine":
                    Filter = new TasksByFilterAr() { TasksAccessFilter = TasksAccessFilter.Executor, TasksTypeFilter = TasksTypeFilter.DeadLine };
                    break;
                case "InComplate":
                    Filter = new TasksByFilterAr() { TasksAccessFilter = TasksAccessFilter.Executor, TasksTypeFilter = TasksTypeFilter.InCompleted };
                    break;
                case "Complate":
                    Filter = new TasksByFilterAr() { TasksAccessFilter = TasksAccessFilter.Executor, TasksTypeFilter = TasksTypeFilter.Completed };
                    break;
                case "All":
                    Filter = new TasksByFilterAr() { TasksAccessFilter = TasksAccessFilter.Executor, TasksTypeFilter = TasksTypeFilter.All };
                    break;
                case "Urgency":
                    Filter = new TasksByFilterAr() { TasksAccessFilter = TasksAccessFilter.Executor, TasksTypeFilter = TasksTypeFilter.Urgency };
                    break;
            }
            Pro(Filter);
        }
    }
}
