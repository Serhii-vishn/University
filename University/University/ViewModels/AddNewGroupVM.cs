﻿using System.Collections.ObjectModel;
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
    public class AddNewGroupVM : 
        ViewModelBase
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        private string _groupName;
        private Curriculum _selectedCurriculum;
        private Teacher _selectedTeacher;
        private ObservableCollection<Student> _selectedStudents;

        private ObservableCollection<Curriculum> _curriculums;     
        private ObservableCollection<Teacher> _teachers;
        private ObservableCollection<Student> _students;

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
        public ICommand AddGroupCommand { get; }

        public AddNewGroupVM()
        {
            var appDBContext = new ApplicationDbContext();

            _curriculumService = new CurriculumService(new CurriculumRepository(appDBContext));
            _groupService = new GroupService(new GroupRepository(appDBContext));
            _studentService = new StudentService(new StudentRepository(appDBContext));
            _teacherService = new TeacherService(new TeacherRepository(appDBContext));

            LoadDataInLists();
            AddGroupCommand = new RelayCommand(async () => await SaveGroupDataAsync());
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

        private async Task SaveGroupDataAsync ()
        {
            string str = string.Empty;
            foreach (var item in _selectedStudents)
            {
                str += $"{item.Id}";
            }
            MessageBox.Show($"{str}");
        }
    }
}
