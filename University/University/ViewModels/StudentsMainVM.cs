using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;
using University.Views;

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
                OnPropertyChanged(nameof(Students));
            }
        }

        public StudentsMainVM() 
        {
            var appDBContext = new ApplicationDbContext();

            _studentService = new StudentService(new StudentRepository(appDBContext));

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
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
                    OnPropertyChanged(nameof(Students));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteEditCommand(Student student)
        {
            try
            {
                if (taskWindow == null || !taskWindow.IsVisible)
                {
                    taskWindow = new AddEditStudentView(_studentService, student.Id);
                    taskWindow.Closed += async (s, eventArgs) =>
                    {
                        taskWindow = null;
                        await LoadDataAsync();
                        OnPropertyChanged(nameof(Students));
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

        private void ExecuteAddNewGroupCommand()
        {
            try
            {
                if (taskWindow == null || !taskWindow.IsVisible)
                {
                    taskWindow = new AddEditStudentView();
                    taskWindow.Closed += (s, eventArgs) => taskWindow = null;
                    taskWindow.Show();
                }
                else
                {
                    taskWindow.Focus();
                }

                taskWindow.Closed += async (s, e) =>
                {
                    await LoadDataAsync(); 
                    OnPropertyChanged(nameof(Students));
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
