namespace University.Views.Controls.Teacher
{
    public partial class GroupsMainCL : UserControl
    {
        public GroupsMainCL()
        {
            InitializeComponent();
            DataContext = new GroupsMainVM();
        }
    }
}
