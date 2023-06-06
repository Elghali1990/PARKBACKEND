using chrep.core.park.Enums;
using chrep.core.park.Models;

namespace chrep.core.park.InputVm
{
    public class DemandeVm
    {
        public int Id { get; set; }
        public string? Objet { get; set; }
        public string? Detail { get; set; }
        public DateTime? DateDepart { get; set; }
        public string? HourDepart { get; set; }
        public DateTime? DateBack { get; set; }
        public string? HourBack { get; set; }
        public string? Observation { get; set; }
        public int? Userid { get; set; }
        public List<int> Userids { get; set; }
    }
}
