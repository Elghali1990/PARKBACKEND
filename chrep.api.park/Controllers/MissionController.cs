using chrep.core.park.InputVm;
using chrep.core.park.uof;
using chrep.helpers.park.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chrep.api.park.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly IUnitofworks _unitofworks;
        private readonly ILogger<MissionController> _logger;
        public MissionController(IUnitofworks unitofworks, ILogger<MissionController> logger)
        {
            _unitofworks = unitofworks;
            _logger = logger;

        }
        [HttpGet,Route("getMissionsByidUser/{idUser}")]
        public async Task<IActionResult> GetMissionsByidUser(int idUser)
        {
            try
            {
                _logger.LogInformation("run end point insert mission", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.missionService.GetAllMissionsByIdUser(idUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error on add user pleas view log file.");
            }
        }

        [HttpGet, Route("getAllMission")]
        public async Task<IActionResult> getAllMission()
        {
            try
            {
                _logger.LogInformation("run end point insert mission", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.missionService.FindAsyncAll(x=>x.Id>0, new[] {Tables.Demandes} );
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error on add user pleas view log file.");
            }
        }


        [HttpPost, Route("insertMission")]
        public async Task<IActionResult> insertMission([FromBody] MissionVm missionVm)
        {
            try
            {
                _logger.LogInformation("run end point insert mission", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.missionService.InsertMission(missionVm);
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
