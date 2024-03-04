using CsvHelper;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Windows;
using University.Exceptions;
using University.Models;
using University.Repositories.Interfaces;
using University.Services.Interfaces;

namespace University.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid student id");

            var student = await _studentRepository.GetAsync(id);

            if (student is null)
                throw new NotFoundException($"Student with id = {id} does not exist");

            return student;
        }

        public async Task<IList<Student>> ListAsync()
        {
            return await _studentRepository.ListAsync();
        }

        public async Task<IList<Student>> GetAllStudentsDataAsync()
        {
            return await _studentRepository.ListAllAsync();
        }

        public async Task<IList<Student>> GetAllFreeStudentsDataAsync()
        {
           var result = await _studentRepository.ListAllAsync();

            return result.Where(s => s.Group is null).ToList();
        }

        public async Task<int> AddAsync(Student student)
        {
            ValidationStudent(student);
            return await _studentRepository.AddAsync(student);
        }

        public Task<IList<Student>> AddFromFileAsync(string filePath)
        {
            ValidateFilePath(filePath);
            var students = ReadStudentsFromCSV(filePath);
            MessageBox.Show($"{students.Count()}");
            return null;
        }

        public async Task<int> UpdateAsync(Student student)
        {
            ValidationStudent(student);
            return await _studentRepository.UpdateAsync(student);
        }

        public async Task<int> DeleteAsync(int id)
        {
            await GetStudentByIdAsync(id);
            return await _studentRepository.DeleteAsync(id);
        }
        
        private static void ValidationStudent(Student student)
        {
            if (student is null)
                throw new ArgumentNullException(nameof(student), "Student is empty");

            if(student.Course >= 1 &&  student.Course <= 6)
                throw new ArgumentException(nameof(student.Course), "Course must be from 1 to 6");

            ValidateStudentSpeciality(student.Speciality);
        }

        private static void ValidateStudentSpeciality(string speciality)
        {
            if (string.IsNullOrWhiteSpace(speciality))
            {
                throw new ArgumentNullException(nameof(speciality), "Speciality is empty");
            }
            else
            {
                speciality = speciality.Trim();

                if (speciality.Length > 30)
                    throw new ArgumentException(nameof(speciality), "Speciality must be maximum of 30 characters");

                LanguageValidator.ValidateWordEnUa(speciality);
            }
        }

        private static void ValidateFilePath(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File name is empty or null");
            if (new FileInfo(filePath).Length == 0)
                throw new ArgumentException($"File {filePath} is empty");
            if (!filePath.EndsWith(".csv"))
                throw new ArgumentException($"File must be in .csv format");
        }

        private IList<Student> ReadStudentsFromCSV(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvStudentModelMap>();
            var records = csv.GetRecords<Student>().ToList();

            return records;
        }
    }
}
