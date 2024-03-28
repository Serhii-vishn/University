namespace University.Views.Student.Controls
{
    public partial class StudentInfoCL : UserControl
    {
        public StudentInfoCL(int studentId)
        {
            InitializeComponent();
            DataContext = new StudentDataMainVM(studentId);
        }
    }
}
