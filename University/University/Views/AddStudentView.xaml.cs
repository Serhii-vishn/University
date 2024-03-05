using System.Windows;
using University.ViewModels;

namespace University.Views
{
    public partial class AddStudentView : Window
    {
        public AddStudentView()
        {
            InitializeComponent();
            DataContext = new AddStudentVM();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
