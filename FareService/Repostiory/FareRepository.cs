using FareService.Repostiory;
using FMSLibrary.Models;
using FMSLibrary.UserDefinedException;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FareService.Repositroy
{
    public class FareRepository:IFare
    {
        private readonly FareDbContext _context;
        public FareRepository(FareDbContext context)
        {
            _context = context;
        }
        public async Task<Fare> GetFareById(int fareId)
        {
            try
            {
                if (fareId < 0) throw new Exception("Fare can't be less than zero");
                var res = await _context.Fares.FirstOrDefaultAsync(c => c.FareId == fareId);
                if (res == null) throw new IdNotFoundException("Id not found");
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Fare> GetFareByFlightById(int flightId)
        {
            try
            {
                if (flightId < 0) throw new Exception("Fare can't be less than zero");
                var res = await _context.Fares.FirstOrDefaultAsync(c => c.FlightId == flightId);
                if (res == null) throw new IdNotFoundException("Id not found");
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        public async Task<bool> AddFare(Fare fare)
        {
            
            try
            {
                if (fare is null) throw new ArgumentException("fare Data can't be null");
                var res = await _context.Fares
                    .FirstOrDefaultAsync(c => c.FareId == fare.FareId);
                if (res is not null) throw new Exception("Id already exist");
                _context.Fares.Add(fare);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                throw;
            
            }
        }

        public async Task<Fare> UpdateFare(int fareId, Fare fare)
        {
            try
            {
                if (fareId != fare.FareId) throw new Exception("Id mismatched");
                if (fare is null) throw new ArgumentException("fare Data can't be null");
                var res = await _context.Fares
                    .FirstOrDefaultAsync(c => c.FareId == fare.FareId);
                if (res is  null) throw new IdNotFoundException("Id not found");
                res.BasePrice = fare.BasePrice;
                res.ConvenienceFee= fare.ConvenienceFee;
                await _context.SaveChangesAsync();
                return res;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<Fare> UpdateConveninceFee(int fareId, int conveninceFees)
        {
            try
            {
                if (fareId <0 || conveninceFees<0) throw new Exception("Fare Value cant be null");
                var res = await _context.Fares
                    .FirstOrDefaultAsync(c => c.FareId == fareId);
                if (res is null) throw new IdNotFoundException("Id not found");
                res.ConvenienceFee = conveninceFees;
                await _context.SaveChangesAsync();
                return res;
            }
            catch (Exception ex) {
                throw;
            }
        }
    }
}
