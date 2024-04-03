using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GoSave.Models
{
    public class Vault
    {
        [JsonIgnore]
        private static Guid _seedId = Guid.Empty;
        public Guid? Id { get; private set; }

        [JsonIgnore]
        public Guid OwnerId { get; set; }
        public string? Name { get; set; }

        [Required(ErrorMessage = "Goal field is required")]
        public double? Goal { get; set; }

        public double? currentCapacity { get; set; }
        public double? PercentToTarget { get => CalcPercentTarget(); }

        public Vault(string name, Guid ownerId, double? goal)
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

    }
}
