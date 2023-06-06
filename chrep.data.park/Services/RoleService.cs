using chrep.core.park.Interfaces;
using chrep.core.park.Models;
using chrep.data.park.SqlServer;

namespace chrep.data.park.Services
{
    public class RoleService:DataHelper<Role>, IRoleService
    {
        private readonly AppDbContext _appDbContext ;
        public RoleService(AppDbContext appDbContext):base(appDbContext) 
        {
            _appDbContext = appDbContext;
        }

     
    }
}
