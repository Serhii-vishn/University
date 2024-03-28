namespace University.ViewModels
{
    public class StudentInfoMainVM : ViewModelBase
    {
        private readonly IStudentService _studentService;


        public StudentInfoMainVM(IStudentService studentService, int id)
        {
            _studentService = studentService;
        }
    }
}
