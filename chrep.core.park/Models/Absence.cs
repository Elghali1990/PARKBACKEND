namespace chrep.core.park.Models
{
    public class Absence
    {
        public int Id { get; set; }
        public bool IsAbsent { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int MissionId { get; set; }
        public virtual Mission Mission { get; set; }
    }
}
