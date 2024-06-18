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
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void AddUser(User user)
        {
            
        }
    }
}
