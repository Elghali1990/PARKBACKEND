using chrep.core.park.Enums;
using chrep.core.park.Models;

namespace chrep.core.park.InputVm
{
    public class MissionVm
    {
        public int ? Id { get; set; }
        public DateTime? DateDepart { get; set; }
        public string ? HourDepart { get; set; }
        public string? Instruction { get; set; }
        public string? Observation { get; set; }
        public string? ChauffeurName { get; set; }
        public string? MarqueVehicule { get; set; }
        public int DemandeId { get; set; }
        public MissionTypeEnum MissionType { get; set; }
         public List<int> UserIds { get; set; }
        //public List<User> ? Users { get; set; } = new();
    }
}
