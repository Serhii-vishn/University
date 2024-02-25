using University.Models;

namespace University.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Student?> GetUserByIdAsync(int id);
        Task<IList<Student>> ListAsync();
        Task<int> AddAsync(Student student);
        Task<int> UpdateAsync(Student student);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<Student>> GetStudentsForGroupAsync(int id);
    }
}
