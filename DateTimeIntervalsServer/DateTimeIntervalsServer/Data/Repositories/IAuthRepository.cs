using System.Threading.Tasks;
using DateTimeIntervalsServer.Data.DomainModels;

namespace DateTimeIntervalsServer.Data.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string login, string password);

        Task<bool> UserExists(string login);
    }
}
