using Objects;
using Services; 
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class DepartmentManagement : Window
    {
        private readonly DepartmentService _departmentService;
        private readonly EmployeeService _employeeService;
        private Objects.Account _account;
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }

        public DepartmentManagement(Objects.Account account)
        {
            InitializeComponent();
            _departmentService = new DepartmentService();
            _employeeService = new EmployeeService();
            _account = account;
            Departments = new ObservableCollection<Department>();
            Employees = new ObservableCollection<Employee>();

            cbDepartments.ItemsSource = Departments;
            dgEmployees.ItemsSource = Employees;

            LoadDepartments();
        }

        private void LoadDepartments()
        {
            var departments = _departmentService.GetDepartments();
            Departments.Clear();
            foreach (var department in departments)
            {
                Departments.Add(department);
            }
        }

        private void LoadEmployeesForDepartment(Department department)
        {
            var employees = _employeeService.GetEmployeesByDepartmentId(department.DepartmentId);
            Employees.Clear();
            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
        }

        private void cbDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDepartments.SelectedItem is Department selectedDepartment)
            {
                txtDepartmentName.Text = selectedDepartment.DepartmentName;
                LoadEmployeesForDepartment(selectedDepartment);
            }
        }

        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            var newDepartmentName = txtDepartmentName.Text.Trim();
            if (string.IsNullOrEmpty(newDepartmentName))
            {
                MessageBox.Show("Please enter a department name.");
                return;
            }

            var newDepartment = new Department { DepartmentName = newDepartmentName };
            _departmentService.AddDepartment(newDepartment);
            LoadDepartments();
            MessageBox.Show("Department added successfully!");
        }

        private void UpdateDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (cbDepartments.SelectedItem is Department selectedDepartment)
            {
                var updatedDepartmentName = txtDepartmentName.Text.Trim();
                if (string.IsNullOrEmpty(updatedDepartmentName))
                {
                    MessageBox.Show("Please enter a new department name.");
                    return;
                }

                selectedDepartment.DepartmentName = updatedDepartmentName;
                _departmentService.UpdateDepartment(selectedDepartment);
                LoadDepartments();
                MessageBox.Show("Department updated successfully!");
            }
            else
            {
                MessageBox.Show("Please select a department to update.");
            }
        }

        private void DeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (cbDepartments.SelectedItem is Department selectedDepartment)
            {
                _departmentService.DeleteDepartment(selectedDepartment);
                LoadDepartments();
                Employees.Clear();
                MessageBox.Show("Department deleted successfully!");
            }
            else
            {
                MessageBox.Show("Please select a department to delete.");
            }
        }

        

        private void dgEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow home = new HomeWindow(_account);
            home.Show();
            this.Close();
        }
    }
}
