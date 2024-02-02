using GoSave.Data;
using GoSave.Models;

namespace GoSave.Repositories
{
    public class UserRepo
    {
        UserData _userData = new UserData();

        public User FindUser(int id)
        {
            try
            {
                // TODO increase performance, binary search?

                User foundUser = _userData.Users.FirstOrDefault(x => x.Id == id);

                if (foundUser == null)
                {
                    return null;
                }

                return foundUser;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
