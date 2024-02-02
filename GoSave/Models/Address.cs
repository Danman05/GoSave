namespace GoSave.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }


        /// <summary>
        /// Overidden ToString() Method
        /// </summary>
        /// <returns>Full address</returns>
        public override string ToString()
        {
            return $"{Country}, {Street}, {City} {PostalCode}";
        }
    }
}
