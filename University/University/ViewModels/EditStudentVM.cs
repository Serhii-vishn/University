using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public EditStudentVM(int studentId)
        {
            var appDBContext = new ApplicationDbContext();

            _studentService = new StudentService(new StudentRepository(appDBContext));

            LoadGroupDataAsync(studentId);
        }

        private async void LoadGroupDataAsync(int groupId)
        {
            try
            {
                var studentInfo = await _studentService.GetAllStudentDataAsync(groupId);

                _course = studentInfo.Course;
                _speciality = studentInfo.Speciality;
                _firstName = studentInfo.Human.FirstName;
                _lastName = studentInfo.Human.LastName;
                _gender = studentInfo.Human.Gender;
                _address = studentInfo.Human.Address;
                _email = studentInfo.Human.Email;
                _phone = studentInfo.Human.Phone;
                _dateOfBirth = studentInfo.Human.DateOfBirth;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
