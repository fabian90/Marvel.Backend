using Marvel.Domain.Entities;
using System.Threading.Tasks;

namespace Marvel.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
