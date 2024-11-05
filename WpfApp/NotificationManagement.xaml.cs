using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using Objects;
using Services;

namespace WpfApp
{
    public partial class NotificationManagement : Window
    {
        private readonly NotificationService notificationService;
        private readonly DepartmentService departmentService;
        private Notification selectedNotification;
        private Objects.Account _account;
        public NotificationManagement(Objects.Account account)
        {
            InitializeComponent();
            notificationService = new NotificationService();
            _account = account;
            departmentService = new DepartmentService();
            LoadDepartments();
            LoadNotifications();
            UpdatePlaceholders();
        }

        private void LoadDepartments()
        {
            // Fetch departments and bind to ComboBox
            List<Department> departments = departmentService.GetDepartments();
            DepartmentComboBox.ItemsSource = departments; // Bind departments to ComboBox
        }

        private void LoadNotifications()
        {
            var notifications = notificationService.GetNotis();
            dgNotifications.ItemsSource = notifications; // Bind notifications to DataGrid
            dgNotifications.SelectedItem = null; // Clear selection
        }

        private void AddNotification_Click(object sender, RoutedEventArgs e)
        {
            var newNotification = new Notification
            {
                Title = TitleTextBox.Text,
                Content = ContentTextBox.Text,
                DepartmentId = (int)DepartmentComboBox.SelectedValue, // Get selected department ID
                CreatedDate = DateTime.Now
            };

            notificationService.AddNotis(newNotification);
            LoadNotifications(); // Refresh the DataGrid
            ClearInputFields();
        }

        private void UpdateNotification_Click(object sender, RoutedEventArgs e)
        {
            if (selectedNotification != null)
            {
                selectedNotification.Title = TitleTextBox.Text;
                selectedNotification.Content = ContentTextBox.Text;
                selectedNotification.DepartmentId = (int)DepartmentComboBox.SelectedValue; // Get selected department ID

                notificationService.UpdateNotis(selectedNotification);
                LoadNotifications(); // Refresh the DataGrid
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please select a notification to update.");
            }
        }

        private void DeleteNotification_Click(object sender, RoutedEventArgs e)
        {
            if (selectedNotification != null)
            {
                notificationService.DeleteNotis(selectedNotification);
                LoadNotifications(); // Refresh the DataGrid
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please select a notification to delete.");
            }
        }

        private void dgNotifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get selected notification
            selectedNotification = dgNotifications.SelectedItem as Notification;

            if (selectedNotification != null)
            {
                TitleTextBox.Text = selectedNotification.Title;
                ContentTextBox.Text = selectedNotification.Content;
                DepartmentComboBox.SelectedValue = selectedNotification.DepartmentId; // Set selected department
            }
            else
            {
                ClearInputFields(); // Clear fields if selection is removed
            }
        }

        private void DepartmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePlaceholders();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePlaceholders();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholders();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholders();
        }

        private void UpdatePlaceholders()
        {
            TitlePlaceholder.Visibility = string.IsNullOrEmpty(TitleTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            ContentPlaceholder.Visibility = string.IsNullOrEmpty(ContentTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            DepartmentIdPlaceholder.Visibility = DepartmentComboBox.SelectedItem == null ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ClearInputFields()
        {
            TitleTextBox.Clear();
            ContentTextBox.Clear();
            DepartmentComboBox.SelectedItem = null; // Clear selection in ComboBox
            selectedNotification = null; // Clear the selected notification
            dgNotifications.SelectedItem = null; // Clear selection in DataGrid
            UpdatePlaceholders();
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(_account);
            homeWindow.Show();
            this.Close();
        }
    }
}
