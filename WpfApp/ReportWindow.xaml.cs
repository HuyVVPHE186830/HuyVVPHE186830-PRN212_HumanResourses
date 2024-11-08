using Objects;
using OfficeOpenXml;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using DataAccessLayer;

namespace WpfApp
{
    public partial class ReportWindow : Window
    {
        private readonly DepartmentService _departmentService;
        private readonly PositionService _positionService;
        private readonly EmployeeService _employeeService;
        private readonly SalaryService _salaryService;
        private Objects.Account _account;

        public ReportWindow(Objects.Account account)
        {
            _account = account;
            InitializeComponent();
            _departmentService = new DepartmentService();
            _positionService = new PositionService();
            _employeeService = new EmployeeService();
            _salaryService = new SalaryService();

            LoadDepartments();
            LoadPositions();
            LoadGenders();

            cbDepartment.SelectedIndex = 0;
            cbPosition.SelectedIndex = 0;
            cbGender.SelectedIndex = 0;
            cbTimePeriod.SelectedIndex = 0;
            cbMonth.SelectedIndex = 0;
            cbMonth.Visibility = Visibility.Visible;
            cbQuarter.Visibility = Visibility.Collapsed;
            LoadSalariesByMonth(1);
        }

        private void LoadDepartments()
        {
            var departments = _departmentService.GetDepartments();
            departments.Insert(0, new Department { DepartmentId = 0, DepartmentName = "All" });
            cbDepartment.ItemsSource = departments;
            cbDepartment.DisplayMemberPath = "DepartmentName";
            cbDepartment.SelectedValuePath = "DepartmentId";
        }

        private void LoadPositions()
        {
            var positions = _positionService.GetPositions();
            positions.Insert(0, new Position { PositionId = 0, PositionName = "All" });
            cbPosition.ItemsSource = positions;
            cbPosition.DisplayMemberPath = "PositionName";
            cbPosition.SelectedValuePath = "PositionId";
        }

        private void LoadGenders()
        {
            cbGender.ItemsSource = new List<string> { "All", "Male", "Female" };
        }

        private void UpdateStatistics()
        {
            int departmentId = cbDepartment.SelectedValue is int depId ? depId : 0;
            int positionId = cbPosition.SelectedValue is int posId ? posId : 0;
            string selectedGender = cbGender.SelectedItem as string;

            var employees = _employeeService.GetEmployees();
            if (departmentId > 0)
            {
                employees = employees.Where(e => e.DepartmentId == departmentId).ToList();
            }
            if (positionId > 0)
            {
                employees = employees.Where(e => e.PositionId == positionId).ToList();
            }
            if (selectedGender != "All" && !string.IsNullOrEmpty(selectedGender))
            {
                employees = employees.Where(e => e.Gender == selectedGender).ToList();
            }
            dgData.ItemsSource = employees.ToList();
            txtTotalEmployees.Text = employees.Count.ToString();
        }

        private void cbDepartment_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateStatistics();
        }

