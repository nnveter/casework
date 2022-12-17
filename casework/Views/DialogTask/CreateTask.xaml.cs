// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using ABI.System;
using casework.Model;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace casework.Views.DialogTask
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateTask : ContentDialog
    {
        public NewTask Task;
        public CreateTask()
        {
            this.InitializeComponent();

            DeadLineDatePicker.SelectedDate = DateTime.Now;
            DeadLineTimePicker.SelectedTime = DateTime.Now.TimeOfDay;
        }

        private void AddUserCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxAddUser.Visibility = Visibility.Visible;
        }

        private void AddUserCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxAddUser.Visibility = Visibility.Collapsed;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AssignmentBox.Document.GetText((Microsoft.UI.Text.TextGetOptions)Windows.UI.Text.TextSetOptions.None, out string RichText);
            RichText = RichText.TrimEnd('\r');

            DateTime date = DeadLineDatePicker.Date.Date + DeadLineTimePicker.Time;
            String User = null;

            if ((bool)AddUserCheckBox.IsChecked) {
                User = TextBoxAddUser.Text;
            }
            Task = new NewTask() {Title = TitleBox.Text, Assignment = RichText, Urgency = (int)UrgencySlider.Value, DeadLine = ((System.DateTimeOffset)date).ToUnixTimeSeconds(), User = User };
        }
    }
}
