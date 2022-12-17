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
using CaseWork.Models;
using casework.SplashScreen;
using System.Drawing;
using Microsoft.UI;
using Windows.UI;


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
        }

        private void RuLocalization() {
            Do.Text = "До";
            Ot.Text = "От";
            TextUrgency.Text = "Приоритет";
            TextDescription.Text = "Описание";
        }
    }
}
