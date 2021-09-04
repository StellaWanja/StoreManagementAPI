using Management.Data.DTOs;
using Management.Data.DTOs.Mappings;
using Management.Data.DTOs.UserDTOs;
using Management.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Management.BusinessLogic
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public Authentication(UserManager<AppUser> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        //handle user login
        public async Task<UserResponseDTO> Login(UserRequest userRequest)
        {
            AppUser user = await _userManager.FindByEmailAsync(userRequest.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, userRequest.Password))
                {
                    var response =  UserMappings.GetUserResponse(user);
                    response.Token = await _tokenGenerator.GenerateToken(user);

                    return response;
                }

                throw new AccessViolationException("Invalid credentials");
            }
            throw new AccessViolationException("Invalid credentials");
        }

        //handle user registration
        public async Task<UserResponseDTO> Register(RegistrationRequest registrationRequest)
        {
            AppUser user = UserMappings.GetUser(registrationRequest);

            IdentityResult result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded)
            {
                return UserMappings.GetUserResponse(user);
            }

            string errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }

            throw new MissingFieldException(errors);
        }
    }
}
