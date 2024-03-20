namespace University.Services
{
    public class CurriculumService : ICurriculumService
    {
        private ICurriculumRepository _curriculumRepository;

        public CurriculumService(ICurriculumRepository curriculumRepository)
        {
            _curriculumRepository = curriculumRepository;
        }

        public async Task<Curriculum?> GetCurriculumByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid curriculum id");

            var curriculum = await _curriculumRepository.GetAsync(id);

            if (curriculum is null)
                throw new NotFoundException($"Curriculum with id = {id} does not exist");

            return curriculum;
        }

        public async Task<IList<Curriculum>> ListAsync()
        {            
            return await _curriculumRepository.ListAsync();
        }

        public async Task<int> AddAsync(Curriculum curriculum)
        {
            ValidateCurriculum(curriculum);            
            return await _curriculumRepository.AddAsync(curriculum);
        }

        public async Task<int> UpdateAsync(Curriculum curriculum)
        {
            ValidateCurriculum(curriculum);
            return await _curriculumRepository.UpdateAsync(curriculum);
        }

        public async Task<int> DeleteAsync(int id)
        {
            await GetCurriculumByIdAsync(id);
            return await _curriculumRepository.DeleteAsync(id);
        }

        private static void ValidateCurriculum(Curriculum curriculum)
        {
            if (curriculum is null)
                throw new ArgumentNullException(nameof(curriculum), "Curriculum is empty");

            ValidateCurriculumName(curriculum.Name);
            ValidateCurriculumDescription(curriculum?.Description);
        }

        private static void ValidateCurriculumName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Curriculum name is empty");
            }
            else
            {
                name = name.Trim();

                if (name.Length > 50)
                    throw new ArgumentException(nameof(name), "Curriculum name must be maximum of 50 characters");

                LanguageValidator.ValidateWordEnUa(name);
            }
        }

        private static void ValidateCurriculumDescription(string? description)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                description = description.Trim();

                if (description.Length > 100)
                    throw new ArgumentException(nameof(description), "Curriculum description must be maximum of 100 characters");
            }
        }
    }
}
