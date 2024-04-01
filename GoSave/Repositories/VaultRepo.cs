using GoSave.Data;
using GoSave.Models;

namespace GoSave.Repositories
{

    /// <summary>
    /// Responsible for performing CRUD operations, in the Vault context
    /// </summary>
    public class VaultRepo
    {
        private VaultData _vaultData = new VaultData();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vault"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddVault(Vault vault)
        {

            // Valid checks
            if (string.IsNullOrEmpty(vault.Name))
                throw new ArgumentNullException("Vault needs a name");
            else if (vault.Goal < 0)
                throw new ArgumentNullException("Vault needs a goal");

            _vaultData.VaultToList(vault);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vault object</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Vault GetVault(int id)
        {
            return _vaultData.ExistingVault(id) ?? throw new ArgumentNullException("Vault not found");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Vault</returns>
        /// <exception cref="NotImplementedException"></exception>
        public ICollection<Vault> GetVaults(int userId)
        {
            return _vaultData.Vaults.Where(vaultOwner => vaultOwner.OwnerId == userId).ToList();
        }


        /// <summary>
        /// Updates currentCapacity
        /// </summary>
        /// <param name="id">vault id</param>
        /// <param name="amount">Amount to be added</param>
        /// <param name="user">User performing action</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void AddToVault(int id, double amount)
        {
            GetVault(id).currentCapacity += amount;

            //throw new NotImplementedException();
        }
         

        /// <summary>
        /// Delete a vault
        /// Admins can delete any vault
        /// Users can delete their own vaults
        /// </summary>
        /// <param name="id">Vault Id</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteVault(int id)
        {
            //Vault? vault = _vaultData.Vaults.FirstOrDefault(x => x.Id == id);
            //_vaultData.Vaults.Remove(vault);
            throw new NotImplementedException();
        }
    }
}
