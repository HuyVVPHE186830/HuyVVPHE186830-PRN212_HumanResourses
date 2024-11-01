using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Objects;
using Services;
using System.Windows.Controls;

namespace WpfApp.ViewModel
{
    public class ViewModalBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public class LoginVM: ViewModalBase
    {
        private Objects.Account account;
        private readonly IAccountService accountService;
        private string _password;

        public LoginVM()
        {
            account = new Objects.Account();
            LoginCommand = new RelayCommand(param => Login());
        }
        public LoginVM(IAccountService accountService) : this()
        {
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public string Username
        {
            get => account.Username;
            set {
                account.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand LoginCommand { get; }

        public void Login()
        {
            try
            {
                var accounts = accountService.GetAccounts();
                var account = accounts.FirstOrDefault(a => a.Username == Username && a.Password == Password);
                if (account != null)
                {
                    HomeWindow homeWindow = new HomeWindow(account);
                    homeWindow.Show();
                    Application.Current.MainWindow.Close();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
