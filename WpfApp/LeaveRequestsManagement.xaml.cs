﻿using Objects;
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
        private readonly LeaveRequestService leaveRequestService;
        public LeaveRequestsManagement()
        {
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var leaveRequests = leaveRequestService.SearchLeaveRequest(SearchLeaveTextBox.Text);
            LeaveRequestsDataGrid.ItemsSource = leaveRequests;
        }

        private void ComboBoxYearFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxYearFilter.SelectedItem is ComboBoxItem selectedItem && int.TryParse(selectedItem.Content.ToString(), out int selectedYear))
            {
                var leaveRequests = leaveRequestService.GetLeaveRequestsByYear(selectedYear);
                LeaveRequestsDataGrid.ItemsSource = leaveRequests;
            }
        }

        private void ComboBoxStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedStatus = ComboBoxStatusFilter.SelectedIndex;
            if (selectedStatus == 1)
            {
                var leaveRequests = leaveRequestService.GetLeaveRequestsByStatus("Approved");
                LeaveRequestsDataGrid.ItemsSource = leaveRequests;
            }
            else if (selectedStatus == 2)
            {
                var leaveRequests = leaveRequestService.GetLeaveRequestsByStatus("Pending");
                LeaveRequestsDataGrid.ItemsSource = leaveRequests;
            }
            else if (selectedStatus == 3)
            {
                var leaveRequests = leaveRequestService.GetLeaveRequestsByStatus("Rejected");
                LeaveRequestsDataGrid.ItemsSource = leaveRequests;
            }
        }

        
    }
}
