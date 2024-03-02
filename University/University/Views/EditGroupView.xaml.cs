using University.Models;
using System.Windows;

namespace University.Views
{
    public partial class EditGroupView : Window
    {
        public EditGroupView(Group group)
        {
            InitializeComponent();
            MessageBox.Show(group.GroupName);
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
