namespace University.Views.Student.Controls
{
    public partial class StudentInfoCL : UserControl
    {
        public StudentInfoCL(IStudentService studentService, int studentId)
        {
            InitializeComponent();
            DataContext = new StudentInfoMainVM(studentService, studentId);
        }
    }
}
