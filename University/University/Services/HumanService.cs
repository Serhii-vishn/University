using University.Models;
using University.Repositories.Interfaces;
using University.Services.Interfaces;

namespace University.Services
{
    public class HumanService : IHumanService
    {
        private readonly IHumanRepository _humanRepository;

        public HumanService(IHumanRepository humanRepository) 
        {
            _humanRepository = humanRepository;
        }

        public Task<int> AddAsync(Human human)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Human?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Human>> ListAsync()
        {
           return await _humanRepository.ListAsync();
        }

        public Task<int> UpdateAsync(Human human)
        {
            throw new NotImplementedException();
        }
    }
}
