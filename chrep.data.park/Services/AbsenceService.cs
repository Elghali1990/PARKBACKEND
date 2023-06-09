using chrep.core.park.InputVm;
using chrep.core.park.Interfaces;
using chrep.core.park.Models;
using chrep.data.park.SqlServer;
using chrep.helpers.park.Constants;

namespace chrep.data.park.Services
{
    public class AbsenceService : DataHelper<Absence>, IAbsenceService
    {
        private readonly AppDbContext _appDbContext;
        private readonly MissionService _missionService;
        private readonly UserService _userService;
        public AbsenceService(AppDbContext appDbContext):base(appDbContext)
        {
            _missionService = new MissionService(appDbContext);
            _userService = new UserService(appDbContext);
        }
        public async Task<List<Absence>> InsertAbsence(AbsenceVm absenceVm)
        {
            var mission = await _missionService.FindAsync(m => m.Id.Equals(absenceVm.IdMission), new[] {Tables.Users});
            if (mission is Mission)
            {
                List<Absence> absences = new List<Absence>();
                foreach (var userId in absenceVm.Userids)
                {
                    var user =await _userService.FindAsync(u=>u.Id == userId);
                    absences.Add(new Absence {MissionId=absenceVm.IdMission,User=user,Mission=mission,UserId=userId,IsAbsent=false });
                }
                var users =mission.Users.ToList();
                foreach (var user in users)
                {
                    if (!absenceVm.Userids.Contains(user.Id))
                    {
                        absences.Add(new Absence { MissionId = absenceVm.IdMission, User = user, Mission = mission, UserId = user.Id, IsAbsent = true });
                    }
                }
                await AddRangeAsync(absences);
                return absences;
            }
            return null;
        }
    }
}
