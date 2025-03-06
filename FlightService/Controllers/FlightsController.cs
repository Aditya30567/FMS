﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FMSLibrary.Models;
using FlightService.Process;
using FMSLibrary.UserDefinedException;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;

namespace FlightService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightProcess process;

        public FlightsController(FlightProcess process)
        {
            this.process= process;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFlight(Flight flight)
        {
            try
            {
                var res=await process.AddFlight(flight);
                return Ok("Added data successfully");
            }catch(IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getFlight")]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlightByDepartureDate([FromQuery]string from, [FromQuery] string to, [FromQuery] DateOnly date)
        {
            try
            {
                var res=await process.GetFlightByDepartureDate(from,to,date);
                return Ok(res);
            }catch(IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("del/{flightId}")]
        public async Task<IActionResult> RemoveResult(int flightId)
        {
            try
            {
                var res = await process.RemoveFlight(flightId);
                return Ok("Deleted succesfully");
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update/{flightId}")]
        public async Task<ActionResult<Flight>> UpdateAvailableSeat(int flightId, int AvailableSeat)
        {
            try
            {
                var res = await process.UpdateAvailableSeat(flightId, AvailableSeat);
                return Ok(res);
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("updateFlightDetail/{flightId}")]
        public async Task<ActionResult<Flight>> UpdateFlightDetails(int flightId,Flight flight)
        {
            try
            {
                var res=await process.UpdateFlight(flightId, flight);
                return Ok(res);
            }catch(IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{flightId}")]
        public async Task<ActionResult<Flight>> GetFlightById(int flightId)
        {
            try
            {
                var res = await process.GetFlightById(flightId);
                return Ok(res);
            }catch(IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    
}
