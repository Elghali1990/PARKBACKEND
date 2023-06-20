using chrep.core.park.Dtos;
using chrep.core.park.InputVm;
using chrep.core.park.Models;

namespace chrep.core.park.Interfaces
{
    public interface IUserService:IDataHelper<User>
    {
        public Task<User> AddUserWithRole(User user,int roleId);
        public Task<List<User>> Filter(string FirstName,string LastName);
        public Task<User> Login(string userName, string password);

        public Task<List<UserTocken>> getUserTockens(UserIds userIds);
        public Task<List<UserDots>> getUsersDtos();
        public Task<User> SetUserTocken(UserTocken userTocken);

    }
}
