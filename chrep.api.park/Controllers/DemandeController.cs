using chrep.core.park.Dtos;
using chrep.core.park.InputVm;
using chrep.core.park.Models;
using chrep.core.park.uof;
using chrep.data.park.SqlServer;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost,Route("InsertDemande")]
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

        [HttpGet,Route("getAllDemandes")]
        public async Task<IActionResult> GetAllDemandes()
        {
            try
            {
                _logger.LogInformation("run end point get all demandes", DateTime.UtcNow.ToLongTimeString());
                var demandes = await _unitofworks.demandeService.GetAllAsync();
                var result = demandes.Select(d => new DemandeDtos { Id = d.Id, DateDemande = d.DateDemande, Objet = d.Objet }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error on get all demandes pleas view log file.");
            }
        }

        [HttpGet,Route("getDemandeByUserId")]
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

        [HttpGet,Route("getDemandeByid")]
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

        [HttpDelete,Route("DeleteDemande/{id}")]
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

        [HttpPut,Route("UpdateDemande")]
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

        [HttpPut,Route("closeDemande/{id}")]
        public async Task<IActionResult> CloseDemande(int Id)
        {
            try
            {
                _logger.LogInformation("run end point close demande", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.demandeService.closeDemande(Id);
                _unitofworks.commite();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error on close demande pleas view log file.");
            }
        }
    }
}
