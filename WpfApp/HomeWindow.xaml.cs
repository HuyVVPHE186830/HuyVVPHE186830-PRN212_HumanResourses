using Objects;
using Services;
using System;
using System.Collections.Generic;
using System.Windows;
using WpfApp.View;

namespace WpfApp
{
    public partial class HomeWindow : Window
    {
        private Objects.Account _account;
        private readonly NotificationService notificationService;
        private readonly EmployeeService employeeService;
        public HomeWindow(Objects.Account account)
        {
            InitializeComponent();
            _account = account;
            notificationService = new NotificationService();
            employeeService = new EmployeeService(); 
            CheckPermissions();
            LoadNotificationsByDepartmentId();
        }

        private void LoadNotificationsByDepartmentId()
        {
            Employee employee = employeeService.GetEmployeeByAccountId(_account.AccountId);

            if (employee != null)
            {
                List<Notification> notifications = notificationService.GetNotisByDepartId(employee.DepartmentId);
                DepartmentNotificationsListBox.ItemsSource = notifications; 
            }
            else
            {
                MessageBox.Show("Employee not found.");
            }
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

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow reportWindow = new ReportWindow();
            reportWindow.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void CheckPermissions()
        {
            if (_account.RoleId == 1 || _account.RoleId == 2)
            {
                btnReport.Visibility = Visibility.Visible;
            }
            else if (_account.RoleId == 3)
            {
                btnReport.Visibility = Visibility.Collapsed;
            }
        }

        private void btnDepartmentManagement_Click(object sender, RoutedEventArgs e)
        {
            DepartmentManagement departmentManagement = new DepartmentManagement(_account);
            departmentManagement.Show();
            this.Close();
        }

        private void btnNotificationManagement_Click(object sender, RoutedEventArgs e)
        {
            NotificationManagement notificationManagement = new NotificationManagement(_account);
            notificationManagement.Show();
            this.Close();
        }
    }
}
