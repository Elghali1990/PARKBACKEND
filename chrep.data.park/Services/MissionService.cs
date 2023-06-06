using chrep.core.park.Interfaces;
using chrep.core.park.Models;
using chrep.data.park.SqlServer;

namespace chrep.data.park.Services
{
    public class MissionService:DataHelper<Mission>, IMissionService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserService _userService;
        public MissionService(AppDbContext appDb) : base(appDb)
        {
            _userService = new UserService(appDb);
        }

        public async Task<Mission> addmissionwithusermission(Mission m, List<int> Userids)
        {
            var missionaded = await AddAsync(m);
            foreach (var id in Userids)
            {
                missionaded.Users.Add(await _userService.getByIdAsync(id));
            }
            return missionaded;
        }
    }
}
