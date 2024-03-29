﻿namespace University.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        private Window? taskWindow;

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

        public LoginVM()
        {
            _userService = new UserService(new UserRepository(new ApplicationDbContext()));
            taskWindow = null;
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
                        if (taskWindow == null || !taskWindow.IsVisible)
                        {
                            taskWindow = new TeacherMainView(user.Id);
                            taskWindow.Closed += (s, eventArgs) =>
                            {
                                taskWindow = null;
                            };

                            taskWindow.Show();

                            Application.Current.Windows.OfType<LoginView>().FirstOrDefault()?.Close();
                        }
                        else
                        {
                            taskWindow.Focus();
                        }
                        break;
                    }
                case "student":
                    {
                        if (taskWindow == null || !taskWindow.IsVisible)
                        {
                            taskWindow = new StudentMainView(user.Id);
                            taskWindow.Closed += (s, eventArgs) =>
                            {
                                taskWindow = null;
                            };

                            taskWindow.Show();
                            Application.Current.Windows.OfType<LoginView>().FirstOrDefault()?.Close();
                        }
                        else
                        {
                            taskWindow.Focus();
                        }
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
