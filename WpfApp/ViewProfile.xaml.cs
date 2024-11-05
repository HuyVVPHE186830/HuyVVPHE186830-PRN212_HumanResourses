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
using Microsoft.Win32;
using Objects;
using Services;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for ViewProfile.xaml
    /// </summary>
    public partial class ViewProfile : Window
    {
        private readonly IAccountService iAccountService;
        private readonly IEmployeeService iEmployeeService;
        private readonly IActivityLogService iActivityLogService;
        private readonly IDepartmentService iDepartmentService;
        private readonly IPositionService iPositionService;
        private Account currentUser;
        private Objects.Account _account;
        public ViewProfile(Account account)
        {
            InitializeComponent();
            _account = account;
            currentUser = account;
            iAccountService = new AccountService();
            iEmployeeService = new EmployeeService();
            iActivityLogService = new ActivityLogService();
            iDepartmentService = new DepartmentService();
            iPositionService = new PositionService();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var employee = iEmployeeService.GetEmployeeByAccountId(currentUser.AccountId);

            if (employee != null)
            {
                var departments = iDepartmentService.GetDepartments().ToList();
                var department = departments.FirstOrDefault(d => d.DepartmentId == employee.DepartmentId);
                string depName = department?.DepartmentName;
                var positions = iPositionService.GetPositions().ToList();
                var position = positions.FirstOrDefault(p => p.PositionId == employee.PositionId);
                string posName = position?.PositionName;
                txtAccountId.Text = currentUser.AccountId.ToString();
                txtUsername.Text = currentUser.Username;
                txtPassword.Password = currentUser.Password;
                txtFullname.Text = employee.FullName;
                txtDob.SelectedDate = employee.DateOfBirth;
                txtPhoneNumber.Text = employee.PhoneNumber;
                txtAddress.Text = employee.Address;
                txtSalary.Text = employee.Salary.ToString();
                cboGender.SelectedValue = employee.Gender;
                cboDepartment.Text = depName;
                cboPosition.Text = posName;

                if (!string.IsNullOrEmpty(employee.ProfilePicture))
                {
                    avatarImage.Source = new BitmapImage(new Uri(employee.ProfilePicture, UriKind.RelativeOrAbsolute));
                }
                else
                {
                    avatarImage.Source = null;
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                currentUser.AccountId = Int32.Parse(txtAccountId.Text);
                currentUser.Username = txtUsername.Text;
                currentUser.Password = txtPassword.Password;
                Employee employee = iEmployeeService.GetEmployeeByAccountId(currentUser.AccountId);
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
                employee.ProfilePicture = avatarImage.Source != null ? avatarImage.Source.ToString() : null;
                iAccountService.UpdateAccount(currentUser);
                iEmployeeService.UpdateEmployee(employee);
                MessageBox.Show("Update Successfully!");
                ActivityLog activityLog = new ActivityLog();
                activityLog.AccountId = currentUser.AccountId;
                activityLog.Action = "Update Profile";
                activityLog.Timestamp = DateTime.Now;
                iActivityLogService.AddActivityLog(activityLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(currentUser);
            homeWindow.Show();
            this.Close();
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
    }
}