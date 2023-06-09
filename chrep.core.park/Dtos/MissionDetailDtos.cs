namespace chrep.core.park.Dtos
{
    public class MissionDetailDtos
    {
        public int Id { get; set; }
        public string ? Objet { get; set; }
        public string ? Detail { get; set; }
        public string ? Instruction { get; set; }
        public string ? Observation { get; set; }
        public string ? Chauffeur { get; set; }
        public string ? Vehicule { get; set; }
        public int ? MissionType { get; set; }
        public int DemandeId { get; set; }
        public DateTime DateDepart { get; set; }
        public string ? HeurDepart { get; set; }
       public List<UserMissionDtos> UsersMission { get; set; }
    }
}
