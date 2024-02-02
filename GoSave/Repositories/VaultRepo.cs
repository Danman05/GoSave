using GoSave.Models;

namespace GoSave.Repositories
{

    /// <summary>
    /// Responsible for performing CRUD operations, in the Vault context
    /// </summary>
    public class VaultRepo
    {
        private UserRepo _userRepo;

        private static List<Vault> _vaults = new List<Vault> { };


        public VaultRepo()
        {
            _userRepo = new UserRepo();
            _vaults = new List<Vault>()
            {
                new Vault(id: 1, name: "Vault", user: _userRepo.FindUser(1), 12500.5),
                new Vault(id: 2, name: "Vault", user: _userRepo.FindUser(2), 1000.75)

            };
        }

        /// <summary>
        /// Add to specific vault
        /// </summary>
        /// <param name="id">vault id</param>
        /// <param name="amount">Amount to be added</param>
        /// <param name="user">User performing action</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Vault AddToVault(int id, double amount, User user)
        {            
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vault object</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public Vault? GetVault(int id) 
        {
            try

            {
                Vault vault = _vaults.Find(x => x.Id == id);

                if (vault == null) { throw new ArgumentNullException(); }
                
                return vault;
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch(Exception) {return null; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Vault objects</returns>
        /// <exception cref="NotImplementedException"></exception>
        public ICollection<Vault> GetVaults() 
        {
            return _vaults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteVault(int id) 
        {
            throw new NotImplementedException();
        }
    }
}
