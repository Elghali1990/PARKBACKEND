using chrep.core.park.InputVm;
using chrep.core.park.Models;

namespace chrep.core.park.Interfaces
{
    public interface IMissionService:IDataHelper<Mission>
    {
         Task<Mission> InsertMission(MissionVm missionVm);
        Task<List<Mission>> GetAllMissionsByIdUser(int idUser );
        
    }
}
