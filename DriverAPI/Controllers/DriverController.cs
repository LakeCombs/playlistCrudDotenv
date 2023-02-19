using DriverAPI.Models;
using DriverAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriverAPI.Controllers
{

    [ApiController]
    [Route("api/driver")]
    public class DriverController : ControllerBase
    {
        private readonly DriverService _driverService;

        public DriverController(DriverService driverService) => _driverService = driverService;


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var existingDriver = await _driverService.GetAsync(id);
            if (existingDriver is null)
            {
                return NotFound();
            }

            return Ok(existingDriver);
        }


        [HttpPost]
        public async Task<IActionResult> Get()
        {
            var allDrivers : List<Driver> = await _driverService.GetAsync();

            if (allDrivers.Any())
            {
                return Ok(allDrivers);
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Post(Driver driver)
        {
            await _driverService.CreateAsync(driver);
            return CreatedAtAction(nameof(Get), routeValues: new { id: driver.Id }, value: driver);


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Driver driver)
        {
            var existingDriver = await _driverService.GetAsync(id);

            if (existingDriver is null)
            {
                return BadRequest();
            }

            driver.Id = existingDriver.Id;

            await _driverService.UpdateAsync(driver);
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(string id) {

        }

    }
}


