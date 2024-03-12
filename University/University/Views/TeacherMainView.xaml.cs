using System.Windows;
using System.Windows.Input;
using University.ViewModels;

namespace University.Views
{
    public partial class TeacherMainView : Window
    {
        public TeacherMainView(int userId)
        {
            InitializeComponent();
            DataContext = new TeacherMainVM(userId);
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
    }
}
