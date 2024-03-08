using System.Windows;
using System.Collections.ObjectModel;
using Prism.Commands;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;
using Microsoft.Win32;
using System.IO;

namespace University.ViewModels
{
    public class AddGroupVM : 
        ViewModelBase
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        private string _groupName;
        private Curriculum? _curriculum;
        private Teacher? _curator;
        private ObservableCollection<Student> _groupStudents;

        private ObservableCollection<Curriculum> _curriculumsList;     
        private ObservableCollection<Teacher> _teachers;
        private ObservableCollection<Student> _studentsList;

        private DelegateCommand _saveGroupCommand;
        private DelegateCommand _importStudentsCommand;
        private DelegateCommand<Student> _addStudentCommand;
        private DelegateCommand<Student> _removeStudentCommand;

        public DelegateCommand SaveGroupCommand =>
            _saveGroupCommand ?? (_saveGroupCommand = new DelegateCommand(ExecuteSaveGroupCommand));
        public DelegateCommand ImportStudentsCommand =>
           _importStudentsCommand ?? (_importStudentsCommand = new DelegateCommand(ExecuteImportStudentsCommand));
        public DelegateCommand<Student> AddStudentCommand =>
            _addStudentCommand ?? (_addStudentCommand = new DelegateCommand<Student>(ExecuteAddStudentCommand));
        public DelegateCommand<Student> RemoveStudentCommand =>
            _removeStudentCommand ?? (_removeStudentCommand = new DelegateCommand<Student>(ExecuteRemoveStudentCommand));

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(GroupName));
            }
        }
        public Curriculum Curriculum
        {
            get { return _curriculum; }
            set
            {
                _curriculum = value;
                OnPropertyChanged(nameof(Curriculum));
            }
        }
        public Teacher Curator
        {
            get { return _curator; }
            set
            {
                _curator = value;
                OnPropertyChanged(nameof(Curator));
            }
        }
        public ObservableCollection<Student> GroupStudents
        {
            get { return _groupStudents; }
            set
            {
                _groupStudents = value;
                OnPropertyChanged(nameof(GroupStudents));
            }
        }
        public ObservableCollection<Curriculum> CurriculumsList
        {
            get { return _curriculumsList; }
            set
            {
                _curriculumsList = value;
                OnPropertyChanged(nameof(CurriculumsList));
            }
        }
        public ObservableCollection<Teacher> TeachersList
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                OnPropertyChanged(nameof(TeachersList));
            }
        }
        public ObservableCollection<Student> StudentsList
        {
            get { return _studentsList; }
            set
            {
                _studentsList = value;
                OnPropertyChanged(nameof(StudentsList));
            }
        }

        public AddGroupVM()
        {
            var appDBContext = new ApplicationDbContext();

            _curriculumService = new CurriculumService(new CurriculumRepository(appDBContext));
            _groupService = new GroupService(new GroupRepository(appDBContext));
            _studentService = new StudentService(new StudentRepository(appDBContext));
            _teacherService = new TeacherService(new TeacherRepository(appDBContext));
            GroupStudents = new ObservableCollection<Student>();

            LoadDataInLists();
        }

        private async void LoadDataInLists()
        {
            var curriculums = await _curriculumService.ListAsync();
            CurriculumsList = new ObservableCollection<Curriculum>(curriculums);

            var teachers = await _teacherService.GetAllTeacherDataAsync();
            TeachersList = new ObservableCollection<Teacher>(teachers);

            var students = await _studentService.GetAllFreeStudentsDataAsync();
            StudentsList = new ObservableCollection<Student>(students);
        }

        private void ExecuteAddStudentCommand(Student selectedStudent)
        {
            GroupStudents.Add(selectedStudent);
            StudentsList.Remove(selectedStudent);
        }

        private void ExecuteRemoveStudentCommand(Student student)
        {
            StudentsList.Add(student);
            GroupStudents.Remove(student);
        }

        private async void ExecuteSaveGroupCommand()
        {
            try
            {
                if(string.IsNullOrWhiteSpace(_groupName))
                    throw new ArgumentNullException("Group name");
                if (_curator is null)
                    throw new ArgumentNullException("Curator");
                if (_curriculum is null)
                    throw new ArgumentNullException("Curriculum");

                var newGroup = new Group()
                {
                    GroupName = _groupName,
                    CuratorId = _curator.Id,
                    CurriculumId = _curriculum.Id,
                    Students = _groupStudents.ToList(),
                };

                await _groupService.AddAsync(newGroup);
                MessageBox.Show($"{GroupName} added successfully.");

                ClearAddWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteImportStudentsCommand()
        {
            try
            {
                var fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() is true)
                {
                    GroupStudents.Clear();
                    var students = await _studentService.AddFromFileAsync(fileDialog.FileName);
                    
                    GroupStudents = new ObservableCollection<Student>(students);
                }
            }
            catch(FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearAddWindow()
        {
            _groupName = string.Empty;
            _curator = null;
            _curriculum = null;
            _groupStudents.Clear();

            OnPropertyChanged(nameof(GroupName));
            OnPropertyChanged(nameof(Curator));
            OnPropertyChanged(nameof(Curriculum));
            OnPropertyChanged(nameof(GroupStudents));
        }
    }
}
