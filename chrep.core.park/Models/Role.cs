namespace chrep.core.park.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string ? RoleName { get; set; }
        public virtual List<User> Users { get; } = new();
    }
}
