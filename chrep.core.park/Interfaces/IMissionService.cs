using chrep.core.park.Dtos;
using chrep.core.park.InputVm;
using chrep.core.park.Models;

namespace chrep.core.park.Interfaces
{
    public interface IMissionService : IDataHelper<Mission>
    {
        Task<Mission> InsertMission(MissionVm missionVm);
        Task<List<MissionDtos>> GetAllMissionsByIdUser(int idUser);
        Task<List<MissionDtos>> getAllMissions();
        Task<MissionDetailDtos> GetMissionDetail(int id);
        Task<List<UserMission>> setUserMissionAbsent(List<int> ids, int idMission);
     
    }
}
