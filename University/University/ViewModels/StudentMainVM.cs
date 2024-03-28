using University.Models;
using University.Services;

namespace University.ViewModels
{
    public class StudentMainVM : ViewModelBase
    {
        private readonly IStudentService _studentService;
        private readonly IHumanService _humanService;

        private Student _student;
        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
                OnPropertyChanged(nameof(Student));
            }
        }

        private object _content;
        public object Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        private DelegateCommand _homePageCommand;
        private DelegateCommand _groupPageCommand;

        public DelegateCommand InfoPageCommand =>
            _homePageCommand ??= new DelegateCommand(ExecuteInfoCommand);
        public DelegateCommand GroupPageCommand =>
            _groupPageCommand ??= new DelegateCommand(ExecuteGroupPageCommand);

        public StudentMainVM(int userId)
        {
            _studentService = new StudentService(new StudentRepository(new ApplicationDbContext()));
            _humanService = new HumanService(new HumanRepository(new ApplicationDbContext()));

            LoadDataAsync(userId);
        }

        private async void LoadDataAsync(int userId)
        {
            
            try
            {
                var human = await _humanService.GetHumanByUserIdAsync(userId);
                Student = await _studentService.GetAllStudentDataByHumanAsync(human.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteInfoCommand()
        {
            Content = new StudentInfoCL(_studentService, Student.Id);
        }

        private async void ExecuteGroupPageCommand()
        {
            //var group = await _studentService.GetAllStudentDataAsync(_studentId);
            //Content = new StudentGroupInfoCL(group.Group.Id);
        }
    }
}
