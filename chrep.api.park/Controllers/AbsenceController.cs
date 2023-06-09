using chrep.core.park.InputVm;
using chrep.core.park.Models;
using chrep.core.park.uof;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chrep.api.park.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IUnitofworks _unitofworks;
        private readonly ILogger<AbsenceController> _logger;
        public AbsenceController(IUnitofworks unitofworks ,ILogger<AbsenceController> logger)
        {
            _unitofworks = unitofworks;
            _logger = logger;
        }
        [HttpPost, Route("insertAbsence")]
        public async Task<IActionResult> InsertAbsence([FromBody] AbsenceVm absenceVm)
        {
            try
            {
                _logger.LogInformation("run end point insert absence", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.absenceService.InsertAbsence(absenceVm);
                _unitofworks.commite();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error on insert absence pleas view log file.");
            }
        }
    }
}
