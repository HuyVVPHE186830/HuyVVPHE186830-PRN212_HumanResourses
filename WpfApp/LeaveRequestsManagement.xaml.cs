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

    }
}
