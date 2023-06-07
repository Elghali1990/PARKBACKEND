using chrep.core.park.Dtos;
using chrep.core.park.Enums;
using chrep.core.park.InputVm;
using chrep.core.park.Interfaces;
using chrep.core.park.Models;
using chrep.data.park.SqlServer;
using chrep.helpers.park.Constants;
using Microsoft.EntityFrameworkCore;

namespace chrep.data.park.Services
{
    public class MissionService:DataHelper<Mission>, IMissionService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserService _userService;
        private readonly DemandeService _demandeService;
        private readonly VehicleService _vehicleService;
        private readonly RoleService _roleService;
        public MissionService(AppDbContext appDb) : base(appDb)
        {
            _userService = new UserService(appDb);
            _demandeService = new DemandeService(appDb);
            _vehicleService = new VehicleService(appDb);
            _roleService = new RoleService(appDb);
        }

        public async Task<List<Mission>> GetAllMissionsByIdUser(int idUser)
        {
            var missions = await GetAllWithOptionAsync(m=>m.Demande.UserId.Equals(idUser) && m.DemandeId !=null);
            var user = await _userService.FindAsync(u => u.Id.Equals(idUser));
            var data = await GetAllWithOptionAsync(m => m.Users.Contains(user));
            foreach (var mission in data)
            {
                int count = missions.Count(m => m.Id == mission.Id);
                if(count ==0)missions.Add(mission);
            }
            return missions;
        }

        public async Task<Mission> InsertMission(MissionVm missionVm)
        {
            var demande = await _demandeService.FindAsync(d => d.Id == missionVm.DemandeId, new[] {Tables.Users} );
            if (demande is Demande)
            {
                demande.StatusEnum = StatusEnum.VALIDATE;
                await _demandeService.Update(demande);

                var role = await _roleService.FindAsync(r => r.Id == 3);
                var users = await _userService.GetAllWithOptionAsync(u => u.Roles.Contains(role));
                var chauffeurs = users.Select(c => new UserDots { Id = c.Id, FullName = c.FirstName + " " + c.LastName }).ToList();
                var chauffeur = chauffeurs.FirstOrDefault(c => c.FullName == missionVm.ChauffeurName);
                var vehicule = await _vehicleService.FindAsync(v=>v.Marque==missionVm.MarqueVehicule);
                var mission = new Mission()
                {
                    DateDepart=missionVm.DateDepart,
                    HourDepart=TimeSpan.Parse(missionVm.HourDepart),
                    Instruction=missionVm.Instruction,
                    Observation=missionVm.Observation,
                    ChauffeurId=chauffeur?.Id,
                    ChauffeurName=missionVm.ChauffeurName,
                    VehicleId=vehicule.Id,
                    Vehicle=vehicule,
                    DemandeId=missionVm.DemandeId,
                    Demande=demande,
                    MissionType=missionVm.MissionType,
                };
                await AddAsync(mission);
                mission.Users.AddRange(demande.Users);
                return mission;
            }
            return null;
        }
    }
}
