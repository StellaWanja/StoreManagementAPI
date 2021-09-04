using Management.Models;
using Management.Data.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Management.Data.DTOs.Mappings
{

    // structure of the response 
    public class UserMappings
    {
        public static UserResponseDTO GetUserResponse(AppUser user)
        {
            return new UserResponseDTO
            {
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Id = user.Id
            };
        }

        public static AppUser GetUser(RegistrationRequest request)
        {
            return new AppUser
            {
                Email = request.Email,
                LastName = request.LastName,
                FirstName = request.FistName,
                UserName = string.IsNullOrWhiteSpace(request.UserName) ? request.Email : request.UserName,

            };
        }
    }
}