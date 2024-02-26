using System.Text.RegularExpressions;
using University.Exceptions;
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

        public async Task<Human?> GetHumanByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid human id");

            var human = await _humanRepository.GetAsync(id);

            if (human is null)
                throw new NotFoundException($"Human with id = {id} does not exist");

            return human;
        }

        public async Task<IList<Human>> ListAsync()
        {
            return await _humanRepository.ListAsync();
        }

        public async Task<int> AddAsync(Human human)
        {
            ValidateHuman(human);
            return await _humanRepository.UpdateAsync(human);
        }

        public async Task<int> UpdateAsync(Human human)
        {
            ValidateHuman(human);
            return await _humanRepository.UpdateAsync(human);
        }

        public async Task<int> DeleteAsync(int id)
        {
            await GetHumanByIdAsync(id);
            return await _humanRepository.DeleteAsync(id);
        }

        private static void ValidateHuman(Human human)
        {
            if (human is null)
                throw new ArgumentNullException(nameof(human), "Human is empty");

            ValidateHumanHumanFullName(human.FirstName, human.LastName);
            ValidateUserPhone(human?.Phone);
        }

        private static void ValidateHumanHumanFullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentNullException("FirstName/LastName is empty");
            }
            else
            {
                firstName = firstName.Trim();
                lastName = lastName.Trim();

                if (firstName.Length > 30)
                    throw new ArgumentException(nameof(firstName), "FirstName must be maximum of 30 characters");
                if (lastName.Length > 30)
                    throw new ArgumentException(nameof(lastName), "LastName must be maximum of 30 characters");

                LanguageValidator.ValidateWordEnUa(firstName);
                LanguageValidator.ValidateWordEnUa(lastName);
            }
        }

        private static void ValidateUserPhone(string? phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                phoneNumber = phoneNumber.Trim();

                const string ukrainianPhoneNumberPattern = @"^\+380\d{9}$";

                if (!Regex.IsMatch(phoneNumber, ukrainianPhoneNumberPattern))
                    throw new ArgumentException(nameof(phoneNumber), "Phone is invalid");
            }
        }
    }
}
