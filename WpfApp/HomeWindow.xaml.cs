﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }
        private void SetButtonVisibility()
        {
            //if (currentUser.RoleId == 2) 
            //{
            //    btnEmployeeManagement.Visibility = Visibility.Collapsed; 
            //    btnLeaveRequests.Visibility = Visibility.Collapsed; 
            //    btnSalary.Visibility = Visibility.Collapsed; 
            //}
            //else if (currentUser.RoleId == 1)
            //{
            //    btnEmployeeManagement.Visibility = Visibility.Visible;
            //    btnLeaveRequests.Visibility = Visibility.Visible;
            //    btnSalary.Visibility = Visibility.Visible;
            //}
        }
        private void btnEmployeeManagement_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagementWindow empWindow = new EmployeeManagementWindow();
            empWindow.Show();
            this.Close();
        }

        private void btnLeaveRequests_Click(object sender, RoutedEventArgs e)
        {
            LeaveRequestsManagement leaveWindow = new LeaveRequestsManagement();
            leaveWindow.Show();
            this.Close();
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

