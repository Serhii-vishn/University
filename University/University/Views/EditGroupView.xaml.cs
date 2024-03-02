using System.Windows;
using University.ViewModels;
using University.Models;

namespace University.Views
{
    public partial class EditGroupView : Window
    {
        public EditGroupView(Group group)
        {
            InitializeComponent();
            DataContext = new EditGroupVM(group);
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
