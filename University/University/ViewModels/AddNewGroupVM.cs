using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using University.Commands;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;

namespace University.ViewModels
{
    public class AddNewGroupVM : ViewModelBase
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IGroupService _groupService;
        private readonly IHumanService _humanService;
        private readonly IStudentService _studentService;

        private string _groupName;
        private Curriculum _selectedCurriculum;

        private ObservableCollection<Curriculum> _curriculums;     
        private ObservableCollection<Teacher> _teachers;

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
        public ObservableCollection<Teacher> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                OnPropertyChanged(nameof(Teachers));
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
        public ICommand AddGroupCommand { get; }      

        public AddNewGroupVM()
        {
            var appDBContext = new ApplicationDbContext();

            _curriculumService = new CurriculumService(new CurriculumRepository(appDBContext));
            _groupService = new GroupService(new GroupRepository(appDBContext));
            _studentService = new StudentService(new StudentRepository(appDBContext));
            _humanService = new HumanService(new HumanRepository(appDBContext));

            LoadDataInLists();

            AddGroupCommand = new RelayCommand(async () => await SaveGroupDataAsync());
        }

        private async void LoadDataInLists()
        {
            var curriculums = await _curriculumService.ListAsync();
            Curriculums = new ObservableCollection<Curriculum>(curriculums);

            //var teachers = await _teachersService.ListAsync();
            //Te
        }

        private async Task SaveGroupDataAsync ()
        {
            MessageBox.Show($"{_groupName}  {_selectedCurriculum.Name}");
        }
    }
}
