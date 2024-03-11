using University.Models;

namespace University.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task<Teacher?> GetAllTeacherDataAsync(int id);
        Task<IList<Teacher>> ListAsync();
        Task<IList<Teacher>> GetAllTeachersDataAsync();
        Task<int> AddAsync(Teacher teacher);
        Task<int> UpdateAsync(Teacher teacher);
        Task<int> DeleteAsync(int id);
    }
}
