namespace University.ViewModels
{
    public class StudentsMainVM : ViewModelBase
    {
        private Window? taskWindow = null;
        private readonly IStudentService _studentService;
        private readonly ApplicationDbContext appDBContext;
        private ObservableCollection<Student> _students;
        private string _searchName;
        private int _teacherId;

        public string Search
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
                OnPropertyChanged(nameof(Search));
            }
        }

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        private DelegateCommand<Student> _deleteCommand;
        private DelegateCommand<Student> _editCommand;
        private DelegateCommand<Student> _feedBackCommand;
        private DelegateCommand _addNewCommand;
        private DelegateCommand _searchCommand;

        public DelegateCommand<Student> DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new DelegateCommand<Student>(ExecuteDeleteCommand));
        public DelegateCommand<Student> EditCommand =>
            _editCommand ?? (_editCommand = new DelegateCommand<Student>(ExecuteEditCommand));
        public DelegateCommand<Student> FeedBackCommand =>
            _feedBackCommand ?? (_feedBackCommand = new DelegateCommand<Student>(ExecuteFeedBackCommand));
        public DelegateCommand AddNewCommand =>
            _addNewCommand ?? (_addNewCommand = new DelegateCommand(ExecuteAddNewCommand));
        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(ExecuteSearchCommand));

        public StudentsMainVM(int teacherId) 
        {
            appDBContext = new ApplicationDbContext();
            _studentService = new StudentService(new StudentRepository(appDBContext));

            _teacherId = teacherId;
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                var students = await _studentService.GetAllStudentsDataAsync();
                Students = new ObservableCollection<Student>(students);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteEditCommand(Student student)
        {
            try
            {
                if (taskWindow == null || !taskWindow.IsVisible)
                {
                    taskWindow = new AddEditStudentView(appDBContext, student.Id);
                    taskWindow.Closed += (s, eventArgs) =>
                    {
                        taskWindow = null;
                        LoadDataAsync();
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

        private void ExecuteAddNewCommand()
        {
            try
            {
                if (taskWindow == null || !taskWindow.IsVisible)
                {
                    taskWindow = new AddEditStudentView();
                    taskWindow.Closed += (s, eventArgs) =>
                    {
                        taskWindow = null;
                        LoadDataAsync();
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

        private async void ExecuteDeleteCommand(Student student)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this student?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await _studentService.DeleteAsync(student.Id);
                    Students.Remove(student);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteFeedBackCommand(Student student)
        {
            try
            {
                if (taskWindow == null || !taskWindow.IsVisible)
                {
                    taskWindow = new StudentInfoView(appDBContext, student.Id, _teacherId);
                    taskWindow.Closed += (s, eventArgs) =>
                    {
                        taskWindow = null;
                        LoadDataAsync();
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

        private async void ExecuteSearchCommand()
        {
            if (!string.IsNullOrEmpty(_searchName))
            {
               var filterStudents = await _studentService.FilterByFullNameListAsync(_searchName);
                Students = new ObservableCollection<Student>(filterStudents);
            }
            else
            {
                LoadDataAsync();
            }
        }
    }
}
