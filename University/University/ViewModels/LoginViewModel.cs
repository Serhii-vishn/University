using System.Windows;
using System.Windows.Input;
using University.Services.Interfaces;
using University.Models;
using University.Services;
using University.Repositories;
using University.DbContexts;
using University.Commands;

namespace University.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private string _login;
        private string _password;
        private bool _isLoggingIn;

        public string Login
        {
            get => _login;
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public bool IsLoggingIn
        {
            get => _isLoggingIn;
            set
            {
                if (_isLoggingIn != value)
                {
                    _isLoggingIn = value;
                    OnPropertyChanged(nameof(IsLoggingIn));
                    ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public LoginViewModel()
        {
            _userService = new UserService(new UserRepository(new ApplicationDbContext()));
            LoginCommand = new RelayCommand(async () => await LoginAsync(), () => !IsLoggingIn);
        }

        public ICommand LoginCommand { get; }

        private async Task LoginAsync()
        {
            IsLoggingIn = true;
            try
            {
                User? user = await _userService.GetUserByLogPassAsync(_login, _password);
                MessageBox.Show(user.Id.ToString());
            }
            catch (Exception ex)
            {
                // Обробка помилок
            }
            finally
            {
                IsLoggingIn = false;
            }
        }
    }
}
