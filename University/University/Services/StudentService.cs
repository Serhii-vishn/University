using University.DbContexts;
using University.Models;
using University.Repositories;
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

        public Task<int> AddAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Student?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Student>> ListAsync()
        {
            return await _studentRepository.ListAsync();
        }

        public Task<int> UpdateAsync(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
