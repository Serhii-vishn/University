using Prism.Commands;
using System.Windows;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;

namespace University.ViewModels
{
    public class EditStudentVM :
        ViewModelBase
    {
        private readonly IStudentService _studentService;
        private readonly IHumanService _humanService;

        private Student? _student;
        private string _lastName;
        private string _firstName;
        private DateTime _dateOfBirth;
        private int _course;
        private string _speciality;
        private string? _gender;
        private string? _address;
        private string? _email;
        private string? _phone;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
        public int Course
        {
            get { return _course; }
            set
            {
                _course = value;
                OnPropertyChanged(nameof(Course));
            }
        }
        public string Speciality
        {
            get { return _speciality; }
            set
            {
                _speciality = value;
                OnPropertyChanged(nameof(Speciality));
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private DelegateCommand _saveStudentCommand;
        public DelegateCommand SaveStudentCommand =>
            _saveStudentCommand ?? (_saveStudentCommand = new DelegateCommand(ExecuteSaveStudentCommand));

        public EditStudentVM(IStudentService studentService, int studentId)
        {
            var appDBContext = new ApplicationDbContext();

            _studentService = studentService;
            _humanService = new HumanService(new HumanRepository(appDBContext));

            LoadGroupDataAsync(studentId);
        }

        private async void LoadGroupDataAsync(int groupId)
        {
            try
            {
                _student = await _studentService.GetAllStudentDataAsync(groupId);

                Course = _student.Course;
                Speciality = _student.Speciality;
                FirstName = _student.Human.FirstName;
                LastName = _student.Human.LastName;
                Gender = _student.Human.Gender;
                Address = _student.Human.Address;
                Email = _student.Human.Email;
                Phone = _student.Human.Phone;
                DateOfBirth = _student.Human.DateOfBirth;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteSaveStudentCommand()
        {
            try
            {
                _student.Course = Course;
                _student.Speciality = Speciality;
                _student.Human.FirstName = FirstName;
                _student.Human.LastName = LastName;
                _student.Human.Gender = Gender;
                _student.Human.Address = Address;
                _student.Human.Email = Email;
                _student.Human.Phone = Phone;
                _student.Human.DateOfBirth = DateOfBirth;
                
                await _studentService.UpdateAsync(_student);
                MessageBox.Show($"{FirstName} {LastName} updated successfully.");

                CloseCurrentWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating: {ex.Message}");
            }
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
