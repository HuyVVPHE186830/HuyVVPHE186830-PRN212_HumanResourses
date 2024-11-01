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
        private int employeeID;

        public AddSlaWindow(int employeeID, double baseSalary)
        {
            InitializeComponent();
            iSalaryService = new SalaryService();
            iEmployeeService = new EmployeeService();

            this.employeeID = employeeID;
            BaseSalaryTextBox.Text = baseSalary.ToString();

            // Lấy tên nhân viên từ ID
            var employee = iEmployeeService.GetEmployeeByEmployeeId(employeeID);
            if (employee != null)
            {
                EmployeeNameTextBlock.Text = employee.FullName; // Hiển thị tên nhân viên
                LoadSalaryInfo(employeeID); // Tải thông tin lương
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            double baseSalary, allowance, bonus, penalty;

            if (!double.TryParse(BaseSalaryTextBox.Text, out baseSalary) ||
                !double.TryParse(AllowanceTextBox.Text, out allowance) ||
                !double.TryParse(BonusTextBox.Text, out bonus) ||
                !double.TryParse(PenaltyTextBox.Text, out penalty))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Tạo đối tượng Salary
            var salary = new Salary
            {
                EmployeeId = employeeID,
                BaseSalary = baseSalary,
                Allowance = allowance,
                Bonus = bonus,
                Penalty = penalty,
                PaymentDate = DateOnly.FromDateTime(DateTime.Now)
            };

            // Lưu thông tin lương vào cơ sở dữ liệu
            try
            {
                iSalaryService.AddSalary(salary);
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
