using System.Windows;
using University.Services.Interfaces;
using University.ViewModels;

namespace University.Views
{
    public partial class AddEditStudentView : Window
    {
        public AddEditStudentView()
        {
            InitializeComponent();
            DataContext = new AddStudentVM();
        }

        public AddEditStudentView(IStudentService _studentService, int studentId)
        {
            InitializeComponent();
            DataContext = new EditStudentVM(_studentService, studentId);
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
