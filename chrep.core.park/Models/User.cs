using chrep.core.park.Enums;

namespace chrep.core.park.Models
{
    public class User
    {
        public int Id { get; set; }
        public string ? FirstName { get; set; }
        public string ? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Tocken    { get; set; }
        public string? Password    { get; set; }
        public UserTypeEnum ? UserTypeEnum { get; set; }
        public virtual List<Role> Roles { get; } = new();
        public virtual List<Demande> Demandes { get; } = new();
        public virtual List<Mission> Missions { get; } = new();

    }
}
