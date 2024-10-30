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
using DataAccessLayer;
using Objects;

namespace WpfApp
{
    public partial class EditSlaWindow : Window
    {
        public event Action SalaryUpdated;
        private readonly EmployeeDAO employeeDAO = new();
        private readonly SalaryDAO salaryDAO = new SalaryDAO();
        public Salary Salary { get; private set; }

        public EditSlaWindow(Salary salary)
        {
            InitializeComponent();
            Salary = salary;
            LoadSalaryInfo();
        }

        private void LoadSalaryInfo()
        {
            EmployeeNameTextBlock.Text = Salary.Employee.FullName; // Giả sử Salary có thuộc tính Employee
            BaseSalaryTextBox.Text = Salary.BaseSalary.ToString();
            AllowanceTextBox.Text = Salary.Allowance?.ToString();
            BonusTextBox.Text = Salary.Bonus?.ToString();
            PenaltyTextBox.Text = Salary.Penalty?.ToString();
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

            // Cập nhật thông tin lương
            Salary.BaseSalary = baseSalary;
            Salary.Allowance = allowance;
            Salary.Bonus = bonus;
            Salary.Penalty = penalty;

            // Lưu thay đổi vào cơ sở dữ liệu
            try
            {
                SalaryDAO.UpdateSalary(Salary); // Giả sử bạn đã có phương thức này trong SalaryDAO
                MessageBox.Show("Cập nhật lương thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
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
