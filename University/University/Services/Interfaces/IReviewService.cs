namespace University.Services.Interfaces
{
    public interface IReviewService
    {
        Task<Review?> GetReviewByIdAsync(int id);
        Task<IList<Review>> ListAsync();
        Task<int> AddAsync(Review review);
        Task<int> UpdateAsync(Review review);
        Task<int> DeleteAsync(int id);
    }
}
