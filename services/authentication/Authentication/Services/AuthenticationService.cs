using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Authentication.Database;
using Authentication.Models;
using Authentication.Providers;
using Authentication.Repositories;
using Authentication.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUsersService _usersService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly UnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork, IUsersService usersService, IOptions<AppSettings> appSettings)
        {
            _usersService = usersService;
            _appSettings = appSettings;
            _unitOfWork = (UnitOfWork) unitOfWork;
        }

        public User Get(Guid id)
        {
            return _unitOfWork.AuthenticationRepository.GetAsync(id).Result;
        }

        public async Task<User> Register(string username, string password)
        {
            var userServiceModel = await _usersService.RegisterUser(username);

            if (userServiceModel != null)
            {
                var cipher = CryptoProvider.Encrypt(new CryptoProvider.Plain {Password = password});
                var user = new User
                {
                    UserId = userServiceModel.UserId, 
                    Hash = cipher.Hash, 
                    Salt = cipher.Salt
                };

                _unitOfWork.AuthenticationRepository.Register(user);
                _unitOfWork.CommitAsync();
                return user;
            }

            return null;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var user = await _unitOfWork.AuthenticationRepository.GetAsync(username);

            if (user == null)
                return null;

            if (!CryptoProvider.VerifyPassword(
                new CryptoProvider.Plain {Password = password},
                new CryptoProvider.Cipher {Hash = user.Hash, Salt = user.Salt}))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(new KeyProvider(_appSettings).GetKey());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.AuthenticationRepository.GetAllAsync().ToEnumerable();
        }

        public void Delete(Guid id)
        {
            _unitOfWork.AuthenticationRepository.Delete(id);
            _unitOfWork.CommitAsync();
        }

        public void Update(User user)
        {
            _unitOfWork.AuthenticationRepository.Update(user);
            _unitOfWork.CommitAsync();
        }

        public User Get(string email)
        {
            return _unitOfWork.AuthenticationRepository.GetAsync(email).Result;
        }
    }
}