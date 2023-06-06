using chrep.core.park.Dtos;
using chrep.core.park.Models;

namespace chrep.core.park.Interfaces
{
    public interface IUserService:IDataHelper<User>
    {
        public Task<User> AddUserWithRole(User user,int roleId);
        public Task<List<User>> Filter(string FirstName,string LastName);
        public Task<User> Login(string userName, string password);

        Task<List<UserDots>> getUsersDtos();

    }
}
