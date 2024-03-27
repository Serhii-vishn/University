
using University.DbContexts;

namespace University.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReviewRepository(ApplicationDbContext applicationContext)
        {
            _applicationDbContext = applicationContext;
        }

        public async Task<Review?> GetAsync(int id)
        {
            return await _applicationDbContext.Reviews
                .Where(r => (r.Id == id))
                .SingleOrDefaultAsync();
        }

        public async Task<Review?> GetAllAsync(int id)
        {
            return await _applicationDbContext.Reviews
                .Where(r => (r.Id == id))
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .SingleOrDefaultAsync();
        }

        public async Task<IList<Review>> ListAsync(int studentId)
        {
            return await _applicationDbContext.Reviews
                .Include(r => r.Student)
                .Where(s => (s.Student.Id == studentId))
                .ToListAsync();
        }

        public async Task<IList<Review>> ListAllAsync()
        {
            return await _applicationDbContext.Reviews
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .ToListAsync();
        }

        public async Task<int> AddAsync(Review review)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await _applicationDbContext.Reviews.AddAsync(review);
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return result.Entity.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> UpdateAsync(Review review)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                _applicationDbContext.Entry(review).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return review.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                _applicationDbContext.Entry((await GetAsync(id))!).State = EntityState.Deleted;
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
