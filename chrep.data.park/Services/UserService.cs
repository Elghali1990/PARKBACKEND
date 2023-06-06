using chrep.core.park.Dtos;
using chrep.core.park.Interfaces;
using chrep.core.park.Models;
using chrep.data.park.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace chrep.data.park.Services
{
    public class UserService : DataHelper<User>, IUserService
    {

        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        public UserService(AppDbContext appDbContext):base(appDbContext) 
        {
            _appDbContext = appDbContext;
            _roleService = new(appDbContext);
        }
        public async Task<User> AddUserWithRole(User user, int roleroleId)
        {
            var userWithRole =  await AddAsync(user);
            userWithRole.Roles.Add(await _roleService.getByIdAsync(roleroleId));
            return userWithRole;
        }

        public async Task<List<User>> Filter(string FirstName, string LastName)
        {
            var result =await GetAllWithOptionAsync(u=>u.FirstName.Equals(FirstName) && u.LastName.Equals(u.LastName));
            return result;
        }

        public async Task<List<UserDots>> getUsersDtos()
        {
           var users =await _appDbContext.Users.Select(u=>new UserDots() { Id=u.Id,FullName=u.FirstName +" "+ u.LastName}).ToListAsync();
            return users;
        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await FindAsync(c => c.UserName.Equals(userName) && c.Password.Equals(password));
            return user;
        }
    }
}
