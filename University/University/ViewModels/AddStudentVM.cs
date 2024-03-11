using System.Windows;
using Prism.Commands;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;

namespace University.ViewModels
{
    public class AddStudentVM:
        ViewModelBase
    {
        private readonly IStudentService _studentService;

        private string _lastName;
        private string _firstName;
        private DateTime _dateOfBirth = DateTime.Now;
        private int _course;
        private string _speciality;
        private string? _gender;
        private string? _address;
        private string? _email;  
        private string? _phone;

        private DateTime _currentDate = DateTime.Now;
        private DateTime _startDate = DateTime.Parse("1940-01-01");

        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

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

        public AddStudentVM()
        {
            var appDBContext = new ApplicationDbContext();

            _studentService = new StudentService(new StudentRepository(appDBContext));
        }

        private async void ExecuteSaveStudentCommand()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FirstName))
                    throw new ArgumentNullException("FirstName name");
                if (string.IsNullOrWhiteSpace(LastName))
                    throw new ArgumentNullException("LastName name");
                if (DateOfBirth.Date == _currentDate.Date)
                    throw new ArgumentException("Entert date of birth");
                if (0 >= Course && Course <= 7)
                    throw new ArgumentException("Invalid course");
                if (string.IsNullOrWhiteSpace(Speciality))
                    throw new ArgumentNullException("Speciality");

                var newStudent = new Student()
                {
                    Course = _course,
                    Speciality = _speciality,
                    Human = new Human()
                    {
                        FirstName = _firstName,
                        LastName = _lastName,
                        DateOfBirth = _dateOfBirth,
                        Gender = _gender,
                        Address = _address,
                        Email = _email,
                        Phone = _phone,
                    },
                };

                await _studentService.AddAsync(newStudent);

                MessageBox.Show($"{FirstName} {LastName} added successfully.");
                ClearAddWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearAddWindow()
        {
            _course = 0;
            _speciality = string.Empty;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _gender = string.Empty;
            _address = string.Empty;
            _email = string.Empty;
            _phone = string.Empty;
            _dateOfBirth = _currentDate;

            OnPropertyChanged(nameof(Course));
            OnPropertyChanged(nameof(Speciality));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(DateOfBirth));
            OnPropertyChanged(nameof(Gender));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Phone));
        }
    }
}
