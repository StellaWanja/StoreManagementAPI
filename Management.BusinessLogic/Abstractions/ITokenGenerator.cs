using Management.Models;
using System.Threading.Tasks;

namespace Management.BusinessLogic
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AppUser user);
    }
}