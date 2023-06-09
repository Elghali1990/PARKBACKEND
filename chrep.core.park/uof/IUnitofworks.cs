using chrep.core.park.Interfaces;
using chrep.core.park.Models;

namespace chrep.core.park.uof
{
    public interface IUnitofworks: IDisposable
    {
        public IRoleService roleService { get; }
        public IDemandeService demandeService { get; }
        public IMissionService missionService { get; }
        public IVehicleService vehicleService { get; }
        public IUserService userService { get; }
        public IAbsenceService absenceService { get; }
        public int commite();
    }
}
