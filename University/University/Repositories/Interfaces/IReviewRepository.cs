namespace University.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review?> GetAsync(int id);
        Task<Review?> GetAllAsync(int id);
        Task<IList<Review>> ListAsync();
        Task<IList<Review>> ListAllAsync();
        Task<int> AddAsync(Review review);
        Task<int> UpdateAsync(Review review);
        Task<int> DeleteAsync(int id);
    }
}
