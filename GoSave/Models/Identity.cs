namespace GoSave.Models
{
    /// <summary>
    /// Identity model class, contains required information to perform validation
    /// </summary>
    public class Identity
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Identity(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
