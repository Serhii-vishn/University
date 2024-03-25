namespace University.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task<Review?> GetReviewByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Review>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
