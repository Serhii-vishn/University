﻿namespace University.ViewModels
{
    public class CurriculumsOuputVM : ViewModelBase
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;

        private List<Curriculum> _curriculums;
        private List<Groups> _groups;
        private List<Student> _students;

        public List<Curriculum> Curriculums
        {
            get { return _curriculums; }
            set
            {
                _curriculums = value;
                OnPropertyChanged(nameof(Curriculums));
            }
        }

        public List<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        public List<Groups> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public CurriculumsOuputVM()
        {
            var appDBContext = new ApplicationDbContext();

            _curriculumService = new CurriculumService(new CurriculumRepository(appDBContext));
            _groupService = new GroupService(new GroupRepository(appDBContext));
            _studentService = new StudentService(new StudentRepository(appDBContext));

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var result = await _curriculumService.ListAsync();
                Curriculums = new List<Curriculum>(result);

                var groups = await _groupService.ListAsync();
                Groups = new List<Groups>(groups);

                var students = await _studentService.GetAllStudentsDataAsync();
                Students = new List<Student>(students);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}