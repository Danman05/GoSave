using System.ComponentModel.DataAnnotations;

namespace GoSave.Models
{
    public class Vault
    {
        private static int _seedId = 50;
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Goal field is required")]
        public double? Goal { get; set; }

        public double currentCapacity { get; set; }
        public double PercentToTarget { get => CalcPercentTarget(); }

        public Vault(string name, int ownerId, double? goal)
        {
            this.Id = _seedId;
            this.Name = name;
            this.OwnerId = ownerId;
            this.Goal = goal;
        }
        private double CalcPercentTarget()
        {
            if (currentCapacity <= 0)
                return 0;
            return (double)currentCapacity / (double)Goal * 100;
        }

        public static void IncrementSeedId()
        {
            _seedId++;
        }

    }
}
