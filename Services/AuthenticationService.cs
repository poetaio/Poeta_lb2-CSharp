using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Wallets.BusinessLayer.Users;
using Wallets.DataStorage;
using System.Threading.Tasks;

namespace Wallets.Services
{
    public class AuthenticationService
    {
        private static FileDataStorage<DBUser> _storage = new FileDataStorage<DBUser>();
        public async Task<User> Authenticate(AuthenticationUser authUser)
        {
            if (String.IsNullOrWhiteSpace(authUser.Login) || String.IsNullOrWhiteSpace(authUser.Password))
                throw new ArgumentException("Login or Password is Empty");

            byte[] data = System.Text.Encoding.ASCII.GetBytes(authUser.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            authUser.Password = hash;

            List<DBUser> users = await _storage.GetAllAsync();

            var dbUser = users.FirstOrDefault(user => user.Login == authUser.Login && user.Password == authUser.Password);

            if (dbUser == null)
                throw new Exception("Wrong Login or Password");

            return new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Email, dbUser.Login);
        }

        public async Task<bool> RegisterUser(RegistrationUser regUser)
        {
            var users = await _storage.GetAllAsync();

            var dbUser = users.FirstOrDefault(user => user.Login == regUser.Login);
            if (dbUser != null)
                throw new Exception("user already exists");

            if (String.IsNullOrWhiteSpace(regUser.Login) || String.IsNullOrWhiteSpace(regUser.Password))
                throw new ArgumentException("Login or Password is Empty");

            byte[] data = System.Text.Encoding.ASCII.GetBytes(regUser.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            regUser.Password = hash;

            dbUser = new DBUser(Guid.NewGuid(), regUser.FirstName, regUser.LastName, regUser.Email, regUser.Login, regUser.Password);
            await _storage.AddOrUpdateAsync(dbUser);
            return true;
        }
    }
}
