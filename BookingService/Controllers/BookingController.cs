using BookingService.Process;
using BookingService.Repository;
using FMSLibrary.Models;
using FMSLibrary.UserDefinedException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingProcess process;
        public BookingController(BookingProcess process)
        {
            this.process = process;
        }
        [HttpPost()]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            try
            {
                var res = await process.AddBooking(booking);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new FaultContract
                {
                    StatusCode = 500,
                    ErrorMessage = ex.Message,
                    Details = "Booking Failed"
                });
            }
        }
        [HttpPut("checkin/{bookingId}")]
        public async Task<IActionResult> UpdateCheckInStatus(int bookingId)
        {
            var (success, message,list) = await process.UpdateCheckInStatus(bookingId);

            if (!success)
                return BadRequest(new { Message = message });

            return Ok(new
            {
                Message = message,
                PassengerList=list
            });
        }
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            try
            {

                var booking = await process.GetBookingById(bookingId);
                //return Ok(new
                //{
                //    BookingId = booking.BookingId,
                //    Status = booking.Status,
                //    IsActive = booking.IsActive,
                //    PassengerCount = booking.Passengers.Count
                //});
                return Ok(booking);
            }catch (IdNotFoundException ex)
            {
                return NotFound(new FaultContract
                {
                    StatusCode = 404,
                    ErrorMessage = ex.Message,
                    Details = "Booking not found with given id"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new FaultContract
                {

                    StatusCode = 500,
                    ErrorMessage = ex.Message,
                    Details = "Error"
                });
            }

        }
        [HttpGet("passenger/{bookingId}")]
        public async Task<IActionResult> GetPassengerByBookingId(int bookingId)
        {
            try
            {
                var res = await process.GetAllPassenger(bookingId);
                return Ok(res);
            }catch(Exception ex)
            {
                return BadRequest(new FaultContract
                {
                    StatusCode = 500,
                    ErrorMessage = ex.Message,
                    Details = "Error"
                });
            }
        }
        [HttpGet("flightDetailsWithId/{bookingId}")]
        public async Task<IActionResult> GetAllBookingDetailsWithFlight(int bookingId)
        {
            try
            {
                //if (bookingId.GetType() != typeof(int)) throw new Exception("Invalid Type");
                var (flightDatawithId, list,booking) = await process.GetAllBookingDetailsWithFlight(bookingId);
                return Ok(new
                {
                    flightData=flightDatawithId,
                    passengerDetails=list,
                    bookingDetails=booking
                });
            }catch(IdNotFoundException ex)
            {
                return NotFound(new FaultContract
                {
                    StatusCode = 404,
                    ErrorMessage = ex.Message,
                    Details = "Booking is not found"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
