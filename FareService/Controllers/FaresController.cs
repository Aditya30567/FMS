using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FMSLibrary.Models;
using FareService.Process;
using FMSLibrary.UserDefinedException;
using Azure.Core;
using Microsoft.OpenApi.Validations;

namespace FareService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaresController : ControllerBase
    {
        private readonly FareProcess process;
        public FaresController(FareProcess process)
        {
            this.process = process;
        }

        // GET: api/Fares
        [HttpPost("addfare")]
        public async Task<IActionResult> AddFareAsync(Fare fare)
        {
            try
            {
                await process.AddFare(fare);
                return Ok(new { message = "Flight added successfully" });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new FaultContract
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Request",
                    Details = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                return StatusCode(500, new FaultContract
                {
                    StatusCode = 500,
                    ErrorMessage =ex.Message,
                    Details = "An unexpected error occurred. Please try again later."
                });
            }

        }
        [HttpPost("update/{fareId}")]
        public async Task<IActionResult> UpdateFare(int fareId,Fare fare)
        {
            try
            {
                var res = await process.UpdateFare(fareId, fare);
                return Ok(res);
            }
            catch(IdNotFoundException ex)
            {
                return NotFound(new FaultContract
                {
                    StatusCode = 404,
                    ErrorMessage = "Invalid Request",
                    Details = ex.Message
                });
            }catch(ArgumentException ex)
            {
                return BadRequest(new FaultContract
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Request",
                    Details = ex.Message
                });
            }catch(Exception ex)
            {
                return StatusCode(500, new FaultContract
                {
                    StatusCode = 500,
                    ErrorMessage = ex.Message,
                    Details = "An unexpected error occurred. Please try again later."
                });
            }
        }
        [HttpPut("updateConvenincefee/{fareId}")]
        public async Task<IActionResult> UpdateConvenincefee(int fareId,int convenance)
        {
            try
            {
                var res = await process.UpdateConveninceFee(fareId, convenance);
                return Ok(res);
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(new FaultContract
                {
                    StatusCode = 404,
                    ErrorMessage = "Invalid Request",
                    Details = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new FaultContract
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Request",
                    Details = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new FaultContract
                {
                    StatusCode = 500,
                    ErrorMessage = ex.Message,
                    Details = "An unexpected error occurred. Please try again later."
                });
            }
        }
        [HttpGet("flightById/{flightId}")]
        public async Task<IActionResult> GetFareByFlightById(int flightId)
        {
            try
            {
                var res = await process.GetFareByFlightById(flightId);
                return Ok(res);
            }
            catch(IdNotFoundException ex)
            {
                return NotFound(new FaultContract
                {
                    StatusCode = 404,
                    ErrorMessage = "Invalid Request",
                    Details = ex.Message
                });
            }catch(ArgumentException ex)
            {
                return BadRequest(new FaultContract
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Request",
                    Details = ex.Message
                });
            }catch(Exception ex)
            {
                return StatusCode(500, new FaultContract
                {
                    StatusCode = 500,
                    ErrorMessage = ex.Message,
                    Details = "An unexpected error occurred. Please try again later."
                });
            }
        }
        [HttpGet("getById/{fareId}")]
        public async Task<IActionResult> GetFareById(int fareId)
        {
            try
            {
                var res = await process.GetFareById(fareId);
                return Ok(res);
            }
            catch(IdNotFoundException ex)
            {
                return NotFound(new FaultContract
                {
                    StatusCode = 404,
                    ErrorMessage = "Invalid Request",
                    Details = ex.Message
                });
            }catch(ArgumentException ex)
            {
                return BadRequest(new FaultContract
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Request",
                    Details = ex.Message
                });
            }catch(Exception ex)
            {
                return StatusCode(500, new FaultContract
                {
                    StatusCode = 500,
                    ErrorMessage = ex.Message,
                    Details = "An unexpected error occurred. Please try again later."
                });
            }
        }
        
   
    }
}
