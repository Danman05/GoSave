namespace GoSave.Models
{
    public class User
    {
        private static int _seedId = 0;

        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        public Identity Identity { get; set; }


        public User(string firstName, string lastName, Address address, Identity identity)
        {
            _seedId++;
            Id = _seedId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Identity = identity;

        }

    }
}
