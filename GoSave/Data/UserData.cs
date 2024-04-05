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
        };

        public ICollection<User> Users { get { return _users; } }

        /// <summary>
        /// Adds user to list of users
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns></returns>
        public void UserToList(User user)
        {
                _users.Add(user);
        }

        /// <summary>
        /// Attempts to find account out from username
        /// </summary>
        /// <param name="username">unique username</param>
        /// <returns>Account or Null</returns>
        public User? ExistingAccount(string username)
        {
            return null;
        }
    }

}
