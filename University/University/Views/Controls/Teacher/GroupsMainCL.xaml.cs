using System.Windows.Controls;
using University.ViewModels;

namespace University.Views.Controls.Teacher
{
    public partial class GroupsEditDeleteCL : UserControl
    {
        public GroupsEditDeleteCL()
        {
            InitializeComponent();
            DataContext = new GroupsMainVM();
        }
    }
}
