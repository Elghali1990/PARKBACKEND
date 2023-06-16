using chrep.core.park.Dtos;
using chrep.core.park.Enums;
using chrep.core.park.InputVm;
using chrep.core.park.Interfaces;
using chrep.core.park.Models;
using chrep.data.park.SqlServer;
using chrep.helpers.park.Constants;

namespace chrep.data.park.Services
{
    public class MissionService : DataHelper<Mission>, IMissionService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserService _userService;
        private readonly DemandeService _demandeService;
        private readonly VehicleService _vehicleService;
        private readonly RoleService _roleService;
        private readonly AbsenceService _absenceService;
        public MissionService(AppDbContext appDb) : base(appDb)
        {
            _userService = new UserService(appDb);
            _demandeService = new DemandeService(appDb);
            _vehicleService = new VehicleService(appDb);
            _roleService = new RoleService(appDb);
        }

   

        public async Task<List<MissionDtos>> getAllMissions()
        {
            List<MissionDtos> missionsDtos =new();
            var missions =await GetAllAsync();
            foreach (var mission in missions)
            {
                var demande = await _demandeService.FindAsync(d => d.Id == mission.DemandeId);
                if(demande is Demande)
                {
                    missionsDtos.Add(new MissionDtos { Id = mission.Id, Objet = demande.Objet, Detail = demande.Detail, DateDemande = demande.DateDemande, });
                }
            }
            return missionsDtos;
        }

        public async Task<List<MissionDtos>> GetAllMissionsByIdUser(int idUser)
        {
            List<MissionDtos> missionsDtos = new();
            var missions = await GetAllWithOptionAsync(m => m.Demande.UserId.Equals(idUser) && m.DemandeId != null);
            var user = await _userService.FindAsync(u => u.Id.Equals(idUser));
            foreach (var mission in missions)
            {
                var demande = await _demandeService.FindAsync(d => d.Id.Equals(mission.DemandeId));
                if (missionsDtos.Count(e => e.Id.Equals(mission.Id)) == 0)
                {
                    missionsDtos.Add(new MissionDtos { Id = mission.Id, DateDemande = demande.DateDemande, Objet = demande.Objet, Detail = demande.Detail });
                }
            }
            var userMission = await GetAllWithOptionAsync(m => m.Users.Contains(user));
            foreach (var mission in userMission)
            {
                var demande = await _demandeService.FindAsync(d => d.Id.Equals(mission.DemandeId));
                if (missionsDtos.Count(e => e.Id.Equals(mission.Id)) == 0)
                {
                    missionsDtos.Add(new MissionDtos { Id = mission.Id, DateDemande = demande.DateDemande, Objet = demande.Objet, Detail = demande.Detail });
                }
            }
            return missionsDtos;
        }

        public async Task<MissionDetailDtos> GetMissionDetail(int id)
        {
            var mission = await FindAsync(m=>m.Id == id, new[] {Tables.Users,Tables.Absences});
            if(mission is Mission)
            {
                var demande = await _demandeService.FindAsync(d => d.Id == mission.DemandeId, new[] { Tables.Users });
                List<UserMissionDtos> users = new();
                var vehicule =await _vehicleService.FindAsync(v=>v.Id==mission.VehicleId);
                var absences = mission.Absences;
                if(absences.Count > 0)
                {
                    foreach (var absence in absences)
                    {
                        users.Add(new UserMissionDtos { Id = absence.Id, FullName = absence.User.FirstName + " " + absence.User.LastName, IsAbsent = absence.IsAbsent });
                    }
                }
                else
                {
                    users.AddRange(mission.Users.Select(u => new UserMissionDtos { Id = u.Id, FullName = u.FirstName + " " + u.LastName, IsAbsent = false }).ToList());
                }
             
                var missionDetail = new MissionDetailDtos
                {
                    Id= mission.Id,
                    Objet=demande.Objet,
                    Detail = demande.Detail,
                    Instruction=mission.Instruction,
                    Observation=mission.Observation,
                    Chauffeur=mission.ChauffeurName,
                    Vehicule=vehicule.Type_Matricule,
                    DemandeId=mission.DemandeId,
                    MissionType=(int)mission.MissionType,
                    DateDepart=Convert.ToDateTime(mission.DateDepart),
                    HeurDepart=mission.HourDepart.ToString(),
                    UsersMission=users
                };
                return missionDetail;
            }
            return null;
        }

        public async Task<Mission> InsertMission(MissionVm missionVm)
        {
            var demande = await _demandeService.FindAsync(d => d.Id == missionVm.DemandeId);
            if (demande is Demande)
            {
                demande.StatusEnum = StatusEnum.VALIDATE;
                await _demandeService.Update(demande);

                var role = await _roleService.FindAsync(r => r.Id == 3);
                var users = await _userService.GetAllWithOptionAsync(u => u.Roles.Contains(role));
                var chauffeurs = users.Select(c => new UserDots { Id = c.Id, FullName = c.FirstName + " " + c.LastName }).ToList();
                var chauffeur = chauffeurs.FirstOrDefault(c => c.FullName == missionVm.ChauffeurName);
                var vehicule = await _vehicleService.FindAsync(v => v.Marque == missionVm.MarqueVehicule);
                var mission = new Mission()
                {
                    DateDepart = missionVm.DateDepart,
                    HourDepart = TimeSpan.Parse(missionVm.HourDepart),
                    Instruction = missionVm.Instruction,
                    Observation = missionVm.Observation,
                    ChauffeurId = chauffeur?.Id,
                    ChauffeurName = missionVm.ChauffeurName,
                    VehicleId = vehicule.Id,
                    Vehicle = vehicule,
                    DemandeId = missionVm.DemandeId,
                    Demande = demande,
                    MissionType = missionVm.MissionType,
                };
                await AddAsync(mission);
                List<User> usersToInsert = new List<User>();
                foreach (var userId in missionVm.UserIds)
                {
                    var userMission = await _userService.FindAsync(u => u.Id == userId);
                    if(userMission is User)
                    {
                        usersToInsert.Add(userMission);
                    }
                }
                mission.Users.AddRange(usersToInsert);
                return mission;
            }
            return null;
        }

        public async Task<List<UserMission>> setUserMissionAbsent(List<int> ids,int idMission)
        {
           var mission = await FindAsync(m=>m.Id == idMission, new[] {Tables.Users});

            foreach (var user in mission.Users)
            {
                
            }
            return null;
        }
    }
}
