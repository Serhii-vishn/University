using System.Windows;
using System.Windows.Input;
using University.Services.Interfaces;
using University.Services;
using University.Repositories;
using University.DbContexts;
using University.Views;

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

            LoginCommand = new RelayCommand(async (_) => await LoginAsync(), (_) => !IsLoggingIn);
        }

        public ICommand LoginCommand { get; }

        private async Task LoginAsync()
        {
            if(_login != null && _password != null)
            {
                IsLoggingIn = true;
                try
                {
                    var user = await _userService.GetUserByLogPassAsync(_login, _password);

                    if (user is not null)
                    {
                        SwitchToUserPage(user.Role);
                    }
                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    IsLoggingIn = false;
                }
            }          
        }

        private void SwitchToUserPage(string role)
        {
            switch (role.ToLower()) 
            {
                case "teacher":
                    {
                        TeacherMainView taskWindow = new();
                        taskWindow.Show();
                        break;
                    }
                case "student":
                    {
                        StudentMainView taskWindow = new();
                        taskWindow.Show();
                        break;
                    }
                default:
                    {
                        throw new ArgumentException($"User have invalid role {role}");                    
                    }
            }

            if (App.Current.MainWindow != null)
                App.Current.MainWindow.Close();
        }
    }
}
