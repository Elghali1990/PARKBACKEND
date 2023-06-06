using chrep.core.park.InputVm;
using chrep.core.park.Models;
using chrep.core.park.uof;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace chrep.api.park.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeController : ControllerBase
    {
        private readonly IUnitofworks _unitofworks;
        private readonly ILogger<DemandeController> _logger;
        public DemandeController(IUnitofworks unitofworks, ILogger<DemandeController> logger)
        {
            _unitofworks = unitofworks;
            _logger = logger;
        }

        [HttpPost("InsertDemande")]
        public async Task<IActionResult> InsertDemande([FromBody] DemandeVm demandeVm)
        {
            try
            {
                _logger.LogInformation("run end point add new demande", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.demandeService.InsertDemande(demandeVm);
                _unitofworks.commite();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error on add user pleas view log file.");
            }
        }


        [HttpGet("getDemandeByUserId")]
        public async Task<IActionResult> getDemandeByUserId(int userId)
        {
            try
            {
                _logger.LogInformation("run end point get demandes by id user", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.demandeService.getDemandeByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error on add user pleas view log file.");
            }
        }

        [HttpGet("getDemandeByid")]
        public async Task<IActionResult> getDemandeByid(int id)
        {
            try
            {
                _logger.LogInformation("run end point get demandes by id", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.demandeService.getDemandeDetailById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error on add user pleas view log file.");
            }

        }

        [HttpDelete("DeleteDemande/{id}")]
        public async Task<IActionResult> DeleteDemande(int id)
        {
            try
            {
                _logger.LogInformation("run end point get delete demande", DateTime.UtcNow.ToLongTimeString());
                Demande demandeToDelete = await _unitofworks.demandeService.getByIdAsync(id);
                if (demandeToDelete is null)
                {
                    return NotFound($"demande where id is ! {id} not exist");
                }
                var result =await _unitofworks.demandeService.DeleteAsync(demandeToDelete);
                _unitofworks.commite();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error on add user pleas view log file.");
            }

        }

        [HttpPut("UpdateDemande")]
        public async Task<IActionResult> UpdateDemande([FromBody]DemandeVm demandeVm)
        {
            try
            {
                _logger.LogInformation("run end point update demande", DateTime.UtcNow.ToLongTimeString());
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                var result = await _unitofworks.demandeService.updateDemande(demandeVm); ;
                _unitofworks.commite();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error on add user pleas view log file.");
            }

        }
    }
}
