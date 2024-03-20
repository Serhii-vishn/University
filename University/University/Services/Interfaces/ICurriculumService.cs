namespace University.Services.Interfaces
{
    public interface ICurriculumService
    {
        Task<Curriculum?> GetCurriculumByIdAsync(int id);
        Task<IList<Curriculum>> ListAsync();
        Task<int> AddAsync(Curriculum curriculum);
        Task<int> UpdateAsync(Curriculum curriculum);
        Task<int> DeleteAsync(int id);
    }
}
