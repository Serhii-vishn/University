namespace University.Views.Controls.Teacher
{
    public partial class StudentsMainCL : UserControl
    {
        public StudentsMainCL(int teacherId)
        {
            InitializeComponent();
            DataContext = new StudentsMainVM(teacherId);
        }
    }
}
