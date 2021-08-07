using App.Common.DTO;
using App.Domain.Base;
using App.Infrastructure.Base;
using App.Infrastructure.Database.Entities;
using App.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace App.Domain.Services
{
    public interface IUserService : IBaseService<UserDTO>
    {
      string GenerateJSONWebToken(LoginDTO login);
      List<UserDTO> GetUser();
       UserDTO CreateUser(UserDTO user);
       UserDTO UpdateUser(UserDTO user);
       UserDTO DeleteUser(UserDTO user);
       UserDTO filterUser(UserDTO user);

    }
    public class UserService : BaseService<UserDTO, Users>, IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IBaseRepository<Users> repository,
            IMapper mapper,
            IConfiguration configuration,
            IUserRepository userRepository)
            : base(repository, mapper, configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

   

        public string GenerateJSONWebToken(LoginDTO login)
        {
            string user = string.Empty;
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            if (IsvalidateUser(login.UserId)) 
            {
                user = _configuration["Jwt:Issuer"];
            }
            else
            {
                user = _configuration["Jwtgeneral:Issuer"];
            }
            JwtSecurityToken token = new JwtSecurityToken(user,
              _configuration["Jwt:Audience"],
              null,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public UserDTO CreateUser(UserDTO user)
        {
            Users users = _mapper.Map<Users>(user);
            _userRepository.Create(users);
            _userRepository.SaveChanges();
            return user;
        }

        public UserDTO UpdateUser(UserDTO user)
        {
             Users users = _mapper.Map<Users>(user);
            _userRepository.Update(users);
            _userRepository.SaveChanges();
            return user;
        }

        public UserDTO DeleteUser(UserDTO user) 
        {
            Users users = _mapper.Map<Users>(user);
            _userRepository.Delete(users);
            _userRepository.SaveChanges();
            return user;
        }

        public UserDTO filterUser(UserDTO user) 
        {
            Users result = _userRepository.FindById(user.IdUser);
            UserDTO users = _mapper.Map<UserDTO>(result);
            return users;
        }


        public List<UserDTO> GetUser() 
        {
            List<UserDTO> resultUser = new List<UserDTO>();
             var users = _userRepository.GetAll();
            resultUser= _mapper.Map<List<UserDTO>>(users);
            return resultUser;
        }

        private bool IsvalidateUser(string IdUser)
        {
            bool result = false;
            var userDTO = _userRepository.FindById(IdUser);
            if (userDTO.IdRol == 1)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
