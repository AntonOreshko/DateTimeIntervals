using System.Threading.Tasks;
using DateTimeIntervals.DomainLayer.DomainModels;

namespace DateTimeIntervals.DomainLayer.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string login, string password);

        Task<bool> UserExists(string login);
    }
}
