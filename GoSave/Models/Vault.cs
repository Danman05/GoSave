namespace GoSave.Models
{
    public class Vault
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public double Goal { get; set; }

        public double currentCapicity { get; set; }
        public double PercentToTarget { get => calcPercentTarget(); }

        public Vault(int id, string name, User user, double goal)
        {
            this.Id = id;
            this.Name = name;
            this.Owner = user;
            this.Goal = goal;
        }
        private double calcPercentTarget()
        {
            return (double)currentCapicity / (double)Goal;
        }

    }
}
