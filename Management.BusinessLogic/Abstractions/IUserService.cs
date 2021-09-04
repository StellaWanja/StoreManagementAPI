using Management.Data.DTOs.UserDTOs;
using System.Threading.Tasks;

namespace Management.BusinessLogic
{
    public interface IUserService
    {
        Task<bool> DeleteUser(string userId);
        Task<UserResponseDTO> GetUser(string userId);
        Task<bool> UpdateUser(string userId, UpdateUserRequest updateUser);
    }
}