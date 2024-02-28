using System.Windows;
using System.Windows.Controls;
using University.ViewModels;

namespace University.Views.Controls.Teacher
{
    public partial class GroupsEditDeleteCL : UserControl
    {
        private AddNewGroupView taskWindow = null;
        public GroupsEditDeleteCL()
        {
            InitializeComponent();
            DataContext = new GroupsEditDeleteVM();
        }

        private void AddNewGroup(object sender, RoutedEventArgs e)
        {
            if (taskWindow == null || !taskWindow.IsVisible)
            {
                taskWindow = new AddNewGroupView();
                taskWindow.Closed += (s, eventArgs) => taskWindow = null;
                taskWindow.Show();
            }
            else
            {
                taskWindow.Focus();
            }
        }
    }
}
