using chrep.core.park.Models;

namespace chrep.core.park.Interfaces
{
    public interface IMissionService:IDataHelper<Mission>
    {
        public Task<Mission> addmissionwithusermission(Mission m, List<int> Userids);
    }
}
