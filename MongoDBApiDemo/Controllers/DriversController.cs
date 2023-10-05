using Microsoft.AspNetCore.Mvc;
using MongoDBApiDemo.Models;
using MongoDBApiDemo.Services;

namespace MongoDBApiDemo.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class DriversController : ControllerBase
{
    private readonly DriverService _driverService;

    public DriversController(DriverService driverService) =>
        _driverService = driverService;


    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        var driver = await _driverService.GetAsync(id);

        if (driver == null)
            return NotFound();

        return Ok(driver);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var allDrivers = await _driverService.GetAsync();
        
        if(allDrivers is null)
            return NotFound();
            
        return Ok(allDrivers);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Driver driver)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _driverService.CreateAsync(driver);
        return CreatedAtAction(nameof(Get), new { id = driver.Id }, driver);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Driver driver)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var exsitingDriver = await _driverService.GetAsync(id);

        if (exsitingDriver is null)
            return BadRequest();

        driver.Id = exsitingDriver.Id;

        await _driverService.UpdateAsync(driver);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Remove(string id)
    {
        var exsitingDriver = await _driverService.GetAsync(id);

        if (exsitingDriver is null)
            return BadRequest();

        await _driverService.RemoveAsync(id);

        return NoContent();
    }
}
