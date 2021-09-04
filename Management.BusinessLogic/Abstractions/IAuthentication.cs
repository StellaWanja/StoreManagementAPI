using Management.Data.DTOs.UserDTOs;
using System.Threading.Tasks;

namespace Management.BusinessLogic
{
    public interface IAuthentication
    {
        Task<UserResponseDTO> Login(UserRequest userRequest);
        Task<UserResponseDTO> Register(RegistrationRequest registrationRequest);
    }
}