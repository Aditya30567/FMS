using CityService.Interface;
using CityService.Models;
using CityService.Process;
using CityService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        ICity repo;
        CityProcess process;
        ILogger<CityController> logger;
        /*public CityController(ICity repo) { 
        this.repo = repo;
        }*/
        public CityController(CityProcess p, ILogger<CityController> log) => 
            (process, logger) = (p, log); 

        [HttpPost("addCity")]
        public async Task<IActionResult> AddCity(City city)
        {
            try
            {
                // var res = await repo.AddCity(city);
                var res = process.AddCity(city);
                logger.LogInformation("Row added successfully");
                return Ok("Added Data Successfully");
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message, ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("update/{cityId}")]
        public async Task<IActionResult> UpdateCity(int cityId, City city)
        {
            try
            {
                var res = await repo.UpdateCity(cityId, city);
                return Ok("Updated Data Succussfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete/{cityId}")]

        public async Task<IActionResult> DeleteCity(int cityId)
        {
            try
            {
                var res=await repo.DeleteCity(cityId);
                return Ok("Deleted city successfully");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
               // var res = await repo.GetAllData();
               var res = await process.GetAllCities();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("cityById/{cityId}")]
        public async Task<ActionResult<City>> GetCityById(int cityId)
        {
            try
            {
                var res= await repo.GetCityById(cityId);
                if (res is null) throw new Exception("Not found");
                return Ok(res);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("updateAirportCharge/{cityId}")]
        public async Task<ActionResult<City>> UpdateAirportCharge(int cityId, int airportCharge)
        {
            try
            {
                var res = await repo.UpdateAirportCharge(cityId, airportCharge);
                if (res is null) throw new Exception("Not Found");
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
