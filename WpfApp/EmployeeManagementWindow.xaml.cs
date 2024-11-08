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
using Repositories;
using DataAccessLayer;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for EmployeeManagementWindow.xaml
    /// </summary>
    public partial class EmployeeManagementWindow : Window
    {
        private Account currentUser;
        private Objects.Account _account;
        public event Action SalaryUpdated;
        private readonly IAccountService iAccountService;
        private readonly IEmployeeService iEmployeeService;
        private readonly IRoleService iRoleService;
        private readonly IDepartmentService iDepartmentService;
        private readonly IPositionService iPositionService;
        private readonly IActivityLogService iActivityLogService;

        public EmployeeManagementWindow(Account account)
        {
            InitializeComponent();
            _account = account;
            currentUser = account;
            iAccountService = new AccountService();
            iEmployeeService = new EmployeeService();
            iRoleService = new RoleService();
            iDepartmentService = new DepartmentService();
            iPositionService = new PositionService();
            iActivityLogService = new ActivityLogService();
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
                                       EmployeeID = employee.EmployeeId,
                                       DepartmentId = employee.DepartmentId,
                                       PositionId = employee.PositionId,
                                       RoleId = account.RoleId,
                                       AvatarImage = employee.ProfilePicture,
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
                if (txtAccountId == null)
                {
                    MessageBox.Show("Cannot Add Duplicate Employee");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Please enter a username.");
                    return;  // Dừng ngay sau khi phát hiện lỗi
                }

                var accountList = iAccountService.GetAccounts();
                foreach (var a in accountList)
                {
                    if (txtUsername.Text.Equals(a.Username, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Try Other Username!");
                        txtUsername.Text = "";
                        return;  
                    }
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter a password.");
                    return; 
                }

                if (cboRole.SelectedValue == null)
                {
                    MessageBox.Show("Please select a role.");
                    return;  
                }

                if (string.IsNullOrWhiteSpace(txtFullname.Text))
                {
                    MessageBox.Show("Please enter a full name.");
                    return; 
                }

                if (!txtDob.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a date of birth.");
                    return; 
                }

                if (cboGender.SelectedValue == null || string.IsNullOrWhiteSpace(cboGender.SelectedValue.ToString()))
                {
                    MessageBox.Show("Please select a gender.");
                    return;  
                }

                if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
                {
                    MessageBox.Show("Please enter a phone number.");
                    return;  
                }

                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show("Please enter an address.");
                    return; 
                }

                if (string.IsNullOrWhiteSpace(txtSalary.Text) || !double.TryParse(txtSalary.Text, out double salary))
                {
                    MessageBox.Show("Please enter a valid salary.");
                    return; 
                }

                if (cboDepartment.SelectedValue == null)
                {
                    MessageBox.Show("Please select a department.");
                    return; 
                }

                if (cboPosition.SelectedValue == null)
                {
                    MessageBox.Show("Please select a position.");
                    return;  
                }

                Account account = new Account
                {
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    RoleId = Int32.Parse(cboRole.SelectedValue.ToString())
                };
                iAccountService.AddAccount(account);

                Employee employee = new Employee
                {
                    AccountId = account.AccountId,
                    FullName = txtFullname.Text,
                    DateOfBirth = txtDob.SelectedDate.Value,
                    Gender = cboGender.SelectedValue.ToString(),
                    PhoneNumber = txtPhoneNumber.Text,
                    Address = txtAddress.Text,
                    Salary = double.Parse(txtSalary.Text),
                    DepartmentId = (int)cboDepartment.SelectedValue,
                    PositionId = (int)cboPosition.SelectedValue,
                    ProfilePicture = avatarImage.Source != null ? avatarImage.Source.ToString() : null
                };
                iEmployeeService.AddEmployee(employee);
                MessageBox.Show("Add Successfully!");
                ActivityLog activityLog = new ActivityLog();
                activityLog.AccountId = currentUser.AccountId;
                activityLog.Action = "Add Employee";
                activityLog.Timestamp = DateTime.Now;
                iActivityLogService.AddActivityLog(activityLog);
                resetInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nStack Trace: " + ex.StackTrace);
            }
            finally
            {

                LoadRoleList();
                LoadEmployeeList();
                LoadDepartmentList();
                LoadPositionList();
            }
        }


        private void resetInput()
        {
            txtAccountId.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtFullname.Text = "";
            txtDob.SelectedDate = null;
            txtPhoneNumber.Text = "";
            txtAddress.Text = "";
            txtSalary.Text = "";

            cboRole.SelectedIndex = 0; // Đặt về mặc định không chọn
            cboGender.SelectedIndex = 0; // Đặt về mặc định không chọn
            cboDepartment.SelectedIndex = 0; // Đặt về mặc định không chọn
            cboPosition.SelectedIndex = 0; // Đặt về mặc định không chọn

            avatarImage.Source = null;
        }

        private void btnAttendance_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> employees = iEmployeeService.GetEmployees();
            IAttendanceRepository attendanceRepository = new AttendanceRepository();
            IAttendanceService attendanceService = new AttendanceService(attendanceRepository); // Cần khởi tạo đối tượng của class implement IAttendanceService
            AttendanceWindow attendanceWindow = new AttendanceWindow(employees, attendanceService);
            attendanceWindow.ShowDialog();
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
                // Kiểm tra tất cả các trường nhập liệu
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Please enter a username.");
                    return;  
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter a password.");
                    return;  
                }

                if (cboRole.SelectedValue == null)
                {
                    MessageBox.Show("Please select a role.");
                    return;  
                }

                if (string.IsNullOrWhiteSpace(txtFullname.Text))
                {
                    MessageBox.Show("Please enter a full name.");
                    return;  
                }

                if (!txtDob.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a date of birth.");
                    return;  
                }

                if (cboGender.SelectedValue == null || string.IsNullOrWhiteSpace(cboGender.SelectedValue.ToString()))
                {
                    MessageBox.Show("Please select a gender.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
                {
                    MessageBox.Show("Please enter a phone number.");
                    return;  
                }

                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show("Please enter an address.");
                    return;  
                }

                if (string.IsNullOrWhiteSpace(txtSalary.Text) || !double.TryParse(txtSalary.Text, out double salary))
                {
                    MessageBox.Show("Please enter a valid salary.");
                    return; 
                }

                if (cboDepartment.SelectedValue == null)
                {
                    MessageBox.Show("Please select a department.");
                    return;  
                }

                if (cboPosition.SelectedValue == null)
                {
                    MessageBox.Show("Please select a position.");
                    return; 
                }

                // Kiểm tra AccountId
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
                        return;  // Dừng lại nếu không chọn ngày sinh
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
                    MessageBox.Show("Update Successfully!");
                    // Log activity
                    ActivityLog activityLog = new ActivityLog();
                    activityLog.AccountId = currentUser.AccountId;
                    activityLog.Action = "Update Employee";
                    activityLog.Timestamp = DateTime.Now;
                    iActivityLogService.AddActivityLog(activityLog);
                    resetInput();
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
                    using var db = new FuhrmContext();
                    var b1 = db.ActivityLogs.Where(b => b.AccountId == account.AccountId).ToList();
                    var b2 = db.Attendances.Where(a => a.EmployeeId == employee.EmployeeId).ToList();
                    if (b1.Any())
                    {
                        db.ActivityLogs.RemoveRange(b1);
                    }
                    if (b2.Any())
                    {
                        db.Attendances.RemoveRange(b2);
                    }

                    iEmployeeService.DeleteEmployee(employee);
                    iAccountService.DeleteAccount(account);
                    MessageBox.Show("Delete Successfully!");
                    ActivityLog activityLog = new ActivityLog();
                    activityLog.AccountId = currentUser.AccountId;
                    activityLog.Action = "Delete Employee";
                    activityLog.Timestamp = DateTime.Now;
                    iActivityLogService.AddActivityLog(activityLog);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("You must select a Employee");
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\nInner Exception: " + ex.InnerException.Message;
                }
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            dynamic selectedItem = dgData.SelectedItem;
            if (selectedItem != null)
            {
                txtAccountId.Text = selectedItem.AccountId.ToString();
                txtUsername.Text = selectedItem.Username;
                txtPassword.Text = selectedItem.Password;
                cboRole.SelectedValue = selectedItem.RoleId;
                txtFullname.Text = selectedItem.FullName;
                txtDob.Text = selectedItem.DateOfBirth.ToString();
                cboGender.SelectedValue = selectedItem.Gender;
                txtPhoneNumber.Text = selectedItem.PhoneNumber;
                txtAddress.Text = selectedItem.Address;
                txtSalary.Text = selectedItem.Salary.ToString();
                cboDepartment.SelectedValue = selectedItem.DepartmentId;
                cboPosition.SelectedValue = selectedItem.PositionId;
                if (!string.IsNullOrEmpty(selectedItem.AvatarImage))
                {
                    avatarImage.Source = new BitmapImage(new Uri(selectedItem.AvatarImage, UriKind.RelativeOrAbsolute));
                }
                else
                {
                    avatarImage.Source = null;
                }
            }

        }
        private void ViewSalaryHistory_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag != null)
            {
                try
                {
                    int employeeId = int.Parse(button.Tag.ToString()); // Chuyển đổi Tag thành int
                    SlaryWindow salaryWindow = new SlaryWindow(employeeId);
                    salaryWindow.Show(); // Mở cửa sổ SlaryWindow
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Invalid Employee ID.", "Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
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
                Employee newEmployee = new Employee
                {
                    FullName = employee.FullName,
                    DateOfBirth = employee.DateOfBirth,
                    Gender = employee.Gender,
                    Address = employee.Address,
                    PhoneNumber = employee.PhoneNumber,
                    DepartmentId = employee.DepartmentId,
                    PositionId = employee.PositionId,
                    AccountId = employee.AccountId,
                    Salary = employee.Salary,
                    StartDate = employee.StartDate,
                    ProfilePicture = employee.ProfilePicture
                };
                iEmployeeService.AddEmployee(newEmployee);
            }
            // Cập nhật danh sách nhân viên trong ứng dụng của bạn
            LoadEmployeeList();
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
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(currentUser);
            homeWindow.Show();
            this.Close();
        }
        private void btnExpand_Checked(object sender, RoutedEventArgs e)
        {
            optionPanel.Visibility = Visibility.Visible;
        }

        private void btnExpand_Unchecked(object sender, RoutedEventArgs e)
        {
            optionPanel.Visibility = Visibility.Collapsed;
        }
    }
}