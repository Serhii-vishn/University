using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using University.Models;
using University.Services.Interfaces;

namespace University.ViewModels
{
    public class TeacherInfoVM :
        ViewModelBase
    {
        private Teacher? _teacher;
        private string _lastName;
        private string _firstName;
        private DateTime _dateOfBirth = DateTime.Now;
        private string _position;
        private string _academicDegreee;
        private string? _gender;
        private string? _address;
        private string? _email;
        private string? _phone;

        private ObservableCollection<Group> _groupsList;
        private ObservableCollection<Curriculum> _curriculumsList;

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
        public string AcademicDegreee
        {
            get { return _academicDegreee; }
            set
            {
                _academicDegreee = value;
                OnPropertyChanged(nameof(AcademicDegreee));
            }
        }
        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
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

        public ObservableCollection<Group> Groups
        {
            get { return _groupsList; }
            set
            {
                _groupsList = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public ObservableCollection<Curriculum> Curriculums
        {
            get { return _curriculumsList; }
            set
            {
                _curriculumsList = value;
                OnPropertyChanged(nameof(Curriculums));
            }
        }

        private ITeacherService _teacherService;
        private DelegateCommand _saveChangesCommand;

        public DelegateCommand SaveChangesCommand =>
            _saveChangesCommand ??= new DelegateCommand(ExecuteSaveChangesCommandAsync);

        public TeacherInfoVM(ITeacherService teacherService, int teacherId)
        {
            _teacherService = teacherService;

            LoadDataAsync(teacherId);
        }

        private async void LoadDataAsync(int teacherId)
        {
            _teacher = await _teacherService.GetAllTeacherDataByHumanAsync(teacherId);

            FirstName = _teacher.Human.FirstName;
            LastName = _teacher.Human.LastName;
            AcademicDegreee = _teacher.AcademicDegreee;
            DateOfBirth = _teacher.Human.DateOfBirth;
            Position = _teacher.Position;
            Address = _teacher.Human.Address;
            Gender = _teacher.Human.Gender;
            Email = _teacher.Human.Email;
            Phone = _teacher.Human.Phone;
            Groups = new ObservableCollection<Group> (_teacher.Groups);
            Curriculums = new ObservableCollection<Curriculum>(_teacher.Curriculums);
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

                _teacher.AcademicDegreee = _academicDegreee;
                _teacher.Position = _position;
                _teacher.Human.FirstName = _firstName;
                _teacher.Human.LastName = _lastName;
                _teacher.Human.DateOfBirth = _dateOfBirth.Date;
                _teacher.Human.Gender = _gender;
                _teacher.Human.Address = _address;
                _teacher.Human.Email = _email;
                _teacher.Human.Phone = _phone;

                await _teacherService.UpdateAsync(_teacher);

                MessageBox.Show($"Updated successfully.");
                CloseCurrentWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
