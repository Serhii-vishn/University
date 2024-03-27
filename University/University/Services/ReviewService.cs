namespace University.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid review id");

            var review = await _reviewRepository.GetAsync(id);

            if (review is null)
                throw new NotFoundException($"Review with id = {id} does not exist");

            return review;
        }

        public async Task<IList<Review>> ListByStudentIdAsync(int studentId)
        {
            return await _reviewRepository.ListAsync(studentId);
        }

        public async Task<int> AddAsync(Review review)
        {
            ValidateReview(review);
            return await _reviewRepository.AddAsync(review);
        }

        public async Task<int> UpdateAsync(Review review)
        {
            ValidateReview(review);
            return await _reviewRepository.UpdateAsync(review);
        }

        public async Task<int> DeleteAsync(int id)
        {
            await GetReviewByIdAsync(id);
            return await _reviewRepository.DeleteAsync(id);
        }

        private void ValidateReview(Review review)
        {
            if(review is null)
                throw new ArgumentNullException(nameof(review), "Review is empty");

            if (review.FeedBack is null || review.FeedBack.Length > 100)
                throw new ArgumentException("FeedBack must be no more than 100 characters");
        }
    }
}
