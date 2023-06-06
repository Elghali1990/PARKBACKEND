using chrep.core.park.Models;
using chrep.core.park.uof;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chrep.api.park.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IUnitofworks uof;
        public RoleController(IUnitofworks _uof)
        {
            uof = _uof;
        }

        [HttpGet,Route("getAllRoles")]
        public async Task<IActionResult> getAllRoles()
        {
           var roles = await uof.roleService.GetAllAsync();
            return Ok(roles);
        }
        [HttpPost ,Route("createRole")]
        public async Task<IActionResult> createRole([FromBody] Role role)
        {
            var result = await uof.roleService.AddAsync(role);
            uof.commite();
            return Ok(result);
        }
        
        [HttpGet, Route("getRoleById/{Id}")]
        public async Task<IActionResult> getRoleById(int Id)
        {
            var role = await uof.roleService.getByIdAsync(Id) ;
            if (role is not Role)
            {
                return NotFound($"role whit id={Id} is not exist");
            }
            return Ok(role);
        }

        [HttpPut, Route("updateRole")]
        public async Task<IActionResult> updateRole([FromBody] Role newRole)
        {
            var role =await uof.roleService.Update(newRole);
            uof.commite();
            return Ok(role);
        }

        [HttpDelete, Route("deleteRole")]
        public async Task<IActionResult> deleteRole([FromBody] Role roleToDelete)
        {
            var role = await uof.roleService.DeleteAsync(roleToDelete);
            uof.commite();
            return Ok(role);
        }
    }
}
