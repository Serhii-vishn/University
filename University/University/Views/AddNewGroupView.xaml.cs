using System.Windows;

namespace University.Views
{
    public partial class AddNewGroupView : Window
    {
        public AddNewGroupView()
        {
            InitializeComponent();
            DataContext = new ViewModels.AddNewGroupVM();
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
