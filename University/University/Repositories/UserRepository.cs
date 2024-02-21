using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;
using University.Repositories.Interfaces;

namespace University.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetAsync(int id)
        {
            var user = await _context.Users.Where(u => (u.Id == id)).SingleOrDefaultAsync();
            return user;
        }

        public async Task<IList<User>> ListAsync()
        {
            var usersList = await _context.Users.ToListAsync();
            return usersList;
        }

        public async Task<int> AddAsync(User user)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var result = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return result.Entity.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }            
        }

        public async Task<int> UpdateAsync(User user)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return user.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Entry((await GetAsync(id))!).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
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
