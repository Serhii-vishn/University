using System.Windows;
using University.Services.Interfaces;
using University.ViewModels;

namespace University.Views
{
    public partial class AddEditGroupView : Window
    {
        public AddEditGroupView()
        {
            InitializeComponent();
            DataContext = new AddGroupVM();
        }

        public AddEditGroupView(IGroupService groupService, int groupId)
        {
            InitializeComponent();
            DataContext = new EditGroupVM(groupService, groupId);
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
