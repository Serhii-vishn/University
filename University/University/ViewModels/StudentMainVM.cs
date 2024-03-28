namespace University.ViewModels
{
    public class StudentMainVM : ViewModelBase
    {
        private Human? _human;
        private Student? _student;

        private string _lastName;
        private string _firstName;
        private int? _course;
        private DateTime _dateOfBirth = DateTime.Now;
        private string? _gender;
        private string? _address;
        private string? _email;
        private string? _phone;
        private string? _groupName;
        private string _speciality;

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

        public int? Course
        {
            get { return _course; }
            set
            {
                _course = value;
                OnPropertyChanged(nameof(Course));
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
        public string? Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string? Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public string? Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string? Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string? GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(GroupName));
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

        private readonly IStudentService _studentService;
        private readonly IHumanService _humanService;

        private DelegateCommand _saveChangesCommand;

        public DelegateCommand SaveChangesCommand =>
            _saveChangesCommand ??= new DelegateCommand(ExecuteSaveChangesCommandAsync);

        public StudentMainVM(int userId)
        {
            _humanService = new HumanService(new HumanRepository(new ApplicationDbContext()));
            _studentService = new StudentService(new StudentRepository(new ApplicationDbContext()));        

            LoadDataAsync(userId);
        }

        private async void LoadDataAsync(int userId)
        {
            try
            {
                _human = await _humanService.GetHumanByUserIdAsync(userId);

                FirstName = _human.FirstName;
                LastName = _human.LastName;
                DateOfBirth = _human.DateOfBirth;
                Address = _human.Address;
                Gender = _human.Gender;
                Email = _human.Email;
                Phone = _human.Phone;

                _student = await _studentService.GetAllStudentDataByHumanAsync(_human.Id);
                GroupName = _student.Group?.GroupName;
                Speciality = _student?.Speciality;
                Course = _student?.Course;
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show("Student is not connected to any group");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteSaveChangesCommandAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FirstName))
                    throw new ArgumentNullException("FirstName name");
                if (string.IsNullOrWhiteSpace(LastName))
                    throw new ArgumentNullException("LastName name");
                if (DateOfBirth.Date == _currentDate.Date)
                    throw new ArgumentException("Entert date of birth");

                _human.FirstName = _firstName;
                _human.LastName = _lastName;
                _human.DateOfBirth = _dateOfBirth.Date;
                _human.Gender = _gender;
                _human.Address = _address;
                _human.Email = _email;
                _human.Phone = _phone;

                await _humanService.UpdateAsync(_human);

                MessageBox.Show($"Updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
