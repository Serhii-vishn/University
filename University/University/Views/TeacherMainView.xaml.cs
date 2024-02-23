using System.Windows;
using System.Windows.Input;
using University.ViewModels;
using University.Views.Controls.Teacher;

namespace University.Views
{
    public partial class TeacherMainView : Window
    {
        public TeacherMainView()
        {
            InitializeComponent();
            DataContext = new TeacherViewModel();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowHomePageControl(object sender, RoutedEventArgs e)
        {
            myContentControl.Content = new UserControlHome(DataContext);
        }

        private void ShowGroupPageControl(object sender, RoutedEventArgs e)
        {
            myContentControl.Content = new UserControlGroup();
        }

        private void ShowStudentPageControl(object sender, RoutedEventArgs e)
        {
            myContentControl.Content = new UserControlStudent();
        }


    }
}
