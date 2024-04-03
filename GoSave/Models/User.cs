namespace GoSave.Models
{
    public class User
    {
        public User(){}

        public User(string firstName, string lastName, Address address, Identity identity)
        {
            this.Id = Guid.NewGuid();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.Identity = identity;
        }


        public Guid? Id { get; private set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        //foreign key reference
        public Identity Identity { get; set; }
    }
}
