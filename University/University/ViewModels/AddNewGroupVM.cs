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
    public class AddNewGroupVM : 
        ViewModelBase
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        private string _groupName;
        private Curriculum? _selectedCurriculum;
        private Teacher? _selectedTeacher;
        private ObservableCollection<Student> _selectedStudents;

        private ObservableCollection<Curriculum> _curriculums;     
        private ObservableCollection<Teacher> _teachers;
        private ObservableCollection<Student> _students;

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
        public Curriculum SelectedCurriculum
        {
            get { return _selectedCurriculum; }
            set
            {
                _selectedCurriculum = value;
                OnPropertyChanged(nameof(SelectedCurriculum));
            }
        }
        public Teacher SelectedTeacher
        {
            get { return _selectedTeacher; }
            set
            {
                _selectedTeacher = value;
                OnPropertyChanged(nameof(SelectedTeacher));
            }
        }
        public ObservableCollection<Student> SelectedStudents
        {
            get { return _selectedStudents; }
            set
            {
                _selectedStudents = value;
                OnPropertyChanged(nameof(SelectedStudents));
            }
        }
        public ObservableCollection<Curriculum> Curriculums
        {
            get { return _curriculums; }
            set
            {
                _curriculums = value;
                OnPropertyChanged(nameof(Curriculums));
            }
        }
        public ObservableCollection<Teacher> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                OnPropertyChanged(nameof(Teachers));
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

        public AddNewGroupVM()
        {
            var appDBContext = new ApplicationDbContext();

            _curriculumService = new CurriculumService(new CurriculumRepository(appDBContext));
            _groupService = new GroupService(new GroupRepository(appDBContext));
            _studentService = new StudentService(new StudentRepository(appDBContext));
            _teacherService = new TeacherService(new TeacherRepository(appDBContext));
            SelectedStudents = new ObservableCollection<Student>();

            LoadDataInLists();
        }

        private async void LoadDataInLists()
        {
            var curriculums = await _curriculumService.ListAsync();
            Curriculums = new ObservableCollection<Curriculum>(curriculums);

            var teachers = await _teacherService.GetAllTeacherDataAsync();
            Teachers = new ObservableCollection<Teacher>(teachers);

            var students = await _studentService.GetAllFreeStudentsDataAsync();
            Students = new ObservableCollection<Student>(students);
        }

        private void ExecuteAddStudentCommand(Student selectedStudent)
        {
            SelectedStudents.Add(selectedStudent);
            Students.Remove(selectedStudent);
        }

        private void ExecuteRemoveStudentCommand(Student student)
        {
            Students.Add(student);
            SelectedStudents.Remove(student);
        }

        private async void ExecuteSaveGroupCommand()
        {
            try
            {
                if(string.IsNullOrWhiteSpace(_groupName))
                    throw new ArgumentNullException("Group name");
                if (_selectedTeacher is null)
                    throw new ArgumentNullException("Curator");
                if (_selectedCurriculum is null)
                    throw new ArgumentNullException("Curriculum");

                var newGroup = new Group()
                {
                    GroupName = _groupName,
                    CuratorId = _selectedTeacher.Id,
                    CurriculumId = _selectedCurriculum.Id,
                    Students = _selectedStudents.ToList(),
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
                    SelectedStudents.Clear();
                    var students = await _studentService.AddFromFileAsync(fileDialog.FileName);
                    
                    SelectedStudents = new ObservableCollection<Student>(students);
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
            _selectedTeacher = null;
            _selectedCurriculum = null;
            _selectedStudents.Clear();

            OnPropertyChanged(nameof(GroupName));
            OnPropertyChanged(nameof(SelectedTeacher));
            OnPropertyChanged(nameof(SelectedCurriculum));
            OnPropertyChanged(nameof(SelectedStudents));
        }
    }
}
