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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

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
        }

        private void PopulateProjects()
        {
            List<Project> Projects = new List<Project>();

            Project newProject = new Project();

            newProject.Activities.Add(new Activity()
            { Id = 0 , Name = "Activity 1", Complete = true, DueDate = startDate.AddDays(4) });

            newProject.Activities.Add(new Activity()
            { Id = 1, Name = "Activity 2", Complete = true, DueDate = startDate.AddDays(5) });

            Projects.Add(newProject);

            cvsProjects.Source = Projects;
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Activity a = (Activity)e.ClickedItem;
            txt.Text = a.Id.ToString();
            SplashScreenPage.NavigateNextPage("OpenTaskPage", a.Name);
            
            
            
        }
    }

    public class Project
    {
        public Project()
        {
            Activities = new ObservableCollection<Activity>();
        }
        public ObservableCollection<Activity> Activities { get; private set; }
    }

    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Complete { get; set; }
        public string Project { get; set; }
    }
}
