using GoSave.Models;

namespace GoSave.Data
{
    public class VaultData
    {
        private UserData _userData = new UserData();

        private static List<Vault> _vaults = new List<Vault>()
        {
            new Vault(name: "Vault", ownerId: 100, 12500),
        };

        public ICollection<Vault> Vaults { get { return _vaults; } }


        /// <summary>
        /// Adds user to list of users. username is unique
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void VaultToList(Vault vault)
        {
            Vault.IncrementSeedId();
            _vaults.Add(vault);
        }

        /// <summary>
        /// Attempts to find vault out from id
        /// </summary>
        /// <param name="username">unique username</param>
        /// <returns>Account or Null</returns>
        public Vault? ExistingVault(int id)
        {
            return _vaults.Find(v => v.Id == id);
        }
    }
}
