﻿using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;

namespace University.ViewModels
{
    public class StudentsMainVM:
        ViewModelBase
    {
        private Window? taskWindow = null;
        private readonly IStudentService _studentService;

        private ObservableCollection<Student> _students;

        private DelegateCommand<Student> _deleteCommand;
        private DelegateCommand<Student> _editCommand;
        private DelegateCommand _addNewCommand;

        public DelegateCommand<Student> DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new DelegateCommand<Student>(ExecuteDeleteCommand));
        public DelegateCommand<Student> EditCommand =>
            _editCommand ?? (_editCommand = new DelegateCommand<Student>(ExecuteEditCommand));
        public DelegateCommand AddNewCommand =>
            _addNewCommand ?? (_addNewCommand = new DelegateCommand(ExecuteAddNewGroupCommand));

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged(nameof(_students));
            }
        }

        public StudentsMainVM() 
        {
            var appDBContext = new ApplicationDbContext();

            _studentService = new StudentService(new StudentRepository(appDBContext));

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                var groups = await _studentService.GetAllStudentsDataAsync();
                Students = new ObservableCollection<Student>(groups);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteDeleteCommand(Student student)
        {
            throw new NotImplementedException();
        }

        private void ExecuteEditCommand(Student student)
        {
            throw new NotImplementedException();
        }

        private void ExecuteAddNewGroupCommand()
        {
            throw new NotImplementedException();
        }
    }
}
