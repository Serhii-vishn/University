using University.Exceptions;
using University.Models;
using University.Repositories;
using University.Repositories.Interfaces;
using University.Services.Interfaces;

namespace University.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid teacher id");

            var teacher = await _teacherRepository.GetAsync(id);

            if (teacher is null)
                throw new NotFoundException($"Teacher with id = {id} does not exist");

            return teacher;
        }

        public async Task<Teacher?> GetAllTeacherDataByHumanAsync(int humanId)
        {
            if (humanId <= 0)
                throw new ArgumentException("Invalid human id");

            var teacher = await _teacherRepository.GetAllByHumanAsync(humanId);

            if (teacher is null)
                throw new NotFoundException($"Human with id = {humanId} does not exist");

            return teacher;
        }

        public async Task<IList<Teacher>> GetAllTeachersDataAsync()
        {
            return await _teacherRepository.ListAllAsync();
        }

        public async Task<IList<Teacher>> ListAsync()
        {
            return await _teacherRepository.ListAsync();
        }

        public async Task<int> AddAsync(Teacher teacher)
        {
            ValidationTeacher(teacher);
            return await _teacherRepository.AddAsync(teacher);
        }

        public async Task<int> UpdateAsync(Teacher teacher)
        {
            ValidationTeacher(teacher);
            return await _teacherRepository.UpdateAsync(teacher);
        }

        public async Task<int> DeleteAsync(int id)
        {
            await GetTeacherByIdAsync(id);
            return await _teacherRepository.DeleteAsync(id);
        }

        private void ValidationTeacher(Teacher teacher)
        {
            if (teacher is null)
                throw new ArgumentNullException(nameof(teacher), "Teacher is empty");

            ValidateTeacherPosition(teacher.Position);
            ValidateTeacherAcademicDegreee(teacher.AcademicDegreee);
        }

        private static void ValidateTeacherPosition(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
            {
                throw new ArgumentNullException(nameof(position), "Position is empty");
            }
            else
            {
                position = position.Trim();

                if (position.Length > 40)
                    throw new ArgumentException(nameof(position), "Position must be maximum of 40 characters");

                LanguageValidator.ValidateWordEnUa(position);
            }
        }

        private static void ValidateTeacherAcademicDegreee(string academicDegreee)
        {
            if (string.IsNullOrWhiteSpace(academicDegreee))
            {
                throw new ArgumentNullException(nameof(academicDegreee), "AcademicDegreee is empty");
            }
            else
            {
                academicDegreee = academicDegreee.Trim();

                if (academicDegreee.Length > 40)
                    throw new ArgumentException(nameof(academicDegreee), "AcademicDegreee must be maximum of 40 characters");

                LanguageValidator.ValidateWordEnUa(academicDegreee);
            }
        }
    }
}
