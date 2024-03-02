using System.Windows;
using University.ViewModels;

namespace University.Views
{
    public partial class AddNewGroupView : Window
    {
        public AddNewGroupView()
        {
            InitializeComponent();
            DataContext = new AddNewGroupVM();
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
