using chrep.core.park.uof;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using chrep.core.park.Models;
using System.Linq;
using chrep.core.park.Dtos;

namespace chrep.api.park.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitofworks _unitofworks;
        private readonly ILogger<UserController> logger;
        public UserController(IUnitofworks unitofworks, ILogger<UserController> logger)
        {
            _unitofworks = unitofworks;
            this.logger = logger;
        }

        [HttpGet, Route("getAllUsers")]
        public async Task<IActionResult> getAllUsers()
        {
            try
            {
                logger.LogInformation("run end point getAllUser", DateTime.UtcNow.ToLongTimeString());
                var users = await _unitofworks.userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Error on get all users pleas view log file");
            }
        }

        [HttpGet, Route("getAllUsersDtos")]
        public async Task<IActionResult> getAllUsersDtos()
        {
            try
            {
                logger.LogInformation("run end point getAllUsersDtos", DateTime.UtcNow.ToLongTimeString());
                var users = await _unitofworks.userService.getUsersDtos();
                return Ok(users);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Error on get all users pleas view log file");
            }
        }

        [HttpGet, Route("getUserById/{Id}")]
        public async Task<IActionResult> getUserById(int Id)
        {
            try
            {
                logger.LogInformation("run end point get user by id", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.userService.getByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Error on get user by id pleas view log file");
            }
        }

        [HttpPost, Route("addUser")]
        public async Task<IActionResult> addUser([FromBody] User user)
        {
            try
            {
                logger.LogInformation("run end point add new user", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.userService.AddAsync(user);
                _unitofworks.commite();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Error on add user pleas view log file.");
            }
        }

        [HttpPost, Route("addUserWithRole")]
        public async Task<IActionResult> addUserWithRole([FromBody] User user, int roleId)
        {
            try
            {
                logger.LogInformation("run end point add new user whith rol", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.userService.AddUserWithRole(user, roleId);
                _unitofworks.commite();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Error on add user whith rol pleas view log file.");
            }
        }

        [HttpDelete, Route("deleteUser")]
        public async Task<IActionResult> deleteUser([FromBody] User user)
        {
            try
            {
                logger.LogInformation("run end point add new user whith rol", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.userService.DeleteAsync(user);
                _unitofworks.commite();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Error on delete user pleas view log file.");
            }
        }

        [HttpPut, Route("updateUser")]
        public async Task<IActionResult> updateUser([FromBody] User user)
        {
            try
            {
                logger.LogInformation("run end point update user", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.userService.Update(user);
                _unitofworks.commite();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Error on update user pleas view log file.");
            }
        }

        [HttpGet, Route("Filter/{FistName}/{LastName}")]
        public async Task<IActionResult> Filter(string FistName, string LastName)
        {
            try
            {
                logger.LogInformation("run end filter users", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.userService.Filter(FistName, LastName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Error on update user pleas view log file.");
            }
        }

        [HttpGet, Route("Login")]
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            try
            {
                logger.LogInformation("run end User Login", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.userService.Login(UserName, Password);
                if (result is User)
                {
                    return Ok(result);
                }
                return Ok(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Error on user login pleas view log file.");
            }
        }

        [HttpGet, Route("getChaufeurs")]
        public async Task<IActionResult> GetChaufeurs()
        {
            try
            {
                logger.LogInformation("run end point get chauffeur", DateTime.UtcNow.ToLongTimeString());
                var role = await _unitofworks.roleService.FindAsync(r => r.Id == 3);
                var result = await _unitofworks.userService.GetAllWithOptionAsync(u => u.Roles.Contains(role));
                return Ok(result.Select(u=> new UserDots { Id=u.Id ,FullName =u.FirstName +" "+u.LastName}));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Error on user login pleas view log file.");
            }
        }


    }
}
