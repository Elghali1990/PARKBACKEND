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
    public class DemandeService : DataHelper<Demande>, IDemandeService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserService _userService;
        public DemandeService(AppDbContext appDb) : base(appDb)
        {
            _userService = new UserService(appDb);
            //_appDbContext = new AppDbContext(dbContextOptions);
            _appDbContext = appDb;
        }

        public async Task<List<DemandeDtos>> getDemandeByUserId(int userId)
        {
            var demandes = await _appDbContext.Demandes.Where(d => d.UserId.Equals(userId)).ToListAsync();
            return demandes.Select(d => new DemandeDtos { Id = d.Id, DateDemande = d.DateDemande, Objet = d.Objet }).ToList();
        }

        public async Task<DemandeDetailDtos> getDemandeDetailById(int Id)
        {
            var _demande = new DemandeDetailDtos();
            var demande = await FindAsync(d => d.Id.Equals(Id), new[] {Tables.Users});
            if (demande is Demande)
            {
                _demande.Id = demande.Id;
                _demande.DateDemande = demande.DateDemande;
                _demande.Objet = demande.Objet;
                _demande.Detail = demande.Detail;
                _demande.DateDepart = demande.DateDepart;
                _demande.HourDepart = demande.HourDepart;
                _demande.DateBack = demande.DateBack;
                _demande.HourBack = demande.HourBack;
                _demande.Observation = demande.Observation;
                _demande.StatusEnum = demande.StatusEnum;
                _demande.UserId = demande.UserId;
                if (demande.Users.Count > 0)
                {
                    _demande.Users = await Task.Run(() => demande.Users.Select(u => new UserDots { Id = u.Id, FullName = u.FirstName + " " + u.LastName }).ToList());
                }

            }
            return _demande;
        }

        public async Task<Demande> InsertDemande(DemandeVm demandeVm)
        {
            List<User> users = new List<User>();
            foreach (var id in demandeVm.Userids)
            {
                var user = await _userService.FindAsync(x => x.Id == id);
                if (user is not null)
                {
                    users.Add(user);
                }
            }
            Demande demande = new()
            {
                DateDemande = DateTime.Now,
                Objet = demandeVm.Objet,
                Detail = demandeVm.Detail,
                DateDepart = demandeVm.DateDepart,
                HourDepart = TimeSpan.Parse(demandeVm.HourDepart),
                DateBack = demandeVm.DateBack,
                HourBack = TimeSpan.Parse(demandeVm.HourBack),
                Observation = demandeVm.Observation,
                UserId = demandeVm.Userid,
                StatusEnum = StatusEnum.BEING_VALIDATED,
                Users = users
            };
            await AddAsync(demande);
            demande.Users.AddRange(users);
            return demande;
        }

        public async Task<Demande> updateDemande(DemandeVm demandeVm)
        {
            var demande = await FindAsync(d => d.Id.Equals(demandeVm.Id), new[] { "Users" });
            if (demande is Demande)
            {
                var users = await Task.Run(() => demande.Users.Select(u => new UserDots { Id = u.Id, FullName = u.FirstName + " " + u.LastName }).ToList());
                List<User> usersToRemouve = new();
                foreach (var user in users)
                {
                    var userToDelete = await _userService.FindAsync(u => u.Id == user.Id);
                    if (userToDelete is User)
                    {
                        demande.Users.Remove(userToDelete);
                        await _appDbContext.SaveChangesAsync();
                    }
                }

                List<User> usersToInsert = new();
                foreach (var id in demandeVm.Userids)
                {
                    var user = await _userService.FindAsync(x => x.Id == id);
                    if (user is User)
                    {
                        usersToInsert.Add(user);
                    }
                }

                demande.Objet = demandeVm.Objet;
                demande.Detail = demandeVm.Detail;
                demande.DateDepart = demandeVm.DateDepart;
                demande.HourDepart = TimeSpan.Parse(demandeVm.HourDepart);
                demande.DateBack = demandeVm.DateBack;
                demande.HourBack = TimeSpan.Parse(demandeVm.HourBack);
                demande.Observation = demandeVm.Observation;
                demande.StatusEnum = StatusEnum.BEING_VALIDATED;
                await Update(demande);
                demande.Users.AddRange(usersToInsert);

            }
            return demande;
        }
    }
}
