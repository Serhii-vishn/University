namespace University.ViewModels
{
    public class TeacherMainVM :
        ViewModelBase
    {
        private Window? taskWindow = null;

        private ITeacherService _teacherService;
        private IHumanService _humanService;
        private Teacher? _teacher;

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

        public Teacher Teacher
        {
            get { return _teacher; }
            set
            {
                _teacher = value;
                OnPropertyChanged(nameof(Teacher));
            }
        }

        private DelegateCommand _teacherInfoCommand;

        private DelegateCommand _homePageCommand;
        private DelegateCommand _groupPageCommand;
        private DelegateCommand _studentPageCommand;

        public DelegateCommand InfoCommand => 
            _teacherInfoCommand ??= new DelegateCommand(ExecuteInfoCommand);
        public DelegateCommand HomePageCommand =>
            _homePageCommand ??= new DelegateCommand(ExecuteHomePageCommand);
        public DelegateCommand GroupPageCommand => 
            _groupPageCommand ??= new DelegateCommand(ExecuteGroupPageCommand);
        public DelegateCommand StudentPageCommand => 
            _studentPageCommand ??= new DelegateCommand(ExecuteStudentPageCommand);

        public TeacherMainVM(int userId)
        {
            var appDBContext = new ApplicationDbContext();

            _teacherService = new TeacherService(new TeacherRepository(appDBContext));
            _humanService = new HumanService(new HumanRepository(appDBContext));

            LoadGroupDataAsync(userId);
            ExecuteHomePageCommand();
        }

        private async void LoadGroupDataAsync(int userId)
        {
            try
            {
                var human = await _humanService.GetHumanByIdAsync(userId);
                Teacher = await _teacherService.GetAllTeacherDataByHumanAsync(human.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteInfoCommand()
        {
            try
            {
                if (taskWindow == null || !taskWindow.IsVisible)
                {
                    taskWindow = new TeacherInfoView(_teacherService, Teacher.HumanId);
                    taskWindow.Closed += (s, eventArgs) =>
                    {
                        taskWindow = null;
                        LoadGroupDataAsync(Teacher.HumanId);
                    };
                    taskWindow.Show();
                }
                else
                {
                    taskWindow.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteHomePageCommand()
        {
            Content = new CurriculumsOuputCL();
        }

        private void ExecuteStudentPageCommand()
        {
            Content = new StudentsMainCL();
        }

        private void ExecuteGroupPageCommand()
        {
            Content = new GroupsMainCL();
        }
    }
}
