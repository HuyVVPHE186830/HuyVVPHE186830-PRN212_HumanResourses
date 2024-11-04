using Objects;
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
        public HomeWindow()
        {
            InitializeComponent();
        }

        public HomeWindow(Objects.Account account)
        {
            InitializeComponent();
            _account = account;
            currentUser = account;
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
            DepartmentManagement departmentManagement = new DepartmentManagement();
            departmentManagement.Show();
            this.Close();
        }
    }
}

