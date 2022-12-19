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
using CaseWork.Model;
using casework.SplashScreen;
using System.Drawing;
using Microsoft.UI;
using Windows.UI;
using App2;
using Windows.Storage;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OpenTaskPage : Page
    {
        public static Task task;
        public string colors = Constants.White;
        public OpenTaskPage()
        {
            this.InitializeComponent();

            RuLocalization();

            task = SplashScreenPage.task;
            Title.Text = task.title;
            Urgency.Text = task.urgency.ToString() + "%";
            UrgencyBar.Value = task.urgency;
            Assignment.Text = task.assignment;
            Executor.Text = task.executor.firstName[0] + ". " + task.executor.lastName;
            DeadLine.Text = task.DeadLineGetString;

            if (task.DeadLineGet < DateTime.Now)
            {
                colors = Constants.Red;
            }
            else if (SplashScreenPage.UnixTimeStampToDateTime(task.deadLine - 172800) < DateTime.Now)
            {
                colors = Constants.Yellow;
            }
            if ((bool)task.isComplete)
            { 
                CompleteButton.IsEnabled = false;
                CompleteButton.Content = "task completed";
                DeadLine.Text = task.CompletedTimeGetString;
                Do.Text = "Выполнено";
                if (task.DeadLineGet < task.CompletedTimeGet)
                {
                    colors = Constants.Red;
                }
            }
        }

        private void RuLocalization() {
            Do.Text = "До";
            Ot.Text = "От";
            TextUrgency.Text = "Приоритет";
            TextDescription.Text = "Описание";
        }

        private async void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            CompleteButton.IsEnabled = false;
            CompleteButton.Content = "task completed";
            Do.Text = "Выполнено";
            task.completedTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
            DeadLine.Text = task.CompletedTimeGetString;
            if (task.DeadLineGet < task.CompletedTimeGet)
            {
                colors = Constants.Red;
            }

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            await new ReqService().Get($"{Constants.URL}Tasks/complete/{task.id}", localSettings.Values["JwtToken"] as string);
        }
    }
}
