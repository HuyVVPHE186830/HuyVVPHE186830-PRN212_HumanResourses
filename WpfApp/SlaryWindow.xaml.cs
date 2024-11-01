using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SlaryWindow : Window
    {
        FuhrmContext context = new FuhrmContext();
        int _employeeID;
        public int SalaryId { get; set; }

        public int EmployeeId { get; set; }

        public double BaseSalary { get; set; }

        public double? Allowance { get; set; }

        public double? Bonus { get; set; }

        public double? Penalty { get; set; }

        public double? TotalIncome { get; set; }

        public DateOnly PaymentDate { get; set; }
        public SlaryWindow(int employeeID)
        {
            InitializeComponent();
            _employeeID = employeeID;
            LoadData();
        }
        private void LoadData()
        {
            // Thay thế YourDbContext bằng tên DbContext của bạn
            // Tìm nhân viên theo EmployeeId
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == _employeeID);

            if (employee != null)
            {
                // Lấy các bản ghi lương cho nhân viên này
                var salaries = context.Salaries
                    .Where(s => s.EmployeeId == _employeeID) // Chỉ lấy bản ghi lương cho nhân viên cụ thể
                    .Include("Employee")
                    .ToList();

                SalaryDataGrid.ItemsSource = salaries; // Gán các bản ghi lương vào DataGrid
            }
            else
            {
                MessageBox.Show("Employee not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == _employeeID);

            if (employee != null)
            {
                double baseSalary = employee.Salary; // Lấy lương cơ bản từ nhân viên

                // Mở cửa sổ để thêm lương
                AddSlaWindow addSlaWindow = new AddSlaWindow(employee.EmployeeId, baseSalary);

                // Đặt giá trị baseSalary vào BaseSalaryTextBox trong AddSlaWindow
                addSlaWindow.BaseSalaryTextBox.Text = baseSalary.ToString();

                // Để các trường khác trống
                addSlaWindow.AllowanceTextBox.Text = string.Empty;
                addSlaWindow.BonusTextBox.Text = string.Empty;
                addSlaWindow.PenaltyTextBox.Text = string.Empty;

                addSlaWindow.SalaryUpdated += LoadData;

                if (addSlaWindow.ShowDialog() == true)
                {
                    // Lưu thông tin lương đã thêm
                    try
                    {
                        Salary newSalary = new Salary
                        {
                            EmployeeId = employee.EmployeeId,
                            BaseSalary = double.Parse(addSlaWindow.BaseSalaryTextBox.Text),
                            Allowance = string.IsNullOrWhiteSpace(addSlaWindow.AllowanceTextBox.Text) ? (double?)null : double.Parse(addSlaWindow.AllowanceTextBox.Text),
                            Bonus = string.IsNullOrWhiteSpace(addSlaWindow.BonusTextBox.Text) ? (double?)null : double.Parse(addSlaWindow.BonusTextBox.Text),
                            Penalty = string.IsNullOrWhiteSpace(addSlaWindow.PenaltyTextBox.Text) ? (double?)null : double.Parse(addSlaWindow.PenaltyTextBox.Text),
                            PaymentDate = DateOnly.FromDateTime(DateTime.Now)
                        };

                        context.Salaries.Add(newSalary);
                        context.SaveChanges();
                        LoadData();
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Vui lòng nhập đúng định dạng số.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void EditSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            if (SalaryDataGrid.SelectedItem is Salary selectedSalary)
            {
                // Mở cửa sổ chỉnh sửa và truyền dữ liệu hiện tại vào
                EditSlaWindow editSlaWindow = new EditSlaWindow(selectedSalary);
                {


                    editSlaWindow.EmployeeNameTextBlock.Text = selectedSalary.Employee.FullName;
                    editSlaWindow.BaseSalaryTextBox.Text = selectedSalary.BaseSalary.ToString();
                    editSlaWindow.AllowanceTextBox.Text = selectedSalary.Allowance?.ToString();
                    editSlaWindow.BonusTextBox.Text = selectedSalary.Bonus?.ToString();
                    editSlaWindow.PenaltyTextBox.Text = selectedSalary.Penalty?.ToString();
                };
                editSlaWindow.SalaryUpdated += LoadData;
                if (editSlaWindow.ShowDialog() == true)
                {
                    // Cập nhật thông tin lương

                    selectedSalary.BaseSalary = double.Parse(editSlaWindow.BaseSalaryTextBox.Text);
                    selectedSalary.Allowance = string.IsNullOrWhiteSpace(editSlaWindow.AllowanceTextBox.Text) ? (double?)null : double.Parse(editSlaWindow.AllowanceTextBox.Text);
                    selectedSalary.Bonus = string.IsNullOrWhiteSpace(editSlaWindow.BonusTextBox.Text) ? (double?)null : double.Parse(editSlaWindow.BonusTextBox.Text);
                    selectedSalary.Penalty = string.IsNullOrWhiteSpace(editSlaWindow.PenaltyTextBox.Text) ? (double?)null : double.Parse(editSlaWindow.PenaltyTextBox.Text);
                    selectedSalary.PaymentDate = DateOnly.FromDateTime(DateTime.Now); // Hoặc lấy từ dialog

                    context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                    LoadData(); // Cập nhật DataGrid
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để chỉnh sửa.");
            }
        }

    }
}
