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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for LeaveRequestsManagement.xaml
    /// </summary>
    public partial class LeaveRequestsManagement : Window
    {
        private Objects.Account _account;
        private readonly LeaveRequestService leaveRequestService;
        public LeaveRequestsManagement(Objects.Account account)
        {
            _account = account;
            InitializeComponent();
            leaveRequestService = new LeaveRequestService();
            LoadLeaveRequests();
        }

        public void LoadLeaveRequests()
        {
            var leaveRequests = leaveRequestService.GetLeaveRequests();
            LeaveRequestsDataGrid.ItemsSource = leaveRequests;
        }

        private void DataGridLeaveRequest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var leaveRequest = LeaveRequestsDataGrid.SelectedItem as LeaveRequest;
            DisplayLeaveRequestDetails(leaveRequest);
        }
        private void DisplayLeaveRequestDetails(LeaveRequest leaveRequest)
        {
            if (leaveRequest != null)
            {
                TextBoxEmployee.Text = leaveRequest.Employee.FullName;
                TextBoxStartDate.Text = leaveRequest.StartDate.ToString();
                TextBoxEndDate.Text = leaveRequest.EndDate.ToString();
                TextBoxLeaveType.Text = leaveRequest.LeaveType.ToString();
                if (leaveRequest.Status == "Pending")
                {
                    ComboBoxStatus.SelectedIndex = 0;
                }
                else if (leaveRequest.Status == "Approved")
                {
                    ComboBoxStatus.SelectedIndex = 1;
                }
                else
                {
                    ComboBoxStatus.SelectedIndex = 2;
                }
            }
        }

        private void UpdateLeaveRequest_Click(object sender, RoutedEventArgs e)
        {
            var leaveRequest = LeaveRequestsDataGrid.SelectedItem as LeaveRequest;
            if (leaveRequest == null)
            {
                MessageBox.Show("Please select a leave request to update.");
            }
            else
            {
                leaveRequest.Status = ComboBoxStatus.Text;
                leaveRequestService.UpdateLeaveRequest(leaveRequest);
                LoadLeaveRequests();
            }
        }
        private void BacktoHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow home = new HomeWindow(_account);
            home.Show();
            this.Close();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
