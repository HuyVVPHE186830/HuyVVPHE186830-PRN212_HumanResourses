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
        private double _salary;
        private int _employeeID;
        FuhrmContext context = new FuhrmContext();
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
            //_salary = salary;
            LoadData();
        }
        private void LoadData()
        {
            
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == _employeeID);

            if (employee != null)
            {
                
                var salaries = context.Salaries
                    .Where(s => s.EmployeeId == _employeeID)
                    .Include("Employee")
                    .ToList();

                SalaryDataGrid.ItemsSource = salaries; 
            }
            else
            {
                MessageBox.Show("Employee not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy nhân viên từ bảng Employees
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == _employeeID);

            // Nếu nhân viên tồn tại, sử dụng Salary làm BaseSalary
            double baseSalary = employee?.Salary ?? 0; // Gán 0 nếu không tìm thấy

            // Tạo cửa sổ thêm lương
            AddSlaWindow addSlaWindow = new AddSlaWindow(_employeeID, baseSalary);
            addSlaWindow.SalaryUpdated += LoadData;

            if (addSlaWindow.ShowDialog() == true)
            {
                Salary newSalary = new Salary
                {
                    EmployeeId = _employeeID,
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
        }

        private void EditSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            if (SalaryDataGrid.SelectedItem is Salary selectedSalary)
            {
              
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
                   

                    selectedSalary.BaseSalary = double.Parse(editSlaWindow.BaseSalaryTextBox.Text);
                    selectedSalary.Allowance = string.IsNullOrWhiteSpace(editSlaWindow.AllowanceTextBox.Text) ? (double?)null : double.Parse(editSlaWindow.AllowanceTextBox.Text);
                    selectedSalary.Bonus = string.IsNullOrWhiteSpace(editSlaWindow.BonusTextBox.Text) ? (double?)null : double.Parse(editSlaWindow.BonusTextBox.Text);
                    selectedSalary.Penalty = string.IsNullOrWhiteSpace(editSlaWindow.PenaltyTextBox.Text) ? (double?)null : double.Parse(editSlaWindow.PenaltyTextBox.Text);
                    selectedSalary.PaymentDate = DateOnly.FromDateTime(DateTime.Now);

                    context.SaveChanges(); 
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để chỉnh sửa.");
            }
        }

    }
}
