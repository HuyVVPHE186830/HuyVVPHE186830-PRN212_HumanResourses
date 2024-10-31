using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;
using Microsoft.Win32;
using Objects;
using Services;
using System.IO;
using System.Text.Json;
using System.Diagnostics;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for EmployeeManagementWindow.xaml
    /// </summary>
    public partial class EmployeeManagementWindow : Window
    {
        private readonly IAccountService iAccountService;
        private readonly IEmployeeService iEmployeeService;
        private readonly IRoleService iRoleService;
        private readonly IDepartmentService iDepartmentService;
        private readonly IPositionService iPositionService;
      
        public EmployeeManagementWindow()
        {
            InitializeComponent();
            iAccountService = new AccountService();
            iEmployeeService = new EmployeeService();
            iRoleService = new RoleService();
            iDepartmentService = new DepartmentService();
            iPositionService = new PositionService();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadRoleList();
            LoadEmployeeList();
            LoadDepartmentList();
            LoadPositionList();
        }


        public void LoadEmployeeList()
        {
            try
            {
                var accountList = iAccountService.GetAccounts();
                var employeeList = iEmployeeService.GetEmployees();
                var roleList = iRoleService.GetRoles();
                var departmentList = iDepartmentService.GetDepartments();
                var positionList = iPositionService.GetPositions();

                // Kết hợp hai danh sách dựa trên accountId
                var combinedList = from account in accountList
                                   join role in roleList on account.RoleId equals role.RoleId
                                   join employee in employeeList on account.AccountId equals employee.AccountId
                                   join department in departmentList on employee.DepartmentId equals department.DepartmentId
                                   join position in positionList on employee.PositionId equals position.PositionId

                                   select new
                                   {
                                       AccountId = account.AccountId,
                                       Username = account.Username,
                                       Password = account.Password,
                                       FullName = employee.FullName,
                                       RoleName = role.RoleName,
                                       DateOfBirth = employee.DateOfBirth,
                                       Gender = employee.Gender,
                                       Address = employee.Address,
                                       PhoneNumber = employee.PhoneNumber,
                                       Salary = employee.Salary,
                                       DepartmentName = department.DepartmentName,
                                       PositionName = position.PositionName,
                                   };

                dgData.ItemsSource = combinedList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Exception");
            }
            finally
            {
                //resetInput();
            }
        }

        public void LoadRoleList()
        {
            try
            {
                var roleList = iRoleService.GetRoles();
                cboRole.DisplayMemberPath = "RoleName";
                cboRole.SelectedValuePath = "RoleId";
                cboRole.ItemsSource = roleList;
                if (roleList.Any())
                {
                    cboRole.SelectedValue = roleList.First().RoleId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Exception");
            }
            finally
            {
                //resetInput();
            }
        }

        public void LoadDepartmentList()
        {
            try
            {
                var departmentList = iDepartmentService.GetDepartments();
                cboDepartment.DisplayMemberPath = "DepartmentName";
                cboDepartment.SelectedValuePath = "DepartmentId";
                cboDepartment.ItemsSource = departmentList;
                if (departmentList.Any())
                {
                    cboDepartment.SelectedValue = departmentList.First().DepartmentId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Exception");
            }
            finally
            {
                //resetInput();
            }
        }

        public void LoadPositionList()
        {
            try
            {
                var positionList = iPositionService.GetPositions();
                cboPosition.DisplayMemberPath = "PositionName";
                cboPosition.SelectedValuePath = "PositionId";
                cboPosition.ItemsSource = positionList;
                if (positionList.Any())
                {
                    cboPosition.SelectedValue = positionList.First().PositionId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Exception");
            }
            finally
            {
                //resetInput();
            }
        }


        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            // Mở hộp thoại chọn file ảnh
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Chọn ảnh avatar"
            };

            // Kiểm tra nếu người dùng chọn OK
            if (openFileDialog.ShowDialog() == true)
            {
                // Nạp ảnh vào Image control
                avatarImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account account = new Account();
                account.Username = txtUsername.Text;
                account.Password = txtPassword.Text;
                account.RoleId = Int32.Parse(cboRole.SelectedValue.ToString());
                iAccountService.AddAccount(account);
                Employee employee = new Employee();

                employee.AccountId = account.AccountId;
                employee.FullName = txtFullname.Text;
                if (txtDob.SelectedDate.HasValue)
                {
                    employee.DateOfBirth = txtDob.SelectedDate.Value;
                }
                else
                {
                    MessageBox.Show("Please select Date of Birth.");
                    return;
                }
                employee.Gender = cboGender.SelectedValue.ToString();
                employee.PhoneNumber = txtPhoneNumber.Text;
                employee.Address = txtAddress.Text;
                employee.Salary = double.Parse(txtSalary.Text);
                employee.DepartmentId = (int)cboDepartment.SelectedValue;
                employee.PositionId = (int)cboPosition.SelectedValue;
                employee.ProfilePicture = avatarImage.Source != null ? avatarImage.Source.ToString() : null;
                iEmployeeService.AddEmployee(employee);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nStack Trace: " + ex.StackTrace);
            }
            finally
            {
                resetInput();
                LoadRoleList();
                LoadEmployeeList();
                LoadDepartmentList();
                LoadPositionList();
            }
        }

        private void txtSearch_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                var employeeList = iEmployeeService.SearchEmployee(keyword);
                var accountList = iAccountService.GetAccounts();
                var roleList = iRoleService.GetRoles();
                var departmentList = iDepartmentService.GetDepartments();
                var positionList = iPositionService.GetPositions();

                var searchResults = from employee in employeeList
                                    join department in departmentList on employee.DepartmentId equals department.DepartmentId
                                    join position in positionList on employee.PositionId equals position.PositionId
                                    join account in accountList on employee.AccountId equals account.AccountId
                                    join role in roleList on account.RoleId equals role.RoleId
                                    select new
                                    {
                                        AccountId = account.AccountId,
                                        Username = account.Username,
                                        Password = account.Password,
                                        FullName = employee.FullName,
                                        RoleName = role.RoleName,
                                        DateOfBirth = employee.DateOfBirth,
                                        Gender = employee.Gender,
                                        Address = employee.Address,
                                        PhoneNumber = employee.PhoneNumber,
                                        Salary = employee.Salary,
                                        DepartmentName = department.DepartmentName,
                                        PositionName = position.PositionName,
                                    };
                dgData.ItemsSource = searchResults;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error during search");
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtAccountId.Text.Length > 0)
                {
                    Account account = new Account();
                    account.AccountId = Int32.Parse(txtAccountId.Text);
                    account.Username = txtUsername.Text;
                    account.Password = txtPassword.Text;
                    account.RoleId = Int32.Parse(cboRole.SelectedValue.ToString());
                    Employee employee = iEmployeeService.GetEmployeeByAccountId(account.AccountId);
                    employee.FullName = txtFullname.Text;
                    if (txtDob.SelectedDate.HasValue)
                    {
                        employee.DateOfBirth = txtDob.SelectedDate.Value;
                    }
                    else
                    {
                        MessageBox.Show("Please select Date of Birth.");
                        return;
                    }
                    employee.Gender = cboGender.SelectedValue.ToString();
                    employee.PhoneNumber = txtPhoneNumber.Text;
                    employee.Address = txtAddress.Text;
                    employee.Salary = double.Parse(txtSalary.Text);
                    employee.DepartmentId = (int)cboDepartment.SelectedValue;
                    employee.PositionId = (int)cboPosition.SelectedValue;
                    employee.ProfilePicture = avatarImage.Source != null ? avatarImage.Source.ToString() : null;
                    iAccountService.UpdateAccount(account);
                    iEmployeeService.UpdateEmployee(employee);
                }
                else
                {
                    MessageBox.Show("You must select a Employee");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                resetInput();
                LoadRoleList();
                LoadEmployeeList();
                LoadDepartmentList();
                LoadPositionList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtAccountId.Text.Length > 0)
                {
                    int accountId = Int32.Parse(txtAccountId.Text);
                    var accountList = iAccountService.GetAccounts();
                    var employeeList = iEmployeeService.GetEmployees();
                    var account = accountList.FirstOrDefault(a => a.AccountId == accountId);
                    var employee = employeeList.FirstOrDefault(a => a.AccountId == accountId);
                    iEmployeeService.DeleteEmployee(employee);
                    iAccountService.DeleteAccount(account);
                }
                else
                {
                    MessageBox.Show("You must select a Employee");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                resetInput();
                LoadRoleList();
                LoadEmployeeList();
                LoadDepartmentList();
                LoadPositionList();
            }

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtAccountId.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtFullname.Text = "";
            txtDob.SelectedDate = null; 
            txtPhoneNumber.Text = "";
            txtAddress.Text = "";
            txtSalary.Text = "";

            cboRole.SelectedIndex = 0;
            cboGender.SelectedIndex = 0;
            cboDepartment.SelectedIndex = 0;
            cboPosition.SelectedIndex = 0;

            avatarImage.Source = null;
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedIndex >= 0)
            {

                DataGrid dataGrid = sender as DataGrid;
                DataGridRow row =
                    (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dgData.SelectedIndex);
                if (row != null)

                {
                    DataGridCell RowColumn =
                    dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                    string id = ((TextBlock)RowColumn.Content).Text;
                    Account account = iAccountService.GetAccountByAccountId(Int32.Parse(id));
                    Employee employee = iEmployeeService.GetEmployeeByAccountId(account.AccountId);
                    txtAccountId.Text = account.AccountId.ToString();
                    txtUsername.Text = account.Username;
                    txtPassword.Text = account.Password;
                    cboRole.SelectedValue = account.RoleId;
                    txtFullname.Text = employee.FullName;
                    txtDob.Text = employee.DateOfBirth.ToString();
                    cboGender.SelectedValue = employee.Gender;
                    txtPhoneNumber.Text = employee.PhoneNumber;
                    txtAddress.Text = employee.Address;
                    txtSalary.Text = employee.Salary.ToString();
                    cboDepartment.SelectedValue = employee.DepartmentId;
                    cboPosition.SelectedValue = employee.PositionId;
                    if (!string.IsNullOrEmpty(employee.ProfilePicture))
                    {
                        avatarImage.Source = new BitmapImage(new Uri(employee.ProfilePicture, UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        avatarImage.Source = null;
                    }
                }
                else
                {
                    MessageBox.Show("Row has not been generated yet.");
                }
            }

        }
        public void resetInput()
        {
            txtAccountId.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtFullname.Text = "";
            txtDob.SelectedDate = null;
            txtPhoneNumber.Text = "";
            txtAddress.Text = "";
            txtSalary.Text = "";

            cboRole.SelectedIndex = 0;
            cboGender.SelectedIndex = 0;
            cboDepartment.SelectedIndex = 0;
            cboPosition.SelectedIndex = 0;

            avatarImage.Source = null;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var employees = iEmployeeService.GetEmployees();
            BackupEmployees(employees, "employees_backup.json"); 
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "employees_backup.json";

            // Kiểm tra xem file có tồn tại không
            if (!File.Exists(filePath))
            {
                MessageBox.Show("File sao lưu không tồn tại.");
                return;
            }

            // Nếu file tồn tại, phục hồi dữ liệu
            var employees = RestoreEmployees(filePath);
            foreach (var employee in employees)
            {
                Debug.WriteLine(employee.FullName);
            }
            // Cập nhật danh sách nhân viên trong ứng dụng của bạn
            
        }
        public void BackupEmployees(List<Employee> employees, string filePath)
        {
            try
            {
                // Serialize the employee list to JSON
                var json = JsonSerializer.Serialize(employees);

                // Write the JSON to a file (automatically creates the file if it doesn't exist)
                File.WriteAllText(filePath, json);

                MessageBox.Show("Sao lưu dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Lỗi trong quá trình sao lưu: {ex.Message}");
            }
        }
        public List<Employee> RestoreEmployees(string filePath)
        {
            // Read the JSON from the file
            var json = File.ReadAllText(filePath);

            // Deserialize the JSON back to a list of employees
            var employees = JsonSerializer.Deserialize<List<Employee>>(json);

            return employees ?? new List<Employee>();
        }
    }
}
