namespace University.Views.Student.Controls
{
    public partial class StudentGroupInfoCL : UserControl
    {
        public StudentGroupInfoCL(int groupId)
        {
            InitializeComponent();
            DataContext = new StudentGroupInfoCL(groupId);
        }
    }
}
