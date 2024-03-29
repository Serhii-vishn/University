﻿namespace University.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public async Task<User?> GetAsync(int id)
        {
            return await _applicationDbContext.Users.Where(u => (u.Id == id)).SingleOrDefaultAsync();
        }

        public async Task<User?> GetByLogPassAsync(string login, string password)
        {
            return await _applicationDbContext.Users.Where(u => (u.Login == login))
                                            .Where(u => u.Password == password).SingleOrDefaultAsync();
        }

        public async Task<IList<User>> ListAsync()
        {
            return await _applicationDbContext.Users.ToListAsync();
        }

        public async Task<int> AddAsync(User user)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await _applicationDbContext.Users.AddAsync(user);
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

        public async Task<int> UpdateAsync(User user)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                _applicationDbContext.Entry(user).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();
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
