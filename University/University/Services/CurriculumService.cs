using University.Models;
using University.Repositories.Interfaces;
using University.Services.Interfaces;

namespace University.Services
{
    public class CurriculumService : ICurriculumService
    {
        private ICurriculumRepository _curriculumRepository;

        public CurriculumService(ICurriculumRepository curriculumRepository)
        {
            _curriculumRepository = curriculumRepository;
        }

        public async Task<IList<Curriculum>> ListAsync()
        {            
            return await _curriculumRepository.ListAsync();
        }

        public Task<int> AddAsync(Curriculum curriculum)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Curriculum?> GetCurriculumAsync(int id)
        {
            throw new NotImplementedException();
        }



        public Task<int> UpdateAsync(Curriculum curriculum)
        {
            throw new NotImplementedException();
        }
    }
}
