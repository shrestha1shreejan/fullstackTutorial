using DatingApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DatingApp.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        #region Constructer

        public Seed(DataContext context)
        {
            _context = context;
        }

        #endregion

        public void SeedUser()
        {
            // read all data from file
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");

            // converstion of text to object
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();

                _context.Users.Add(user);
            }

            _context.SaveChanges();
        }

        #region Private method

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // used using statement so that the IDisposable dispose is called so that the resource is freed
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                // Using System.Text.Encoding to convert the password to byte array and generating the password hash
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        #endregion
    }
}
