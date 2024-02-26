using University.Models;

namespace University.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Student?> GetStudentByIdAsync(int id);
        Task<IList<Student>> ListAsync();
        Task<int> AddAsync(Student student);
        Task<int> AddFromFileAsync(string filePath);
        Task<int> UpdateAsync(Student student);
        Task<int> DeleteAsync(int id);
    }
}
