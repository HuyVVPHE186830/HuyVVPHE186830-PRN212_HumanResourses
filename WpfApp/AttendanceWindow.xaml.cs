using DataAccessLayer;
using Objects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for AttendanceDialog.xaml
    /// </summary>
    public partial class AttendanceWindow : Window
    {
        public List<Employee> Employees { get; set; }
        private readonly IAttendanceService iAttendanceService;
        private Employee selectedEmployee;

        public AttendanceWindow(List<Employee> employees, IAttendanceService attendanceService)
        {
            InitializeComponent();
            Employees = employees;
            iAttendanceService = attendanceService;  
            dgAttendanceList.ItemsSource = Employees;
        }



        

            private void dgAttendanceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                selectedEmployee = dgAttendanceList.SelectedItem as Employee;

                if (selectedEmployee != null)
                {
                    PresentButton.IsEnabled = true;
                    AbsentButton.IsEnabled = true;
                    ViewButton.IsEnabled = true;  
                }
                else
                {
                    PresentButton.IsEnabled = false;
                    AbsentButton.IsEnabled = false;
                    ViewButton.IsEnabled = false;  
                }
            }

        

        private void PresentButton_Click(object sender, RoutedEventArgs e)
        {
            if (iAttendanceService == null)
            {
                MessageBox.Show("Attendance service is not initialized.");
                return;
            }

            if (selectedEmployee != null)
            {
                var today = DateOnly.FromDateTime(DateTime.Now);

                if (iAttendanceService.AttendanceExists(selectedEmployee.EmployeeId, today))
                {
                    MessageBox.Show("Attendance already recorded for today.");
                    return;
                }

                Attendance newAttendance = new Attendance
                {
                    EmployeeId = selectedEmployee.EmployeeId,
                    Date = today,
                    Status = "Present"
                };

                OvertimeDialog overtimeDialog = new OvertimeDialog();
                if (overtimeDialog.ShowDialog() == true)
                {
                    newAttendance.OvertimeHours = overtimeDialog.OvertimeHours;
                }

                iAttendanceService.AddAttendance(newAttendance);

                ViewAttendanceButton_Click(sender, e);
            }
        }


        private void AbsentButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee != null)
            {
                Attendance newAttendance = new Attendance
                {
                    EmployeeId = selectedEmployee.EmployeeId,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Status = "Absent",
                    OvertimeHours = 0
                };

                iAttendanceService.AddAttendance(newAttendance);

                ViewAttendanceButton_Click(sender, e);
            }
        }

        private void ViewAttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee == null)
            {
                MessageBox.Show("Please select an employee.");
                return;
            }
            var attendances = iAttendanceService.GetAttendancesByEmployeeId(selectedEmployee.EmployeeId);

            AttendanceListDialog attendanceDialog = new AttendanceListDialog(attendances);
            attendanceDialog.ShowDialog();
        }

    }
}
