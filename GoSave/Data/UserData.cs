using GoSave.Models;

namespace GoSave.Data
{

    /// <summary>
    /// Data layer for Users
    /// </summary>
    public class UserData
    {
        private static List<User> _users = new List<User>
        {
            new User("Daniel", "S", new Address(), new Identity("DanielS", "1234")),
            new User("Amanda", "S", new Address(), new Identity("AmandaS", "1234")),
        };

        public ICollection<User> Users { get { return _users; } }

        /// <summary>
        /// Adds user to list of users. id is unique
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(User user)
        {
            try
            {
                // Null checks
                if (string.IsNullOrEmpty(user.Identity.Username)
                    ||
                    string.IsNullOrEmpty(user.Identity.Password)
                    )
                    throw new ArgumentNullException("Username and password cannot be null");

                // User already exists 
                if (ExistingAccount(user.Identity.Username) != null)
                    throw new InvalidOperationException("Username not valid");

                _users.Add(user);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to find account from username
        /// </summary>
        /// <param name="username">unique username</param>
        /// <returns>Account or Null</returns>
        public User? ExistingAccount(string username)
        {
            return _users.FirstOrDefault(existingAccount => existingAccount.Identity.Username == username);
        }
    }

}
