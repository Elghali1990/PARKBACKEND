using chrep.core.park.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace chrep.core.park.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public DateTime? DateDepart { get; set; }
        public TimeSpan? HourDepart { get; set; }
        public string ? Instruction { get; set; }
        public string ? Observation { get; set; }
        public int ? ChauffeurId { get; set; }
        public string ? ChauffeurName { get; set; }
        public int ? VehicleId { get; set; }
        public Vehicle  ? Vehicle { get; set; }
        public int DemandeId { get; set; }
        public Demande? Demande { get; set; }
        public MissionTypeEnum MissionType { get; set; }
        public List<User> Users { get; } = new();
    }
}
