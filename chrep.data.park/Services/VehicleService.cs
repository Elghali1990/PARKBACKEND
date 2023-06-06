using chrep.core.park.Interfaces;
using chrep.core.park.Models;
using chrep.data.park.SqlServer;

namespace chrep.data.park.Services
{
    public class VehicleService:DataHelper<Vehicle>,IVehicleService
    {
        private readonly AppDbContext _appDbContext;
        public VehicleService(AppDbContext appDbContext):base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
