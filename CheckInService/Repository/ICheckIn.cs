using FMSLibrary.Models;

namespace CheckInService.Repository
{
    public interface ICheckIn
    {
       Task<bool> CheckInAsync(CheckIn checkIn);
    }
}
