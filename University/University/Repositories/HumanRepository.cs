using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;
using University.Repositories.Interfaces;

namespace University.Repositories
{
    public class HumanRepository : IHumanRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HumanRepository(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Human?> GetAsync(int id)
        {
            return await _applicationDbContext.Humans.Where(h => (h.Id == id)).SingleOrDefaultAsync();
        }

        public async Task<IList<Human>> ListAsync()
        {
            return await _applicationDbContext.Humans.ToListAsync();
        }

        public async Task<int> AddAsync(Human human)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await _applicationDbContext.Humans.AddAsync(human);
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
        public async Task<int> UpdateAsync(Human human)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                _applicationDbContext.Entry(human).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return human.Id;
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
