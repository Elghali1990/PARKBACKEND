using chrep.core.park.uof;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chrep.api.park.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitofworks _unitofworks;
        private readonly ILogger<VehicleController> _logger;
        public VehicleController(IUnitofworks unitofworks, ILogger<VehicleController> logger)
        {
            _unitofworks = unitofworks;
            _logger = logger;
        }

        [HttpGet,Route("getVehicules")]
        public async Task<IActionResult> GetVehicules()
        {
            try
            {
                _logger.LogInformation("run end point get vehicule", DateTime.UtcNow.ToLongTimeString());
                var result = await _unitofworks.vehicleService.GetAllAsync();
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
