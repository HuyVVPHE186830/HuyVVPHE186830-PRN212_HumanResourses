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
    /// Interaction logic for AddLeaveRequest.xaml
    /// </summary>
    public partial class AddLeaveRequest : Window
    {
        private readonly LeaveRequestService leaveRequestService;
        public AddLeaveRequest()
        {
            leaveRequestService = new LeaveRequestService();
            InitializeComponent();
        }

        private void AddLeaveRequest_Click(object sender, RoutedEventArgs e)
        {
            var startDate = DatePicker_StartDate.SelectedDate.Value;
            var endDate = DatePicker_Endate.SelectedDate.Value;
            if (startDate > endDate)
            {
                MessageBox.Show("Start date must be before end date");
                return;
            }
            else if(startDate < DateTime.Now)
            {
                MessageBox.Show("Start date must be in the future");
                return;
            }
            var leaveType = TextBoxLeaveType.Text;
            var leaveRequest = new Objects.LeaveRequest
            {
                EmployeeId = 1,
                StartDate = DateOnly.FromDateTime(startDate),
                EndDate = DateOnly.FromDateTime(endDate),
                LeaveType = leaveType,
                Status = "Pending"
            };
            leaveRequestService.AddLeaveRequest(leaveRequest);
            MessageBox.Show("Leave request added successfully");
            this.Close();

        }
    }
}
