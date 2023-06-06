using chrep.core.park.Enums;

namespace chrep.core.park.Models
{
    public class Demande
    {
        public int Id { get; set; }
        public DateTime DateDemande { get; set; }
        public string ? Objet { get; set; }
        public string ? Detail { get; set; }
        public DateTime ? DateDepart { get; set; }
        public TimeSpan? HourDepart { get; set; }
        public DateTime? DateBack { get; set; }
        public TimeSpan? HourBack { get; set; }
        public string? Observation { get; set; }
        public StatusEnum StatusEnum { get; set; }
        public int ? UserId { get; set; }
        public virtual List<User> Users { get; set; }   = new ();
    }
}
