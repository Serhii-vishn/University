﻿using Microsoft.VisualBasic.Logging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using University.Exceptions;
using University.Models;
using University.Repositories.Interfaces;
using University.Services.Interfaces;

namespace University.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            if(id <= 0)
                throw new ArgumentException("Invalid user id");

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user is null)
                throw new NotFoundException($"User with id = {id} does not exist");

            return user;
        }

        public async Task<User?> GetUserByLogPassAsync(string login, string password)
        {
            var passwordHash = GetHashPassword(password);

            var user = await _userRepository.GetUserByLogPassAsync(login, passwordHash);

            if (user is null)
                throw new NotFoundException("User with this login or password does not exist");

            return user;
        }

        public Task<IList<User>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        private string GetHashPassword(string password)
        {
            var md5 = MD5.Create();

            var hashPass = md5.ComputeHash
                (
                    Encoding.UTF8.GetBytes(password)
                ); 

            return Convert.ToBase64String(hashPass);
        }

        private static void ValidateUserLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException(nameof(login), "User name is empty");
            }
            else
            {
                login = login.Trim();

                if (login.Length > 50)
                {
                    throw new ArgumentException(nameof(login), "User name must be maximum of 55 characters");
                }

                Regex englishWordPattern = new("^[a-zA-Z -]+$");
                Regex ukrainianWordPattern = new("^[АаБбВвГгҐґДдЕеЄєЖжЗзИиІіЇїЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщьЮюЯя -]+$");

                if (!englishWordPattern.IsMatch(login))
                {
                    if (!ukrainianWordPattern.IsMatch(login))
                    {
                        throw new ArgumentException(nameof(login), "User name must consist of english or ukrainian letters only");
                    }
                }
            }
        }

    }
}
