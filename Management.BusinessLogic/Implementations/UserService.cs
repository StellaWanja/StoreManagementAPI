using Management.Data.DTOs;
using Management.Data.DTOs.UserDTOs;
using Management.Data.DTOs.Mappings;
using Management.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Management.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        //update the user
        public async Task<bool> UpdateUser(string userId, UpdateUserRequest updateUser)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.FirstName = string.IsNullOrWhiteSpace(updateUser.FirstName) ? user.FirstName : updateUser.FirstName;
                user.LastName = string.IsNullOrWhiteSpace(updateUser.LastName) ? user.LastName : updateUser.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }

                string errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }

                throw new MissingMemberException(errors);
            }

            throw new ArgumentException("Resource not found");
        }

        //delete the user
        public async Task<bool> DeleteUser(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return true;
                }

                string errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }

                throw new MissingMemberException(errors);
            }
            throw new ArgumentException("Resource not found");

        }

        //get the user
        public async Task<UserResponseDTO> GetUser(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return UserMappings.GetUserResponse(user);
            }
            throw new ArgumentException("Resource not found");
        }
    }
}
