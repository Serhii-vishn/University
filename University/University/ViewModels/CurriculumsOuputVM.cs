﻿using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services.Interfaces;
using University.Services;
using System.Windows;

namespace University.ViewModels
{
    public class CurriculumsOuputVM : ViewModelBase
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly IHumanService _humanService;

        private List<Curriculum> _curriculums;
        private List<Group> _groups;
        private List<Student> _students;
        private List<Human> _humans;
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
        public List<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }
        public List<Human> Humans
        {
            get { return _humans; }
            set
            {
                _humans = value;
                OnPropertyChanged(nameof(Humans));
            }
        }

        public CurriculumsOuputVM()
        {
            var appDBContext = new ApplicationDbContext();

            _curriculumService = new CurriculumService(new CurriculumRepository(appDBContext));
            _groupService = new GroupService(new GroupRepository(appDBContext));
            _studentService = new StudentService(new StudentRepository(appDBContext));
            _humanService = new HumanService(new HumanRepository(appDBContext));

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var result = await _curriculumService.ListAsync();
                Curriculums = new List<Curriculum>(result);

                var groups = await _groupService.ListAsync();
                Groups = new List<Group>(groups);

                var students = await _studentService.ListAsync();
                Students = new List<Student>(students);

                var humans = await _humanService.ListAsync();
                Humans = new List<Human>(humans);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}