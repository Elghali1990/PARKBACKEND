using chrep.core.park.InputVm;
using chrep.core.park.Models;

namespace chrep.core.park.Interfaces
{
    public interface IAbsenceService:IDataHelper<Absence>
    {
        Task<List<Absence>> InsertAbsence(AbsenceVm absenceVm);
    }
}
