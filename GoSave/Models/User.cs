namespace GoSave.Models
{
    public class User
    {
        public User(){}

        public User(string firstName, string lastName, Address address, Guid identity)
        {
            this.Id = Guid.NewGuid();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.IdentityId = identity;
        }


        public Guid Id { get; private set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        //foreign key reference
        public Guid IdentityId { get; set; }
    }
}
