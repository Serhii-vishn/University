using University.Models;

namespace University.Repositories.Interfaces
{
    public interface ICurriculumRepository
    {
        Task<Curriculum?> GetCurriculumAsync(int id);
        Task<IList<Curriculum>> ListAsync();
        Task<int> AddAsync(Curriculum curriculum);
        Task<int> UpdateAsync(Curriculum curriculum);
        Task<int> DeleteAsync(int id);
    }
}
