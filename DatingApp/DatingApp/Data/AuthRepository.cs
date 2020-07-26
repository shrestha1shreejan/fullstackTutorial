using System.Threading.Tasks;
using DatingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        #region Constructor

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        #endregion

        #region IAuthRepository Implementation

        /// <summary>
        /// Checks the user entered credentials are valid or invalid
        /// <paramref name="username"/>
        /// <paramref name="password"/>        
        /// </summary>        
        /// <returns></returns>
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username == username);

            // incase user doesn't exist return null
            if (user == null)
            {
                return null;
            }

            // if password is invalid return null
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            // incase of success 
            return user;
        }

        /// <summary>
        /// Registers the username and hashed password in database
        /// <paramref name="user"/>
        /// <paramref name="password"/>        
        /// </summary>        
        /// <returns></returns>
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // adding the credentials to the database and saving the credentials
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Checks if the username already exists
        /// <paramref name="username"/>            
        /// </summary>        
        /// <returns></returns>
        public async Task<bool> UserExists(string username)
        {
            // check if the supplied username exists in the database
            if (await _context.Users.AnyAsync( x => x.Username == username))
            {
                return true;
            }

            // incase supplied username is not found in database retun false
            return false;
        }

        #endregion


        #region Private Methods

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // used using statement so that the IDisposable dispose is called so that the resource is freed
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                // Using System.Text.Encoding to convert the password to byte array and generating the password hash
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                // since we are using the password salt to create the hmac
                // the computedHash should match with the one in the database for the user
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

    }
}
