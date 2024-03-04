using Microsoft.Win32;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;

namespace University.ViewModels
{
    public class EditGroupVM
         : ViewModelBase
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        private Group? _group;
        private string _groupName;
        private Curriculum _curriculum;
        private Teacher _curator;
        private ObservableCollection<Student> _students;

        private ObservableCollection<Curriculum> _curriculums;
        private ObservableCollection<Teacher> _teachers;
        private ObservableCollection<Student> _groupStudents;

        private DelegateCommand _saveChangesCommand;
        private DelegateCommand<Student> _removeStudentCommand;
        private DelegateCommand<Student> _addStudentCommand;
        private DelegateCommand _importStudentsCommand;

        public DelegateCommand SaveChangesCommand =>
            _saveChangesCommand ?? (_saveChangesCommand = new DelegateCommand(ExecuteSaveChangesCommand));
        public DelegateCommand<Student> RemoveStudentCommand =>
            _removeStudentCommand ?? (_removeStudentCommand = new DelegateCommand<Student>(ExecuteRemoveStudentCommand));
        public DelegateCommand<Student> AddStudentCommand =>
            _addStudentCommand ?? (_addStudentCommand = new DelegateCommand<Student>(ExecuteAddStudentCommand));
        public DelegateCommand ImportStudentsCommand =>
          _importStudentsCommand ?? (_importStudentsCommand = new DelegateCommand(ExecuteImportStudentsCommand));

        public string GroupName
        {
            get{ return _groupName; }
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
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged(nameof(GroupStudents));
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
            get { return _groupStudents; }
            set
            {
                _groupStudents = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        public EditGroupVM(int groupId) 
        {
            var appDBContext = new ApplicationDbContext();

            _groupService = new GroupService(new GroupRepository(appDBContext));
            _curriculumService = new CurriculumService(new CurriculumRepository(appDBContext));
            _studentService = new StudentService(new StudentRepository(appDBContext));
            _teacherService = new TeacherService(new TeacherRepository(appDBContext));

            LoadGroupDataAsync(groupId);            
        }

        private async void LoadGroupDataAsync(int groupId)
        {
            try
            {    
                var curriculums = await _curriculumService.ListAsync();
                Curriculums = new ObservableCollection<Curriculum>(curriculums);

                var teachers = await _teacherService.GetAllTeacherDataAsync();
                Teachers = new ObservableCollection<Teacher>(teachers);

                var students = await _studentService.GetAllFreeStudentsDataAsync();
                Students = new ObservableCollection<Student>(students);

                _group = await _groupService.GetAllGroupDataAsync(groupId);

                GroupName = _group.GroupName;
                Curriculum = _group.Curriculum;
                Curator = _group.Teacher;
                GroupStudents = new ObservableCollection<Student>(_group.Students);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteSaveChangesCommand()
        {
            try
            {
                _group.GroupName = _groupName;
                _group.Curriculum = _curriculum;
                _group.Teacher = _curator;
                _group.Students = GroupStudents.ToList();

                await _groupService.UpdateAsync(_group);
                MessageBox.Show($"{GroupName} updated successfully.");

                CloseCurrentWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteRemoveStudentCommand(Student selectedStudent)
        {
            GroupStudents.Remove(selectedStudent);
            Students.Add(selectedStudent);
        }

        private void ExecuteAddStudentCommand(Student selectedStudent)
        {
            Students.Remove(selectedStudent);
            GroupStudents.Add(selectedStudent);
        }

        private async void ExecuteImportStudentsCommand()
        {
            try
            {
                ClearGroup();

                var fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() is true)
                {
                    var students = await _studentService.AddFromFileAsync(fileDialog.FileName);

                    GroupStudents = new ObservableCollection<Student>(students);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ClearGroup()
        {
            foreach (var student in GroupStudents)
            {
                Students.Add(student);
            }

            GroupStudents.Clear();
        }

        private void CloseCurrentWindow()
        {
            var currentWindow = Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(x => x.DataContext == this);
            currentWindow?.Close();
        }
    }
}