        private void cbPosition_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateStatistics();
        }

        private void cbGender_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateStatistics();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var employees = dgData.ItemsSource as IEnumerable<Employee>;
            if (employees == null || !employees.Any())
            {
                MessageBox.Show("No data available to export.");
                return;
            }
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employee Data");
                worksheet.Cells[1, 1].Value = "Full Name";
                worksheet.Cells[1, 2].Value = "Date Of Birth";
                worksheet.Cells[1, 3].Value = "Gender";
                worksheet.Cells[1, 4].Value = "Address";
                worksheet.Cells[1, 5].Value = "Phone Number";
                worksheet.Cells[1, 6].Value = "Salary";
                worksheet.Cells[1, 7].Value = "Department";
                worksheet.Cells[1, 8].Value = "Position";
                int row = 2;
                foreach (var employee in employees)
                {
                    worksheet.Cells[row, 1].Value = employee.FullName;
                    worksheet.Cells[row, 2].Value = employee.DateOfBirth.ToShortDateString();
                    worksheet.Cells[row, 3].Value = employee.Gender;
                    worksheet.Cells[row, 4].Value = employee.Address;
                    worksheet.Cells[row, 5].Value = employee.PhoneNumber;
                    worksheet.Cells[row, 6].Value = employee.Salary;
                    worksheet.Cells[row, 7].Value = employee.Department.DepartmentName;
                    worksheet.Cells[row, 8].Value = employee.Position.PositionName;
                    row++;
                }
                worksheet.Cells[row, 1].Value = "Total Employees:";
                worksheet.Cells[row, 2].Value = employees.Count();
                worksheet.Cells[row, 1, row, 2].Style.Font.Bold = true;

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Save an Excel File"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    FileInfo fi = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(fi);
                    MessageBox.Show("Data exported successfully.");
                }
            }
        }

        private void LoadSalaryData()
        {
            try
            {
                var salaries = SalaryDAO.GetSalaries();
                dgSalaryData.ItemsSource = salaries;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading salaries: " + ex.Message);
            }
        }

        private void LoadAllSalaries()
        {
            try
            {
                var allSalaries = SalaryDAO.GetSalaries();
                dgSalaryData.ItemsSource = allSalaries.ToList();
                txtTotalSalary.Text = allSalaries.Sum(s => s.TotalIncome).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading all salaries: " + ex.Message);
            }
        }

        private void btnExportSalary_Click(object sender, RoutedEventArgs e)
        {
            var salaries = dgSalaryData.ItemsSource as IEnumerable<Salary>; ;
            if (salaries == null || !salaries.Any())
            {
                MessageBox.Show("No data available to export.");
                return;
            }
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Salary Data");
                worksheet.Cells[1, 1].Value = "Employee Name";
                worksheet.Cells[1, 2].Value = "Salary";
                worksheet.Cells[1, 3].Value = "Payment Date";

                int row = 2;
                decimal totalSalary = 0;

                foreach (var salary in salaries)
                {
                    worksheet.Cells[row, 1].Value = salary.Employee.FullName;
                    decimal salaryAmount = (decimal)(salary.TotalIncome ?? 0);

                    worksheet.Cells[row, 2].Value = salaryAmount;
                    worksheet.Cells[row, 3].Value = salary.PaymentDate.ToShortDateString();
                    totalSalary += salaryAmount;
                    row++;
                }

                worksheet.Cells[row, 1].Value = "Total Salary:";
                worksheet.Cells[row, 2].Value = totalSalary;
                worksheet.Cells[row, 1, row, 2].Style.Font.Bold = true;

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Save an Excel File"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    FileInfo fi = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(fi);
                    MessageBox.Show("Salary data exported successfully.");
                }
            }
        }



        private void cbTimePeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTimePeriod.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content.ToString() == "Month")
                {
                    cbMonth.Visibility = Visibility.Visible;
                    cbQuarter.Visibility = Visibility.Collapsed;
                    cbMonth.SelectedIndex = -1;
                }
                else if (selectedItem.Content.ToString() == "Quarter")
                {
                    cbMonth.Visibility = Visibility.Collapsed;
                    cbQuarter.Visibility = Visibility.Visible;
                    cbQuarter.SelectedIndex = -1;
                }
            }
        }


        private void cbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbMonth.SelectedItem is ComboBoxItem selectedItem)
            {
                int selectedMonth = int.Parse(selectedItem.Content.ToString());
                LoadSalariesByMonth(selectedMonth);
            }
        }

        private void cbQuarter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbQuarter.SelectedItem is ComboBoxItem selectedItem)
            {
                int selectedQuarter = int.Parse(selectedItem.Content.ToString());
                LoadSalariesByQuarter(selectedQuarter);
            }
        }

        private void LoadSalariesByMonth(int month)
        {
            try
            {
                var salaries = SalaryDAO.GetSalaries()
                    .Where(s => s.PaymentDate.Month == month)
                    .ToList();

                dgSalaryData.ItemsSource = salaries;
                txtTotalSalary.Text = salaries.Sum(s => s.TotalIncome).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading salaries: " + ex.Message);
            }
        }

        private void LoadSalariesByQuarter(int quarter)
        {
            try
            {
                int startMonth = (quarter - 1) * 3 + 1;
                int endMonth = startMonth + 2;

                var salaries = SalaryDAO.GetSalaries()
                    .Where(s => s.PaymentDate.Month >= startMonth && s.PaymentDate.Month <= endMonth)
                    .ToList();

                dgSalaryData.ItemsSource = salaries;
                txtTotalSalary.Text = salaries.Sum(s => s.TotalIncome).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading salaries: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow reportWindow = new HomeWindow(_account);
            reportWindow.Show();
            this.Close();
        }
    }
}
