namespace University.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<Groups?> GetAsync(int id);
        Task<Groups?> GetByNameAsync(string name);
        Task<IList<Groups>> ListByCurriculumIdAsync(int curriculumId);
        Task<Groups?> GetAllAsync(int id);
        Task<IList<Groups>> ListAsync();
        Task<int> AddAsync(Groups group);
        Task<int> UpdateAsync(Groups group);
        Task<int> DeleteAsync(int id);
        Task<IList<Groups>> FilterByNameListAsync(string name);
    }
}
