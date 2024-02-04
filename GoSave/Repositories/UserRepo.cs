using GoSave.Data;
using GoSave.Models;

namespace GoSave.Repositories
{
    /// <summary>
    /// Class that acts like a middleman between controller and data layer
    /// </summary>
    public class UserRepo
    {
        UserData _userData = new UserData();

        /// <summary>
        /// Verifies login
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public User VerifyLogin(Identity identity)
        {
            try
            {
                // Find account by username
                User? foundAccount = _userData.ExistingAccount(identity.Username);

                // Account exist check
                if (foundAccount == null) { return null; }

                // Private keys match check
                if (foundAccount.Identity.Password != identity.Password) { return null; }

                // Login verified
                return foundAccount;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void AddUser(User user)
        {
            // Null checks on Username & Password
            // Required because it's part of the authentication
            if (string.IsNullOrEmpty(user.Identity.Username) || string.IsNullOrEmpty(user.Identity.Password))
                throw new ArgumentNullException("Username and password cannot be null");

            // User already exists - username is unique
            if (_userData.ExistingAccount(user.Identity.Username) != null)
                throw new InvalidOperationException("Username not valid");

            _userData.UserToList(user);
        }
    }
}
