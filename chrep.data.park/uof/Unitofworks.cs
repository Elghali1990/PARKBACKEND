using chrep.core.park.Interfaces;
using chrep.core.park.Models;
using chrep.core.park.uof;
using chrep.data.park.Services;
using chrep.data.park.SqlServer;

namespace chrep.data.park.uof
{
    public class Unitofworks : IUnitofworks
    {
        private readonly AppDbContext _appDbContext;
        public IDataHelper<Role> role { get; private set; }

        public IDemandeService demandeService { get; private set; }

        public IRoleService roleService  { get; private set; }

        public IUserService userService { get; private set; }

        public IMissionService missionService { get; private set; }

        public IVehicleService vehicleService { get; private set; }


        public Unitofworks(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            role = new DataHelper<Role>(_appDbContext);
            demandeService = new DemandeService(appDbContext);
            roleService= new RoleService(appDbContext);
            userService = new UserService(appDbContext);
            missionService = new MissionService(appDbContext);
            vehicleService = new VehicleService(appDbContext);
        }
        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public int commite()
        {
           return _appDbContext.SaveChanges();
        }
    }
}
