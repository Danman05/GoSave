namespace GoSave.Models
{
    public class VaultImage
    {
        public VaultImage()
        {
            
        }
        public VaultImage(Guid vaultId, string base64Image)
        {
            this.Id = Guid.NewGuid();
            this.Base64Image = base64Image;
            this.VaultId = vaultId;
        }
        public Guid Id { get; set; }
        public Guid VaultId { get; set; }
        public string Base64Image { get; set; }
    }
}
