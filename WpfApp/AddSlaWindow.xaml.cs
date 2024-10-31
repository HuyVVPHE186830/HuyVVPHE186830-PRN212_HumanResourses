using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataAccessLayer;
using Services;

namespace WpfApp
{
    public partial class AddSlaWindow : Window
    {
        public event Action SalaryUpdated;

        private readonly ISalaryService iSalaryService;
        private readonly IEmployeeService iEmployeeService;
        private string _selectedEmployeeName;

        public AddSlaWindow(int employeeId, double baseSalary)
        {
            InitializeComponent();
            BaseSalaryTextBox.Text = baseSalary.ToString();
            
        }

        private void LoadEmployeeNames()
        {

            List<string> employeeNames = iEmployeeService.GetAvailableEmployeeNames();
            EmployeeComboBox.ItemsSource = employeeNames;
        }

        private void LoadSelectedEmployee()
        {
            if (!string.IsNullOrEmpty(_selectedEmployeeName))
            {
                EmployeeComboBox.SelectedItem = _selectedEmployeeName;
                var employee = iEmployeeService.GetEmployees().FirstOrDefault(e => e.FullName == _selectedEmployeeName);
                if (employee != null)
                {
                    LoadSalaryInfo(employee.EmployeeId);
                }
            }
        }

        private void LoadSalaryInfo(int employeeId)
        {
            var salary = iSalaryService.GetSalaryByEmployeeId(employeeId);
            if (salary != null)
            {
                BaseSalaryTextBox.Text = salary.BaseSalary.ToString();
                AllowanceTextBox.Text = salary.Allowance.ToString();
                BonusTextBox.Text = salary.Bonus.ToString();
                PenaltyTextBox.Text = salary.Penalty.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedEmployeeName = EmployeeComboBox.SelectedItem as string;
            double baseSalary, allowance, bonus, penalty;

            if (string.IsNullOrEmpty(selectedEmployeeName) ||
               !double.TryParse(BaseSalaryTextBox.Text, out baseSalary) ||
               !double.TryParse(AllowanceTextBox.Text, out allowance) ||
               !double.TryParse(BonusTextBox.Text, out bonus) ||
               !double.TryParse(PenaltyTextBox.Text, out penalty))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var employee = EmployeeDAO.GetEmployees().FirstOrDefault(e => e.FullName == selectedEmployeeName);
            if (employee == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Tạo đối tượng Salary
            var salary = new Salary
            {
                EmployeeId = employee.EmployeeId,
                BaseSalary = baseSalary,
                Allowance = allowance,
                Bonus = bonus,
                Penalty = penalty,
                PaymentDate = DateOnly.FromDateTime(DateTime.Now)
            };

            // Lưu thông tin lương vào cơ sở dữ liệu
            try
            {
                iSalaryService.AddSalary(salary); ; // Giả sử bạn đã có phương thức này trong SalaryDAO
                MessageBox.Show("Thêm lương thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                SalaryUpdated?.Invoke();
                this.Close(); // Đóng cửa sổ sau khi lưu
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
