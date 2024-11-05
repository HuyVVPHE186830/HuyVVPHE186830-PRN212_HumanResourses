using Objects;
using Services;
using System;
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
using WpfApp.View;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private Account currentUser;
        private Objects.Account _account;
        private readonly NotificationService notificationService;
        private readonly EmployeeService employeeService;
        public HomeWindow()
        {
            InitializeComponent();
        }

        public HomeWindow(Objects.Account account)
        {
            InitializeComponent();
            _account = account;
            currentUser = account;
            notificationService = new NotificationService();
            employeeService = new EmployeeService();
            CheckPermissions();
            SetButtonVisibility();

        }

        private void SetButtonVisibility()
        {

            if (currentUser.RoleId == 3)
            {
                btnEmployeeManagement.Visibility = Visibility.Collapsed;
                btnLeaveRequests.Visibility = Visibility.Collapsed;
                btnDepartmentManagement.Visibility = Visibility.Collapsed;
                btnViewProfile.Visibility = Visibility.Visible;
            }
            else if (currentUser.RoleId == 1)
            {
                btnEmployeeManagement.Visibility = Visibility.Visible;
                btnLeaveRequests.Visibility = Visibility.Visible;
                btnDepartmentManagement.Visibility = Visibility.Visible;
                btnViewProfile.Visibility = Visibility.Collapsed;
            }
        }
        private void btnViewProfile_Click(object sender, RoutedEventArgs e)
        {
            ViewProfile vieWindow = new ViewProfile(currentUser);
            vieWindow.Show();
            this.Close();
        }
        private void btnEmployeeManagement_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagementWindow empWindow = new EmployeeManagementWindow(currentUser);
            empWindow.Show();
            this.Close();
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
            }
        }
        private void btnLeaveRequests_Click(object sender, RoutedEventArgs e)
        {
            LeaveRequestsManagement leaveWindow = new LeaveRequestsManagement(_account);
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
            DepartmentManagement departmentManagement = new DepartmentManagement(currentUser);
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

