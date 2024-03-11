using System.Windows;
using Prism.Commands;
using University.Services.Interfaces;
using University.Services;
using University.Repositories;
using University.DbContexts;
using University.Views;
using University.Models;

namespace University.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private string _login;
        private string _password;
        private User? user;

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

        private DelegateCommand _loginCommand;

        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand));

        public LoginViewModel()
        {
            _userService = new UserService(new UserRepository(new ApplicationDbContext()));
        }

        private async void ExecuteLoginCommand()
        {
            if(_login != null && _password != null)
            {
                try
                {
                    user = await _userService.GetUserByLogPassAsync(_login, _password);

                    if (user is not null)
                        SwitchToUserPage(user.Role);
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
            }          
        }

        private void SwitchToUserPage(string role)
        {
            switch (role.ToLower()) 
            {
                case "teacher":
                    {
                        var taskWindow = new TeacherMainView(user.Id);
                        taskWindow.Show();
                        break;
                    }
                case "student":
                    {
                        var taskWindow = new StudentMainView(user.Id);
                        taskWindow.Show();
                        break;
                    }
                default:
                    {
                        throw new ArgumentException($"User have invalid role {role}");                    
                    }
            }

            App.Current.MainWindow?.Close();
        }
    }
}
