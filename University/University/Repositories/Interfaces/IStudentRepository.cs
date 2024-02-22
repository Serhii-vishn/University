using University.Models;

namespace University.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetAsync(int id);
        Task<IList<Student>> ListAsync();
        Task<int> AddAsync(Student student);
        Task<int> UpdateAsync(Student student);
        Task<int> DeleteAsync(int id);
    }
}
