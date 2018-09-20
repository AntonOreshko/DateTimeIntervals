using System.Threading.Tasks;
using DateTimeIntervalsServer.Data.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DateTimeIntervalsServer.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private DateTimeIntervalContext _context;

        public AuthRepository(DateTimeIntervalContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Login(string login, string password)
        {
            var user = await _context.Users.Include(u => u.Intervals).FirstOrDefaultAsync(x => x.Login == login);

            if (user == null) return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<bool> UserExists(string login)
        {
            if (await _context.Users.AnyAsync(x => x.Login == login))
                return true;

            return false;
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }
    }
}
