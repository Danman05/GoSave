namespace GoSave.Models
{
    public class User
    {
        private static int _seedId = 100;

        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        public Identity Identity { get; set; }


        public User(string firstName, string lastName, Address address, Identity identity)
        {
            Id = _seedId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Identity = identity;

        }

        public static void IncrementSeedId()
        {
            _seedId++;
        }

    }
}
