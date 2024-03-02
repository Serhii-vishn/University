using System.Windows;
using University.ViewModels;

namespace University.Views
{
    public partial class EditGroupView : Window
    {
        public EditGroupView(int groupId)
        {
            InitializeComponent();
            DataContext = new EditGroupVM(groupId);
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
